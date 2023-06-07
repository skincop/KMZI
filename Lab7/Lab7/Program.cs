using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CriptaLab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Алгоритм DES-EE2, шифруем 3 раза на пером и 3 шаге применяем 1 ключ, на 2ом шаге 2ой");
            Console.WriteLine("Введите текст");
            string text = Console.ReadLine();

            text = GetBinaryString(text);

            Console.WriteLine("Введите ключ 1");
            string key1 = Console.ReadLine();
            Console.WriteLine("Введите ключ 2");
            string key2 = Console.ReadLine();
            key1 = GetBinaryString(key1);
            key2 = GetBinaryString(key2);

            var des = new DesEnctyptManager();
            sw.Start();
            var res = des.GetEncryptTextWithKey(text, key1);
            res = des.GetEncryptTextWithKey(res, key2);
            res = des.GetEncryptTextWithKey(res, key1);
            sw.Stop();

            Console.WriteLine($"Зашифрованное слово: {res} за время {sw.Elapsed} {sw.ElapsedMilliseconds}");

            var des2 = new DesEnctyptManager();
            var res2 = des.GetDecryptTextWithKey(res, key1);
            res2 = des.GetDecryptTextWithKey(res2, key2);
            res2 = des.GetDecryptTextWithKey(res2, key1);
            res2 = Regex.Replace(new string(Encoding.ASCII.GetChars(GetByteArrayFromHexString(res2))).Trim(), @"[^\u0020-\u007E]", string.Empty);
            Console.WriteLine($"Расшифрованное слово: {res2} за время {sw.Elapsed} {sw.ElapsedMilliseconds}");
            Console.ReadKey();

        }

        public static byte[] GetByteArrayFromHexString(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        public static string GetBinaryString(string text)
        {
            text = string.Join("", Encoding.ASCII.GetBytes(text).Select(c => c.ToString("X2")));
            return text;
        }
    }
}
