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
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
