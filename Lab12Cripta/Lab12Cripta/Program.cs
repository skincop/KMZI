using System.Diagnostics;

namespace Lab12Cripta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"\nRSA Sign\n");

            var rsa = new RSA();
            string M = File.ReadAllText("in.txt");
            Process.Start("in.txt");
            int p = 101;
            int q = 103;

            string hash = M.GetHashCode().ToString();
            int n = p * q;
            int m = (p - 1) * (q - 1);
            int d = rsa.getCoefD(m);
            int e_ = rsa.getCoefE(d, m);
            Console.WriteLine($" p = {p}\n q = {q}\n n = {n}\n ф(n) = {m}\n d = {d}\n e = {e_}\n M = {M}\n Хеш = {hash}\n");

            List<string> sign = rsa.Encode(hash, e_, n);

            while (true)
            {
                Console.ReadKey();
                {
                    List<string> input = new List<string>();
                    string hash2 = File.ReadAllText("in.txt").GetHashCode().ToString();


                    string result = rsa.Decode(sign, d, n);
                    Console.WriteLine($"Хэш эл подписи = {result}");
                    Console.WriteLine($"Хэш файла = {hash2}");

                    if (result.Equals(hash2)) Console.WriteLine("Valid Sign \n");
                    else Console.WriteLine("Invalid Sign\n");

                }
            }
        }
    }
}