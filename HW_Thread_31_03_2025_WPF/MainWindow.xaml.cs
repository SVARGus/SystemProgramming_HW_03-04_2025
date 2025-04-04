using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



/*
Задание 1
	Создайте оконное приложение,генерирующее набор простых чисел в диапазоне, указанном пользователем. 
    Если не указана нижняя граница, поток с стартует с 2. 
    Если не указана верхняя граница, генерирование происходит до завершения приложения. 
    Используйте механизм потоков. Числа должны отображаться в оконном интерфейсе
Задание 2
	Добавьте к первому заданию поток, генерирующий набор чисел Фибоначчи. Числа должны отображаться в оконном интерфейсе.
Задание 3
	Добавьте ко второму заданию кнопки для полной остановки каждого из потоков. Одна кнопка на один поток. 
    Если пользователь нажал на кнопку остановки, поток полностью прекращает свою работу.
Задание 4
	Добавьте к третьему заданию кнопки для приостановления и возобновления каждого из потоков. 
    Например, пользователь может приостановить генерацию чисел Фибоначчи по нажатию на кнопку. 
    Продолжение генерации возможно по нажатию на другую кнопку.
Задание 5
	Добавьте к четвертому заданию возможность полного рестарта потоков с новыми границами.
*/

namespace HW_Thread_31_03_2025_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Переменные простых чисел
        private Thread _primeThread;
        private int _primeTo = 2; // Если не указана нижняя граница, поток с стартует с 2.
        private int? _primeFor = null; // Если не указана верхняя граница, генерирование происходит до завершения приложения.
        private bool _primeRunning = false; // флаг запуска
        private bool _primePaused = false; // флаг паузы

        // Переменные числа Фибоначчи
        private Thread _fibonacciThread;
        private int _fibonacciCount = 0;
        private bool _fibonacciRunning = false;
        private bool _fibonacciPaused = false;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void GeneratePrimes() // Генератор простых чисел в PrimeNumbersTextBlock
        {

        }

        private bool IsPrime(int number) // Проверка числа (простое или нет)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for(int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        private void PrimeStartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrimeStopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrimePauseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrimeResumeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrimeRestartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GenerateFibonacci() // Генератор чисел Фибоначчи
        {

        }

        private void FibonacciStartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FibonacciStopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FibonacciPauseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FibonacciResumeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FibonacciRestartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FullRestartButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
