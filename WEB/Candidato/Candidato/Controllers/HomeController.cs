using Candidato.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Candidato.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            ViewData["Nome"] = "William Santos Marques";
            ViewData["Cargo"] = "Desenvolvedor .Net Pleno";
            ViewData["Salário"] = "Pretensão salarial R$ 6.000";
            ViewData["Data"] = "16/16/2023";
            
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "DESAFIO";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}