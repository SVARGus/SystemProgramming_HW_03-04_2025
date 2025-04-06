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
        private int _primeFrom = 2; // Если не указана нижняя граница, поток с стартует с 2.
        private int? _primeTo = null; // Если не указана верхняя граница, генерирование происходит до завершения приложения.
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
            int current = _primeFrom;
            StringBuilder primesBuilder = new StringBuilder(capacity: 500);
            while (_primeRunning)
            {
                if(IsPrime(current))
                {
                    Dispatcher.Invoke(() => {
                        PrimeNumbersTextBlock.AppendText(current + " ");
                        PrimeNumbersTextBlock.ScrollToEnd();
                    });
                    Thread.Sleep(100);
                }
                if(_primeTo.HasValue && current > _primeTo.Value) 
                    break;
                current++;
            }
            primesBuilder.Clear();
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

        private void UpdatePrimeButtons(bool running)
        {
            Dispatcher.Invoke(() =>
            {
                PrimeStartButton.IsEnabled = !running;
                PrimeStopButton.IsEnabled = running;
                PrimePauseButton.IsEnabled = running && !_primePaused;
                PrimeResumeButton.IsEnabled = running && _primePaused;
                PrimeRestartButton.IsEnabled = running;
            });
        }

        private void PrimeStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PrimeFromTextBox.Text, out _primeFrom) || _primeFrom < 2)
                _primeFrom = 2;
            if (int.TryParse(PrimeToTextBox.Text, out int to) && to >= _primeFrom)
                _primeTo = to;
            else
                _primeTo = null;

            PrimeNumbersTextBlock.Text = null;
            _primeRunning = true;

            _primeThread = new Thread(GeneratePrimes);
            _primeThread.IsBackground = true;
            _primeThread.Start();

            UpdatePrimeButtons(true);
        }

        private void PrimeStopButton_Click(object sender, RoutedEventArgs e)
        {
            _primeRunning = false;
            UpdatePrimeButtons(false);
        }

        private void PrimePauseButton_Click(object sender, RoutedEventArgs e)
        {
            _primeThread.Suspend();
            _primePaused = true;
            UpdatePrimeButtons(true);
        }

        private void PrimeResumeButton_Click(object sender, RoutedEventArgs e)
        {
            _primeThread.Resume();
            _primePaused = false;
            UpdatePrimeButtons(true);
        }

        private void PrimeRestartButton_Click(object sender, RoutedEventArgs e)
        {
            _primeThread.Suspend();
            PrimeStopButton_Click(sender, e);
            PrimeStartButton_Click(sender, e);
        }

        private void GenerateFibonacci() // Генератор чисел Фибоначчи
        {
            long a = 0;
            long b = 1;
            int count = 0;
            StringBuilder fibBuilder = new StringBuilder(capacity: 500);

            Dispatcher.Invoke(() =>
            {
                FibonacciNumbersTextBlock.AppendText(a + " " + b + " ");
                FibonacciNumbersTextBlock.ScrollToEnd();
            });
            if (_fibonacciCount == 1) return;
            if (_fibonacciCount == 2) return;

            while(_fibonacciRunning && (_fibonacciCount == 0 || count < _fibonacciCount - 2))
            {
                long next = a + b;
                Dispatcher.Invoke(() =>
                {
                    FibonacciNumbersTextBlock.AppendText(next + " ");
                    FibonacciNumbersTextBlock.ScrollToEnd();
                });
                a = b;
                b = next; 
                count++;
                Thread.Sleep(100);
            }
            fibBuilder.Clear();
        }

        private void UpdateFibonacciButtons(bool running)
        {
            Dispatcher.Invoke(() =>
            {
                FibonacciStartButton.IsEnabled = !running;
                FibonacciStopButton.IsEnabled = running;
                FibonacciPauseButton.IsEnabled = running && !_fibonacciPaused;
                FibonacciResumeButton.IsEnabled = running && _fibonacciPaused;
                FibonacciRestartButton.IsEnabled = running;
            });
        }

        private void FibonacciStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(FibonacciCountTextBox.Text, out _fibonacciCount) || _fibonacciCount <= 0)
                _fibonacciCount = 0;

            FibonacciCountTextBox.Clear();
            _fibonacciRunning = true;
            _fibonacciThread = new Thread(GenerateFibonacci);
            _fibonacciThread.IsBackground = true;
            _fibonacciThread.Start();
            UpdateFibonacciButtons(true);
        }

        private void FibonacciStopButton_Click(object sender, RoutedEventArgs e)
        {
            _fibonacciRunning = false;
            UpdateFibonacciButtons(false);
        }

        private void FibonacciPauseButton_Click(object sender, RoutedEventArgs e)
        {
            _fibonacciThread.Suspend();
            _fibonacciPaused = true;
            UpdateFibonacciButtons(true);
        }

        private void FibonacciResumeButton_Click(object sender, RoutedEventArgs e)
        {
            _fibonacciThread.Resume();
            _fibonacciPaused = false;
            UpdateFibonacciButtons(true);
        }

        private void FibonacciRestartButton_Click(object sender, RoutedEventArgs e)
        {
            _fibonacciThread.Suspend();
            FibonacciStopButton_Click(sender, e);
            FibonacciStartButton_Click(sender, e);
        }

        private void FullRestartButton_Click(object sender, RoutedEventArgs e)
        {
            FibonacciRestartButton_Click(sender, e);
            PrimeRestartButton_Click(sender, e);
        }
    }
}
