using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restoran_Loyiha.Migrations
{
    /// <inheritdoc />
    public partial class biz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonLink",
                table: "BizHaqimizda");

            migrationBuilder.DropColumn(
                name: "ButtonText",
                table: "BizHaqimizda");

            migrationBuilder.DropColumn(
                name: "Rasm2",
                table: "BizHaqimizda");

            migrationBuilder.DropColumn(
                name: "Rasm3",
                table: "BizHaqimizda");

            migrationBuilder.DropColumn(
                name: "Rasm4",
                table: "BizHaqimizda");

            migrationBuilder.AddColumn<int>(
                name: "OshpazlarSoni",
                table: "BizHaqimizda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TajribaYil",
                table: "BizHaqimizda",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OshpazlarSoni",
                table: "BizHaqimizda");

            migrationBuilder.DropColumn(
                name: "TajribaYil",
                table: "BizHaqimizda");

            migrationBuilder.AddColumn<string>(
                name: "ButtonLink",
                table: "BizHaqimizda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ButtonText",
                table: "BizHaqimizda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rasm2",
                table: "BizHaqimizda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rasm3",
                table: "BizHaqimizda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rasm4",
                table: "BizHaqimizda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
