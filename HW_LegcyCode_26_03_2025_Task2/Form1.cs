using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/*
Задание 2 (будет выполнено в другом проекте)
Разработайте приложение, которое использует унаследованный код.
Вам необходимо использовать функции FindWindow (поиск окна в системе), SendMessage (отсылка сообщений) из WindowsAPI. 
Приложение должно произвести поиск окна вашего оконного приложения (можно реализовать его с помощью Windows Forms и т.д.). 
Если окно найдено необходимо послать ему сообщение в за висимости от выбора пользователя:
 ■ об изменении заголовка окна на заголовок, введенный пользователем о закрытии окна;
 ■ ваш вариант
*/

namespace HW_LegcyCode_26_03_2025_Task2
{
    public partial class Form1: Form
    {
        // Поиск окна в системе
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Отсылка сообщения
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // Изменение размера окна
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const uint WM_SETTEXT = 0x000C;
        private const uint WM_CLOSE = 0x0010;
        private const uint SWP_NOMOVE = 0x0002;


        public Form1()
        {
            InitializeComponent();
            SelectComboBox.Items.AddRange(new object[] { "Изменить заголовок", "Закрыть окно", "Изменить размер окна" });
        }

        

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            // Поиск окна с заголовком SearchTitleTextBox.Text
            IntPtr targetWindow = FindWindow(null, SearchTitleTextBox.Text);

            if (targetWindow == IntPtr.Zero)
            {
                MessageBox.Show("Окно не найдено!");
                return;
            }

            // Выбор действия
            switch (SelectComboBox.SelectedIndex)
            {
                case 0: // Изменить заголовок
                    string newTitle = EnterTitleTextBox.Text;
                    SendMessage(targetWindow, WM_SETTEXT, IntPtr.Zero, Marshal.StringToHGlobalAuto(newTitle));
                    break;

                case 1: // Закрыть окно
                    SendMessage(targetWindow, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    break;

                case 2: // Изменить размер
                    int width = 200;
                    int height = 200;
                    SetWindowPos(targetWindow, IntPtr.Zero, 0, 0, width, height, SWP_NOMOVE);
                    break;
            }
        }
    }
}
