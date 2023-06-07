using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace CriptaLab7
{
    class DesEnctyptManager
    {
        private readonly RoundManager _des = new RoundManager();
        private readonly KeyManager _key = new KeyManager();
        private BitArray lSide;
        private BitArray rSide;
        private BitArray tempBuffer;
        const int S = 64;
        const int R = 16;

        public string GetDecryptTextWithKey(string cipher, string key)
        {
            cipher = string.Join(string.Empty,
                cipher.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), R), 2).PadLeft(4, '0')
                )
            );

            key = string.Join(string.Empty,
                key.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), R), 2).PadLeft(4, '0')
                )
            );

            var keyBits = new BitArray(key.Select(c => c == '1').ToArray());

            var result = "";
            var output = Enumerable.Range(0, cipher.Length / S)
                .Select(x => cipher.Substring(x * S, S)).ToList();


            var newInput = output.ToArray();

            var subKeys = _key.GenerateBitArrayKeys(keyBits).Reverse().ToArray();
            for (var p = 0; p < newInput.Length; p++)
            {
                var bits = new BitArray(newInput[p].Select(c => c == '1').ToArray());
                var chunk = _des.DoInitialPermut(bits);

                var sides = chunk.SplitToBitArrays();
                lSide = sides[0];
                rSide = sides[1];
                lSide = sides[1];
                tempBuffer = sides[0];

                for (var k = 0; k < R; k++)
                {
                    rSide = _des.DoRound(rSide, subKeys[k]);
                    rSide = tempBuffer.Xor(rSide);

                    tempBuffer = lSide;
                    lSide = rSide;
                }

                var lastPermutation = rSide.AppendElementAfter(tempBuffer);
                lastPermutation = _des.DoFinalPermut(lastPermutation);
                result += GetHexStringFromBitArray(lastPermutation);
            }
            return result;
        }

        private string GetHexStringFromBitArray(BitArray bitsArray)
        {
            var builder = new StringBuilder(bitsArray.Length / 4);

            for (var i = 0; i < bitsArray.Length; i += 4)
            {
                var result = (bitsArray[i] ? 8 : 0) | (bitsArray[i + 1] ? 4 : 0) | (bitsArray[i + 2] ? 2 : 0) | (bitsArray[i + 3] ? 1 : 0);
                builder.Append(result.ToString("X1"));
            }

            return builder.ToString();
        }

        public string GetEncryptTextWithKey(string text, string key)
        {
            text = string.Join(string.Empty,
                text.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), R), 2).PadLeft(4, '0')));
            key = string.Join(string.Empty,
                key.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), R), 2).PadLeft(4, '0'))
            );
            var keysBitArray = new BitArray(key.Select(c => c == '1').ToArray());
            var outputResult = "";
            var output = Enumerable.Range(0, text.Length / S).Select(x => text.Substring(x * S, S)).ToList();
            if (text.Length % S != 0)
            {
                var missing = text.Skip(output.Count * S).Take(S).ToList();
                while (missing.Count() != S)
                {
                    missing.Add('0');
                }
                output.Add(string.Join("", missing));
            }
            var newInput = output.ToArray();
            var subKeys = _key.GenerateBitArrayKeys(keysBitArray);
            for (var k = 0; k < newInput.Length; k++)
            {
                var bits = new BitArray(newInput[k].Select(c => c == '1').ToArray());
                var chunk = _des.DoInitialPermut(bits);
                var parts = chunk.SplitToBitArrays();
                lSide = parts[0];
                rSide = parts[1];
                lSide = parts[1];
                tempBuffer = parts[0];
                for (var l = 0; l < R; l++)
                {
                    rSide = _des.DoRound(rSide, subKeys[l]);
                    rSide = tempBuffer.Xor(rSide);

                    tempBuffer = lSide;
                    lSide = rSide;
                }
                var finalPerm = rSide.AppendElementAfter(tempBuffer);
                finalPerm = _des.DoFinalPermut(finalPerm);
                outputResult += GetHexStringFromBitArray(finalPerm);
            }
            return outputResult;
        }

    }
}
