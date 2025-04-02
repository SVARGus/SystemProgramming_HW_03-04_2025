using System;
using System.Collections.Generic;
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
	Консольное приложение генерирует набор чисел, состоящий из 10000 элементов. С помощью механизма потоков нужно найти максимум, минимум и среднее в этом наборе. Для каждой из задач выделите поток
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
            WriteLine("Введите начальный диапазон вывода:");
            int start = Convert.ToInt32(Console.ReadLine());
            WriteLine("Введите конечный диапазон вывода:");
            int end = Convert.ToInt32(Console.ReadLine());
            if(start > end)
            {
                (start, end) = (end, start);
            }
            List<int> mas = new List<int> { start, end };

            ParameterizedThreadStart ts = new ParameterizedThreadStart(Method);
            Thread t = new Thread(ts);
            t.Start((object)mas);
        }
        static void Method(object mas)
        {
            List<int> mas1 = (List<int>)mas;
            for (int i = mas1[0]; i <= mas1[1]; i++)
            {
                WriteLine(i);
            }
        }
    }
}
