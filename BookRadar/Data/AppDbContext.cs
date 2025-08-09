using Microsoft.EntityFrameworkCore;
using BookRadar.Models;

namespace BookRadar.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BusquedaHistorial> Historial { get; set; }
    }
}
