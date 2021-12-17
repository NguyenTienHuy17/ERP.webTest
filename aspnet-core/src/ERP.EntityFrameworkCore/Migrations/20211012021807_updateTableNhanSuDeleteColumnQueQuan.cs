using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class updateTableNhanSuDeleteColumnQueQuan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueQuan",
                table: "NhanSus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QueQuan",
                table: "NhanSus",
                nullable: true);
        }
    }
}
