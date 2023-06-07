using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab12Cripta
{
    public class RSA
    {
        char[] alphabet = new char[] { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };
        public bool isPrime(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }
        public int getCoefD(int m)
        {
            int d = m - 1;

            for (int i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0)) ///если имеют общие делители
                {
                    d--;
                    i = 1;
                }
            return d;
        }

        public int getCoefE(int d, int m)
        {
            int e = 10;

            while (true)
            {
                if ((e * d) % m == 1) break;
                else e++;
            }
            return (int)e;
        }

        public List<string> Encode(string s, long e, long n)
        {
            List<string> result = new List<string>();
            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(alphabet, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }
            return result;
        }

        public string Decode(List<string> input, long d, long n)
        {
            string result = "";

            System.Numerics.BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += alphabet[index].ToString();
            }

            return result;
        }
    }
}
