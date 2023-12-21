using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class update_phieu_thu_hoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chi_tiet_phieu_thu_hoi_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                table: "chi_tiet_phieu_thu_hoi");

            migrationBuilder.DropIndex(
                name: "IX_chi_tiet_phieu_thu_hoi_ChiTietThietBiId",
                table: "chi_tiet_phieu_thu_hoi");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c21c845-d79f-4fa1-91e8-d486b7d70dd3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e442dfd6-ec03-4e73-8e5f-42f4b307c843");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "faf9dbe7-fc4f-4f81-9e57-227d589b17bb");

            migrationBuilder.RenameColumn(
                name: "ChiTietThietBiId",
                table: "chi_tiet_phieu_thu_hoi",
                newName: "NhanVienId");

            migrationBuilder.AddColumn<string>(
                name: "MaThietBi",
                table: "chi_tiet_phieu_thu_hoi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1acf6c52-2178-4550-a6a0-f2d91bdf38b0", "873c011b-19a9-441c-b585-8e9c9327c161", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e55e8e4-cf30-4673-819c-62326234a4db", "d65a6b67-0f37-424d-9a38-8ca06811ea61", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b62d21f2-8d65-4bf3-abd9-0495c9b87222", "3c562560-d736-4b21-aa99-3119315ab466", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1acf6c52-2178-4550-a6a0-f2d91bdf38b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e55e8e4-cf30-4673-819c-62326234a4db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b62d21f2-8d65-4bf3-abd9-0495c9b87222");

            migrationBuilder.DropColumn(
                name: "MaThietBi",
                table: "chi_tiet_phieu_thu_hoi");

            migrationBuilder.RenameColumn(
                name: "NhanVienId",
                table: "chi_tiet_phieu_thu_hoi",
                newName: "ChiTietThietBiId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0c21c845-d79f-4fa1-91e8-d486b7d70dd3", "7a42e522-645e-4940-b55e-73b6bfbcb1d4", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e442dfd6-ec03-4e73-8e5f-42f4b307c843", "ff18c91d-29f5-40e4-9596-29735ef14c07", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "faf9dbe7-fc4f-4f81-9e57-227d589b17bb", "11684f52-0061-45ae-bf4a-7942763fa7b4", "Manager", "MANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_thu_hoi_ChiTietThietBiId",
                table: "chi_tiet_phieu_thu_hoi",
                column: "ChiTietThietBiId");

            migrationBuilder.AddForeignKey(
                name: "FK_chi_tiet_phieu_thu_hoi_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                table: "chi_tiet_phieu_thu_hoi",
                column: "ChiTietThietBiId",
                principalTable: "thong_tin_chi_tiet_thiet_bi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
