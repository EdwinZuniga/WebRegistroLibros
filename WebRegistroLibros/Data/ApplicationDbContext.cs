using Microsoft.EntityFrameworkCore;
using WebRegistroLibros.Models;

namespace WebRegistroLibros.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<Libro> Libro { get; set; }
    }
}
