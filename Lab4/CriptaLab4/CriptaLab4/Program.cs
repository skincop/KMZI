using System.Diagnostics;

namespace CriptaLab4
{
    internal class Program
    {
        static async Task Main()
        {
            EncryptService encrypt = new EncryptService();
            var sw = new Stopwatch();

            sw.Start();
            await encrypt.EncrypteText("../../../from.txt", "../../../to.txt");
            sw.Stop();

            await Console.Out.WriteLineAsync("EncrypteTimeBySdvig"+sw.Elapsed.TotalMilliseconds);

            sw.Start();
            await encrypt.DecrypteText("../../../to.txt", "../../../decrEncr.txt");
            sw.Stop();

            await Console.Out.WriteLineAsync("DecrypteTimeBySdvig" + sw.Elapsed.TotalMilliseconds);


            sw.Start();
            await encrypt.EncrypteByTrisemus("enigma","../../../fromTrisemus.txt", "../../../toTrisemus.txt");
            sw.Stop();
            await Console.Out.WriteLineAsync("EncrypteTimeByTableTrisem" + sw.Elapsed.TotalMilliseconds);

            sw.Start();
            await encrypt.DecrypteByTrisemus("enigma","../../../toTrisemus.txt", "../../../decrTrisemus.txt");
            sw.Stop();
            await Console.Out.WriteLineAsync("DecrypteTimeByTableTrisem" + sw.Elapsed.TotalMilliseconds);

            var arrayFrom = await encrypt.CountSymbolFromFile("../../../fromTrisemus.txt");
            var arrayTo = await encrypt.CountSymbolFromFile("../../../to.txt");

            //foreach (var symbol in arrayFrom)
            //{
            //    Console.WriteLine(symbol);
            //}
            //await Console.Out.WriteLineAsync(new string('-',50));
            
            //foreach (var symbol in arrayTo)
            //{
            //    Console.WriteLine(symbol);
            //}
        }
    }
}