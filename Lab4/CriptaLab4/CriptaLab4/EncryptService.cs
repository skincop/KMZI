using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CriptaLab4
{
    public class EncryptService
    {
        private readonly List<char> alphabetList;
        private readonly int k = 7;
        private readonly int n;
        public EncryptService()
        {
            alphabetList = new List<char> { 'A', 'Ä', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
                'I', 'J', 'K', 'L', 'M', 'N', 'O', 'Ö', 'P', 'Q', 'R', 'S', 'ẞ', 'T', 'U', 'Ü', 'V', 'W', 'X', 'Y', 'Z',' ','!','?',',','.','\n'
            };
            n = alphabetList.Count;
           
        }

        

        public async Task EncrypteText(string from, string to)
        {
            string text = await readTextFromFileAsync(from);
            string encrText = encrypteText(text);
            await writeTextToFileAsync(encrText,to);
        }

        public async Task EncrypteByTrisemus(string word,string from,string to)
        {
            string text = await readTextFromFileAsync(from);
            string encrText = encrypteByTrisemus(text,word);
            await writeTextToFileAsync(encrText, to);
        }

        public async Task<int[]> CountSymbolFromFile(string name)
        {
            int[] array = new int[alphabetList.Count];
            string text = await readTextFromFileAsync(name);

            for (int i=0;i<alphabetList.Count; i++)
            {
                array[i] = text.Count(ch => ch == alphabetList[i]);
            }
            return array;

        }

        public async Task DecrypteText(string from,string to)
        {
            string text = await readTextFromFileAsync(from);
            string decrText = decrypteText(text);
            await writeTextToFileAsync(decrText, to);
        }

        public async Task DecrypteByTrisemus(string word,string from, string to)
        {
            string text = await readTextFromFileAsync(from);
            string decrText = decrypteByTrisemus(text,word);
            await writeTextToFileAsync(decrText, to);
        }

        private async Task<string> readTextFromFileAsync(string fileName)
        {
            string text;
            using (StreamReader sr = new StreamReader(fileName))
            {
                text = await sr.ReadToEndAsync();
            }
            return text.ToUpper();
        }

        private async Task writeTextToFileAsync(string text, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                await writer.WriteLineAsync(text);
            }
        }

        private string encrypteText(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                int pos = alphabetList.FindIndex(x => x == c);
                sb.Append(getEncrtyptChar(pos));
            }
            return sb.ToString();
        }
        private string encrypteByTrisemus(string text,string word)
        {
            int step = 0;
            var table = getTrisemusTable(word);
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                int pos = (table.FindIndex(x => x == c) + step++) % n;
                sb.Append(table[pos]);
            }
            Console.WriteLine("last Step" +step);
            return sb.ToString();
        }


        private string decrypteText(string text)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length - 2; i++)
            {
                int pos = alphabetList.FindIndex(x => x == text[i]);
                sb.Append(getDecrypteChar(pos));
            }
            return sb.ToString();
        }

        private string decrypteByTrisemus(string text, string word)
        {
            int step = 0;
            var table = getTrisemusTable(word);
            StringBuilder sb = new StringBuilder();
            for (int i=0;i<text.Length-2;i++)
            {

                int pos = table.FindIndex(x => x == text[i]);
                int index = (pos - (step % n)) <0 ? n + (pos - (step % n)) : (pos - (step % n));
                step++;
                sb.Append(table[index]);
            }
            return sb.ToString();
        }

        private char getEncrtyptChar(int pos)
        {
            return alphabetList[(pos + k) % n];
        }

        private char getDecrypteChar(int pos)
        {
            int index = pos >= k ? (pos - k) % n : n + ((pos - k) % n);
            return alphabetList[index];
        }

        private List<char> getTrisemusTable(string word)
        {
            var list = new List<char>();
            foreach (char c in word.ToUpper())
            {
                list.Add(c);
            }
            foreach (char c in alphabetList)
            {
                if (!list.Exists(x => x == c))
                {
                    list.Add(c);
                }
            }
            return list;
        }


    }
}
