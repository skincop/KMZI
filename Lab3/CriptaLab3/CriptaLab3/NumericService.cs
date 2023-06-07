using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptaLab3
{
    public class NumericService
    {
        public List<int> GetPrimeList(int start,int end)
        {
            var list = new List<int>();
            for (int i = start; i <= end; i++)
            {
                if (IsPrimeNumeric(i))
                {
                    list.Add(i);
                }
            }
            return list;
        }
        public bool IsPrimeNumeric(int numeric)
        {
            if (numeric == 1) return false;
            for (int i = 2; i < numeric; i++)
            {
                if (numeric % i == 0)
                    return false;
            }
            return true;
        }


        public string GetCannonForm(int numeric)
        {
            int factor = 2;
            string res = "";


            while (numeric != 1)
                if (numeric % factor == 0)
                {
                    res += string.Format("{0}*", factor);
                    numeric /= factor;
                }
                else
                    factor++;
            return res.Remove(res.Length - 1);
        }

        public int GetNod(int a,int b)
        {
            int min = Math.Min(a, b);
            while (min > 1)
            {
                if (a % min == 0 && b % min == 0)
                    break;
                min--;
            }
            return min;
        }

        public int GetNod(int a, int b, int c)
        {
            int min = Math.Min(a, Math.Min(b, c));
            while (min > 1)
            {
                if (a % min == 0 && b % min == 0 && c % min == 0)
                    break;
                min--;
            }
            return min;
        }

    }
}
