using Lab14Cripta;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab14Cripta
{
    public partial class Form1 : Form
    {
        string srcFilename = "", destFilename = "", filename = "", matrixFilename = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void takeFile(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                srcFilename = openFileDialog.FileName;
            else return;
            fileForHide.Text = srcFilename;
        }


        private void encryptFile(object sender, EventArgs e)
        {
            FileStream readStream;
            try
            {
                readStream = new FileStream(filename, FileMode.Open);
            }
            catch (IOException)
            {
                MessageBox.Show("Opening file error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string result = LsbSteganography.ExtractTextFromBmpImage(new Bitmap(readStream));
            readStream.Close();
            extractedMessage.Text = result;
            filename = "";
        }

        private void takeEnctyptFile(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                filename = openFileDialog.FileName;
            else return;
            fileForExtract.Text = filename;
        }

        private void decryptFile(object sender, EventArgs e)
        {
            FileStream readStream;
            try
            {
                readStream = new FileStream(srcFilename, FileMode.Open);
            }
            catch (IOException)
            {
                MessageBox.Show("Opening file error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Bitmap result =  LsbSteganography.HideTextInBmpImage(hiddenMessage.Text, new Bitmap(readStream));
            Bitmap colorMatrix = LsbSteganography.CreateMatrixOfRGBContainer(new Bitmap(readStream));
            readStream.Close();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                destFilename = saveFileDialog.FileName;
            }
            else return;
            FileStream writeStream;
            FileStream writeStream2;
            try
            {
                writeStream = new FileStream(destFilename, FileMode.Create); 
                writeStream2 = new FileStream($"{destFilename.Substring(0,destFilename.Length-4)}Matrix.bmp", FileMode.Create);
            }
            catch (IOException)
            {
                MessageBox.Show("Opening file error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            result.Save(writeStream, System.Drawing.Imaging.ImageFormat.Bmp);
            colorMatrix.Save(writeStream2, System.Drawing.Imaging.ImageFormat.Bmp);
            writeStream.Close();
            writeStream2.Close();
            srcFilename = ""; destFilename = "";
        }
    }
}
