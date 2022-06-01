using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOM.Migrations
{
    public partial class EditDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
