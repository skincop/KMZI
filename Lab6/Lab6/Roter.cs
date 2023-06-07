using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Roter
    {
        private List<char> inputSymbols;
        private List<char> symbolsChain;
        private int step;
        private int shiftPos = 0;

        public Roter(char[] array, int step)
        {
            inputSymbols = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
                'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            symbolsChain = new List<char>(array);
            this.step = step;

        }

        public void resetShiftPos()
        {
            shiftPos = 0;
        }

        public char GetRouteSymbol(char symbol)
        {
            var inputIndex = inputSymbols.IndexOf(symbol);
            var outputIndex = (inputIndex + shiftPos) % symbolsChain.Count;
            return symbolsChain[outputIndex];

        }
        public char GetReturnRouteSymbol(char symbol)
        {
            var inputIndex = symbolsChain.IndexOf(symbol);
            var outputIndex = (inputIndex+shiftPos) % symbolsChain.Count;
            return inputSymbols[outputIndex];
        }

        public void SetRoterShift(int shift)
        {
            shiftPos = shift;
        }

        public bool shiftRoute()
        {
            shiftPos += step;
            if (shiftPos % 26 == 0) return true; 
            else return false;
        } 
    }
}
