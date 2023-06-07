using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab5
{
    public class SemiTranspositionCipher
    {
        private string columnKeyWord;
        private string rowKeyWord;
        private List<int> columnNumberKey;
        private List<int> rowNumberKey;
        private int ROWS;
        private int COLUMNS;

        private List<char> germanAlphabet;

        public SemiTranspositionCipher(string columnKeyWord, string rowKeyWord)
        {
            germanAlphabet = new List<char>
            {
                'A','B','C','D','E','F','G','H','I','J',
                'K','L','M','N','O','P','Q','R','S','T',
                'U','V','W','X','Y','Z','Ä','Ö','Ü','ß',
                ' ',',','!','.','?'
            };

            this.columnKeyWord = columnKeyWord;
            this.rowKeyWord = rowKeyWord;

            COLUMNS = columnKeyWord.Length;
            ROWS = rowKeyWord.Length;

            columnNumberKey = getNumberKey(columnKeyWord);
            rowNumberKey = getNumberKey(rowKeyWord);

        }

        public string EncrypteText(string text)
        {
            var resultArray = new char[COLUMNS*ROWS];
            int koefColumn = 0;
            int koefRow = 0;
            if (text.Length > COLUMNS * ROWS)
            {
                return $"Text is too big for this keys\nMax string lehgth is {resultArray.Length}";
            }
            var charArray = fillMatrix(text);

            for (int i = 0;i< COLUMNS; i++)
            {
                koefColumn = columnNumberKey[i];
                for (int j = 0;j< ROWS; j++)
                {
                    koefRow = rowNumberKey[j];
                    resultArray[(koefColumn * ROWS) + koefRow] = charArray[j,i];
                }
            }

            return new string(resultArray);
        }

        public string DecrypteText(string text)
        {
            var resultArray = new char[ROWS,COLUMNS];
            string result = "";
            int koefColumn = 0;
            int koefRow = 0;

            for (int i = 0; i < COLUMNS; i++)
            {
                koefColumn = columnNumberKey[i];
                for (int j = 0; j < ROWS; j++)
                {
                    koefRow = rowNumberKey[j];
                    resultArray[j, i] = text[(koefColumn * ROWS) + koefRow];
                }
            }
            foreach (char c in resultArray)
            {
                result += c;
            }
            return result;
        }

        private char[,] fillMatrix(string text)
        {
            int index = 0;
            char[,] inputMatrix = new char[ROWS, COLUMNS];
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    index = j + i * COLUMNS;
                    index = index <= text.Length - 1 ? index : 0;
                    inputMatrix[i, j] = text[index];
                }
            }
            return inputMatrix;
        }


        private List<int> getNumberKey(string keyWord)
        {
            int[] symbolAlphabetNumbers = new int[keyWord.Length];
            int[] indexArray = new int[keyWord.Length];

            for (int i=0;i<keyWord.Length; i++)
            {
                symbolAlphabetNumbers[i] = germanAlphabet.IndexOf(keyWord[i]);
            }
            var res = sortMassive(symbolAlphabetNumbers.ToList());
            return res.ToList();
        }


        private int[] sortMassive(List<int> array)
        {

            int min,minIndex;
            var result = new int[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                min = array.Min();
                minIndex = array.IndexOf(min);
                array[minIndex] = int.MaxValue;
                result[minIndex] = i;
            }
            return result;

        }


    }
}
