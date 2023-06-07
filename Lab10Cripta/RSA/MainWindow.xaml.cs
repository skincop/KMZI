using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
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

namespace RSA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RSA rsaService = new RSA();
        private Stopwatch stopwatch = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Code(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pCoefInput.Text.Length > 0 && qCoefInput.Text.Length > 0)
                {
                    int pCoef = Convert.ToInt32(pCoefInput.Text);
                    int qCoef = Convert.ToInt32(qCoefInput.Text);
                    if (rsaService.isPrime(qCoef) && rsaService.isPrime(pCoef))
                    {
                        string inputMessage = string.Empty;
                        StreamReader sr = new StreamReader("input.txt");
                        while (!sr.EndOfStream)
                            inputMessage += sr.ReadLine();
                        sr.Close();
                        inputMessage = inputMessage.ToUpper();
                        long nCoef = pCoef * qCoef;
                        long eilerFunc = (pCoef - 1) * (qCoef - 1);
                        long eCoef = rsaService.getCoefE(eilerFunc);
                        long dCoef = rsaService.getCoefD(eCoef, eilerFunc);
                        stopwatch.Start();
                        List<string> result = rsaService.Encode(inputMessage, eCoef, nCoef);
                        stopwatch.Stop();
                        var resultTime = stopwatch.Elapsed;
                        string elapsedTime = $"{resultTime.Hours} {resultTime.Minutes} {resultTime.Seconds} {resultTime.Milliseconds}";
                        timeTB.Text = elapsedTime;
                        StreamWriter sw = new StreamWriter("code.txt");
                        foreach (string item in result)
                            sw.WriteLine(item);
                        sw.Close();
                        dCoefInput.Text = dCoef.ToString();
                        eCoefInput.Text = eCoef.ToString();
                        nCoefInput.Text = nCoef.ToString();
                        nCoefInput2.Text = nCoef.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Decode(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((nCoefInput2.Text.Length > 0) && (dCoefInput.Text.Length > 0))
                {
                    long dCoef = Convert.ToInt64(dCoefInput.Text);
                    long nCoef = Convert.ToInt64(nCoefInput2.Text);
                    List<string> encodedMessage = new List<string>();
                    StreamReader sr = new StreamReader("code.txt");
                    while (!sr.EndOfStream)
                        encodedMessage.Add(sr.ReadLine());
                    sr.Close();
                    stopwatch.Start();
                    string result = rsaService.Decode(encodedMessage, dCoef, nCoef);
                    stopwatch.Stop();
                    var resultTime = stopwatch.Elapsed;
                    string elapsedTime = $"{resultTime.Hours} {resultTime.Minutes} {resultTime.Seconds} {resultTime.Milliseconds}";
                    timeTB.Text = elapsedTime;
                    StreamWriter sw = new StreamWriter("decode.txt");
                    timeTB.Text = $"result {result}";
                    sw.WriteLine(result);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
