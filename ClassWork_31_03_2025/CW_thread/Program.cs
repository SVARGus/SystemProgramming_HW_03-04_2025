using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace CW_thread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Введите начальный диапазон вывода:");
            int start = Convert.ToInt32(Console.ReadLine());
            WriteLine("Введите конечный диапазон вывода:");
            int end = Convert.ToInt32(Console.ReadLine());
            List<int> mas = new List<int> {start, end};
            
            ParameterizedThreadStart ts = new ParameterizedThreadStart(Method);
            Thread t = new Thread(ts);
            t.Start((object)mas);
        }
        static void Method(object mas)
        {
            WriteLine("Сколько потоков запусить: ");
            int countThread = Convert.ToInt32(Console.ReadLine());

            List<int> mas1 = (List<int>)mas;
            for (int i = mas1[0]; i <= mas1[1]; i++)
            {
                WriteLine(i);
            }
        }
    }
}
