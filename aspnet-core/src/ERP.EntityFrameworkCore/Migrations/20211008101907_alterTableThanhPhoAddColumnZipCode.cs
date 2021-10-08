using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class alterTableThanhPhoAddColumnZipCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "ThanhPhos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "ThanhPhos");
        }
    }
}
