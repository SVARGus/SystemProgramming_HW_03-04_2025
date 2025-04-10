using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;


/*
Задание 4
	Создайте приложение для поиска слова в некотором файле. 
    Слово и путь к файлу указываются пользователем. 
    Для решения задачи используйте оконный интерфейс и асинхронность. 
    По итогам поиска приложение должно показать сколько раз слово встретилось в файле. 
 
*/

namespace FileWordSearch_WPF
{
    public partial class MainWindow : Window
    {
        private delegate int SearchTextDelegate(string strSearch, string fileName, bool caseSensitive);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Все файлы (*.*)|*.*";

            if(openFileDialog.ShowDialog() == true)
            {
                SelectFileTextBox.Text = openFileDialog.FileName;
            }
        }

        private void StartSearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            string fileName = SelectFileTextBox.Text;
            bool caseSensitive = CaseSensitiveCheckBox.IsChecked == true;

            if(string.IsNullOrEmpty(searchText) || string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("укажите слово для поиска и выберите файл");
                return;
            }

            SearchTextDelegate searchTextDelegate = new SearchTextDelegate(SearchText);
            AsyncCallback callback = new AsyncCallback(SearchCompleted);

            searchTextDelegate.BeginInvoke(searchText, fileName, caseSensitive, callback, searchTextDelegate);

            StartSearchButton.IsEnabled = false;

        }

        private int SearchText(string strSearch, string fileName, bool caseSensitive)
        {
            int countSearch = 0;
            string filleContent = File.ReadAllText(fileName);

            if(!caseSensitive)
            {
                filleContent = filleContent.ToLower();
                strSearch = strSearch.ToLower();
            }

            int index = 0;
            while((index = filleContent.IndexOf(strSearch, index))!= -1)
            {
                ++countSearch;
                index += strSearch.Length;
            }

            return countSearch;
        }

        private void SearchCompleted(IAsyncResult ar)
        {
            if(ar.AsyncState is SearchTextDelegate del)
            {
                int result = del.EndInvoke(ar);
                Dispatcher.Invoke(() =>
                {
                    CountSearchTextBlock.Text = result.ToString();
                    StartSearchButton.IsEnabled = true;
                });
            }
        }

    }
}
