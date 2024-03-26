using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCol.Data.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comportamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comportamientos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FasesMoviles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasesMoviles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVcto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TamanoObjetivo = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Principios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Principios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelos_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosPrincipios",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    PrincipioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosPrincipios", x => new { x.ProductoId, x.PrincipioId });
                    table.ForeignKey(
                        name: "FK_ProductosPrincipios_Principios_PrincipioId",
                        column: x => x.PrincipioId,
                        principalTable: "Principios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosPrincipios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcedimientosAnalisis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaseMovilId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    PrincipioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedimientosAnalisis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedimientosAnalisis_FasesMoviles_FaseMovilId",
                        column: x => x.FaseMovilId,
                        principalTable: "FasesMoviles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedimientosAnalisis_Principios_PrincipioId",
                        column: x => x.PrincipioId,
                        principalTable: "Principios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedimientosAnalisis_ProductosPrincipios_ProductoId_PrincipioId",
                        columns: x => new { x.ProductoId, x.PrincipioId },
                        principalTable: "ProductosPrincipios",
                        principalColumns: new[] { "ProductoId", "PrincipioId" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProcedimientosAnalisis_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Columnas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEnMarcha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dimension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaseEstacionaria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PresionMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    ProcedimientoAnalisisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columnas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Columnas_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Columnas_ProcedimientosAnalisis_ProcedimientoAnalisisId",
                        column: x => x.ProcedimientoAnalisisId,
                        principalTable: "ProcedimientosAnalisis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AsignacionesColumnas",
                columns: table => new
                {
                    AsignacionColumnaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    ColumnaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcedimientoAnalisisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionesColumnas", x => x.AsignacionColumnaId);
                    table.ForeignKey(
                        name: "FK_AsignacionesColumnas_Columnas_ColumnaId",
                        column: x => x.ColumnaId,
                        principalTable: "Columnas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionesColumnas_ProcedimientosAnalisis_ProcedimientoAnalisisId",
                        column: x => x.ProcedimientoAnalisisId,
                        principalTable: "ProcedimientosAnalisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Analisis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsignacionColumnaId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaOrigen = table.Column<int>(type: "int", nullable: false),
                    LoteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ph = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TiempoCorrida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inyecciones = table.Column<int>(type: "int", nullable: false),
                    Flujo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Temperatura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PresionIni = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PresionFin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlatosIni = table.Column<int>(type: "int", nullable: false),
                    PlatosFin = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrincipiosIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoPrincipioPrincipioId = table.Column<int>(type: "int", nullable: true),
                    ProductoPrincipioProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analisis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analisis_AsignacionesColumnas_AsignacionColumnaId",
                        column: x => x.AsignacionColumnaId,
                        principalTable: "AsignacionesColumnas",
                        principalColumn: "AsignacionColumnaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Analisis_Lotes_LoteId",
                        column: x => x.LoteId,
                        principalTable: "Lotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Analisis_ProductosPrincipios_ProductoPrincipioProductoId_ProductoPrincipioPrincipioId",
                        columns: x => new { x.ProductoPrincipioProductoId, x.ProductoPrincipioPrincipioId },
                        principalTable: "ProductosPrincipios",
                        principalColumns: new[] { "ProductoId", "PrincipioId" });
                });

            migrationBuilder.CreateTable(
                name: "AnalisisComportamiento",
                columns: table => new
                {
                    AnalisisId = table.Column<int>(type: "int", nullable: false),
                    ComportamientosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisisComportamiento", x => new { x.AnalisisId, x.ComportamientosId });
                    table.ForeignKey(
                        name: "FK_AnalisisComportamiento_Analisis_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalisisComportamiento_Comportamientos_ComportamientosId",
                        column: x => x.ComportamientosId,
                        principalTable: "Comportamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LavadosRegeneraciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ColumnaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnalisisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LavadosRegeneraciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LavadosRegeneraciones_Analisis_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LavadosRegeneraciones_Columnas_ColumnaId",
                        column: x => x.ColumnaId,
                        principalTable: "Columnas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analisis_AsignacionColumnaId",
                table: "Analisis",
                column: "AsignacionColumnaId");

            migrationBuilder.CreateIndex(
                name: "IX_Analisis_LoteId",
                table: "Analisis",
                column: "LoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Analisis_ProductoPrincipioProductoId_ProductoPrincipioPrincipioId",
                table: "Analisis",
                columns: new[] { "ProductoPrincipioProductoId", "ProductoPrincipioPrincipioId" });

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisComportamiento_ComportamientosId",
                table: "AnalisisComportamiento",
                column: "ComportamientosId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesColumnas_ColumnaId",
                table: "AsignacionesColumnas",
                column: "ColumnaId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesColumnas_ProcedimientoAnalisisId",
                table: "AsignacionesColumnas",
                column: "ProcedimientoAnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_Columnas_ModeloId",
                table: "Columnas",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Columnas_ProcedimientoAnalisisId",
                table: "Columnas",
                column: "ProcedimientoAnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_LavadosRegeneraciones_AnalisisId",
                table: "LavadosRegeneraciones",
                column: "AnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_LavadosRegeneraciones_ColumnaId",
                table: "LavadosRegeneraciones",
                column: "ColumnaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_ProductoId",
                table: "Lotes",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_ProveedorId",
                table: "Modelos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Principios_ProductoId",
                table: "Principios",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosAnalisis_FaseMovilId",
                table: "ProcedimientosAnalisis",
                column: "FaseMovilId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosAnalisis_PrincipioId",
                table: "ProcedimientosAnalisis",
                column: "PrincipioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosAnalisis_ProductoId_PrincipioId",
                table: "ProcedimientosAnalisis",
                columns: new[] { "ProductoId", "PrincipioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPrincipios_PrincipioId",
                table: "ProductosPrincipios",
                column: "PrincipioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalisisComportamiento");

            migrationBuilder.DropTable(
                name: "LavadosRegeneraciones");

            migrationBuilder.DropTable(
                name: "Comportamientos");

            migrationBuilder.DropTable(
                name: "Analisis");

            migrationBuilder.DropTable(
                name: "AsignacionesColumnas");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Columnas");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "ProcedimientosAnalisis");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "FasesMoviles");

            migrationBuilder.DropTable(
                name: "ProductosPrincipios");

            migrationBuilder.DropTable(
                name: "Principios");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
