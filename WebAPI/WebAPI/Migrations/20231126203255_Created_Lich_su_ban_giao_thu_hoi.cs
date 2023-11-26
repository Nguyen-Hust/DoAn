using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class Created_Lich_su_ban_giao_thu_hoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1e60229-6fb9-41ad-a9ea-ea319401eea3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adcb951e-f14d-4843-8843-c7f1a2358d09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9fbe171-49f2-42e8-9c16-08a432006116");

            migrationBuilder.CreateTable(
                name: "lich_su_ban_giao_thu_hoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayThucHien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lich_su_ban_giao_thu_hoi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lich_su_ban_giao_thu_hoi_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "153e3f80-cbbf-41e4-a2fa-3b9d4de136e9", null, "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84e06092-ee26-4565-af2d-d312a412f9a3", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8c9810c-1855-4bc6-939a-8d7225c6ab35", null, "Manager", "MANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_lich_su_ban_giao_thu_hoi_ChiTietThietBiId",
                table: "lich_su_ban_giao_thu_hoi",
                column: "ChiTietThietBiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lich_su_ban_giao_thu_hoi");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "153e3f80-cbbf-41e4-a2fa-3b9d4de136e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84e06092-ee26-4565-af2d-d312a412f9a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8c9810c-1855-4bc6-939a-8d7225c6ab35");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1e60229-6fb9-41ad-a9ea-ea319401eea3", null, "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "adcb951e-f14d-4843-8843-c7f1a2358d09", null, "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9fbe171-49f2-42e8-9c16-08a432006116", null, "Admin", "ADMIN" });
        }
    }
}
