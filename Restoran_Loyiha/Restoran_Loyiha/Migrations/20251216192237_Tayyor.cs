using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class Tayyor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyurtmalar_AspNetUsers_UserId",
                table: "Buyurtmalar");

            migrationBuilder.DropIndex(
                name: "IX_Buyurtmalar_UserId",
                table: "Buyurtmalar");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Buyurtmalar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Buyurtmalar",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Buyurtmalar_UserId",
                table: "Buyurtmalar",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyurtmalar_AspNetUsers_UserId",
                table: "Buyurtmalar",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
