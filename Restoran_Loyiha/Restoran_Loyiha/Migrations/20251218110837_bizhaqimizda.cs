using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class bizhaqimizda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BizHaqimizda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sarlavha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tavsif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rasm1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rasm2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rasm3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rasm4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ButtonText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ButtonLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BizHaqimizda", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BizHaqimizda");
        }
    }
}
