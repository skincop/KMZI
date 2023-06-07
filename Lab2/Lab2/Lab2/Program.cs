using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region bg
            Console.WriteLine("Task A");

            Alphabets alphabets = new Alphabets();
            var sb = new StringBuilder();

            string textbg = "";
            Regex patternbg = new Regex(@"[а-ъьюяА-ЪЬЮЯ]");

            using (StreamReader sr = new StreamReader("text.txt"))
            {
                textbg = sr.ReadToEnd();
            }

            MatchCollection matches = patternbg.Matches(textbg);

            foreach (Match match in matches)
            {
                sb.Append(match);
            }
            textbg = sb.ToString();

            var bgEntropy = alphabets.getEntropy(textbg, Alphabets.Curillic);

            Console.WriteLine("bg entropy: " + bgEntropy);

            #endregion

            #region nor
            string textnor = "";
            Regex patternnor = new Regex(@"[a-zA-ZæøåÆØÅ]");

            using (StreamReader sr=new StreamReader("engText.txt"))
            {
                textnor = sr.ReadToEnd();
            }

            sb.Clear();
            MatchCollection norMatches=patternnor.Matches(textnor);

            foreach (Match match in norMatches)
            {
                sb.Append(match);
            }
            textnor = sb.ToString();
            var norEntropy = alphabets.getEntropy(textnor, Alphabets.nor);

            Console.WriteLine("nor entropy: " + norEntropy);

            #endregion

            #region Bin
            Console.WriteLine("\nTask B");

            Regex patternBin = new Regex(@"[0-1]");
            sb.Clear();

            foreach(char c in textbg)
            {
                sb.Append(Convert.ToString(c, 2));
            }
            var binTextbg = sb.ToString();

            var binbgEntropy = alphabets.getEntropy(binTextbg, Alphabets.Binary);

            Console.WriteLine("Binbg entropy: " + binbgEntropy);

            sb.Clear();

            foreach (char c in textnor)
            {
                sb.Append(Convert.ToString(c, 2));
            }
            var binTextnor = sb.ToString();

            var binnorEntropy = alphabets.getEntropy(binTextnor, Alphabets.Binary);

            Console.WriteLine("Binnor entropy: " + binnorEntropy);

            #endregion

            #region FioTask
            Console.WriteLine("\nTusk V");


            string bgFio = "ВисоцкиЯнАлександрович";
            string norFio = "VysotskyYanAlexandrovich";


            ////////////////
            string binText;
            using (StreamReader sr = new StreamReader("bin.txt"))
            {
                binText = sr.ReadToEnd();
            }
            var binEntropy= alphabets.getEntropy(binText, Alphabets.Binary);
            Console.WriteLine("BINNNN"+binEntropy*13*8);
            //////////////////


            byte[] bgFioAsciiArr = Encoding.ASCII.GetBytes(bgFio);
            byte[] norFioAsciiArr = Encoding.ASCII.GetBytes(norFio);

            string bgFioAscii = "";
            string norFioAscii = "";

            foreach (var b in bgFioAsciiArr)
                bgFioAscii += b;

            foreach (var b in norFioAsciiArr)
                norFioAscii += b;

            Console.WriteLine(new string('-', 100));
            Console.WriteLine($"BG Length {bgFio.Length} Nor Length {norFio.Length}");

            String bgFioBin="";
            String norFioBin="";


            for (int i = 0; i < bgFio.Length; i++)
            {
                bgFioBin += Convert.ToString((int)bgFio[i], 2) + " ";
            }

            for (int i = 0; i < norFio.Length; i++)
            {
                norFioBin += Convert.ToString((int)norFio[i], 2) + " ";
            }






            Console.WriteLine($"BgBin {bgFioBin.Length} NorBin {norFioBin.Length}");
            Console.WriteLine($"BGASCII Length {bgFioAscii.Length} NorASCII Length {norFioAscii.Length}");
            Console.WriteLine(new string('-', 100));


            Console.WriteLine("Information count in bg message: " + alphabets.getInfoCount(bgEntropy,bgFio.Length));
            Console.WriteLine("Information count in nor message: " + alphabets.getInfoCount(norEntropy, norFio.Length));

            Console.WriteLine("Information count in bg Bin message: " + alphabets.getInfoCount(binbgEntropy, bgFioBin.Length));
            Console.WriteLine("Information count in nor Bin message: " + alphabets.getInfoCount(binnorEntropy, norFioBin.Length));

            Console.WriteLine("Information count in bg ASCII message: " + alphabets.getInfoCount(binbgEntropy, bgFioAscii.Length));
            Console.WriteLine("Information count in nor ASCII message: " + alphabets.getInfoCount(binnorEntropy, norFioAscii.Length));

            #endregion

            #region InformationValueWithErrorProbability
            Console.WriteLine("Task G");

            Console.WriteLine("bg with error probability 0,1: " + alphabets.getInfoCountWithErrorProbability(bgFio.Length, 0.1));
            Console.WriteLine("bg with error probability 0,5: " + alphabets.getInfoCountWithErrorProbability(bgFio.Length, 0.5));
            Console.WriteLine("bg with error probability 1: " + alphabets.getInfoCountWithErrorProbability(bgFio.Length, 0.9999999999));
            Console.WriteLine("Nor with error probability 0,1: " + alphabets.getInfoCountWithErrorProbability(norFio.Length, 0.1));
            Console.WriteLine("Nor with error probability 0,5: " + alphabets.getInfoCountWithErrorProbability(norFio.Length, 0.5));
            Console.WriteLine("Nor with error probability 1: " + alphabets.getInfoCountWithErrorProbability(norFio.Length, 0.9999999999));

            Console.WriteLine(new string('-',100));

            Console.WriteLine("BgASCII with error probability 0,1: " + alphabets.getInfoCountWithErrorProbability(bgFioAscii.Length, 0.1));
            Console.WriteLine("BgASCII with error probability 0,5: " + alphabets.getInfoCountWithErrorProbability(bgFioAscii.Length, 0.5));
            Console.WriteLine("BgASCII with error probability 1: " + alphabets.getInfoCountWithErrorProbability(bgFioAscii.Length, 0.9999999999999));
            Console.WriteLine("NorASCII with error probability 0,1: " + alphabets.getInfoCountWithErrorProbability(norFioAscii.Length, 0.1));
            Console.WriteLine("NorASCII with error probability 0,5: " + alphabets.getInfoCountWithErrorProbability(norFioAscii.Length, 0.5));
            Console.WriteLine("NorASCII with error probability 1: " + alphabets.getInfoCountWithErrorProbability(norFioAscii.Length, 0.9999999999999));

            Console.WriteLine("NorASCII with error probability 1: " + alphabets.getInfoCountWithErrorProbability(104, 0.26));

            #endregion
        }
    }
}