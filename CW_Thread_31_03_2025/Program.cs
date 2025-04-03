using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

/*

Завершение классной работы (повторное выполнение части выполненной работы для практики)

Задание 1
	Создайте консольное приложение, порождающее поток. Этот поток должен отображать в консоль числа от 0 до 50. 
 Задание 2
	Добавьте в первое задание возможность передачи начала и конца диапазона чисел. Границы определяет пользователь.
Задание 3
	Добавьте к первому заданию возможность определения пользователем количества потоков. Границы диапазона чисел также выбираются пользователем.
Задание 4
	Консольное приложение генерирует набор чисел, состоящий из 10000 элементов. С помощью механизма потоков нужно найти максимум, минимум и среднее в этом наборе. 
    Для каждой из задач выделите поток
Задание 5
	К четвертому заданию добавьте поток, выводящий набор чисел и результаты вычислений в файл.
Задание 6
	Переведите первое задание со всеми изменениями в оконный интерфейс.
Задание 7
	Переведите четвертое задание со всеми изменениями в оконный интерфейс. 


*/

namespace CW_Thread_31_03_2025
{
    class CalcResults
    {
        public int[] Numbers { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Выберите задание для проверки:");
            int numTusk = Convert.ToInt32(ReadLine());
            switch(numTusk)
            {
                case 1:
                case 2:
                case 3:
                    WriteLine("Задание 1-3 объеденены в в одно");
                    Task_1_3();
                    break;
                case 4:
                case 5:
                    Task_4_5();
                    break;
            }
        }
        static void Task_4_5()
        {
            var results = new CalcResults();

            Thread genNum = new Thread(MethodGenNum);
            genNum.Start(results);
            genNum.Join();

            List<Thread> threads = new List<Thread>();
            threads.Add(new Thread(MethodMin));
            threads.Add(new Thread(MethodMax));
            threads.Add(new Thread(MethodAverage));
            foreach(var thr in threads)
            {
                thr.Start(results);
            }
            foreach (var thr in threads)
            {
                thr.Join();
            }

            Thread writeToFile = new Thread(MethodWriteResultToFile);
            writeToFile.Start(results);
            writeToFile.Join();
        }
        static void MethodWriteResultToFile(object obj)
        {
            var results = (CalcResults)obj;
            string path = "results.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Сгенерированные числа:");
                foreach (var num in results.Numbers)
                {
                    writer.Write(num + " ");
                }
                writer.WriteLine("\n\nРезультаты вычислений:");
                writer.WriteLine($"Минимальное значение: {results.Min}");
                writer.WriteLine($"Максимальное значение: {results.Max}");
                writer.WriteLine($"Среднее значение: {results.Average:F2}");
            }
            WriteLine($"Результаты успешно записаны в файл {Path.GetFullPath(path)}");
        }
        static void MethodGenNum(object obj)
        {
            var results = (CalcResults)obj;
            int sizeMus = 1000;
            Random r = new Random();
            results.Numbers = new int[sizeMus];
            for (int i = 0; i < sizeMus; ++i)
            {
                results.Numbers[i] = r.Next(9999);
            }
        }
        static void MethodMin(object obj)
        {
            var results = (CalcResults)obj;

            results.Min = results.Numbers[0];
            for(int i = 1; i < results.Numbers.Length; ++i)
            {
                if (results.Numbers[i] < results.Min)
                    results.Min = results.Numbers[i];
            }
            WriteLine("Минимальное значение массива = {0}", results.Min);
        }
        static void MethodMax(object obj)
        {
            var results = (CalcResults)obj;

            results.Max = results.Numbers[0];
            for (int i = 1; i < results.Numbers.Length; ++i)
            {
                if (results.Numbers[i] > results.Max)
                    results.Max = results.Numbers[i];
            }
            WriteLine("Максимальное значение массива = {0}", results.Max);
        }
        static void MethodAverage(object obj)
        {
            var results = (CalcResults)obj;
            results.Average = (double)results.Numbers.Sum() / results.Numbers.Length;
            WriteLine("Среднее значение массива = {0}", results.Average);
        }


        static void Task_1_3()
        {
            WriteLine("Введите начальный диапазон вывода:");
            int start = Convert.ToInt32(Console.ReadLine());
            WriteLine("Введите конечный диапазон вывода:");
            int end = Convert.ToInt32(Console.ReadLine());
            if (start > end)
            {
                (start, end) = (end, start);
            }
            //List<int> mas = new List<int> { start, end};
            WriteLine("Выберите количество потоков");
            int threadCount = Convert.ToInt32(ReadLine());
            ParameterizedThreadStart ts = new ParameterizedThreadStart(Method);
            Thread[] lThread = new Thread[threadCount];
            for (int i = 0; i < threadCount; ++i)
            {
                List<int> mas = new List<int> { start, end, i };
                lThread[i] = new Thread(ts);
                lThread[i].Start((object)mas);
            }
        }
        static void Method(object mas)
        {
            List<int> mas1 = (List<int>)mas;
            string tabs = new string('\t', mas1[2]);
            for (int i = mas1[0]; i <= mas1[1]; i++)
            {
                WriteLine($"{tabs}{i}");
            }
        }
    }
}
