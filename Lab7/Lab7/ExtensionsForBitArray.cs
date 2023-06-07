using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptaLab7
{
    public static class ExtensionsForBitArray
    {

        public static string GetAvalancheEffect(this BitArray bitsArray)
        {
            var output = String.Empty;
            for (var i = 0; i < bitsArray.Length; i++)
                output += bitsArray[i].ConvertBoolToIntegerString();
            System.Diagnostics.Debug.WriteLine(output);
            return output;
        }

        public static BitArray AppendElementAfter(this BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }

        public static BitArray[] SplitToBitArrays(this BitArray current)
        {
            var half1 = new bool[current.Length / 2];
            var half2 = new bool[current.Length / 2];

            for (var i = 0; i < current.Length / 2; i++)
            {
                half1[i] = current[i];
                half2[i] = current[i + current.Length / 2];
            }

            return new[] { new BitArray(half1), new BitArray(half2) };
        }

        public static string ConvertBoolToIntegerString(this bool b)
        {
            return b ? "1" : "0";
        }
    }
}
