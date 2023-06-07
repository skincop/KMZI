using System.Text;

namespace CriptaLab8.Service
{
    public class Rc4Service
    {
        private int[] key = new int[5] { 12, 13, 90, 91, 240 };
        private int[] sTable = new int[256];
        private int[] kTable = new int[256];
        public List<int> Generate(string text)
        {
            prepareStage();
            return generateBytes(text);
        }
        public Rc4Service()
        {
            for (int i=0;i<sTable.Length;i++)
            {
                sTable[i] = i;
                kTable[i] = key[i % key.Length];
            }
        }

        private void prepareStage()
        {
            int j = 0,temp=0;
            for (int i=0;i< sTable.Length;i++)
            {
                j = (j + sTable[i] + kTable[i]) % 256;
                temp = sTable[i];
                sTable[i] = sTable[j];
                sTable[j]=temp;
            }
        }

        private List<int> generateBytes(string text)
        {
            string result = "";
            var list = new List<int>();
            int a = 0, j = 0, temp = 0, i = 0;
            for (int k = 0; k < text.Length; k++)
            {
                i = (i + 1) % 256;
                j = (j + sTable[i]) % 256;
                temp = sTable[i];
                sTable[i] = sTable[j];
                sTable[j] = temp;
                a = (sTable[i] + sTable[j]) % 256;
                list.Add(sTable[a]);
                result += text[k] ^ sTable[(sTable[i]+sTable[i]) % 256]; 
            }
            return list;
        }

    }
}
