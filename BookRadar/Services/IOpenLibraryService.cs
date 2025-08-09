using BookRadar.Models;

namespace BookRadar.Services
{
    public interface IOpenLibraryService
    {
        Task<List<LibroViewModel>> BuscarLibrosPorAutorAsync(string nombreAutor);
    }
}
