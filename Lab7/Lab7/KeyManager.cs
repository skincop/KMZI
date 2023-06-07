using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace CriptaLab7
{
    class KeyManager
    {
        private static readonly int[] arrayForPermut56 = new int[56]
       {
            57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4
       };


        private static readonly int[] arrayForPermut48 = new int[48]
        {
            14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47,
            55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32
        };

        private BitArray firstSide;
        private BitArray lastSide;


        private BitArray ShiftBitArrayToLeft(BitArray keysBitArray, int shiftCount)
        {
            var result = new BitArray(28);
            var k = 0;
            for (var i = shiftCount; k < keysBitArray.Length; i++)
                result[k++] = keysBitArray[i % keysBitArray.Length];
            return result;
        }

        private BitArray GetKeysByRoundNumber(BitArray firstSide, BitArray lastSide, int roudNumber)
        {
            BitArray key;
            int shift;
            if (roudNumber == 0 || roudNumber == 1 || roudNumber == 8 || roudNumber == 15) shift = 1;
            else shift = 2;
            this.firstSide = ShiftBitArrayToLeft(firstSide, shift);
            this.lastSide = ShiftBitArrayToLeft(lastSide, shift);
            key = this.firstSide.AppendElementAfter(this.lastSide);
            key = PermutBitArray48(key);
            return key;
        }

        private BitArray PermutBitArray56(BitArray inputBitArray)
        {
            var result = new BitArray(56);
            for (var i = 0; i < arrayForPermut56.Length; i++)
                result[i] = inputBitArray[arrayForPermut56[i] - 1];
            return result;
        }

        private BitArray PermutBitArray48(BitArray inputBitArray)
        {
            var result = new BitArray(48);
            for (var i = 0; i < arrayForPermut48.Length; i++)
                result[i] = inputBitArray[arrayForPermut48[i] - 1];
            return result;
        }


        public BitArray[] GenerateBitArrayKeys(BitArray keysBitArray)
        {
            var output = new BitArray[16];
            var noParityBits = PermutBitArray56(keysBitArray);
            var parts = noParityBits.SplitToBitArrays();
            firstSide = parts[0];
            lastSide = parts[1];

            for (var i = 0; i < 16; i++)
            {
                output[i] = GetKeysByRoundNumber(firstSide, lastSide, i);
                Debug.WriteLine(i);
                output[i].GetAvalancheEffect();
            }
            return output;
        }
    }
}
