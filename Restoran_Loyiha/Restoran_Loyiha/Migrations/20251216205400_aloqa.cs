using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class aloqa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aloqalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Xabar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sana = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyurtmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aloqalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aloqalar_Buyurtmalar_BuyurtmaId",
                        column: x => x.BuyurtmaId,
                        principalTable: "Buyurtmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aloqalar_BuyurtmaId",
                table: "Aloqalar",
                column: "BuyurtmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aloqalar");
        }
    }
}
