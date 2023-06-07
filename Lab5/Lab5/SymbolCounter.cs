using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class SymbolCounter
    {
        private char[] germanAlphabet;
        public int[] GetSymbolsOccurrences(string text)
        {
            int[] symbolsCount = new int[germanAlphabet.Length];
            for (int i = 0; i < germanAlphabet.Length;i++)
            {
                symbolsCount[i] = text.Count(c => c == germanAlphabet[i]);
            }
            return symbolsCount;
        }
        public SymbolCounter()
        {
            germanAlphabet = new char[]
            {
                'A','B','C','D','E','F','G','H','I','J',
                'K','L','M','N','O','P','Q','R','S','T',
                'U','V','W','X','Y','Z','Ä','Ö','Ü','ß',
                ' ',',','!','.','?'
            };

        }
    }
}
