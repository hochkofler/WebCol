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
                    ModeloId = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Analisis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColumnaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaOrigen = table.Column<int>(type: "int", nullable: false),
                    LoteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ph = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TiempoCorrida = table.Column<TimeOnly>(type: "time", nullable: false),
                    Flujo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Temperatura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PresionIni = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PresionFin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlatosIni = table.Column<int>(type: "int", nullable: false),
                    PlatosFin = table.Column<int>(type: "int", nullable: false),
                    Comportamiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analisis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analisis_Columnas_ColumnaId",
                        column: x => x.ColumnaId,
                        principalTable: "Columnas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Analisis_Lotes_LoteId",
                        column: x => x.LoteId,
                        principalTable: "Lotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColumnaFaseMovil",
                columns: table => new
                {
                    ColumnasId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FasesMovilesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnaFaseMovil", x => new { x.ColumnasId, x.FasesMovilesId });
                    table.ForeignKey(
                        name: "FK_ColumnaFaseMovil_Columnas_ColumnasId",
                        column: x => x.ColumnasId,
                        principalTable: "Columnas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColumnaFaseMovil_FasesMoviles_FasesMovilesId",
                        column: x => x.FasesMovilesId,
                        principalTable: "FasesMoviles",
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
                    AnalisisId = table.Column<int>(type: "int", nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Principios_Analisis_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analisis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Principios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
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
                name: "AsignacionesColumnas",
                columns: table => new
                {
                    AsignacionColumnaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    PrincipioId = table.Column<int>(type: "int", nullable: false),
                    ColumnaId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                        name: "FK_AsignacionesColumnas_ProductosPrincipios_ProductoId_PrincipioId",
                        columns: x => new { x.ProductoId, x.PrincipioId },
                        principalTable: "ProductosPrincipios",
                        principalColumns: new[] { "ProductoId", "PrincipioId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analisis_ColumnaId",
                table: "Analisis",
                column: "ColumnaId");

            migrationBuilder.CreateIndex(
                name: "IX_Analisis_LoteId",
                table: "Analisis",
                column: "LoteId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesColumnas_ColumnaId",
                table: "AsignacionesColumnas",
                column: "ColumnaId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesColumnas_ProductoId_PrincipioId",
                table: "AsignacionesColumnas",
                columns: new[] { "ProductoId", "PrincipioId" });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnaFaseMovil_FasesMovilesId",
                table: "ColumnaFaseMovil",
                column: "FasesMovilesId");

            migrationBuilder.CreateIndex(
                name: "IX_Columnas_ModeloId",
                table: "Columnas",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_ProductoId",
                table: "Lotes",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_ProveedorId",
                table: "Modelos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Principios_AnalisisId",
                table: "Principios",
                column: "AnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_Principios_ProductoId",
                table: "Principios",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPrincipios_PrincipioId",
                table: "ProductosPrincipios",
                column: "PrincipioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionesColumnas");

            migrationBuilder.DropTable(
                name: "ColumnaFaseMovil");

            migrationBuilder.DropTable(
                name: "ProductosPrincipios");

            migrationBuilder.DropTable(
                name: "FasesMoviles");

            migrationBuilder.DropTable(
                name: "Principios");

            migrationBuilder.DropTable(
                name: "Analisis");

            migrationBuilder.DropTable(
                name: "Columnas");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
