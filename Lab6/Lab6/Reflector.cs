using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Reflector
    {
        private List<char> symbolPairs;

        public Reflector(char[] arary)
        {
            symbolPairs = new List<char>(arary);
        }

        public char GetReflectSymbol(char symbol) {
            int index = symbolPairs.IndexOf(symbol);
            return index % 2 != 0 ? symbolPairs[index-1] : symbolPairs[index+1];
        }
    }
}
