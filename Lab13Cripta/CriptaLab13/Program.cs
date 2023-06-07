using System.Diagnostics;

namespace CriptaLab13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var curveManager = new CurvesManager();

            Console.WriteLine("Вариант 4");
            Console.WriteLine("Задание 1");
            int xmax = 140, xmin = 106;
            curveManager.CalculatePoint(xmin, xmax);
            Thread.Sleep(100);
            Console.WriteLine(new string('-',100));


            Console.WriteLine("Задание 1.2");
            int[] P = { 56, 332 }, Q = { 69, 241 }, R = { 83, 373 };
            Console.WriteLine($"P({P[0]}, {P[1]}), Q({Q[0]}, {Q[1]}), R({R[0]}, {R[1]})");
            var k = 9;
            var l = 7;
            var kP = curveManager.CalculateKP(k, P);
            var lQ = curveManager.CalculateKP(l, Q);
            Console.WriteLine($"kP = {k}P = {kP.Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
            Console.WriteLine($"P + Q = {curveManager.CalculateSum(P, Q).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
            Console.WriteLine($"kP + lQ - R = {k}P + {l}Q - R = {curveManager.CalculateSum(curveManager.CalculateSum(kP, lQ), curveManager.GetInversePoint(R)).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
            Console.WriteLine($"P - Q + R = {curveManager.CalculateSum(curveManager.CalculateSum(P, curveManager.GetInversePoint(Q)), R).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
            Thread.Sleep(100);
            Console.WriteLine(new string('-', 100));


            Console.WriteLine("Задание 2");
            var customK = 12;
            var text = "высоцкийян";
            Console.WriteLine($"Text: {text}");
            var stopwatch = Stopwatch.StartNew();
            var encText = curveManager.GetEncryptTextByPointAndGenerator(text, new int[] { 0, 1 },customK);
            stopwatch.Stop();
            Console.WriteLine($"Encrypted text: {string.Join(" ", encText.Cast<int>())}");
            Console.WriteLine($"Encryption time: {stopwatch.ElapsedMilliseconds} ticks");
            stopwatch.Restart();
            var dec= curveManager.GetEncryptTextByGenerator(encText, customK);
            Console.WriteLine($"Decrypted text: {dec}");
            stopwatch.Stop();
            Console.WriteLine($"Decryption time: {stopwatch.ElapsedMilliseconds} ticks");
            Thread.Sleep(100);
            Console.WriteLine(new string('-', 100));


            Console.WriteLine("Задание 3");
            var customD = 4;
            var customQ=13;
            stopwatch.Restart();
            var digitalSign = curveManager.GenerateDigitalSign(new int[] { 416, 55 }, customQ, customD);
            stopwatch.Stop();
            Console.WriteLine($"Digital sign: {digitalSign.Select(el => el.ToString()).Aggregate((prev, current) => prev + " " + current)}");
            Console.WriteLine($"Creating digital sign time: {stopwatch.ElapsedMilliseconds} ticks");
            stopwatch.Restart();
            var IsDSVerify = curveManager.CheckDigitalSign(digitalSign, new int[] { 416, 55 }, customQ, customD);
            Console.WriteLine($"Result of checking digital sign: {IsDSVerify}");
            stopwatch.Stop();
            Console.WriteLine($"Checking digital sign time: {stopwatch.ElapsedMilliseconds} ticks");
        }
    }
}