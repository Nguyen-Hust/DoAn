using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class update_tinh_trang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "TinhTrang",
                table: "thong_tin_chi_tiet_thiet_bi",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<int>(
                name: "TinhTrang",
                table: "thong_tin_chi_tiet_thiet_bi",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
