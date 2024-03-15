using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCol.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewContext2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Principios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Principios_ProductoId",
                table: "Principios",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Principios_Productos_ProductoId",
                table: "Principios",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Principios_Productos_ProductoId",
                table: "Principios");

            migrationBuilder.DropIndex(
                name: "IX_Principios_ProductoId",
                table: "Principios");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Principios");
        }
    }
}
