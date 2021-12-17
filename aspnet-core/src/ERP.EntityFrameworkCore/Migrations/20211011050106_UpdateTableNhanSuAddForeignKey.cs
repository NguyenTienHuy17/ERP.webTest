using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class UpdateTableNhanSuAddForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThanhPhoId",
                table: "NhanSus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NhanSus_ThanhPhoId",
                table: "NhanSus",
                column: "ThanhPhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanSus_ThanhPhos_ThanhPhoId",
                table: "NhanSus",
                column: "ThanhPhoId",
                principalTable: "ThanhPhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanSus_ThanhPhos_ThanhPhoId",
                table: "NhanSus");

            migrationBuilder.DropIndex(
                name: "IX_NhanSus_ThanhPhoId",
                table: "NhanSus");

            migrationBuilder.DropColumn(
                name: "ThanhPhoId",
                table: "NhanSus");
        }
    }
}
