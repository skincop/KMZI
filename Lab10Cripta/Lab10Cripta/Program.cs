using System.Diagnostics;
using System.Numerics;

namespace Lab10Cripta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            var aValues = new decimal[] { 7,33};
            var xValues = new decimal[] { 8100,8281,8464,8649,8836,9025,9216,9409,9604 };
            var nValues = new BigInteger[]
            {
                BigInteger.Pow(2, 1023),
                BigInteger.Pow(2, 2047)
            };
            sw.Start();
            foreach (var a in aValues) {
                foreach (var x in xValues)
                {
                    foreach (var n in nValues)
                    {
                        Console.WriteLine($"A: {a} X: {x} N: {n.GetBitLength()} time: {sw.Elapsed}");
                        sw.Restart();
                    }
                }
            }
            sw.Stop();
        }
    }
}