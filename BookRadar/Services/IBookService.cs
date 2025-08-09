using BookRadar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRadar.Services
{
    public interface IBookService
    {
        Task<(string? mensaje, List<LibroViewModel> libros)> BuscarLibrosPorAutorAsync(string nombreAutor);
        Task<List<BusquedaHistorial>> ObtenerHistorialAsync();
    }
}
