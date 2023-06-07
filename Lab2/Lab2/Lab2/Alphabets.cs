using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Alphabets
    {
        public static char[] Curillic { get; set; }
        public static char[] nor { get; set; }
        public static char[] Binary { get; set; }


        public double getEntropy(string fileContent, char[] alphabet)
        {
            fileContent = fileContent.ToUpper();

            var symbolFrequency = new int[alphabet.Length];
            var symbolProbability = new double[alphabet.Length];

            double probabilitySym=0;
            double entropy = 0;

            for (int i = 0; i < alphabet.Length; i++)
            {
                symbolFrequency[i] = fileContent.Where(symbol => symbol == alphabet[i]).Count();
                symbolProbability[i] = (double)symbolFrequency[i] / fileContent.Length;
                probabilitySym += symbolProbability[i];
                entropy += symbolProbability[i] != 0 ? symbolProbability[i] * Math.Log2(symbolProbability[i]) : 0;

            }
            //Console.WriteLine(probabilitySym);
            //for (int i=0;i<alphabet.Length; i++)
            //{
            //    Console.WriteLine(alphabet[i] + " ");
            //}
            return -entropy;
        }

        public double getInfoCount(double entropy, int N)
        {
            return entropy * (double)N;
        }

        public double getInfoCountWithErrorProbability(int N, double p)
        {
            if (p == 0.9999999999)
                return 0;
            double q = 1 - p;
            double result = 0;
            result = -p * Math.Log(p, 2) - q * Math.Log(q, 2);
            return N * (1 - result);
        }


        public Alphabets()
        {
            Curillic = new char[] {'А','Б','В','Г','Д','Е','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С','Т','У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ь','Ю','Я'};
            nor = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Æ', 'Ø','Å' };
            Binary = new char[] { '0', '1' };
        }
    }
}
