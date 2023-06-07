using CriptaLab8.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CriptaLab8.Controllers
{

    public class SecondController : Controller
    {
        private Rc4Service _rc4=new Rc4Service();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Result(string text)
        {
            var e = Encoding.GetEncoding("iso-8859-1");
            byte[] resultArray = new byte[text.Length];
            Console.WriteLine(text);
            StringBuilder sb = new StringBuilder();
            var result = _rc4.Generate(text);
            Console.WriteLine("TEXT");
            for (int i = 0; i < text.Length; i++)
            {
                int charCode = (int)text[i] ^ result[i];

                //сделать int to char нормальный
                Console.WriteLine("char code" + charCode);
                resultArray[i]= (byte)charCode;
            }
            var rrr = e.GetString(resultArray);
            //string characters = System.Text.Encoding.GetEncoding(437).GetString(resultArray);

            ViewBag.Result = rrr;
            Response.Headers.Add("Content-Type", "text/plain");
            Response.Headers.Add("Content-Disposition", "inline");
            return Ok(rrr);
        }
    }
}
