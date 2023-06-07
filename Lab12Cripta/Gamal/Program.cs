using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gamal
{
    internal class Program
    {
        public static int getParam(int a, int n)
        {
            int res = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((a * i) % n) == 1) return (i);
            }
            return (res);
        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"\nGamal Sign\n");

            sw.Start();
            int pCoef = 2137;
            int gCoef = 2127;
            int x = 1116;
            int y = (int)BigInteger.ModPow(gCoef, x, pCoef);
            int kCoef = 7;
            int a = (int)BigInteger.ModPow(gCoef, kCoef, pCoef);
            Console.WriteLine($"Params   p={pCoef}\n g={gCoef}\n  x={x}\n y={y}\n  k={kCoef}\n a={a}\n\n");
            int H = 2119;
            int m = pCoef - 1;
            int k_1 = getParam(kCoef, pCoef - 1);
            var b = new BigInteger((k_1 * (H - (x * a) % m) % m) % m);
            Console.WriteLine($" Params H={H}\n k_1={k_1}\n    b={b}\n S = {a},{b} \n\n");
            Console.WriteLine("\n Verify:");
            sw.Stop();
            Console.WriteLine($"Generate Time {sw.Elapsed}");
            sw.Reset();
            sw.Start();
            var ya = BigInteger.ModPow(y, a, pCoef);
            var ab = BigInteger.ModPow(a, b, pCoef);
            var hash1 = BigInteger.ModPow(ya * ab, 1, pCoef);
            var has2 = BigInteger.ModPow(gCoef, H, pCoef);
            if (hash1 == has2)
                Console.WriteLine($" {hash1}  =  {has2}\n Valid");
            else Console.WriteLine($"Invalid sign {hash1}!= {has2}");
            sw.Stop();
            Console.WriteLine($"Verify Time {sw.Elapsed}");
            sw.Reset();
            sw.Start();
            Console.ReadKey();
        }
    }
}
