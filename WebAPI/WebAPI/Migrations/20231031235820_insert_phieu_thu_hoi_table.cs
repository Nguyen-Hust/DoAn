using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class insert_phieu_thu_hoi_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6be090a9-9221-4f17-9586-e1d4f94dcf7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aca205f0-33e7-465e-acc1-fed0069b359b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad4cf4f8-470b-4cc8-97f6-4bc3451e5707");

            migrationBuilder.AddColumn<string>(
                name: "Ma",
                table: "phieu_sua_chua",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ma",
                table: "phieu_bao_duong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "phieu_ban_giao",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ma",
                table: "phieu_ban_giao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "phieu_thu_hoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_thu_hoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_phieu_thu_hoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuThuHoiId = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chi_tiet_phieu_thu_hoi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chi_tiet_phieu_thu_hoi_phieu_thu_hoi_PhieuThuHoiId",
                        column: x => x.PhieuThuHoiId,
                        principalTable: "phieu_thu_hoi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chi_tiet_phieu_thu_hoi_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "435b1b88-e62f-4d01-bd52-2d6c9cebc7cf", "f6770534-2ca3-4b22-bfdd-30e2c038342a", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "892b8e5a-29d6-4861-9c52-a9baeddef1df", "b4d7c20b-3496-4c59-b0a8-838e6447e7c1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b5831ab8-6cad-40d8-b908-a084747e04f5", "c1d71c50-fcd4-44ce-b9a1-8c3667f5a5df", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_thu_hoi_ChiTietThietBiId",
                table: "chi_tiet_phieu_thu_hoi",
                column: "ChiTietThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_thu_hoi_PhieuThuHoiId",
                table: "chi_tiet_phieu_thu_hoi",
                column: "PhieuThuHoiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chi_tiet_phieu_thu_hoi");

            migrationBuilder.DropTable(
                name: "phieu_thu_hoi");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "435b1b88-e62f-4d01-bd52-2d6c9cebc7cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "892b8e5a-29d6-4861-9c52-a9baeddef1df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5831ab8-6cad-40d8-b908-a084747e04f5");

            migrationBuilder.DropColumn(
                name: "Ma",
                table: "phieu_sua_chua");

            migrationBuilder.DropColumn(
                name: "Ma",
                table: "phieu_bao_duong");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "phieu_ban_giao");

            migrationBuilder.DropColumn(
                name: "Ma",
                table: "phieu_ban_giao");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6be090a9-9221-4f17-9586-e1d4f94dcf7f", "223afe39-efed-4d13-8252-ea17e1ded52e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aca205f0-33e7-465e-acc1-fed0069b359b", "d9a11781-9249-4266-988f-6084f7da77ab", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ad4cf4f8-470b-4cc8-97f6-4bc3451e5707", "c776686e-913c-42d3-85ef-7deb3f6e1b62", "Manager", "MANAGER" });
        }
    }
}
