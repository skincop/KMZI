using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElGamal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int gCoef;
        private static int pCoef;
        private static int k;
        private static int x;
        private static List<BigInteger> a = new();
        private static List<BigInteger> result = new();
        private static int textLength;
        private Stopwatch stopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Code(object sender, RoutedEventArgs e)
        {
            try
            {
                aNumber.Text = "";
                resultNumber.Text = "";
                pCoef = Search_p();
                Random random = new();

                x = random.Next(1, pCoef - 1); 
                BigInteger y = BigInteger.Pow(gCoef, x) % pCoef; 

                pNumber.Text = pCoef.ToString();
                gNumber.Text = gCoef.ToString();
                xNumber.Text = x.ToString();
                yNumber.Text = y.ToString();

                string s = File.ReadAllText("text.txt").ToUpper();

                textLength = s.Length;
                stopwatch.Start();
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                result = Code(s, pCoef, y);
                stopwatch.Stop();
                var resultTime = stopwatch.Elapsed;
                string elapsedTime = $"{resultTime.Hours} {resultTime.Minutes} {resultTime.Seconds} {resultTime.Milliseconds}";
                timeWork.Text = elapsedTime;
                kNumber.Text = k.ToString();

                using StreamWriter sw = new("code.txt");

                foreach (var item in a)
                {
                    aNumber.Text += item.ToString() + '\n';
                }
                foreach (var item in result)
                {
                    sw.WriteLine(item);
                    resultNumber.Text += item.ToString() + '\n';
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Decode(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader sr = new("code.txt");
                List<string> input = sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                sr.Close();

                stopwatch.Start();
                string resultDecode = Decode(textLength, result, x, pCoef);
                stopwatch.Stop();
                var resultTime = stopwatch.Elapsed;
                string elapsedTime = $"{resultTime.Hours} {resultTime.Minutes} {resultTime.Seconds} {resultTime.Milliseconds}";
                timeWork.Text = elapsedTime;

                using StreamWriter sw = new("decode.txt");
                sw.WriteLine(resultDecode);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static List<BigInteger> Code(string text, int p, BigInteger y)
        {
            List<BigInteger> array = new();
            a.Clear();
            Random random = new();

            k = random.Next(1, p - 1);

            foreach (char c in text)
            {
                a.Add(BigInteger.Pow(gCoef, k) % p);

                array.Add((BigInteger.Pow(y, k) * (int)c) % p);
            }
            return array;
        }

        public static string Decode(int length_text, List<BigInteger> array_number, int x, int p)
        {
            string save_text = "";
            BigInteger integer;

            for (int i = 0; i != length_text; i++)
            {
                integer = (array_number[i] * (BigInteger.Pow(a[i], p - 1 - x))) % p;
                save_text += (char)integer;
            }
            return save_text;
        }

        public static bool Search_g(int p, int g)
        {
            List<BigInteger> array_mod_number = new();

            BigInteger integer = ((BigInteger.Pow(g, 1)) % p);
            array_mod_number.Add(integer);

            for (int i = 2; i != p; i++)
            {
                integer = BigInteger.Pow(g, i) % p;
                for (int j = 0; j != i - 1; j++)
                {
                    if (array_mod_number[j] == integer)
                    {
                        g--;
                        array_mod_number.Clear();
                        i = 1;
                        integer = BigInteger.Pow(g, 1) % p;
                        array_mod_number.Add(integer);
                        break;
                    }

                    if ((j == i - 2) && (array_mod_number[j] != integer))
                    {
                        array_mod_number.Add(integer);
                    }
                }
            }
            gCoef = g;
            return true;
        }


        public static int Search_p()
        {
            Random random = new();
            int p = 0;
            Boolean boolean = false;
            do
            {
                p = random.Next(2000, 2500);

                for (int i = 2; i != p; i++)
                {
                    if (i == p - 1)
                    {
                        boolean = Search_g(p, p - 1);
                        break;
                    }
                    if (p % i == 0) break;
                }
            }
            while (boolean == false);
            return p;
        }
    }
}