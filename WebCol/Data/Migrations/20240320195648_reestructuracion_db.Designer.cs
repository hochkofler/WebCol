﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCol.Data;

#nullable disable

namespace WebCol.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240320195648_reestructuracion_db")]
    partial class reestructuracion_db
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnalisisComportamiento", b =>
                {
                    b.Property<int>("AnalisisId")
                        .HasColumnType("int");

                    b.Property<int>("ComportamientosId")
                        .HasColumnType("int");

                    b.HasKey("AnalisisId", "ComportamientosId");

                    b.HasIndex("ComportamientosId");

                    b.ToTable("AnalisisComportamiento");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebCol.Models.Analisis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AsignacionColumnaId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaOrigen")
                        .HasColumnType("int");

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Flujo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Inyecciones")
                        .HasColumnType("int");

                    b.Property<string>("LoteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Ph")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PlatosFin")
                        .HasColumnType("int");

                    b.Property<int>("PlatosIni")
                        .HasColumnType("int");

                    b.Property<decimal>("PresionFin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PresionIni")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PrincipiosIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductoPrincipioPrincipioId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductoPrincipioProductoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Temperatura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TiempoCorrida")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Usuario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AsignacionColumnaId");

                    b.HasIndex("LoteId");

                    b.HasIndex("ProductoPrincipioProductoId", "ProductoPrincipioPrincipioId");

                    b.ToTable("Analisis");
                });

            modelBuilder.Entity("WebCol.Models.AsignacionColumna", b =>
                {
                    b.Property<int>("AsignacionColumnaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AsignacionColumnaId"));

                    b.Property<bool>("Activa")
                        .HasColumnType("bit");

                    b.Property<string>("ColumnaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProcedimientoAnalisisId")
                        .HasColumnType("int");

                    b.HasKey("AsignacionColumnaId");

                    b.HasIndex("ColumnaId");

                    b.HasIndex("ProcedimientoAnalisisId");

                    b.ToTable("AsignacionesColumnas");
                });

            modelBuilder.Entity("WebCol.Models.Columna", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Clase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dimension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FaseEstacionaria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaEnMarcha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModeloId")
                        .HasColumnType("int");

                    b.Property<decimal>("PhMax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PhMin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PresionMax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProcedimientoAnalisisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModeloId");

                    b.HasIndex("ProcedimientoAnalisisId");

                    b.ToTable("Columnas");
                });

            modelBuilder.Entity("WebCol.Models.Comportamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comportamientos");
                });

            modelBuilder.Entity("WebCol.Models.FaseMovil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FasesMoviles");
                });

            modelBuilder.Entity("WebCol.Models.LavadoRegeneracion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnalisisId")
                        .HasColumnType("int");

                    b.Property<string>("ColumnaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AnalisisId");

                    b.HasIndex("ColumnaId");

                    b.ToTable("LavadoRegeneracion");
                });

            modelBuilder.Entity("WebCol.Models.Lote", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVcto")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("TamanoObjetivo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("WebCol.Models.Modelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Modelos");
                });

            modelBuilder.Entity("WebCol.Models.Principio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("Principios");
                });

            modelBuilder.Entity("WebCol.Models.ProcedimientoAnalisis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FaseMovilId")
                        .HasColumnType("int");

                    b.Property<int>("PrincipioId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FaseMovilId");

                    b.HasIndex("PrincipioId");

                    b.HasIndex("ProductoId", "PrincipioId")
                        .IsUnique();

                    b.ToTable("ProcedimientosAnalisis");
                });

            modelBuilder.Entity("WebCol.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Registro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("WebCol.Models.ProductoPrincipio", b =>
                {
                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("PrincipioId")
                        .HasColumnType("int");

                    b.HasKey("ProductoId", "PrincipioId");

                    b.HasIndex("PrincipioId");

                    b.ToTable("ProductosPrincipios");
                });

            modelBuilder.Entity("WebCol.Models.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("AnalisisComportamiento", b =>
                {
                    b.HasOne("WebCol.Models.Analisis", null)
                        .WithMany()
                        .HasForeignKey("AnalisisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.Comportamiento", null)
                        .WithMany()
                        .HasForeignKey("ComportamientosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebCol.Models.Analisis", b =>
                {
                    b.HasOne("WebCol.Models.AsignacionColumna", "AsignacionColumnas")
                        .WithMany("Analisis")
                        .HasForeignKey("AsignacionColumnaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.Lote", "Lote")
                        .WithMany()
                        .HasForeignKey("LoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.ProductoPrincipio", null)
                        .WithMany("Analisis")
                        .HasForeignKey("ProductoPrincipioProductoId", "ProductoPrincipioPrincipioId");

                    b.Navigation("AsignacionColumnas");

                    b.Navigation("Lote");
                });

            modelBuilder.Entity("WebCol.Models.AsignacionColumna", b =>
                {
                    b.HasOne("WebCol.Models.Columna", "Columnas")
                        .WithMany("AsignacionesColumnas")
                        .HasForeignKey("ColumnaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.ProcedimientoAnalisis", "ProcedimientoAnalisis")
                        .WithMany("AsignacionesColumnas")
                        .HasForeignKey("ProcedimientoAnalisisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Columnas");

                    b.Navigation("ProcedimientoAnalisis");
                });

            modelBuilder.Entity("WebCol.Models.Columna", b =>
                {
                    b.HasOne("WebCol.Models.Modelo", "Modelo")
                        .WithMany("Columnas")
                        .HasForeignKey("ModeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.ProcedimientoAnalisis", null)
                        .WithMany("Columnas")
                        .HasForeignKey("ProcedimientoAnalisisId");

                    b.Navigation("Modelo");
                });

            modelBuilder.Entity("WebCol.Models.LavadoRegeneracion", b =>
                {
                    b.HasOne("WebCol.Models.Analisis", "Analisis")
                        .WithMany("LavadoRegeneracion")
                        .HasForeignKey("AnalisisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.Columna", "Columna")
                        .WithMany("LavadosRegeneraciones")
                        .HasForeignKey("ColumnaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Analisis");

                    b.Navigation("Columna");
                });

            modelBuilder.Entity("WebCol.Models.Lote", b =>
                {
                    b.HasOne("WebCol.Models.Producto", "Producto")
                        .WithMany("Lotes")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("WebCol.Models.Modelo", b =>
                {
                    b.HasOne("WebCol.Models.Proveedor", "Proveedor")
                        .WithMany("Modelo")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("WebCol.Models.Principio", b =>
                {
                    b.HasOne("WebCol.Models.Producto", null)
                        .WithMany("Principios")
                        .HasForeignKey("ProductoId");
                });

            modelBuilder.Entity("WebCol.Models.ProcedimientoAnalisis", b =>
                {
                    b.HasOne("WebCol.Models.FaseMovil", "FaseMovil")
                        .WithMany("ProcedimientoAnalisis")
                        .HasForeignKey("FaseMovilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.Principio", "Principio")
                        .WithMany()
                        .HasForeignKey("PrincipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.ProductoPrincipio", "ProductoPrincipio")
                        .WithOne("ProcedimientoAnalisis")
                        .HasForeignKey("WebCol.Models.ProcedimientoAnalisis", "ProductoId", "PrincipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FaseMovil");

                    b.Navigation("Principio");

                    b.Navigation("Producto");

                    b.Navigation("ProductoPrincipio");
                });

            modelBuilder.Entity("WebCol.Models.ProductoPrincipio", b =>
                {
                    b.HasOne("WebCol.Models.Principio", "Principio")
                        .WithMany("ProductosPrincipios")
                        .HasForeignKey("PrincipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCol.Models.Producto", "Producto")
                        .WithMany("ProductosPrincipios")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Principio");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("WebCol.Models.Analisis", b =>
                {
                    b.Navigation("LavadoRegeneracion");
                });

            modelBuilder.Entity("WebCol.Models.AsignacionColumna", b =>
                {
                    b.Navigation("Analisis");
                });

            modelBuilder.Entity("WebCol.Models.Columna", b =>
                {
                    b.Navigation("AsignacionesColumnas");

                    b.Navigation("LavadosRegeneraciones");
                });

            modelBuilder.Entity("WebCol.Models.FaseMovil", b =>
                {
                    b.Navigation("ProcedimientoAnalisis");
                });

            modelBuilder.Entity("WebCol.Models.Modelo", b =>
                {
                    b.Navigation("Columnas");
                });

            modelBuilder.Entity("WebCol.Models.Principio", b =>
                {
                    b.Navigation("ProductosPrincipios");
                });

            modelBuilder.Entity("WebCol.Models.ProcedimientoAnalisis", b =>
                {
                    b.Navigation("AsignacionesColumnas");

                    b.Navigation("Columnas");
                });

            modelBuilder.Entity("WebCol.Models.Producto", b =>
                {
                    b.Navigation("Lotes");

                    b.Navigation("Principios");

                    b.Navigation("ProductosPrincipios");
                });

            modelBuilder.Entity("WebCol.Models.ProductoPrincipio", b =>
                {
                    b.Navigation("Analisis");

                    b.Navigation("ProcedimientoAnalisis");
                });

            modelBuilder.Entity("WebCol.Models.Proveedor", b =>
                {
                    b.Navigation("Modelo");
                });
#pragma warning restore 612, 618
        }
    }
}
