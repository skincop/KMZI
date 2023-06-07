namespace Lab6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var enigma = new Enigma();

            enigma.SetRightRotor(new char[] { 'A', 'J', 'D', 'K', 'S', 'I', 'R', 'U', 'X', 'B', 'L', 'H', 'W', 'T', 'M', 'C', 'Q', 'G', 'Z', 'N', 'P', 'Y', 'F', 'V', 'O', 'E' }, 4);
            enigma.SetMiddleRotor(new char[] { 'B', 'D', 'F', 'H', 'J', 'L', 'C', 'P', 'R', 'T', 'X', 'V', 'Z', 'N', 'Y', 'E', 'I', 'W', 'G', 'A', 'K', 'M', 'U', 'S', 'Q', 'O' }, 0);
            enigma.SetLeftRotor(new char[] { 'E', 'S', 'O', 'V', 'P', 'Z', 'J', 'A', 'Y', 'Q', 'U', 'I', 'R', 'H', 'X', 'L', 'N', 'F', 'T', 'G', 'K', 'D', 'C', 'M', 'W', 'B' }, 0);

            enigma.SetReflector(new char[] { 'A', 'R', 'B', 'D', 'C', 'O', 'E', 'J', 'F', 'N', 'G', 'T', 'H', 'K', 'I', 'V', 'L', 'M', 'P', 'W', 'Q', 'Z', 'S', 'X', 'U', 'Y' });


            string inputTextr = "A";
            Console.WriteLine($"Input text {inputTextr}");
            string result = enigma.Encrypte(inputTextr);
            enigma.Reset();

            Console.WriteLine($"Enigma params Left - A Middle - B Right - E Result:{result}");
            Console.WriteLine($"DECRYPT Enigma params Left - A Middle - B Right - E Result:{inputTextr}");

            enigma.Reset();
            enigma.SetRightRotorShift(1);
            enigma.SetMiddleRotorShift(2);
            enigma.SetLeftRotorShift(3);
            result = enigma.Encrypte(inputTextr);
            Console.WriteLine($"Enigma params Left - V Middle - O Right - J Result:{result}");

            enigma.Reset();
            enigma.SetRightRotorShift(0);
            enigma.SetMiddleRotorShift(19);
            enigma.SetLeftRotorShift(7);
            result = enigma.Encrypte(inputTextr);
            Console.WriteLine($"Enigma params Left - A Middle - A Right - A Result:{result}");

            enigma.Reset();
            enigma.SetRightRotorShift(3);
            enigma.SetMiddleRotorShift(3);
            enigma.SetLeftRotorShift(3);
            result = enigma.Encrypte(inputTextr);
            Console.WriteLine($"Enigma params Left - K Middle - H Right - V Result:{result}");

            enigma.Reset();
            enigma.SetRightRotorShift(0);
            enigma.SetMiddleRotorShift(11);
            enigma.SetLeftRotorShift(8);
            result = enigma.Encrypte(inputTextr);
            Console.WriteLine($"Enigma params Left - A Middle - V Right - Y Result:{result}");


        }
    }
}