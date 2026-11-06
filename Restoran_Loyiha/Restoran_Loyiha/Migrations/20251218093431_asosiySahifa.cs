using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class asosiySahifa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsosiySahifalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sarlavha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tavsif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RasmUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsosiySahifalar", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsosiySahifalar");
        }
    }
}
