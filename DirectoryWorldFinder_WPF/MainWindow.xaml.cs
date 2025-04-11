using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
//using System.Windows.Shapes;
//using System.Windows.Forms;


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
        private delegate int SearchTextDelegate(string strSearch, string fileName, bool caseSensitive);
        private List<FileSearchResult> results = new List<FileSearchResult>();
        private bool cancelRequested = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            using(var dialog  = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Выберите папку для поиска";
                dialog.ShowNewFolderButton = false;

                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    SelectDerictoriiTextBox.Text = dialog.SelectedPath;
                }
            }

        }

        private void StartSearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            string directory = SelectDerictoriiTextBox.Text;
            bool caseSensitive = CaseSensitiveCheckBox.IsChecked == true;

            if (string.IsNullOrEmpty(searchText) || string.IsNullOrEmpty(directory))
            {
                MessageBox.Show("укажите слово для поиска и выберите папку");
                return;
            }

            cancelRequested = false;
            StartSearchButton.IsEnabled = false;
            ResultsListBox.ItemsSource = null;
            results.Clear();

            string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

            foreach(var file in files)
            {
                SearchTextDelegate searchTextDelegate = new SearchTextDelegate(SearchText);
                searchTextDelegate.BeginInvoke(searchText, file, caseSensitive, SearchCompleted, new object[] { searchTextDelegate, file });
            }

        }

        private int SearchText(string strSearch, string fileName, bool caseSensitive)
        {
            if(cancelRequested)
            {
                return 0;
            }

            int countSearch = 0;
            string filleContent;

            try
            {
                filleContent = File.ReadAllText(fileName);
            }
            catch
            {
                return 0;
            }

            if (!caseSensitive)
            {
                filleContent = filleContent.ToLower();
                strSearch = strSearch.ToLower();
            }

            int index = 0;
            while ((index = filleContent.IndexOf(strSearch, index)) != -1)
            {
                ++countSearch;
                index += strSearch.Length;
            }

            return countSearch;
        }

        private void SearchCompleted(IAsyncResult ar)
        {
            object[] state = (object[])ar.AsyncState;
            SearchTextDelegate del = (SearchTextDelegate)state[0];
            string file = (string)state[1];
            int count = del.EndInvoke(ar);

            if(cancelRequested || count == 0)
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                results.Add(new FileSearchResult
                {
                    fileName = Path.GetFileName(file),
                    filePath = file,
                    wordCount = count
                });

                ResultsListBox.ItemsSource = null;
                ResultsListBox.ItemsSource = results;
            });
        }

        private void StoptSearchButton_Click(object sender, RoutedEventArgs e)
        {
            cancelRequested = true;
            StartSearchButton.IsEnabled = true;
        }
    }
}
