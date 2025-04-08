using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


/*
Задание 3
	Создайте приложение для подсчета всех чисел Фибоначчи от 0 до границы, указанной пользователем. Используйте оконный интерфейс и механизмы асинхронности. 

*/

namespace CalculatedFibonacciAsync
{
    public partial class MainWindow : Window
    {
        private delegate List<long> FibonacciDelegate(long max);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ResultBox.Text = "Вычисление...";
            if(!long.TryParse(InputLimit.Text, out long max) || max < 0)
            {
                MessageBox.Show("Введите корретное число");
                return;
            }
            FibonacciDelegate delegatFib = GenerateFibonacci;
            delegatFib.BeginInvoke(max, new AsyncCallback(ComplitedGenFibonacci), delegatFib);
        }


        private List<long> GenerateFibonacci(long max) // Генератор чисел Фибоначчи
        {
            List<long> result = new List<long>();
            long a = 0;
            long b = 1;
            while(a <= max)
            {
                result.Add(a);
                long temp = a;
                a = b;
                b = temp + a;
            }
            return result;
        }

        private void ComplitedGenFibonacci(IAsyncResult ar)
        {
            FibonacciDelegate delegatFib = (FibonacciDelegate)ar.AsyncState;
            List<long> result = delegatFib.EndInvoke(ar);
            Dispatcher.Invoke(() =>
            {
                StringBuilder fibBuilder = new StringBuilder();
                foreach(var num in result)
                {
                    fibBuilder.Append(num + " ");
                }
                ResultBox.Text = fibBuilder.ToString();
            });

        }
    }
}
