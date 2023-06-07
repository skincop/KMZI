using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class TranspositionCipher
    {
        private int ROWS;
        private int COLUMNS;
        private string Text;
        private char[,] resultMatrix;

        public string EncrypteText(string text)
        {
            Text=text;
            COLUMNS = calculateColumnsCount();
            ROWS = calculateRowsCount();
            var matrix = fillMatrix();
            string encText = Encrypte(matrix);
            return encText;
        }

        public string DecrypteText(string text)
        {
            string decrypteText = Decrypte(text);
            return decrypteText;
        }

        private int calculateColumnsCount()
        {
            return Convert.ToInt32(Math.Ceiling(Math.Sqrt(Text.Length)));
        }

        private int calculateRowsCount()
        {
            return Convert.ToInt32(Math.Ceiling((double)Text.Length / COLUMNS));
        }

        private char[,] fillMatrix()
        {
            int index = 0;
            char[,] inputMatrix = new char[ROWS, COLUMNS];
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    index = j + i * COLUMNS;
                    index = index <= Text.Length - 1 ? index : 0;
                    inputMatrix[i, j] = Text[index];
                }
            }
            return inputMatrix;
        }

        private string Encrypte(char[,] inputMatrix)
        {
            int row = 0;
            int col = 0;
            string result = "";

            result += inputMatrix[row, col];
            while (result.Length < ROWS * COLUMNS)
            {
                //Turn right if u can else turn down
                if (col < COLUMNS - 1)
                    result += inputMatrix[row, ++col];
                else if (row < ROWS - 1)
                    result += inputMatrix[++row, col];
                //diagonally to left bottom
                while (true)
                {
                    if (row < ROWS - 1 && col > 0)
                        result += inputMatrix[++row, --col];
                    else break;
                }
                //Turn down if u can else turn right
                if (row < ROWS - 1)
                    result += inputMatrix[++row, col];
                else if (col < COLUMNS - 1)
                    result += inputMatrix[row, ++col];

                //diagonally to right top
                while (true)
                {
                    if (row > 0 && col < COLUMNS - 1)
                        result += inputMatrix[--row, ++col];
                    else break;
                }

            }
            return result;
        }

        private string Decrypte(string word)
        {
            int row = 0;
            int col = 0;
            string result = "";
            char[,] matrix = new char[ROWS, COLUMNS];
            matrix[0,0] = word[0];
            for (int i = 0; i < word.Length-1;)
            {
                //Turn right if u can else turn down
                if (col < COLUMNS - 1)
                    matrix[row, ++col] = word[++i];
                else if (row < ROWS - 1)
                    matrix[++row, col] = word[++i];
                //diagonally to left bottom
                while (true)
                {
                    if (row < ROWS - 1 && col > 0)
                        matrix[++row, --col] = word[++i];
                    else break;
                }
                //Turn down if u can else turn right
                if (row < ROWS - 1)
                    matrix[++row, col] = word[++i];
                else if (col < COLUMNS - 1)
                    matrix[row, ++col] = word[++i];

                //diagonally to right top
                while (true)
                {
                    if (row > 0 && col < COLUMNS - 1)
                        matrix[--row, ++col] = word[++i];
                    else break;
                }

            }
            foreach (char c in matrix)
            {
                result += c;
            }
            return result;
        }

    }
}
