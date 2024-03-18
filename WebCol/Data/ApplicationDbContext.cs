using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebCol.Models;

namespace WebCol.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebCol.Models.Proveedor> Proveedores { get; set; } = default!;
        public DbSet<WebCol.Models.Analisis> Analisis { get; set; } = default!;
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Principio> Principios { get; set; }
        public DbSet<ProductoPrincipio> ProductosPrincipios { get; set; }
        public DbSet<AsignacionColumna> AsignacionesColumnas { get; set; }
        public DbSet<Columna> Columnas { get; set; }
        public DbSet<FaseMovil> FasesMoviles { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<AnalisisProductoPrincipio> AnalisisProductoPrincipios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductoPrincipio>()
                .HasKey(pp => new { pp.ProductoId, pp.PrincipioId });

            modelBuilder.Entity<ProductoPrincipio>()
                .HasOne(pp => pp.Producto)
                .WithMany(p => p.ProductosPrincipios)
                .HasForeignKey(pp => pp.ProductoId);

            modelBuilder.Entity<ProductoPrincipio>()
                .HasOne(pp => pp.Principio)
                .WithMany(p => p.ProductosPrincipios)
                .HasForeignKey(pp => pp.PrincipioId);


            modelBuilder.Entity<AsignacionColumna>()
                .HasOne(ac => ac.ProductoPrincipio)
                .WithMany()
                .HasForeignKey(ac => new { ac.ProductoId, ac.PrincipioId }); // Usamos la combinación de las dos propiedades

            modelBuilder.Entity<AsignacionColumna>()
                .HasOne(ac => ac.Columna)
                .WithMany()
                .HasForeignKey(ac => ac.ColumnaId);

            modelBuilder.Entity<AnalisisProductoPrincipio>()
                .HasKey(app => new { app.AnalisisId, app.ProductoId, app.PrincipioId });

            modelBuilder.Entity<AnalisisProductoPrincipio>()
                .HasOne(app => app.Analisis)
                .WithMany(a => a.AnalisisProductoPrincipios)
                .HasForeignKey(app => app.AnalisisId);

            modelBuilder.Entity<AnalisisProductoPrincipio>()
                .HasOne(app => app.ProductoPrincipio)
                .WithMany(pp => pp.Analisis)
                .HasForeignKey(app => new { app.ProductoId, app.PrincipioId })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
