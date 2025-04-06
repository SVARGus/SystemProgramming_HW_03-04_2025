using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using System.Diagnostics;

/*
Задание 1
	Разработайте приложение, которое отображает список процессов. 
	Пользователь может указать временной интервал для обновления списка. 
	Обязательно создайте оконный интерфейс приложения
Задание 2
	Добавьте к первому заданию возможность выбора конкретного процесса в списке. 
	При выборе процесса отображается детальная информация о нём. Например:
	■ Идентификатор процесса;
	■ Время старта;
	■ Общее количество процессорного времени, потраченного на этот процесс;
	■ Количество потоков;
	■ Количество копий процесса такого вида (если у вас запущено пять блокнотов, должно появиться число пять).
Задание 3
	Добавьте ко второму заданию возможность завершения выбранного процесса.
Задание 4
	Разработайте приложение, которое предоставляет пользователю возможность запуска других приложений. 
	Пользователь может запустить:
	■ Блокнот;
	■ Калькулятор;
	■ Paint;
	■ Своё собственное другое приложение.
*/


namespace HW_Processes_28_03_2025
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private ProcessInfo selectedProcess;
        public ObservableCollection<ProcessInfo> Processes { get; set; } = new ObservableCollection<ProcessInfo>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Processes;
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5)};
            timer.Tick += UpdateProcessList;
            timer.Start();
        }

        private void UpdateProcessList(object sender = null, EventArgs e = null)
        {
            var currentSelection = selectedProcess?.Id;
            Processes.Clear();
            foreach (var process in Process.GetProcesses())
            {
                Processes.Add(new ProcessInfo(process));
            }
            if (currentSelection != null)
            {
                foreach (var process in Processes)
                {
                    if(process.Id == currentSelection)
                    {
                        ProcessesList.SelectedItem = process;
                        break;
                    }
                }
            }
        }

        private void KillProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProcess == null) return;
            Process.GetProcessById(selectedProcess.Id).Kill();
            UpdateProcessList();
            ClearProcessDetails();
        }

        private void StartApp_Click(object sender, RoutedEventArgs e)
        {
            var app = (sender as FrameworkElement)?.Tag as string;
            if (app == null) return;
            try
            {
                Process.Start(app);
                UpdateProcessList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска: {ex.Message}");
            }
        }

        private void ProcessesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProcess = ProcessesList.SelectedItem as ProcessInfo;
            UpdateProcessDetails();
        }

        private void UpdateProcessDetails()
        {
            if(selectedProcess?.Process == null) return;
            try
            {
                var process = selectedProcess.Process;
                ProcessId.Text = process.Id.ToString();
                ProcessName.Text = process.ProcessName.ToString();
                ProcessStartTime.Text = process.StartTime.ToString();
                ProcessCpuTime.Text = process.TotalProcessorTime.ToString();
                ProcessThreadCount.Text = process.Threads.Count.ToString();
                ProcessInstancesCount.Text = Process.GetProcessesByName(process.ProcessName).Length.ToString();
            }
            catch
            {
                ClearProcessDetails();
            }
        }

        private void ClearProcessDetails()
        {
            ProcessId.Text = string.Empty;
            ProcessName.Text = string.Empty;
            ProcessCpuTime.Text = string.Empty;
            ProcessCpuTime.Text = string.Empty;
            ProcessThreadCount.Text = string.Empty;
            ProcessInstancesCount.Text = string.Empty;
        }
    }
}
