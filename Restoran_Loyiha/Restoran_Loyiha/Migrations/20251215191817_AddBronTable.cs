using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class AddBronTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bron",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MijozIsm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sana = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vaqt = table.Column<TimeSpan>(type: "time", nullable: false),
                    Holat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bron", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bron_Stollar_StolId",
                        column: x => x.StolId,
                        principalTable: "Stollar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bron_StolId",
                table: "Bron",
                column: "StolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bron");
        }
    }
}
