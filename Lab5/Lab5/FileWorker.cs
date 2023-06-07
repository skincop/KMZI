using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class FileWorker
    {
        public async Task<string> readTextFromFileAsync(string fileName)
        {
            string text;
            using (StreamReader sr = new StreamReader(fileName))
            {
                text = await sr.ReadToEndAsync();
            }
            return text.ToUpper();
        }

        public async Task writeTextToFileAsync(string text, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                await writer.WriteLineAsync(text);
            }
        }
    }
}
