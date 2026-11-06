using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class AloqaFullFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aloqalar_Buyurtmalar_BuyurtmaId",
                table: "Aloqalar");

            migrationBuilder.AlterColumn<int>(
                name: "BuyurtmaId",
                table: "Aloqalar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Aloqalar_Buyurtmalar_BuyurtmaId",
                table: "Aloqalar",
                column: "BuyurtmaId",
                principalTable: "Buyurtmalar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aloqalar_Buyurtmalar_BuyurtmaId",
                table: "Aloqalar");

            migrationBuilder.AlterColumn<int>(
                name: "BuyurtmaId",
                table: "Aloqalar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aloqalar_Buyurtmalar_BuyurtmaId",
                table: "Aloqalar",
                column: "BuyurtmaId",
                principalTable: "Buyurtmalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
