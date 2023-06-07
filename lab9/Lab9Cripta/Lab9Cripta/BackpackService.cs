using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9Cripta
{
    class BackpackService
    {
        Random rnd = new Random();
        public int[] GenerateCascadingSequence(int count)
        {
            int[] cascadingSequence = new int[count];
            int cascadingSequenceSum = 0;
            for (int i = 0; i < count; i++)
            {
                cascadingSequence[i] = rnd.Next(cascadingSequenceSum, cascadingSequenceSum + 5);
                cascadingSequenceSum += cascadingSequence[i];
            }
            return cascadingSequence;
        }
        public (int,int) getSecrtetKey(int sum)
        {
            int a;
            while (true)
            {
                a = rnd.Next(1, 1000000);
                if (Evklid(a, sum+1) == 1)
                {
                    break;
                }
            }
            return (sum + 1, a);
        }

        public int[] getPublicKey(int[] cascadSeq, int a, int n)
        {
            int[] publicKey = new int[cascadSeq.Length];

            for (int i = 0; i < cascadSeq.Length; i++)
            {
                publicKey[i] = (cascadSeq[i] * a) % n;
            }
            return publicKey;
        }

        public int[] EncodeInputText(int[] publicKey, string inputText)
        {
            int j = 0;
            int[] result = new int[inputText.Length];
            int total = 0;


            foreach (char Mi in inputText)
            {
                total = 0;
                string expandInput = '0' + ConvertToBinary(Mi.ToString());


                for (int i = 0; i < expandInput.Length; i++)
                {
                    if (expandInput[i] == '1') total += publicKey[i];
                }
                result[j] = total;
                j++;
            }
            return result;
        }

        public string DecodeText(int[] cascadingSeq, int currentNum)
        {
            string res = "";
            string res2 = "";

            for (int i = cascadingSeq.Length; i > 0; i--)
            {
                if (currentNum >= cascadingSeq[i - 1])
                {
                    res += '1';
                    currentNum = currentNum - cascadingSeq[i - 1];
                }
                else
                {
                    res += '0';
                }
            }
            for (int i = res.Length - 1; i > -1; i--)
            {
                res2 += res[i];
            }
            return res2;
        }

        public int GetReverse(int a, int n)
        {
            int res = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((a * i) % n) == 1) return (i);
            }
            return (res);
        }

        private string ConvertToBinary(string input)
        {
            String result = "";
            for (int i = 0; i < input.Length; i++)
            {
                result += Convert.ToString((int)input[i], 2);
            }
            return result;
        }


        public static int Evklid(int a, int b)
        {
            while (a != b)
            {
                if (a > b) a -= b;
                else b -= a;
            }
            return a;
        }
    }
}
