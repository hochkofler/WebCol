using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCol.Data.Migrations
{
    /// <inheritdoc />
    public partial class reestructuracion_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analisis_Columnas_ColumnaId",
                table: "Analisis");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesColumnas_ProductosPrincipios_ProductoId_PrincipioId",
                table: "AsignacionesColumnas");

            migrationBuilder.DropTable(
                name: "AnalisisProductoPrincipio");

            migrationBuilder.DropTable(
                name: "ColumnaFaseMovil");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionesColumnas_ProductoId_PrincipioId",
                table: "AsignacionesColumnas");

            migrationBuilder.DropIndex(
                name: "IX_Analisis_ColumnaId",
                table: "Analisis");

            migrationBuilder.DropColumn(
                name: "PrincipioId",
                table: "AsignacionesColumnas");

            migrationBuilder.DropColumn(
                name: "ColumnaId",
                table: "Analisis");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "AsignacionesColumnas",
                newName: "ProcedimientoAnalisisId");

            migrationBuilder.AddColumn<int>(
                name: "ProcedimientoAnalisisId",
                table: "Columnas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "AsignacionesColumnas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Analisis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PrincipiosIds",
                table: "Analisis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AsignacionColumnaId",
                table: "Analisis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoPrincipioPrincipioId",
                table: "Analisis",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductoPrincipioProductoId",
                table: "Analisis",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LavadoRegeneracion",
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
                    table.PrimaryKey("PK_LavadoRegeneracion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LavadoRegeneracion_Analisis_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LavadoRegeneracion_Columnas_ColumnaId",
                        column: x => x.ColumnaId,
                        principalTable: "Columnas",
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
                        onDelete: ReferentialAction.NoAction);//------------
                    table.ForeignKey(
                        name: "FK_ProcedimientosAnalisis_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Columnas_ProcedimientoAnalisisId",
                table: "Columnas",
                column: "ProcedimientoAnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesColumnas_ProcedimientoAnalisisId",
                table: "AsignacionesColumnas",
                column: "ProcedimientoAnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_Analisis_AsignacionColumnaId",
                table: "Analisis",
                column: "AsignacionColumnaId");

            migrationBuilder.CreateIndex(
                name: "IX_Analisis_ProductoPrincipioProductoId_ProductoPrincipioPrincipioId",
                table: "Analisis",
                columns: new[] { "ProductoPrincipioProductoId", "ProductoPrincipioPrincipioId" });

            migrationBuilder.CreateIndex(
                name: "IX_LavadoRegeneracion_AnalisisId",
                table: "LavadoRegeneracion",
                column: "AnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_LavadoRegeneracion_ColumnaId",
                table: "LavadoRegeneracion",
                column: "ColumnaId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Analisis_AsignacionesColumnas_AsignacionColumnaId",
                table: "Analisis",
                column: "AsignacionColumnaId",
                principalTable: "AsignacionesColumnas",
                principalColumn: "AsignacionColumnaId",
                onDelete: ReferentialAction.NoAction);//------------

            migrationBuilder.AddForeignKey(
                name: "FK_Analisis_ProductosPrincipios_ProductoPrincipioProductoId_ProductoPrincipioPrincipioId",
                table: "Analisis",
                columns: new[] { "ProductoPrincipioProductoId", "ProductoPrincipioPrincipioId" },
                principalTable: "ProductosPrincipios",
                principalColumns: new[] { "ProductoId", "PrincipioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesColumnas_ProcedimientosAnalisis_ProcedimientoAnalisisId",
                table: "AsignacionesColumnas",
                column: "ProcedimientoAnalisisId",
                principalTable: "ProcedimientosAnalisis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Columnas_ProcedimientosAnalisis_ProcedimientoAnalisisId",
                table: "Columnas",
                column: "ProcedimientoAnalisisId",
                principalTable: "ProcedimientosAnalisis",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analisis_AsignacionesColumnas_AsignacionColumnaId",
                table: "Analisis");

            migrationBuilder.DropForeignKey(
                name: "FK_Analisis_ProductosPrincipios_ProductoPrincipioProductoId_ProductoPrincipioPrincipioId",
                table: "Analisis");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesColumnas_ProcedimientosAnalisis_ProcedimientoAnalisisId",
                table: "AsignacionesColumnas");

            migrationBuilder.DropForeignKey(
                name: "FK_Columnas_ProcedimientosAnalisis_ProcedimientoAnalisisId",
                table: "Columnas");

            migrationBuilder.DropTable(
                name: "LavadoRegeneracion");

            migrationBuilder.DropTable(
                name: "ProcedimientosAnalisis");

            migrationBuilder.DropIndex(
                name: "IX_Columnas_ProcedimientoAnalisisId",
                table: "Columnas");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionesColumnas_ProcedimientoAnalisisId",
                table: "AsignacionesColumnas");

            migrationBuilder.DropIndex(
                name: "IX_Analisis_AsignacionColumnaId",
                table: "Analisis");

            migrationBuilder.DropIndex(
                name: "IX_Analisis_ProductoPrincipioProductoId_ProductoPrincipioPrincipioId",
                table: "Analisis");

            migrationBuilder.DropColumn(
                name: "ProcedimientoAnalisisId",
                table: "Columnas");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "AsignacionesColumnas");

            migrationBuilder.DropColumn(
                name: "AsignacionColumnaId",
                table: "Analisis");

            migrationBuilder.DropColumn(
                name: "ProductoPrincipioPrincipioId",
                table: "Analisis");

            migrationBuilder.DropColumn(
                name: "ProductoPrincipioProductoId",
                table: "Analisis");

            migrationBuilder.RenameColumn(
                name: "ProcedimientoAnalisisId",
                table: "AsignacionesColumnas",
                newName: "ProductoId");

            migrationBuilder.AddColumn<int>(
                name: "PrincipioId",
                table: "AsignacionesColumnas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Analisis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrincipiosIds",
                table: "Analisis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColumnaId",
                table: "Analisis",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AnalisisProductoPrincipio",
                columns: table => new
                {
                    AnalisisId = table.Column<int>(type: "int", nullable: false),
                    ProductoPrincipiosProductoId = table.Column<int>(type: "int", nullable: false),
                    ProductoPrincipiosPrincipioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisisProductoPrincipio", x => new { x.AnalisisId, x.ProductoPrincipiosProductoId, x.ProductoPrincipiosPrincipioId });
                    table.ForeignKey(
                        name: "FK_AnalisisProductoPrincipio_Analisis_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnalisisProductoPrincipio_ProductosPrincipios_ProductoPrincipiosProductoId_ProductoPrincipiosPrincipioId",
                        columns: x => new { x.ProductoPrincipiosProductoId, x.ProductoPrincipiosPrincipioId },
                        principalTable: "ProductosPrincipios",
                        principalColumns: new[] { "ProductoId", "PrincipioId" },
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesColumnas_ProductoId_PrincipioId",
                table: "AsignacionesColumnas",
                columns: new[] { "ProductoId", "PrincipioId" });

            migrationBuilder.CreateIndex(
                name: "IX_Analisis_ColumnaId",
                table: "Analisis",
                column: "ColumnaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisProductoPrincipio_ProductoPrincipiosProductoId_ProductoPrincipiosPrincipioId",
                table: "AnalisisProductoPrincipio",
                columns: new[] { "ProductoPrincipiosProductoId", "ProductoPrincipiosPrincipioId" });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnaFaseMovil_FasesMovilesId",
                table: "ColumnaFaseMovil",
                column: "FasesMovilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analisis_Columnas_ColumnaId",
                table: "Analisis",
                column: "ColumnaId",
                principalTable: "Columnas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesColumnas_ProductosPrincipios_ProductoId_PrincipioId",
                table: "AsignacionesColumnas",
                columns: new[] { "ProductoId", "PrincipioId" },
                principalTable: "ProductosPrincipios",
                principalColumns: new[] { "ProductoId", "PrincipioId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
