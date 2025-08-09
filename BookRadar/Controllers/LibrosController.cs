using Microsoft.AspNetCore.Mvc;
using BookRadar.Models;
using BookRadar.Services;
using System.Threading.Tasks;

namespace BookRadar.Controllers
{
    public class LibrosController : Controller
    {
        private readonly IBookService _bookService;

        public LibrosController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Muestra la vista de búsqueda
        }

        [HttpPost]
        public async Task<IActionResult> Index(string nombreAutor)
        {
            var (mensaje, libros) = await _bookService.BuscarLibrosPorAutorAsync(nombreAutor);
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Mensaje = mensaje;
            }

            // Devuelve la vista Index con los resultados de búsqueda
            return View(libros); 
        }
        // Vista del historial
        [HttpGet("Historial")]
        public async Task<IActionResult> Historial()
        {
            var historial = await _bookService.ObtenerHistorialAsync();
            return View(historial); // Muestra la vista Historial.cshtml
        }
        // Nuevo endpoint API para búsqueda en formato JSON
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarLibrosApi([FromQuery] string autor)
        {
            var (mensaje, libros) = await _bookService.BuscarLibrosPorAutorAsync(autor);

            if (!string.IsNullOrEmpty(mensaje))
            {
                // Obtener historial para calcular segundos desde la última búsqueda
                var historial = await _bookService.ObtenerHistorialAsync();
                var registro = historial.FirstOrDefault(h =>
                    h.Autor.Equals(autor, StringComparison.OrdinalIgnoreCase));

                int segundos = registro != null
                    ? (int)(DateTime.Now - registro.FechaConsulta).TotalSeconds
                    : 0;

                return BadRequest(new
                {
                    error = "Búsqueda repetida",
                    mensaje = $"Ya hicimos esta búsqueda hace {segundos} segundos. Intenta nuevamente más tarde."
                });
            }

            return Ok(libros);
        }
    }
}