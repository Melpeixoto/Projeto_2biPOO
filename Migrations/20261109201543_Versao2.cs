using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_2biPOO.Migrations
{
    /// <inheritdoc />
    public partial class Versao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Restaurantes_RestauranteId",
                table: "Produtos");

            migrationBuilder.AlterColumn<int>(
                name: "RestauranteId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Restaurantes_RestauranteId",
                table: "Produtos",
                column: "RestauranteId",
                principalTable: "Restaurantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Restaurantes_RestauranteId",
                table: "Produtos");

            migrationBuilder.AlterColumn<int>(
                name: "RestauranteId",
                table: "Produtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Restaurantes_RestauranteId",
                table: "Produtos",
                column: "RestauranteId",
                principalTable: "Restaurantes",
                principalColumn: "Id");
        }
    }
}
