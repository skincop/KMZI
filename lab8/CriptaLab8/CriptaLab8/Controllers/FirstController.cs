using CriptaLab8.Service;
using Microsoft.AspNetCore.Mvc;

namespace CriptaLab8.Controllers
{
    public class FirstController : Controller
    {
        private BbsService bbsService = new BbsService();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Calculate(int paramP,int paramQ)
        {
            if (!checkParams(paramP, paramQ))
            {
                return Redirect("/first");
            }
            var lsit = bbsService.GenerateNumbers(paramP, paramQ,10);
            Console.WriteLine($"{paramP} : {paramQ}");
            return Ok(lsit);
        }

        private bool checkParams(int paramP,int paramQ)
        {
            if (paramP % 4 != 3) return false;
            if (paramQ % 4 != 3) return false;
            return true;
        }

    }
}
