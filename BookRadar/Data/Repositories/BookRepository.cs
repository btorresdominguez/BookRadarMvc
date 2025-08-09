using BookRadar.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRadar.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtiene la última búsqueda de un autor usando Stored Procedure
        public async Task<BusquedaHistorial?> GetUltimaBusquedaPorAutorAsync(string autor)
        {
            var autorParam = new SqlParameter("@Autor", autor);

            var resultado = await _context.Historial
                .FromSqlRaw("EXEC VerificarBusquedaReciente @Autor", autorParam)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return resultado;
        }

        // Guarda varias búsquedas usando Stored Procedure por cada item
        public async Task GuardarBusquedaAsync(IEnumerable<BusquedaHistorial> historial)
        {
            foreach (var item in historial)
            {
                var autorParam = new SqlParameter("@Autor", item.Autor);
                var tituloParam = new SqlParameter("@Titulo", item.Titulo);
                var anioParam = new SqlParameter("@AnioPublicacion", (object?)item.AnioPublicacion ?? DBNull.Value);
                var editorialParam = new SqlParameter("@Editorial", (object?)item.Editorial ?? DBNull.Value);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC InsertarBusqueda @Autor, @Titulo, @AnioPublicacion, @Editorial",
                    autorParam, tituloParam, anioParam, editorialParam);
            }
        }

        // Obtiene historial completo usando Stored Procedure
        public async Task<List<BusquedaHistorial>> GetHistorialAsync()
        {
            return await _context.Historial
                .FromSqlRaw("EXEC GetHistorialBusquedas")
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
