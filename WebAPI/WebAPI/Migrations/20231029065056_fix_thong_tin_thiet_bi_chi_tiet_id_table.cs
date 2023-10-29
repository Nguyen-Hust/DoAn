using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class fix_thong_tin_thiet_bi_chi_tiet_id_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17cef433-2de7-4b20-a984-b6a02e6ec666");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c1811ce-b12a-4141-8d90-f6fea5a3ee97");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5de48f86-3838-4042-abce-3a97022c92d8");

            migrationBuilder.DropColumn(
                name: "ThietBiId",
                table: "thong_tin_chi_tiet_thiet_bi");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ThietBiId",
                table: "thong_tin_chi_tiet_thiet_bi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "17cef433-2de7-4b20-a984-b6a02e6ec666", "58a0f27e-c4aa-4eb0-b041-cc0641efb6da", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c1811ce-b12a-4141-8d90-f6fea5a3ee97", "caec3745-f6f0-4433-9cff-042dc5ead8a3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5de48f86-3838-4042-abce-3a97022c92d8", "3e4c1a0b-6b1b-40fc-a162-9890954b12cb", "Manager", "MANAGER" });
        }
    }
}
