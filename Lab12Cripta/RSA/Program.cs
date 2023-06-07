using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;

namespace Lab12Cripta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"\nRSA Sign\n");

            var rsa = new RSA();
            string M = File.ReadAllText("in.txt");
            Process.Start("in.txt");
            int p = 101;
            int q = 103;

            sw.Start();

            string hash = M.GetHashCode().ToString();
            int n = p * q;
            int m = (p - 1) * (q - 1);
            int d = rsa.getCoefD(m);
            int e_ = rsa.getCoefE(d, m);
            Console.WriteLine($" p = {p}\n q = {q}\n n = {n}\n ф(n) = {m}\n d = {d}\n e = {e_}\n M = {M}\n Хеш = {hash}\n");

            List<string> sign = rsa.Encode(hash, e_, n);
            sw.Stop();
            Console.WriteLine($"Generate Sign time: {sw.ElapsedMilliseconds}");
            sw.Reset();

            while (true)
            {
                Console.ReadKey();
                {
                    sw.Start();
                    List<string> input = new List<string>();
                    string hash2 = File.ReadAllText("in.txt").GetHashCode().ToString();


                    string result = rsa.Decode(sign, d, n);
                    sw.Stop();
                    Console.WriteLine($"Sigh Hash = {result}");
                    Console.WriteLine($"Message hash = {hash2}");
                    Console.WriteLine($"Verify Sign time: {sw.ElapsedMilliseconds}");

                    if (result.Equals(hash2)) Console.WriteLine("Valid Sign \n");
                    else Console.WriteLine("Invalid Sign\n");
                    sw.Reset();

                }
            }
        }
    }
}