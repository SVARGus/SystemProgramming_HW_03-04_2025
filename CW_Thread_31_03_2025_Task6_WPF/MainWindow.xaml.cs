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

namespace CW_Thread_31_03_2025_Task6_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(StartTB.Text, out int start)||
                !int.TryParse(EndTB.Text, out int end)||
                !int.TryParse(CountThreadTB.Text, out int countThread))
            {
                MessageBox.Show("Введите коректные числа");
                return;
            }
            if(countThread <= 0)
            {
                MessageBox.Show("Количество потоков не может быть меньше 1");
                return;
            }
            if(start > end)
            {
                (start, end) = (end, start);
            }
            ContainerThread.Items.Clear();
            for(int i = 0; i < countThread; ++i)
            {
                List<int> mas = new List<int> { start, end, i };
                new Thread(() => OutPutNumber(mas)).Start();
            }
        }
        private void OutPutNumber(List<int> mas)
        {
            string tabs = new string(' ', mas[2] * 4);

            for (int i = mas[0]; i <= mas[1]; i++)
            {
                string numberText = $"{tabs}{i}";
                Dispatcher.Invoke(() =>
                {
                    ContainerThread.Items.Add(numberText);
                });
            }
        }
    }
}
