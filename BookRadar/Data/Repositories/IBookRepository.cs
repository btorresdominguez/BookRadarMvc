using BookRadar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRadar.Data.Repositories
{
    public interface IBookRepository
    {
        Task<BusquedaHistorial?> GetUltimaBusquedaPorAutorAsync(string autor);
        Task GuardarBusquedaAsync(IEnumerable<BusquedaHistorial> historial);
        Task<List<BusquedaHistorial>> GetHistorialAsync();
    }
}
