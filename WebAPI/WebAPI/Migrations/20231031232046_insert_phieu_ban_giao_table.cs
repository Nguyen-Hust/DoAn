using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class insert_phieu_ban_giao_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70a8b503-90a7-4a79-80cb-909ca9e4e07b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "842e9da2-9095-4713-b4a5-add3dd7b866a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "932c837c-acbd-44f6-b134-9d64f2031a6b");

            migrationBuilder.CreateTable(
                name: "phieu_ban_giao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    NhanVienNhan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_ban_giao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_phieu_ban_giao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuBanGiaoId = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chi_tiet_phieu_ban_giao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chi_tiet_phieu_ban_giao_phieu_ban_giao_PhieuBanGiaoId",
                        column: x => x.PhieuBanGiaoId,
                        principalTable: "phieu_ban_giao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chi_tiet_phieu_ban_giao_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_ban_giao_ChiTietThietBiId",
                table: "chi_tiet_phieu_ban_giao",
                column: "ChiTietThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_ban_giao_PhieuBanGiaoId",
                table: "chi_tiet_phieu_ban_giao",
                column: "PhieuBanGiaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chi_tiet_phieu_ban_giao");

            migrationBuilder.DropTable(
                name: "phieu_ban_giao");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70a8b503-90a7-4a79-80cb-909ca9e4e07b", "7318c232-9586-47a6-b7ba-eae336f5b3fc", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "842e9da2-9095-4713-b4a5-add3dd7b866a", "88bc33ea-4880-4237-a3b5-ac084c4f90b6", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "932c837c-acbd-44f6-b134-9d64f2031a6b", "0f36e607-7f9f-49fc-8062-55bcfa8d9228", "Admin", "ADMIN" });
        }
    }
}
