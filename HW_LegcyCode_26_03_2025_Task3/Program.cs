using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace HW_LegcyCode_26_03_2025_Task3
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Beep(uint dwFreq, uint dwDuration);


        [DllImport("uder32.dll", SetLastError = true)]
        public static extern bool MessageBeep(uint uType);

        // Константы звуков
        public const uint MB_OK = 0x00000000; // ОК
        public const uint MB_ICONERROR = 0x00000010; // Ошибка
        public const uint MB_ICONQUESTION = 0x00000020; // Вопрос
        public const uint MB_ICONWARNING = 0x00000030; // Предупреждение
        public const uint MB_ICONINFORMATION = 0x00000040; // информация
        public const uint MB_SYSTEMMODAL = 0x00001000; // системный модальный

        static void Main(string[] args)
        {
            Melodia2();           

            Console.WriteLine("для выхода нажмите любую кнопку");
            Console.ReadKey();
        }

        public static void Melodia1()
        {
            for (int i = 0; i < 3; ++i)
            {
                Beep(400, 500);
                Thread.Sleep(500);

                // на ноуте выдало исключение: System.DllNotFoundException: "Не удается загрузить DLL "uder32.dll": Не найден указанный модуль. (Исключение из HRESULT: 0x8007007E)"
                MessageBeep(MB_ICONQUESTION);
                Thread.Sleep(500);

                Beep(800, 400);
                Thread.Sleep(500);

                // на ноуте выдало исключение: System.DllNotFoundException: "Не удается загрузить DLL "uder32.dll": Не найден указанный модуль. (Исключение из HRESULT: 0x8007007E)"
                MessageBeep(MB_ICONINFORMATION);
                Thread.Sleep(500);

                Beep(1000, 200);
                Thread.Sleep(500);

                // на ноуте выдало исключение: System.DllNotFoundException: "Не удается загрузить DLL "uder32.dll": Не найден указанный модуль. (Исключение из HRESULT: 0x8007007E)"
                MessageBeep(MB_OK);
                Thread.Sleep(500);

                Beep(600, 500);
                Thread.Sleep(500);

                // на ноуте выдало исключение: System.DllNotFoundException: "Не удается загрузить DLL "uder32.dll": Не найден указанный модуль. (Исключение из HRESULT: 0x8007007E)"
                MessageBeep(MB_SYSTEMMODAL);
                Thread.Sleep(500);
            }
        }
        public static void Melodia2()
        {
            // Первый куплет (Twinkle Twinkle Little Star)
            Beep(523, 300);  // C5
            Beep(523, 300);  // C5
            Beep(784, 300);  // G5
            Beep(784, 300);  // G5
            Beep(880, 300);  // A5
            Beep(880, 300);  // A5
            Beep(784, 600);  // G5 (длинная)
            Thread.Sleep(200);

            Beep(698, 300);  // F5
            Beep(698, 300);  // F5
            Beep(659, 300);  // E5
            Beep(659, 300);  // E5
            Beep(587, 300);  // D5
            Beep(587, 300);  // D5
            Beep(523, 600);  // C5 (длинная)
            Thread.Sleep(200);

            // Второй куплет
            Beep(784, 300);  // G5
            Beep(784, 300);  // G5
            Beep(698, 300);  // F5
            Beep(698, 300);  // F5
            Beep(659, 300);  // E5
            Beep(659, 300);  // E5
            Beep(587, 600);  // D5 (длинная)
            Thread.Sleep(200);

            Beep(784, 300);  // G5
            Beep(784, 300);  // G5
            Beep(698, 300);  // F5
            Beep(698, 300);  // F5
            Beep(659, 300);  // E5
            Beep(659, 300);  // E5
            Beep(587, 600);  // D5 (длинная)
            Thread.Sleep(200);

            // Финал
            Beep(523, 300);  // C5
            Beep(523, 300);  // C5
            Beep(784, 300);  // G5
            Beep(784, 300);  // G5
            Beep(880, 300);  // A5
            Beep(880, 300);  // A5
            Beep(784, 600);  // G5 (длинная)
            Thread.Sleep(200);

            Beep(698, 300);  // F5
            Beep(698, 300);  // F5
            Beep(659, 300);  // E5
            Beep(659, 300);  // E5
            Beep(587, 300);  // D5
            Beep(587, 300);  // D5
            Beep(523, 1000); // C5 (очень длинная)

            Console.WriteLine("Мелодия завершена!");
        }
    }
}
