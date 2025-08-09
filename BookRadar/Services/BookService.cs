using BookRadar.Data.Repositories;
using BookRadar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRadar.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOpenLibraryService _openLibraryService;

        public BookService(IBookRepository bookRepository, IOpenLibraryService openLibraryService)
        {
            _bookRepository = bookRepository;
            _openLibraryService = openLibraryService;
        }

        public async Task<(string? mensaje, List<LibroViewModel> libros)> BuscarLibrosPorAutorAsync(string nombreAutor)
        {
            if (string.IsNullOrWhiteSpace(nombreAutor))
                return ("Debe ingresar un nombre de autor.", new List<LibroViewModel>());

            // Obtener historial completo (puedes cambiar por otro método si tienes uno específico)
            var historial = await _bookRepository.GetHistorialAsync();

            // Buscar registro repetido ignorando mayúsculas/minúsculas
            var registro = historial.FirstOrDefault(h =>
                h.Autor.Equals(nombreAutor, StringComparison.OrdinalIgnoreCase));

            int segundos = registro != null
                ? (int)(DateTime.Now - registro.FechaConsulta).TotalSeconds
                : int.MaxValue; // si no existe, ponemos un valor alto para que no bloquee

            if (segundos < 60)
            {
                return ("Ya hicimos esta búsqueda hace " + segundos + " segundos. Intenta nuevamente más tarde.", new List<LibroViewModel>());
            }

            // Consultar API externa
            var libros = await _openLibraryService.BuscarLibrosPorAutorAsync(nombreAutor);

            // Guardar en BD
            var nuevasBusquedas = new List<BusquedaHistorial>();
            foreach (var libro in libros)
            {
                nuevasBusquedas.Add(new BusquedaHistorial
                {
                    Autor = nombreAutor,
                    Titulo = libro.Titulo ?? "",
                    AnioPublicacion = libro.AnioPublicacion,
                    Editorial = libro.Editorial,
                    FechaConsulta = DateTime.Now
                });
            }
            await _bookRepository.GuardarBusquedaAsync(nuevasBusquedas);

            return (null, libros);
        }

        public async Task<List<BusquedaHistorial>> ObtenerHistorialAsync()
        {
            return await _bookRepository.GetHistorialAsync();
        }
    }
}
