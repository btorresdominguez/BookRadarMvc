using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookRadar.Models;

namespace BookRadar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Libros"); // Redirige a la vista de búsqueda
        }

        public IActionResult Privacy()
        {
            return View(); // Devuelve la vista Privacy.cshtml en /Views/Home/
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}