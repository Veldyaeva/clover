using SewingProduction.Features.CardByNom.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SewingProduction.Features.CardByNom.Services
{
    public static class Block14Composer
    {
        // "NN% + имя + (подтип?)" до следующего процента или конца строки
        private static readonly Regex PartRe = new(
        @"(?<pct>\d{1,3}(?:[.,]\d+)?)\s*%\s*(?<name>[^\d%()]+?)(?:\s*\((?<sub>[^)]*)\))?(?=\s*\d{1,3}(?:[.,]\d+)?\s*%|$)",
        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        //(
        //    @"(?<pct>\d{1,3}(?:[.,]\d+)?)\s*%\s*(?<name>[^\d%()]+?)(?:\s*\((?<sub>[^)]*)\))?(?=\s*\d{1,3}(?:[.,]\d+)?\s*%|$)",
        //    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        /// <summary>
        /// Склеивает в блоки по width (по умолчанию 14). Внутри блока пробелов нет.
        /// Пробел ставится только между блоками. Подтип "(...)" переносится в следующий
        /// блок, если целиком не помещается к текущему.
        /// </summary>
        /// <param name="input">Строка для обработки</param>
        /// <param name="width">длина фрагмента</param>
        /// <param name="cutLongTokens">true: сверхдлинные токены режем жёстко кусками по width.</param>
        /// <param name="cutChar">Символ, которым склеиваются блоки (обычно пробел или перенос строки)</param>
        /// <param name="padLines">Управляет добивкой незаполненных блоков до ровно widht.
        ///false — блоки могут быть короче widht (последний и любые «раньше закрытые»).
        ///true — любой незаполненный блок(включая последний) дополняется справа символом padChar до длины widht.</param>
        /// <param name="padChar">Символ, которым заполняются добивки, когда padLast = true</param>
        /// <returns></returns>
        public static string Format(string input, int width = 14, bool cutLongTokens = false, char cutChar = ' ', bool padLines = false, char padChar = '·')
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            if (width < 1) width = 1;

            // Разбираем на части: "20%Шерсть" + "(ангора)" (sub — отдельно, но приклеим к «хвосту» токена)
            var parts = ParseParts(input);

            var lines = new List<string>();
            var line = new StringBuilder();

            foreach (var p in parts)
            {
                // Голова токена: "NN%Имя" (без пробелов)
                var head = p.Head;

                // Если голова длиннее ширины — будем резать, НО сначала завершим текущую строку,
                // чтобы не оставлять хвост токена на предыдущей строке.
                if (head.Length > width)
                {
                    FlushIfAny(lines, line, width, padLines, padChar);
                    line.Clear();

                    foreach (var slice in HardSlices(head, width))
                    {
                        if (slice.Length == width)
                            lines.Add(slice);
                        else
                            line.Append(slice);
                    }
                }
                else
                {
                    // Пытаемся положить "голову" в текущую строку целиком; если не влезает — переносим на новую строку.
                    if (line.Length + head.Length <= width)
                    {
                        line.Append(head);
                    }
                    else
                    {
                        FlushIfAny(lines, line, width, padLines, padChar);
                        line.Clear();
                        line.Append(head);
                    }
                }

                // Теперь пытаемся приклеить подтип "(...)" к хвосту токена.
                if (!string.IsNullOrEmpty(p.Paren))
                {
                    if (line.Length + p.Paren.Length <= width)
                    {
                        // Влезает — клеим без пробела (внутри строки пробелов нет)
                        line.Append(p.Paren);
                    }
                    else
                    {
                        // Не влезает — переносим целиком на новую строку
                        FlushIfAny(lines, line, width, padLines, padChar);
                        line.Clear();

                        if (p.Paren.Length > width)
                        {
                            // Теоретически редко, но если "(...)" > width — режем жёстко
                            foreach (var slice in HardSlices(p.Paren, width))
                            {
                                if (slice.Length == width)
                                    lines.Add(slice);
                                else
                                    line.Append(slice);
                            }
                        }
                        else
                        {
                            line.Append(p.Paren);
                        }
                    }
                }
            }

            FlushIfAny(lines, line, width, padLines, padChar);
            return string.Join(cutChar, lines);
        }

        // --- helpers ---

        private sealed record Part(string Head, string Paren);

        private static IEnumerable<Part> ParseParts(string input)
        {
            var m = PartRe.Matches(input);
            if (m.Count == 0)
            {
                // Не распознанный формат — печатаем всё как один токен без пробелов
                yield return new Part(Regex.Replace(input, @"\s+", ""), null);
                yield break;
            }

            foreach (Match mm in m)
            {
                var pct = mm.Groups["pct"].Value;                                 // "20"
                var name = Regex.Replace(mm.Groups["name"].Value, @"\s+", "");     // "Шерсть" (без пробелов)
                var head = $"{pct}%{name}";

                string paren = null;
                var sub = mm.Groups["sub"];
                if (sub.Success && sub.Length > 0)
                    paren = "(" + sub.Value.Trim() + ")";

                yield return new Part(head, paren);
            }
        }

        private static IEnumerable<string> HardSlices(string s, int width)
        {
            for (int i = 0; i < s.Length; i += width)
                yield return s.Substring(i, Math.Min(width, s.Length - i));
        }

        private static void FlushIfAny(List<string> lines, StringBuilder line, int width, bool padLines, char padChar)
        {
            if (line.Length == 0) return;
            if (padLines && line.Length < width)
                line.Append(new string(padChar, width - line.Length));
            lines.Add(line.ToString());
        }
    }
}
//пример использования:
//using SewingProduction.Features.CardByNom.Services;
// private void simpleButton1_Click(object sender, EventArgs e)
//{
//    var a = Block14Composer.Format(textEdit2.Text);

//    textEdit3.Text = a.ToString();
//}