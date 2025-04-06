using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;

/*
Задание 1
	Создайте приложение «Танцующие прогресс-бары». 
    Приложение отображает набор прогресс-баров. 
    Их количество определяется пользователем. 
    По нажатию на кнопку прогресс-бары начинают заполняться (величины процесса заполнения и цвет определяются случайным образом). 
    Используйте механизм многопоточности. 

*/

namespace Dancing_progress_bar_Multithread
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random random = new Random();
        private List<Thread> threads = new List<Thread>();
        private bool _stopThreads = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(CountProgressBar.Text, out int count) || count < 1)
            {
                MessageBox.Show("Введите корректное число больше 0");
                return;
            }
            StopButton_Click(null, null);
            ListProgressBar.Children.Clear();
            for(int i = 0; i < count; i++)
            {
                var progressBar = CreateProgressBar();
                ListProgressBar.Children.Add(progressBar);
                Thread thread = new Thread(() => UpdateProgressBar(progressBar));
                thread.IsBackground = true;
                threads.Add(thread);
                thread.Start();
            }
        }

        private ProgressBar CreateProgressBar()
        {
            var progressBar =  new ProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                Margin = new Thickness(5),
                Foreground = new SolidColorBrush(Color.FromRgb(
                    (byte)random.Next(256),
                    (byte)random.Next(256),
                    (byte)random.Next(256))),
                Background = new SolidColorBrush(Color.FromRgb(
                    (byte)random.Next(256),
                    (byte)random.Next(256),
                    (byte)random.Next(256)))
            };
            progressBar.Height = 20;
            return progressBar;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
             _stopThreads = true;
            foreach(var thread in threads)
            {
                if(thread.IsAlive)
                {
                    thread.Join();
                }
            }
            threads.Clear();
            _stopThreads = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            StopButton_Click(null, null);
            ListProgressBar.Children.Clear();
        }

        private void UpdateProgressBar(ProgressBar progressBar)
        {
            double min = 0;
            double max = 100;
            Dispatcher.Invoke(() =>
            {
                min = progressBar.Minimum;
                max = progressBar.Maximum;
            });
            double value = min;
            while(!_stopThreads)
            {
                value += random.NextDouble() * 5;
                if(value > max)
                    value = min;
                progressBar.Dispatcher.Invoke(() =>
                {
                    progressBar.Value = value;
                });
                Thread.Sleep(100);
            }
        }
    }
}
