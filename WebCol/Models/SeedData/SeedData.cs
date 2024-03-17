using Microsoft.EntityFrameworkCore;
using WebCol.Data;
using WebCol.Models;

public static class SeedData
{
    public static List<Columna> GenerateColumnaSeedData(List<FaseMovil> fasesMovilesSeedData)
    {
        List<Columna> seedData = new List<Columna>();
        //fasesMovilesSeedData = GenerateFasesMovilesSeedData();

        // Ejemplos de fases estacionarias típicas de HPLC
        string[] fasesEstacionarias = { "C18", "C8", "C4", "Phenyl", "Cyano", "Amino", "Diol" };

        // Datos de ejemplo para inicializar las columnas
        string[] ids = { "1", "1A", "2", "2B", "3", "3A", "4", "4B", "5", "5A", "6", "6B", "7", "7A", "8" };
        string[] dimensiones = { "50x4.6x3.5", "100x4.6x3.5", "150x4.6x3.5", "250x4.6x3.5", "50x3.0x3.5", "100x3.0x3.5", "150x3.0x3.5", "250x3.0x3.5" };
        decimal[] phMin = { 2.5m, 2.8m, 3.0m, 3.2m, 3.5m, 3.7m, 4.0m, 4.2m, 4.5m, 4.8m, 5.0m, 5.2m, 5.5m, 5.7m, 6.0m };
        decimal[] phMax = { 6.0m, 6.2m, 6.5m, 6.7m, 7.0m, 7.2m, 7.5m, 7.7m, 8.0m, 8.2m, 8.5m, 8.7m, 9.0m, 9.2m, 9.5m };

        Random rnd = new Random();

        // Generar 15 columnas con datos aleatorios
        for (int i = 0; i < 15; i++)
        {
            DateTime fechaIngreso = DateTime.Now.AddDays(-rnd.Next(1, 365)); // Fecha aleatoria en el último año
            DateTime fechaEnMarcha = fechaIngreso.AddDays(rnd.Next(1, (DateTime.Now - fechaIngreso).Days)); // Fecha aleatoria entre FechaIngreso y hoy

            // Seleccionar una o más fases móviles aleatorias
            int numFasesMoviles = rnd.Next(1, 4); // Seleccionar entre 1 y 3 fases móviles
            List<FaseMovil> fasesMoviles = new List<FaseMovil>();
            for (int j = 0; j < numFasesMoviles; j++)
            {
                fasesMoviles.Add(fasesMovilesSeedData[rnd.Next(fasesMovilesSeedData.Count)]);
            }

            Columna columna = new Columna
            {
                Id = ids[i],
                FechaIngreso = fechaIngreso,
                FechaEnMarcha = fechaEnMarcha,
                Dimension = dimensiones[rnd.Next(dimensiones.Length)],
                FaseEstacionaria = fasesEstacionarias[rnd.Next(fasesEstacionarias.Length)],
                Clase = "Clase Ejemplo",
                PhMin = phMin[rnd.Next(phMin.Length)],
                PhMax = phMax[rnd.Next(phMax.Length)],
                PresionMax = rnd.Next(0, 2000),
                ModeloId = rnd.Next(1, 17), // Considerando que hay 17 marcas disponibles
                FasesMoviles = fasesMoviles
            };

            seedData.Add(columna);
        }

        return seedData;
    }

    public static void GenerateProductoPrincipioSeedData(ApplicationDbContext context)
    {
        var random = new Random();
        var productos = context.Productos.ToList();
        var principios = context.Principios.ToList();

        foreach (var producto in productos)
        {
            var numPrincipios = random.Next(1, 6);
            var usedPrincipios = new HashSet<int>();

            for (int i = 0; i < numPrincipios; i++)
            {
                var principioIndex = random.Next(principios.Count);
                while (usedPrincipios.Contains(principioIndex))
                {
                    principioIndex = random.Next(principios.Count);
                }

                usedPrincipios.Add(principioIndex);
                var principio = principios[principioIndex];

                var productoPrincipio = new ProductoPrincipio
                {
                    ProductoId = producto.Id,
                    PrincipioId = principio.Id
                };

                context.ProductosPrincipios.Add(productoPrincipio);
            }
        }
        context.SaveChanges();
    }
    public static List<FaseMovil> GenerateFasesMovilesSeedData()
    {
        List<FaseMovil> fasesMovilesSeedData = new List<FaseMovil>();

        // Ejemplos de fases móviles típicas de HPLC
        string[] fasesMovilesNombres = { "Acetonitrilo", "Metanol", "Agua", "Ácido Fosfórico", "Tetrahidrofurano" };

        // Generar datos para las fases móviles
        for (int i = 0; i < fasesMovilesNombres.Length; i++)
        {
            FaseMovil faseMovil = new FaseMovil
            {
                Nombre = fasesMovilesNombres[i],
            };

            fasesMovilesSeedData.Add(faseMovil);
        }

        return fasesMovilesSeedData;
    }

