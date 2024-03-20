using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebCol.Models;

namespace WebCol.Data
{
    public class ApplicationDbContext : IdentityDbContext
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
        public DbSet<Comportamiento> Comportamientos { get; set; }
        public DbSet<ProcedimientoAnalisis> ProcedimientosAnalisis { get; set; }
        public DbSet<LavadoRegeneracion> LavadosRegeneraciones { get; set; }

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
                .HasKey(ac => ac.AsignacionColumnaId);

            modelBuilder.Entity<AsignacionColumna>()
                .HasOne(ac => ac.ProcedimientoAnalisis)
                .WithMany(pa => pa.AsignacionesColumnas)
                .HasForeignKey(ac => ac.ProcedimientoAnalisisId);

            modelBuilder.Entity<AsignacionColumna>()
                .HasOne(ac => ac.Columnas)
                .WithMany(c => c.AsignacionesColumnas)
                .HasForeignKey(ac => ac.ColumnaId);

            //modelBuilder.Entity<Analisis>()
            //    .HasMany(a => a.ProductoPrincipios)
            //    .WithMany(pp => pp.Analisis)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "AnalisisProductoPrincipio",
            //        j => j
            //            .HasOne<ProductoPrincipio>()
            //            .WithMany()
            //            .OnDelete(DeleteBehavior.Restrict),
            //        j => j
            //            .HasOne<Analisis>()
            //            .WithMany()
            //            .OnDelete(DeleteBehavior.Restrict));

            modelBuilder.Entity<Analisis>()
                .HasMany(a => a.Comportamientos)
                .WithMany(c => c.Analisis);

            modelBuilder.Entity<ProcedimientoAnalisis>()
                .HasIndex(pa => new { pa.ProductoId, pa.PrincipioId })
                .IsUnique();

            modelBuilder.Entity<ProcedimientoAnalisis>()
                .HasOne(pa => pa.ProductoPrincipio)
                .WithOne(pp => pp.ProcedimientoAnalisis)
                .HasForeignKey<ProcedimientoAnalisis>(pa => new { pa.ProductoId, pa.PrincipioId });
        }
    }
}
