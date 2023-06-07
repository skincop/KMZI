using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptaLab13
{
    public class CurvesManager
    {
        private const int PP = 751;
        private const int A = -1;
        private const string defaultAlphabeth = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        private Random random = new Random();
        private int[,] inputPoints =
    { { 189, 297 }, { 189, 454 }, { 192, 32 }, { 192, 719 }, { 194, 205 }, { 194, 546 }, { 197, 145 }, { 197, 606 },
            { 198, 224 }, { 198, 527 }, { 200, 30 }, { 200, 721 }, { 203, 324 }, { 203, 427 }, { 205, 372 }, { 205, 379 },
            { 206, 106 }, { 206, 645 }, { 209, 82 }, { 209, 669 }, { 210, 31 }, { 210, 720 }, { 215, 247 }, { 215, 504 },
            { 218, 150 }, { 218, 601 }, { 221, 138 }, { 221, 613 }, { 226, 9 }, { 226, 742 }, { 227, 299 }, { 227, 542 } };


        public static int CalculaterMod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public static int CalculateInvertMode(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }

        public void CalculatePoint(int xmin,int xmax)
        {
            for (int x = xmin; x <= xmax; x++)
            {
                Console.WriteLine($"При x = {x}" +
                    $" y = {(Math.Sqrt(Math.Pow(x,3)-x+1))%PP}");
            }
        }

        public  int[] CalculateKP(int koef, int[] Point)
        {
            int[] result = Point;
            for (int i = 0; i < (int)Math.Log(koef, 2); i++)
                result = CalculateSum(result);
            koef = koef - (int)Math.Pow(2, (int)Math.Log(koef, 2));
            while (koef > 1)
            {
                for (int i = 0; i < (int)Math.Log(koef, 2); i++)
                    result = CalculateSum(result, CalculateSum(Point));
                koef = koef - (int)Math.Pow(2, (int)Math.Log(koef, 2));
            }
            if (koef == 1) result = CalculateSum(result, Point);
            return result;
        }

        public  int[] CalculateSum(int[] Point, int[] SecondPoint)
        {
            int lambda = CalculateLambda(Point, SecondPoint, PP);
            int x = CalculaterMod(lambda * lambda - Point[0] - SecondPoint[0], PP);
            int y = CalculaterMod(lambda * (Point[0] - x) - Point[1], PP);
            return new int[] { x, y };
        }

        private  int CalculateLambda(int[] Point, int a, int p)
        {
            return CalculaterMod(CalculaterMod(3 * (Point[0] * Point[0]) + a, p) * CalculateInvertMode(2 * Point[1], p), p);
        }

        private  int CalculateLambda(int[] Point, int[] Q, int p)
        {
            return CalculaterMod(CalculaterMod(Q[1] - Point[1], p) * CalculaterMod(CalculateInvertMode(Q[0] + CalculaterMod(-Point[0], p), p), p), p);
        }

        public  int[] CalculateSum(int[] Point)
        {
            int lambda = CalculateLambda(Point, A, PP);
            int x = CalculaterMod(lambda * lambda - Point[0] - Point[0], PP);
            int y = CalculaterMod(lambda * (Point[0] - x) - Point[1], PP);
            return new int[] { x, y };
        }
        public int[] GetInversePoint(int[] Point)
        {
            return new int[2] { Point[0], (-1) * Point[1] };
        }

        public  int[,] GetEncryptTextByPointAndGenerator(string inputText, int[] Point,int generator)
        {
            int[] Q = CalculateKP(generator, Point), P;
            int[,] encryptedTextArray = new int[inputText.Length, 4];
            int j;
            Console.WriteLine($"Point = ({Point[0]}, {Point[1]}), generator = {generator}, Q = ({Q[0]}, {Q[1]})");
            for (int i = 0; i < inputText.Length; i++)
            {
                j = random.Next(2, generator);
                P = Enumerable.Range(0, inputPoints.GetLength(1)).Select(x => inputPoints[defaultAlphabeth.IndexOf(inputText[i]), x]).ToArray();
                int[] C1 = CalculateKP(j, Point), kQ = CalculateKP(j, Q), C2;
                C2 = CalculateSum(P, kQ);
                encryptedTextArray[i, 0] = C1[0]; encryptedTextArray[i, 1] = C1[1];
                encryptedTextArray[i, 2] = C2[0]; encryptedTextArray[i, 3] = C2[1];
            }
            return encryptedTextArray;
        }

        public string GetEncryptTextByGenerator(int[,] encryptedText,int d)
        {
            string decryptedText = "";
            for (int i = 0; i < encryptedText.GetUpperBound(0) + 1; i++)
            {
                int[] C1 = CalculateKP(d, new int[] { encryptedText[i, 0], encryptedText[i, 1] }),
                    C2 = { encryptedText[i, 2], encryptedText[i, 3] };
                int[] P = CalculateSum(C2, GetInversePoint(C1));
                for (int k = 0; k < inputPoints.GetUpperBound(0) + 1; k++)
                {
                    if (inputPoints[k, 0] == P[0] && inputPoints[k, 1] == P[1])
                    {
                        decryptedText += defaultAlphabeth[k];
                        break;
                    }
                }
            }
            return decryptedText;
        }

        public  int[] GenerateDigitalSign(int[] Point, int q, int d)
        {
            Console.WriteLine($"G = ({Point[0]}, {Point[1]}), d = {d}, q = {q}");
            int[] digitalSign = new int[2];
            int[] kG;
            int k, t;
            do
            {
                do
                {
                    k = random.Next(2, q);
                    kG = CalculateKP(k, Point);
                    digitalSign[0] = kG[0] % q;
                } while (digitalSign[0] <= 1);
                t = CalculateInvertMode(k, q);
                int H = inputPoints[defaultAlphabeth.IndexOf("м"), 0] % 13;
                digitalSign[1] = (t * (H + d * digitalSign[0])) % q;
            } while (digitalSign[1] <= 0);
            return digitalSign;
        }

        public  bool CheckDigitalSign(int[] digitalSign, int[] Point, int q, int d)
        {
            if (digitalSign[0] <= 1 || digitalSign[1] >= q)
                return false;
            int H = inputPoints[defaultAlphabeth.IndexOf("м"), 0] % 13;
            int w = CalculateInvertMode(digitalSign[1], q);
            int u1 = (w * H) % q;
            int u2 = (w * digitalSign[0]) % q;
            int[] Q = CalculateKP(d, Point), u1G = CalculateKP(u1, Point), u2Q = CalculateKP(u2, Q);
            int v = CalculateSum(u1G, u2Q)[0] % q;
            return digitalSign[0] == v;
        }
    }
}
