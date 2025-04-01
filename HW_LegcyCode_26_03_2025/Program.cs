using System;
using System.Runtime.InteropServices;


/*
Задание 1
Разработайте приложение, которое использует унаследованный код. Вам необходимо использовать функцию MessageBox из Windows API.
Отобразите с помощью MessageBox информацию о вас. Данные должны быть показаны в нескольких MessageBox (на C#)

Задание 2 (будет выполнено в другом проекте)
Разработайте приложение, которое использует унаследованный код.
Вам необходимо использовать функции FindWindow (поиск окна в системе), SendMessage (отсылка сообщений) из WindowsAPI. 
Приложение должно произвести поиск окна вашего оконного приложения (можно реализовать его с помощью Windows Forms и т.д.). 
Если окно найдено необходимо послать ему сообщение в за висимости от выбора пользователя:
 ■ об изменении заголовка окна на заголовок, введенный пользователем о закрытии окна;
 ■ ваш вариант

Задание 3 (будет выполнено в другом проекте)
Разработайте приложение, которое использует унаследованныйкод. Вам необходимо использовать функции Beep и MessageBeep из Windows API. 
С помощью импортированных функций сгенерируйте набор звуковых сигналов через определенные промежутки времени.
*/

namespace HW_LegacyCode
{
    public class DllImportExample
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1
            DllImportExample.MessageBox(IntPtr.Zero, "Кузнецов Павел Николаевич", "FIO", 0);
            DllImportExample.MessageBox(IntPtr.Zero, "Дата рождения 24.01.1989", "Birth", 0);
            DllImportExample.MessageBox(IntPtr.Zero, "Обучаюсь в Компьютерной академии ТОП по направлению \"Разработка ПО\"", "Education", 0);
        }
    }
}
