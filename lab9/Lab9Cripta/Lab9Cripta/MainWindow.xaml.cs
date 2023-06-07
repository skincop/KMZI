using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace Lab9Cripta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackpackService service = new BackpackService();
        StringBuilder sb = new StringBuilder();
        Stopwatch sw = new Stopwatch();

        int[] cascadingSequence;
        int[] publicKey;
        int[] encryptedArray;
        int secretA;
        int secretN;
        TimeSpan encrTime = new TimeSpan();
        TimeSpan decrTime = new TimeSpan();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cascadingSequence = service.GenerateCascadingSequence(8);
            foreach (int i in cascadingSequence)
            {
                sb.Append(i);
                sb.Append(" ");
            }
            FirstTaskResultTb.Text = sb.ToString();
            sb.Clear();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            int sum = cascadingSequence.Sum();
            (secretN, secretA) = service.getSecrtetKey(sum);
            publicKey = service.getPublicKey(cascadingSequence, secretA, secretN);
            foreach (int i in publicKey)
            {
                sb.Append(i);
                sb.Append(" ");
            }
            //SecondTaskResultTb.Text = sb.ToString();
            SecondTaskResultTb.Text = $"A: {secretA} N: {secretN} FULL: {sb.ToString()}";
            sb.Clear();
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            var inputText = ThirdTaskInputTb.Text;
            sw.Start();
            encryptedArray = service.EncodeInputText(publicKey, inputText);
            foreach (int i in encryptedArray)
            {
                sb.Append(i);
                sb.Append(" ");
            }
            sw.Stop();

            encrTime = sw.Elapsed;
            ThirdTaskResultTb.Text = sb.ToString();
            sb.Clear();
            sw.Reset();
            timeTB.Text = $"Время шифрования: {encrTime.TotalMilliseconds.ToString()} \n Время расшифрования {decrTime.TotalMilliseconds.ToString()}";
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            sw.Start();
            int reverse = service.GetReverse(secretA,secretN);
            int[] array = new int[encryptedArray.Length];

            for (int i = 0; i < encryptedArray.Length; i++)
            {
                array[i] = (encryptedArray[i] * reverse) % secretN;
            }

            foreach (int Si in array)
            {
                string M2i = service.DecodeText(cascadingSequence, Si);
                sb.Append(M2i);
                sb.Append(" ");
            }
            string result = sb.ToString() ;
            sw.Stop();
            sb.Clear();
            result = result.Replace(" ", "");
            var stringArray = Enumerable.Range(0, result.Length / 8).Select(i => Convert.ToByte(result.Substring(i * 8, 8), 2)).ToArray();
            var str = Encoding.UTF8.GetString(stringArray);
            FourthTaskResultTb.Text = str;
            decrTime = sw.Elapsed;
            sw.Reset();
            timeTB.Text = $"Время шифрования: {encrTime.TotalMilliseconds.ToString()} \n Время расшифрования {decrTime.TotalMilliseconds.ToString()}";
        }
    }
}
