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
Задание 5
	Создайте приложения для поиска слова в файлах некоторой директории. 
    Вход в поддиректории обязателен. 
    Слово и путь к директории указываются пользователем. 
    Для решения задачи используйте оконный интерфейс и асинхронность. 
    По итогам поиска приложение должно показать отчет в формате:
	Название файла: ……
	Путь к файлу: ….
	Количество вхождений слова: ….

*/

namespace DirectoryWorldFinder_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
