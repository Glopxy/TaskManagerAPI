using Microsoft.EntityFrameworkCore;

namespace Data;

public class GorevContext : DbContext
{
     public DbSet<Gorev> Gorevler { get; set; }

    //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //  {
    //      optionsBuilder.UseSqlServer("Server=localhost;Database=TaskManagerDb;User Id=sa;Password=1;TrustServerCertificate=True");
    //      base.OnConfiguring(optionsBuilder);
    //  }
     public GorevContext(DbContextOptions options):base(options)
    {
        
    }
}
