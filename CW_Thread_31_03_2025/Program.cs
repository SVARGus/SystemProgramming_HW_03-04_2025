using System;
using System.Collections.Generic;
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
                    Task_4();
                    break;
            }
        }
        static void Task_4()
        {
            int sizeMus = 1000;
            Random r = new Random();
            int[] randNums = new int[sizeMus];
            for(int i = 0; i < sizeMus; ++i)
            {
                randNums[i] = r.Next(9999);
            }
            ParameterizedThreadStart min = new ParameterizedThreadStart(MethodMin);
            ParameterizedThreadStart max = new ParameterizedThreadStart(MethodMax);
            ParameterizedThreadStart average = new ParameterizedThreadStart(MethodAverage);
            Thread minSearch = new Thread(min);
            Thread maxSearch = new Thread(max);
            Thread averageSearch = new Thread(average);
            minSearch.Start((object)randNums);
            maxSearch.Start((object)randNums);
            averageSearch.Start((object)randNums);

        }
        static void MethodMin(object mas)
        {
            int[] arr = (int[])mas;
            int min = arr[0];
            for(int i = 1; i < arr.Length; ++i)
            {
                if (arr[i] < min)
                    min = arr[i];
            }
            WriteLine("Минимальное значение массива = {0}", min);
        }
        static void MethodMax(object mas)
        {
            int[] arr = (int[])mas;
            int max = arr[0];
            for (int i = 1; i < arr.Length; ++i)
            {
                if (arr[i] > max)
                    max = arr[i];
            }
            WriteLine("Максимальное значение массива = {0}", max);
        }
        static void MethodAverage(object mas)
        {
            int[] arr = (int[])mas;
            double average = (double)arr.Sum() / arr.Length;
            WriteLine("Среднее значение массива = {0}", average);
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
