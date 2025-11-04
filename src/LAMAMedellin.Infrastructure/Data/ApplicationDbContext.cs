using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Miembro> Miembros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Miembro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Celular).HasMaxLength(20);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Cargo).HasMaxLength(100);
            entity.Property(e => e.Rango).HasMaxLength(100);
            entity.Property(e => e.Estatus).HasMaxLength(50);
            entity.Property(e => e.Cedula).HasMaxLength(20);
            entity.Property(e => e.RH).HasMaxLength(5);
            entity.Property(e => e.EPS).HasMaxLength(100);
            entity.Property(e => e.Padrino).HasMaxLength(100);
            entity.Property(e => e.Foto).HasMaxLength(255);
            entity.Property(e => e.ContactoEmergencia).HasMaxLength(255);
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Moto).HasMaxLength(100);
            entity.Property(e => e.Marca).HasMaxLength(100);
            entity.Property(e => e.PlacaMatricula).HasMaxLength(20);
        });
    }
}
