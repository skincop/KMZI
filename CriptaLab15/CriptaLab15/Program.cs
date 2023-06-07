using Aspose.Words;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        SizeEncryption();
        SizeDecryption();
        HideMessageByParagraphs();
        DecodeMessageFromParagraphs();
    }

    public static void SizeDecryption()
    {
        //Initialization 
        Document document = new Document("encMessage.docx");
        DocumentBuilder builder = new DocumentBuilder(document);

        int lines_count = document.Sections[0].Body.Paragraphs.Count;
        String arr = "";
        int size = 0;

        for (int i = 0; i < lines_count; i++)
        {
            if (document.Sections[0].Body.Paragraphs[i].Runs[0].Text.Contains("  "))
            {
                size = i;
                break;
            }

            if (document.Sections[0].Body.Paragraphs[i].Runs[0].Text.EndsWith(' '))
            {
                arr += '1';
            }
            else
            {
                arr += '0';
            }
        }



        Console.WriteLine("Message: " + BinaryToString(arr));
    }


    public static void SizeEncryption()
    {
        //Initialization 
        Document document = new Document("message.docx");
        DocumentBuilder builder = new DocumentBuilder(document);

        double lines_count = document.Sections[0].Body.Paragraphs.Count;
        Console.WriteLine("You can encrypt only " + Math.Round(lines_count / 8) + " Bytes of data");
        Console.WriteLine("Enter your message:");
        String data = Console.ReadLine();
        String bin = StringToBinary(data);

        if (bin.Length > Math.Round(lines_count))
        {
            Console.WriteLine("Message length is more than possible");
            return;
        }


        for (int i = 0; i < bin.Length; i++)
        {
            String additional = bin[i] == '0' ? "" : " ";
            document.Sections[0].Body.Paragraphs[i].Runs[0].Text += additional;

            if (i + 1 == bin.Length)
            {
                document.Sections[0].Body.Paragraphs[i + 1].Runs[0].Text += "  ";
            }
        }

        document.Save("encMessage.docx");
    }

    static void HideMessageByParagraphs()
    {
        String data = Console.ReadLine();
        String binaryMessage = StringToBinary(data);

        Document doc = new Document("message.docx");

        // Get all paragraphs      
        Paragraph[] paragraphs = doc.GetChildNodes(NodeType.Paragraph, true)
                                   .OfType<Paragraph>()
                                   .ToArray();

        int bitIndex = 0;

        foreach (Paragraph paragraph in paragraphs)
        {
            if (bitIndex > binaryMessage.Length - 1)
            {
                paragraph.ParagraphFormat.LeftIndent = 0;
                break;
            }
            if (binaryMessage[bitIndex] == '0')
                paragraph.ParagraphFormat.LeftIndent -= 1;
            else
                paragraph.ParagraphFormat.LeftIndent += 1;

            bitIndex++;
        }

        doc.Save("encMessage2.docx");
    }


    static void DecodeMessageFromParagraphs()
    {
        Document doc = new Document("encMessage2.docx");

        Paragraph[] paragraphs = doc.GetChildNodes(NodeType.Paragraph, true)
               .OfType<Paragraph>()
               .ToArray();

        StringBuilder binaryMessage = new StringBuilder();

        int bit = 0;

        foreach (Paragraph paragraph in paragraphs)
        {
            if (paragraph.ParagraphFormat.LeftIndent < 0)
                binaryMessage.Append('0');
            else if (paragraph.ParagraphFormat.LeftIndent == 0)
                break;
            else
                binaryMessage.Append('1');

            bit++;
        }

        Console.WriteLine(BinaryToString(binaryMessage.ToString()));
    }

    //String to Binary method
    public static string StringToBinary(string data)
    {
        String sb = "";

        foreach (char c in data.ToCharArray())
        {
            sb += Convert.ToString(c, 2).PadLeft(8, '0');
        }

        while (sb.Length % 8 != 0)
        {
            sb = "0" + sb;
        }

        return sb;
    }

    //Binary to String
    public static string BinaryToString(string data)
    {
        List<Byte> byteList = new List<Byte>();

        for (int i = 0; i + 8 - 1 <= data.Length; i += 8)
        {
            byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
        }
        return Encoding.ASCII.GetString(byteList.ToArray());
    }
}