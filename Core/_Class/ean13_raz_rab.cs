using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraRichEdit.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SewingProduction
{
    public class BarcodePrinter
    {
        // Строка подключения к базе данных, берется из настроек приложения
        private string connectionString = Properties.Settings.Default.ACEConnectionString3;
        // Имя текущего компьютера
        private string kompName = System.Environment.MachineName;

        // Конструктор класса
        public BarcodePrinter()
        {
        }
        // Функция для генерации штрихкода EAN13 на основе кода
        // Принимает код kod_sh и возвращает сгенерированный штрихкод
        public string GenerateEAN13(string kod_sh)
        {
            // Массивы для кодирования символов EAN13
            string[] ean13_a = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] ean13_b = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            string[] ean13_c = { "&", "'", "(", ")", "*", "+", ",", "-", ".", "/" };
            string[] ean13_d = { "[", "=", "]" };
            // Итоговый штрихкод
            string itog = "";
            // Получаем первый символ кода (контрольную цифру)
            int kont = int.Parse(kod_sh.Substring(0, 1));

            // Кодирование в зависимости от контрольной цифры
            if (kont == 0)
            {
                for (int i = 1; i <= 6; i++)
                {
                    int n = int.Parse(kod_sh.Substring(i + 1, 1));
                    itog += ean13_a[n == 0 ? 9 : n - 1];
                }
            }
            else if (kont == 1)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];
            }
            else if (kont == 2)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];

            }
            else if (kont == 3)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];

            }
            else if (kont == 4)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];
            }
            else if (kont == 5)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];

            }
            else if (kont == 6)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];
            }
            else if (kont == 7)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];

            }
            else if (kont == 8)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];
            }
            else if (kont == 9)
            {
                itog += ean13_a[int.Parse(kod_sh.Substring(1, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(1, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(2, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(2, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(3, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(3, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(4, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(4, 1)) - 1];
                itog += ean13_b[int.Parse(kod_sh.Substring(5, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(5, 1)) - 1];
                itog += ean13_a[int.Parse(kod_sh.Substring(6, 1)) == 0 ? 9 : int.Parse(kod_sh.Substring(6, 1)) - 1];
            }

            itog = ean13_d[0] + itog + ean13_d[1];

            for (int i = 8; i <= 12; i++)
            {
                int m = int.Parse(kod_sh.Substring(i - 1, 1));
                itog += ean13_c[m == 0 ? 9 : m - 1];
            }
            itog += ean13_d[2];
            return itog;
        }
        // Функция для расчета контрольной суммы штрихкода (аналог функции ggg в FoxPro)
        // Принимает строку shtr и возвращает контрольную цифру
        public string CalculateCheckSum(string shtr)
        {
            // Сумма цифр на четных позициях
            int sumEvenPositions = 0;
            // Сумма цифр на нечетных позициях
            int sumOddPositions = 0;

            // Цикл для расчета суммы цифр на нечетных позициях
            for (int i = 1; i <= 11; i += 2)
            {
                sumOddPositions += int.Parse(shtr.Substring(i - 1, 1));
            }
            // Цикл для расчета суммы цифр на четных позициях
            for (int i = 2; i <= 12; i += 2)
            {
                sumEvenPositions += int.Parse(shtr.Substring(i - 1, 1));
            }

            // Общая сумма по формуле (сумма четных * 3 + сумма нечетных)
            int totalSum = sumEvenPositions * 3 + sumOddPositions;
            // Эмуляция ROUND(ff,-1) из FoxPro. Округление до ближайшего десятка
            int roundedSum = (int)Math.Round((double)totalSum / 10.0) * 10;

            // Расчет контрольной цифры
            int checkSumDigit;
            if (roundedSum > totalSum)
                checkSumDigit = (roundedSum - totalSum);
            else if (roundedSum == totalSum)
                checkSumDigit = 0;
            else
                checkSumDigit = (roundedSum + 10 - totalSum);
            // Возвращаем контрольную цифру в виде строки
            return checkSumDigit.ToString();
        }

    }
}