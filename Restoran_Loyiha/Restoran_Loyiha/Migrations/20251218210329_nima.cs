using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class nima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mijoz",
                table: "Buyurtmalar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mijoz",
                table: "Buyurtmalar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
