using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
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

namespace Lab11Cripta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Stopwatch sw = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hash(object sender, RoutedEventArgs e)
        {
            sw.Start();
            var bytes = System.Text.Encoding.UTF8.GetBytes(textInput.Text);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                resultInput.Text = hashedInputStringBuilder.ToString();
            }
            sw.Stop();
            var time = sw.ElapsedTicks;
            timeInput.Text = time.ToString();
            timeInput.Text = time.ToString();
            sw.Reset();
        }
    }
}
