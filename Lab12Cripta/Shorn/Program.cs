using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shorn
{
    internal class Program
    {
        public static BigInteger hashMD5(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Shorn sign");
            sw.Start();
            var t = DateTime.Now;
            BigInteger p = 2267;
            BigInteger q = 103;

            string text = File.ReadAllText(".\\text.txt");
            BigInteger g = 354; 
            BigInteger obg = 967;
            int x = 30;

            BigInteger y = BigInteger.ModPow(obg, x, p);
            BigInteger a = BigInteger.Pow(g, 13) % p;
            BigInteger hash = hashMD5(text + a.ToString());
            sw.Stop();
            Console.WriteLine($"Generate time {sw.ElapsedMilliseconds}");
            sw.Reset();
            sw.Start();

            File.WriteAllText(".\\Test.txt", hash.ToString());
            BigInteger b = (13 + x * hash) % q;
            BigInteger dov = BigInteger.ModPow(g, b, p);
            BigInteger X = (dov * BigInteger.ModPow(y, hash, p)) % p;
            BigInteger hash2 = hashMD5((text + X.ToString()));

            var result = hash == hash2 ? "Valid" : "Invalid";
            Console.WriteLine($"Hash {hash} and {hash2}");
            Console.WriteLine(result);
            sw.Stop();
            Console.WriteLine($"Verify time {sw.Elapsed}");
            sw.Reset();
            string text2 = File.ReadAllText(".\\FakeTest.txt");
            BigInteger hash3 = hashMD5((text2 + X.ToString()));
            result = hash == hash3 ? "Valid" : "Invalid";
            Console.WriteLine($"Hash {hash} and {hash3}");
            Console.WriteLine(result);
            Console.WriteLine("Shnorr:" + (DateTime.Now - t));
            Console.ReadLine();
        }
    }
}
