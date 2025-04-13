using System;
using System.IO;
using System.Linq;
using System.Threading;

/*
    Задание 1
    Создайте приложение, использующее механизм мьютексов. 
    Создайте в коде приложения несколько потоков. 
    Первый поток генерирует набор случайных чисел и записывает их в файл. 
    Второй поток ожидает, когда первый закончит своё исполнение, после чего анализирует содержимое файла и создаёт новый файл, 
в котором должны быть собраны только простые числа из первого файла. 
    Третий поток ожидает, когда закончится второй поток, после чего создаёт новый файл, 
в котором должны быть собраны все простые числа из второго файла у которых последняя цифра равна 7. 
    Выбор типа приложения (консольное или оконное, остаётся за вами).

Задание 2
	Добавьте к первому заданию четвертый поток, который подготовит и выведет отчет о полученных файлах в итоговый файл отчёта. Пример отчёта: 	
    ■ количество чисел в каждом файле;
	■ размер каждого файла в байтах.

*/

namespace MutexNumberPipeline
{
    class Program
    {
        private static Mutex genMutex = null;
        private static Mutex primeMutex = null;
        private static Mutex prime7Mutex = null;
        private static string filePath1 = "numbers.txt";
        private static string filePath2 = "primes.txt";
        private static string filePath3 = "primesEndingWith7.txt";
        private static string filePath4 = "finalreport.txt";
        static void Main(string[] args)
        {
            Thread generatorThread = new Thread(GenNumbers);
            Thread primeFilterThread = new Thread(FilterPrimes);
            Thread primes7FilterThread = new Thread(FilterPrimesEndingWith7);
            Thread reportThread = new Thread(WriteFinalReport);

            generatorThread.Start();
            primeFilterThread.Start();
            primes7FilterThread.Start();
            reportThread.Start();

            generatorThread.Join();
            primeFilterThread.Join();
            primes7FilterThread.Join();
            reportThread.Join();


            Console.WriteLine("Все этапы завершены. Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void GenNumbers()
        {
            genMutex = new Mutex(initiallyOwned: true);
            Random rand = new Random();

            using (StreamWriter streamWriter = new StreamWriter(filePath1))
            {
                for (int i = 0; i < 100; ++i)
                {
                    streamWriter.WriteLine(rand.Next(1, 1000));
                }
            }

            Console.WriteLine($"Числа сгененрированы и записаны в {filePath1}");
            genMutex.ReleaseMutex();
        }

        static void FilterPrimes()
        {
            if (genMutex == null)
            {
                Thread.Sleep(10);
            }
            genMutex.WaitOne();
            genMutex.ReleaseMutex();
            primeMutex = new Mutex(initiallyOwned: true);

            var lines = File.ReadAllLines(filePath1);
            var primes = lines.Select(int.Parse).Where(IsPrime).ToList();

            File.WriteAllLines(filePath2, primes.Select(p => p.ToString()));
            Console.WriteLine($"Простые числа записаны в {filePath2}");

            primeMutex.ReleaseMutex();
        }

        static void FilterPrimesEndingWith7()
        {
            if (primeMutex == null)
            {
                Thread.Sleep(10);
            }
            primeMutex.WaitOne();
            primeMutex.ReleaseMutex();
            prime7Mutex = new Mutex(initiallyOwned: true);

            var lines = File.ReadAllLines(filePath2);
            var primes7 = lines.Select(int.Parse).Where(n => n % 10 == 7).ToList();

            File.WriteAllLines(filePath3, primes7.Select(p => p.ToString()));
            Console.WriteLine($"Простые числа заканчивающиеся на 7 записаны в {filePath3}");

            prime7Mutex.ReleaseMutex();
        }

        static void WriteFinalReport()
        {
            if (prime7Mutex == null)
            {
                Thread.Sleep(10);
            }
            prime7Mutex.WaitOne();
            prime7Mutex.ReleaseMutex();

            using (StreamWriter sw = new StreamWriter(filePath4))
            {
                ReportOnFile(sw, filePath1);
                ReportOnFile(sw, filePath2);
                ReportOnFile(sw, filePath3);
            }

            Console.WriteLine($"Отчёт записан в {filePath4}");
        }

        static void ReportOnFile(StreamWriter sw, string path)
        {
            var lines = File.ReadAllLines(path);
            long size = new FileInfo(path).Length;

            sw.WriteLine($"Файл: {path}");
            sw.WriteLine($"  Кол-во чисел: {lines.Length}");
            sw.WriteLine($"  Размер файла: {size} байт");
            sw.WriteLine();
        }

        static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
