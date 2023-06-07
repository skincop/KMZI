namespace CriptaLab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int m = 421, n = 457;
            var service= new NumericService();
            var list = service.GetPrimeList(2, n);
            var b = n / Math.Log10(n);
            Console.WriteLine("First Task");
            Console.WriteLine( $"Expected {b} | Result {list.Count}");


            var list2=service.GetPrimeList(m,n);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Second Task");
            Console.WriteLine($"Result {list2.Count}");
            foreach (var item in list2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(new string('-',50));
            Console.WriteLine("Third Task");
            Console.WriteLine($"Canon form for {m} {service.GetCannonForm(6224)} ");
            Console.WriteLine($"Canon form for {n} {service.GetCannonForm(n)} ");

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Fourth Task");
            string concatNumber = String.Concat(m, n);
            Console.WriteLine($"Is concat number {concatNumber} prime {service.IsPrimeNumeric(Convert.ToInt32(concatNumber))}");

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Fifth Task");
            Console.WriteLine($"NOD {m} and {n} {service.GetNod(421,457)}");

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Sixth Task");
            Console.WriteLine("Get nod(12,21,99)=" + service.GetNod(12,21,99));
            Console.WriteLine("Get nod(5,15) ="+service.GetNod(5,15));











        }
    }
}