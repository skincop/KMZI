using System.Runtime.CompilerServices;

namespace CriptaLab8.Service
{
    public class BbsService
    {
        private const int X = 3;
        private int P = 0;
        private int Q = 0;
        public int[] GenerateNumbers(int paramP,int paramQ,int count)
        {
            P=paramP;
            Q=paramQ;
            var result = new int[count];
            int startNumber = generateStartNumber();
            Console.WriteLine(startNumber);
            for (int i = 0; i < count; i++) { 
                result[i] = (int)Math.Pow(startNumber, 2) % (P * Q);
                startNumber = result[i];
            }
            return result;

        }

        private int generateStartNumber()
        {
            return (int)Math.Pow(X, 2) % (P * Q);
        }
    }
}
