using System.Diagnostics;

namespace Lab5
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            FileWorker fileWorker = new FileWorker();
            TranspositionCipher cipher = new TranspositionCipher();
            SymbolCounter counter = new SymbolCounter();
            var sw = new Stopwatch();


            Console.WriteLine("Input word");
            string consoleText = Console.ReadLine();

            string consoleTextEncr = cipher.EncrypteText(consoleText);
            string consoleTextDecr = cipher.DecrypteText(consoleTextEncr);

            Console.WriteLine($"Input word: {consoleText}\n Encrypted word: {consoleTextEncr}\n Decrypted word: {consoleTextDecr}");

            string inputText = await fileWorker.readTextFromFileAsync("../../../Text/inputText.txt");
            sw.Start();
            string encrText = cipher.EncrypteText(inputText);
            sw.Stop();
            var encrTime = sw.Elapsed;
            sw.Start();
            string decrText = cipher.DecrypteText(encrText);
            sw.Stop();
            var decrTime = sw.Elapsed;
            await fileWorker.writeTextToFileAsync(encrText, "../../../Text/encryptText.txt");
            await fileWorker.writeTextToFileAsync(decrText, "../../../Text/decryptText.txt");

            Console.WriteLine("Check file");

            Console.WriteLine($"Time for encr {encrTime.Milliseconds} time for dect {decrTime.Milliseconds}");

            var encrCount = counter.GetSymbolsOccurrences(encrText);
            var decrCount = counter.GetSymbolsOccurrences(decrText);

            Console.WriteLine(new string('-',100));

            var serv = new SemiTranspositionCipher("Jan", "VYSOTSKIY");
            string inputValue = "SOME TEXT MAX LENGTH IS 27";
            sw.Start();
            var encText = serv.EncrypteText(inputValue);
            sw.Stop();
            encrTime = sw.Elapsed;
            sw.Start();
            var dectText = serv.DecrypteText(encText);
            sw.Stop();
            decrTime = sw.Elapsed;
            Console.WriteLine($"Input value: {inputValue}");
            Console.WriteLine($"Encrtypted value: {encText}");
            Console.WriteLine($"Decrypted value: {dectText}");
            Console.WriteLine($"Time for encr {encrTime} time for dect {decrTime}");

        }
    }
}