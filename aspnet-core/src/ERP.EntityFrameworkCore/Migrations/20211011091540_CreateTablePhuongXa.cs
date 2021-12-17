using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class CreateTablePhuongXa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhuongXas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    MaPhuong = table.Column<string>(maxLength: 255, nullable: false),
                    TenPhuong = table.Column<string>(maxLength: 255, nullable: false),
                    SoDan = table.Column<int>(nullable: false),
                    ChuTichPhuong = table.Column<string>(maxLength: 255, nullable: false),
                    ThanhPhoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongXas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhuongXas_ThanhPhos_ThanhPhoId",
                        column: x => x.ThanhPhoId,
                        principalTable: "ThanhPhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhuongXas_TenantId",
                table: "PhuongXas",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PhuongXas_ThanhPhoId",
                table: "PhuongXas",
                column: "ThanhPhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhuongXas");
        }
    }
}
