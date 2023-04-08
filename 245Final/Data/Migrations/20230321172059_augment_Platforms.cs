using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _245Final.Data.Migrations
{
    public partial class augment_Platforms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Platform",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Platform");
        }
    }
}
