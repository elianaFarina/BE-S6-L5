using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using static WebApplication1.Models.Clienti;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

    public DbSet<Cliente> Clienti { get; set; }
    public DbSet<Camera> Camere { get; set; }
    public DbSet<Prenotazione> Prenotazioni { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Prenotazione>()
            .HasOne(p => p.Cliente)
            .WithMany()
            .HasForeignKey(p => p.ClienteId);

        modelBuilder.Entity<Prenotazione>()
            .HasOne(p => p.Camera)
            .WithMany()
            .HasForeignKey(p => p.CameraId);

        modelBuilder.Entity<Camera>()
            .Property(c => c.Prezzo)
            .HasColumnType("decimal(18, 2)");
    }
}
