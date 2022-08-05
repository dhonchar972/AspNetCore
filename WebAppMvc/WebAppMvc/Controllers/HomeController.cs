using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMvc.Models;

namespace WebAppMvc.Controllers
{
    public class HomeController : Controller
    {
        //включение логирование
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //стартовая страница
        public IActionResult Index()
        {
            //обращение к представлению
            return View();
        }

        [HttpPost] //[HttpGet], [HttpPost], [HttpPut], [HttpPatch], [HttpDelete],[HttpHead]
        public string Hello() => "Hello ASP.NET";

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}