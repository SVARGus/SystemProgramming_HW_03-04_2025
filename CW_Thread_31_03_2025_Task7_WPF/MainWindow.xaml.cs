using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
//using System.Windows.Shapes;

namespace CW_Thread_31_03_2025_Task7_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CalcResults CalcResults { get; set; }
        private const string ResultsFilePath = "results.txt";
        public MainWindow()
        {
            InitializeComponent();
            CalcResults = new CalcResults();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(ResultsFilePath))
            {
                Process.Start("notepad.exe", ResultsFilePath);
            }
            else
            {
                MessageBox.Show($"Файл {Path.GetFullPath(ResultsFilePath)} не найден",
                              "Файл не существует",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(StartTB.Text, out int minValue) ||
            !int.TryParse(EndTB.Text, out int maxValue) ||
            !int.TryParse(CountTB.Text, out int arraySize))
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения",
                              "Ошибка ввода",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                return;
            }

           

            Thread genNum = new Thread(() => MethodGenNum(minValue, maxValue, arraySize));
            genNum.Start();
            genNum.Join();

            List<Thread> threads = new List<Thread>();
            threads.Add(new Thread(MethodMin));
            threads.Add(new Thread(MethodMax));
            threads.Add(new Thread(MethodAverage));
            foreach (var thr in threads)
            {
                thr.Start();
            }
            foreach (var thr in threads)
            {
                thr.Join();
                MinTB.Text = CalcResults.Min.ToString();
                MaxTB.Text = CalcResults.Max.ToString();
                AvgTB.Text = CalcResults.Average.ToString("F2");
            }

            Thread writeToFile = new Thread(MethodWriteResultToFile);
            writeToFile.Start();
            writeToFile.Join();
        }

        private void MethodWriteResultToFile()
        {
            using (StreamWriter writer = new StreamWriter(ResultsFilePath))
            {
                writer.WriteLine("Сгенерированные числа:");
                foreach (var num in CalcResults.Numbers)
                {
                    writer.Write(num + " ");
                }
                writer.WriteLine("\n\nРезультаты вычислений:");
                writer.WriteLine($"Минимальное значение: {CalcResults.Min}");
                writer.WriteLine($"Максимальное значение: {CalcResults.Max}");
                writer.WriteLine($"Среднее значение: {CalcResults.Average:F2}");
            }
            MessageBox.Show($"Результаты успешно записаны в файл {Path.GetFullPath(ResultsFilePath)}",
                              "Файл успешно записан",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
        }
        private void MethodGenNum(int minValue, int maxValue, int arraySize)
        {
            int sizeMus = arraySize;
            int min = minValue;
            int max = maxValue;
            Random r = new Random();
            CalcResults.Numbers = new int[sizeMus];
            for (int i = 0; i < sizeMus; ++i)
            {
                CalcResults.Numbers[i] = r.Next(min, max);
            }
        }
        private void MethodMin()
        {
            CalcResults.Min = CalcResults.Numbers[0];
            for (int i = 1; i < CalcResults.Numbers.Length; ++i)
            {
                if (CalcResults.Numbers[i] < CalcResults.Min)
                    CalcResults.Min = CalcResults.Numbers[i];
            }
        }
        private void MethodMax()
        {
            CalcResults.Max = CalcResults.Numbers[0];
            for (int i = 1; i < CalcResults.Numbers.Length; ++i)
            {
                if (CalcResults.Numbers[i] > CalcResults.Max)
                    CalcResults.Max = CalcResults.Numbers[i];
            }
        }
        private void MethodAverage()
        {
            CalcResults.Average = (double)CalcResults.Numbers.Sum() / CalcResults.Numbers.Length;
        }
    }
}
