using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ClassWork_31_03_2025
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadStart lisClient = new ThreadStart(LisenerClient);
            Thread lisenerThread = new Thread(lisClient);
            lisenerThread.IsBackground = false;
            lisenerThread.Start();
        }

        static void LisenerClient()
        {
            int counter = 0;

            while (true)
            {
                WriteLine("Enter any key");
                ReadKey(true);

                ParameterizedThreadStart userDel = new ParameterizedThreadStart(UserThreadFunc);
                Thread userWorkThread = new Thread(userDel);
                userWorkThread.Start((object)counter.ToString());

                ++counter;
            }
        }

        static void UserThreadFunc(object a)
        {
            string userName = (string)a;
            WriteLine($"user\t# {userName} connected");

            while (true)
            {
                switch(GetUserCommand())
                {
                    case 0:
                        WriteLine($"# {userName} subscribe to news");
                        break;
                    case 1:
                        WriteLine($"# {userName} begin a chat");
                        break;
                    case 2:
                        WriteLine($"# {userName} shop in a market");
                        break;
                    case 3:
                        WriteLine($"# {userName} send a message");
                        break;
                    case 4:
                        WriteLine($"# {userName} subscribe to news");
                        break;

                }
            }
        }

        static int GetUserCommand()
        {
            Random r = new Random();
            return r.Next(0,4);
        }
    }
}