    public static void GenerateAsignacionColumnaSeedData(ApplicationDbContext context)
    {
        var random = new Random();
        var productosPrincipios = context.ProductosPrincipios.ToList();
        var columnas = context.Columnas.ToList();

        foreach (var productoPrincipio in productosPrincipios)
        {
            var numColumnas = random.Next(1, 4);
            var usedColumnas = new HashSet<int>();

            for (int i = 0; i < numColumnas; i++)
            {
                var columnaIndex = random.Next(columnas.Count);
                while (usedColumnas.Contains(columnaIndex))
                {
                    columnaIndex = random.Next(columnas.Count);
                }

                usedColumnas.Add(columnaIndex);
                var columna = columnas[columnaIndex];

                var asignacionColumna = new AsignacionColumna
                {
                    ProductoId = productoPrincipio.ProductoId,
                    PrincipioId = productoPrincipio.PrincipioId,
                    ColumnaId = columna.Id,
                    Columna = columna
                };

                context.AsignacionesColumnas.Add(asignacionColumna);
            }
        }
        context.SaveChanges();
    }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
           serviceProvider.GetRequiredService<
               DbContextOptions<ApplicationDbContext>>()))
        {
            if (!context.Proveedores.Any())
            {
                var proveedores = new Proveedor[]
                {
                new Proveedor { Nombre = "Agilent Technologies" },
                new Proveedor { Nombre = "Waters Corporation" },
                new Proveedor { Nombre = "Shimadzu Corporation" },
                new Proveedor { Nombre = "Thermo Fisher Scientific" },
                new Proveedor { Nombre = "PerkinElmer" },
                new Proveedor { Nombre = "Hitachi High-Tech" }
                // Agrega más proveedores si es necesario
                };
                context.Proveedores.AddRange(proveedores);
                context.SaveChanges();
            }
            if (!context.Modelos.Any())
            {
                var modelos = new Modelo[]
                {
                new Modelo { Nombre = "InfinityLab LC Series", ProveedorId = 1 }, // Agilent Technologies
                new Modelo { Nombre = "ACQUITY UPLC", ProveedorId = 2 }, // Waters Corporation
                new Modelo { Nombre = "Nexera", ProveedorId = 3 }, // Shimadzu Corporation
                new Modelo { Nombre = "Vanquish", ProveedorId = 4 }, // Thermo Fisher Scientific
                new Modelo { Nombre = "Flexar", ProveedorId = 5 }, // PerkinElmer
                new Modelo { Nombre = "Chromaster", ProveedorId = 6 }, // Hitachi High-Tech
                new Modelo { Nombre = "1290 Infinity II LC", ProveedorId = 1 }, // Agilent Technologies
                new Modelo { Nombre = "ACQUITY Arc", ProveedorId = 2 }, // Waters Corporation
                new Modelo { Nombre = "Prominence", ProveedorId = 3 }, // Shimadzu Corporation
                new Modelo { Nombre = "UltiMate 3000", ProveedorId = 4 }, // Thermo Fisher Scientific
                new Modelo { Nombre = "Flexar FX-15", ProveedorId = 5 }, // PerkinElmer
                new Modelo { Nombre = "Chiralpak", ProveedorId = 6 }, // Hitachi High-Tech
                new Modelo { Nombre = "Alliance HPLC", ProveedorId = 2 }, // Waters Corporation
                new Modelo { Nombre = "1260 Infinity II LC", ProveedorId = 1 }, // Agilent Technologies
                new Modelo { Nombre = "Nexera X2", ProveedorId = 3 }, // Shimadzu Corporation
                new Modelo { Nombre = "Vanquish Flex", ProveedorId = 4 }, // Thermo Fisher Scientific
                new Modelo { Nombre = "Altus HPLC", ProveedorId = 5 }, // PerkinElmer
                new Modelo { Nombre = "Primaide HPLC", ProveedorId = 6 } // Hitachi High-Tech
                                                                        // Agrega más marcas si es necesario
                };
                context.Modelos.AddRange(modelos);
                context.SaveChanges();
            }

            if (!context.FasesMoviles.Any())
            {
                var fasesMoviles = GenerateFasesMovilesSeedData();
                context.FasesMoviles.AddRange(fasesMoviles);
                context.SaveChanges();
            }

            if (!context.Columnas.Any())
            {
                var fasesMoviles = context.FasesMoviles.ToList();
                var columnas = GenerateColumnaSeedData(fasesMoviles);
                context.Columnas.AddRange(columnas);
                context.SaveChanges();
            }

            if (!context.Principios.Any())
            {
                var principiosActivos = new Principio[]
                {
                    new Principio { Nombre = "Paracetamol" },
                    new Principio { Nombre = "Ibuprofeno" },
                    new Principio { Nombre = "Omeprazol" },
                    new Principio { Nombre = "Loratadina" },
                    new Principio { Nombre = "Amoxicilina" }
                };
                context.Principios.AddRange(principiosActivos);
                context.SaveChanges();
            }

            if (!context.Productos.Any())
            {
                var principios = context.Principios.ToList();
                var productos = new Producto[]
                {                
                    new Producto { Nombre = "Panadol", Tipo = "PT", Registro = "123456" },
                    new Producto { Nombre = "Nurofen", Tipo = "PT", Registro = "789012" },
                    new Producto { Nombre = "Prilosec", Tipo = "PT", Registro = "345678" },
                    new Producto { Nombre = "Claritin", Tipo = "PT", Registro = "901234" },
                    new Producto { Nombre = "Amoxil", Tipo = "PT", Registro = "567890" },
                    new Producto { Nombre = "Panadol Plus", Tipo = "Tableta", Registro = "123456"},
                    new Producto { Nombre = "Clarixen", Tipo = "Jarabe", Registro = "789012"},
                    new Producto { Nombre = "Amoxifin", Tipo = "Cápsula", Registro = "345678" }
                 };
                context.Productos.AddRange(productos);
                context.SaveChanges();
            }

            if (!context.Lotes.Any())
            {
                var productos = context.Productos.ToList();
                var lotes = new Lote[]
                {
                    new Lote { Id = "ABC123", FechaCreacion = DateTime.Now, FechaEmision = DateTime.Now, FechaVcto = DateTime.Now.AddYears(1), TamanoObjetivo = 100, ProductoId = 1 },
                    new Lote { Id = "DEF456", FechaCreacion = DateTime.Now, FechaEmision = DateTime.Now, FechaVcto = DateTime.Now.AddYears(1), TamanoObjetivo = 150, ProductoId = 1 },
                    new Lote { Id = "GHI789", FechaCreacion = DateTime.Now, FechaEmision = DateTime.Now, FechaVcto = DateTime.Now.AddYears(1), TamanoObjetivo = 200, ProductoId = 2 },
                    new Lote { Id = "JKL012", FechaCreacion = DateTime.Now, FechaEmision = DateTime.Now, FechaVcto = DateTime.Now.AddYears(1), TamanoObjetivo = 120, ProductoId = 2 },
                    new Lote { Id = "MNO345", FechaCreacion = DateTime.Now, FechaEmision = DateTime.Now, FechaVcto = DateTime.Now.AddYears(1), TamanoObjetivo = 180, ProductoId = 4 }
                };
                context.Lotes.AddRange(lotes);
                context.SaveChanges();
            }

            if (!context.ProductosPrincipios.Any())
            {
                GenerateProductoPrincipioSeedData(context);
            }

            if (!context.AsignacionesColumnas.Any())
            {
                GenerateAsignacionColumnaSeedData(context);
            }

        }
    }    
}

