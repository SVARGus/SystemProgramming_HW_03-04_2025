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
using System.Diagnostics;

/*
Задание 2

    Создайте эмуляцию конных скачек. В гонке участвуют пять лошадей.
    Каждая лошадь — это отдельный прогресс-бар. При нажатии кнопки Старт начинается гонка. 
    Скорость бега каждой лошади определяется в процессе гонки случайным образом. 
    По итогам скачки нужно показать таблицу результатов. 
    Используйте механизм многопоточности.
 
*/

namespace HorseRacingMultithreading
{
    public partial class MainWindow : Window
    {
        private List<string> ListResultRacing;
        private CountdownEvent countdownEvent;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HorseRacing(object progresBar)
        {
            ProgressBar bar = progresBar as ProgressBar;
            if (bar == null)
                return;
            Random random = new Random();
            double value = 0;
            bar.Dispatcher.Invoke(() =>
            {
                bar.Value = value;
            });
            while ( value < 100)
            {
                value += random.NextDouble() * random.Next(1, 5);
                bar.Dispatcher.Invoke(() =>
                {
                    bar.Value = value;
                });
                Thread.Sleep(random.Next(50, 200));
            }
            string tagValue = "";
            bar.Dispatcher.Invoke(() =>
            {
                tagValue = bar.Tag.ToString();
            });
            ListResultRacing.Add(tagValue);
            countdownEvent.Signal();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();
            //ListResultRacing.Clear();
            ListResultRacing = new List<string>();
            var progresBars = MyGrid.Children.OfType<ProgressBar>().ToList();
            countdownEvent = new CountdownEvent(progresBars.Count);
            foreach (var progress in progresBars)
            {
                progress.Value = 0;
                ThreadPool.QueueUserWorkItem(HorseRacing, progress);
            }
            ThreadPool.QueueUserWorkItem(_ =>
            {
                countdownEvent.Wait();
                Dispatcher.Invoke(() =>
                {
                    if (ListResultRacing.Count > 0)
                        FirstPlase.Text = ListResultRacing.ElementAtOrDefault(0);
                    if (ListResultRacing.Count > 1)
                        SecondPlase.Text = ListResultRacing.ElementAtOrDefault(1);
                    if (ListResultRacing.Count > 2)
                        ThirdPlase.Text = ListResultRacing.ElementAtOrDefault(2);
                    if (ListResultRacing.Count > 3)
                        FourthPlase.Text = ListResultRacing.ElementAtOrDefault(3);
                    if (ListResultRacing.Count > 4)
                        FifthPlase.Text = ListResultRacing.ElementAtOrDefault(4);
                });
            });
        }
        private void ClearResults()
        {
            FirstPlase.Text = "";
            SecondPlase.Text = "";
            ThirdPlase.Text = "";
            FourthPlase.Text = "";
            FifthPlase.Text = "";
        }
    }
}
