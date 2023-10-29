using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class add_create_time_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_phieu_bao_duong_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                table: "phieu_bao_duong");

            migrationBuilder.DropIndex(
                name: "IX_phieu_bao_duong_ChiTietThietBiId",
                table: "phieu_bao_duong");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8291bd45-b819-41e5-b35e-12072d133e5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ade0aaab-9d6a-40fa-bcd7-e5bb22c4fd57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf53572a-badd-4613-9999-fa1febad7cdc");

            migrationBuilder.DropColumn(
                name: "ChiTietThietBiId",
                table: "phieu_bao_duong");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "phieu_sua_chua",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "phieu_bao_duong",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuBaoDuongEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuBaoDuongId = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuBaoDuongEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuBaoDuongEntity_phieu_bao_duong_PhieuBaoDuongId",
                        column: x => x.PhieuBaoDuongId,
                        principalTable: "phieu_bao_duong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuBaoDuongEntity_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60e3deba-0d92-49d2-abb1-cc583c26742c", "c40724cf-7f91-47a9-97df-3b2f8c960b2e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f63ea4a-3176-4042-b007-8f2dd14e893c", "d47efc37-3327-4300-86a2-30b638c3cc24", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e92c112e-12db-4eed-8c49-fda2b2527dea", "15f9f314-fd34-42bf-bc5c-5eb4b76d3706", "Manager", "MANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuBaoDuongEntity_ChiTietThietBiId",
                table: "ChiTietPhieuBaoDuongEntity",
                column: "ChiTietThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuBaoDuongEntity_PhieuBaoDuongId",
                table: "ChiTietPhieuBaoDuongEntity",
                column: "PhieuBaoDuongId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietPhieuBaoDuongEntity");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e3deba-0d92-49d2-abb1-cc583c26742c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f63ea4a-3176-4042-b007-8f2dd14e893c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e92c112e-12db-4eed-8c49-fda2b2527dea");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "phieu_sua_chua");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "phieu_bao_duong");

            migrationBuilder.AddColumn<int>(
                name: "ChiTietThietBiId",
                table: "phieu_bao_duong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8291bd45-b819-41e5-b35e-12072d133e5d", "f8094c8e-2d38-4929-b6c6-bc5caca0513d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ade0aaab-9d6a-40fa-bcd7-e5bb22c4fd57", "a734cbd5-94fb-4f72-8322-c48b851e0814", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cf53572a-badd-4613-9999-fa1febad7cdc", "4e4bb684-fab7-487c-a089-c8436e3cdd5c", "Manager", "MANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_phieu_bao_duong_ChiTietThietBiId",
                table: "phieu_bao_duong",
                column: "ChiTietThietBiId");

            migrationBuilder.AddForeignKey(
                name: "FK_phieu_bao_duong_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                table: "phieu_bao_duong",
                column: "ChiTietThietBiId",
                principalTable: "thong_tin_chi_tiet_thiet_bi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
