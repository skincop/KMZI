using System;
using System.Drawing;
using System.Text;

namespace Lab14Cripta
{
    class LsbSteganography
    {
        public enum BiteState
        {
            Used,
            Nullifing
        };

        public static Bitmap HideTextInBmpImage(string inputText, Bitmap inputImage)
        {
            BiteState biteState = BiteState.Used;
            int index = 0;
            int value = 0;
            long pixlelIndex = 0;
            int NullifiedBitsCount = 0;
            int red = 0, green = 0, blue = 0;
            for (int i = 0; i < inputImage.Height; i++)
            {
                for (int j = 0; j < inputImage.Width; j++)
                {
                    Color pixel = inputImage.GetPixel(j, i);
                    red = pixel.R & ~1;
                    green = pixel.G & ~1;
                    blue = pixel.B & ~1;
                    for (int n = 0; n < 3; n++)
                    {
                        if (pixlelIndex % 8 == 0)
                        {
                            if (biteState == BiteState.Nullifing && NullifiedBitsCount == 8)
                            {
                                if ((pixlelIndex - 1) % 3 < 2)
                                inputImage.SetPixel(j, i, Color.FromArgb(red, green, blue));
                                return inputImage;
                            }
                            if (index >= inputText.Length)
                            biteState = BiteState.Nullifing;
                            else
                            value = inputText[index++];
                        }
                        switch (pixlelIndex % 3)
                        {
                            case 0:
                                {
                                    if (biteState == BiteState.Used)
                                    {
                                        red += value & 1;
                                        value >>= 1;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if (biteState == BiteState.Used)
                                    {
                                        green += value & 1;
                                        value >>= 1;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (biteState == BiteState.Used)
                                    {
                                        blue += value & 1;
                                        value >>= 1;
                                    }
                                    inputImage.SetPixel(j, i, Color.FromArgb(red, green, blue));
                                }
                                break;
                        }
                        pixlelIndex++;
                        if (biteState == BiteState.Nullifing)
                        {
                            NullifiedBitsCount++;
                        }
                    }
                }
            }
            return inputImage;
        }
        public static string ExtractTextFromBmpImage(Bitmap inputImage)
        {
            int index = 0;
            int value = 0;
            string outputText = "";
            for (int i = 0; i < inputImage.Height; i++)
            {
                for (int j = 0; j < inputImage.Width; j++)
                {
                    Color pixel = inputImage.GetPixel(j, i);
                    for (int n = 0; n < 3; n++)
                    {
                        switch (index % 3)
                        {
                            case 0:
                                value = value * 2 + (pixel.R & 1);
                                break;
                            case 1:
                                value = value * 2 + (pixel.G & 1);
                                break;
                            case 2:
                                value = value * 2 + (pixel.B & 1);
                                break;
                        }
                        index++;
                        if (index % 8 == 0)
                        {
                            value = ReverseBits(value);
                            if (value == 0)
                                return outputText;
                            char c = (char)value;
                            outputText += c.ToString();
                        }
                    }
                }
            }
            return outputText;
        }

        public static Bitmap CreateMatrixOfRGBContainer(Bitmap inpitImage)
        {
            int red = 0, green = 0, blue = 0;
            for (int i = 0; i < inpitImage.Height; i++)
            {
                for (int j = 0; j < inpitImage.Width; j++)
                {
                    Color pixel = inpitImage.GetPixel(j, i);
                    StringBuilder sbRed = new StringBuilder(Convert.ToString(pixel.R, 2));
                    if (sbRed[sbRed.Length - 1] == '0')
                    red = 0;
                    else
                    red = 255;

                    StringBuilder sbGreen = new StringBuilder(Convert.ToString(pixel.G, 2));
                    if (sbGreen[sbGreen.Length - 1] == '0')
                    green = 0;
                    else
                        green = 255;
                    StringBuilder sbBlue = new StringBuilder(Convert.ToString(pixel.B, 2));
                    if (sbBlue[sbBlue.Length - 1] == '0')
                        blue = 0;
                    else
                        blue = 255;
                    inpitImage.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }
            }
            return inpitImage;
        }
        private static int ReverseBits(int number)
        {
            int output = 0;
            for (int i = 0; i < 8; i++)
            {
                output = output * 2 + number % 2;
                number /= 2;
            }
            return output;
        }
    }
}