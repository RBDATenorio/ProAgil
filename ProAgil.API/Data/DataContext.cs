using Microsoft.EntityFrameworkCore;
using ProAgil.Domain.Entities;

namespace ProAgil.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        public DbSet<Evento> Eventos { get; set; }
    }
}