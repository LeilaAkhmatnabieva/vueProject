using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model
{
    public class GilecContext: DbContext
    {
        public DbSet<Gilec> citizens { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=citizen_app;Username=postgres;Password=leila");
    }
}