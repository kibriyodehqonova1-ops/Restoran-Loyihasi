using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class StolSigim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bron_Stollar_StolId",
                table: "Bron");

            migrationBuilder.DropForeignKey(
                name: "FK_Buyurtmalar_Stollar_StolId",
                table: "Buyurtmalar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stollar",
                table: "Stollar");

            migrationBuilder.RenameTable(
                name: "Stollar",
                newName: "Stol");

            migrationBuilder.AddColumn<bool>(
                name: "Band",
                table: "Stol",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stol",
                table: "Stol",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bron_Stol_StolId",
                table: "Bron",
                column: "StolId",
                principalTable: "Stol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buyurtmalar_Stol_StolId",
                table: "Buyurtmalar",
                column: "StolId",
                principalTable: "Stol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bron_Stol_StolId",
                table: "Bron");

            migrationBuilder.DropForeignKey(
                name: "FK_Buyurtmalar_Stol_StolId",
                table: "Buyurtmalar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stol",
                table: "Stol");

            migrationBuilder.DropColumn(
                name: "Band",
                table: "Stol");

            migrationBuilder.RenameTable(
                name: "Stol",
                newName: "Stollar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stollar",
                table: "Stollar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bron_Stollar_StolId",
                table: "Bron",
                column: "StolId",
                principalTable: "Stollar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buyurtmalar_Stollar_StolId",
                table: "Buyurtmalar",
                column: "StolId",
                principalTable: "Stollar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
