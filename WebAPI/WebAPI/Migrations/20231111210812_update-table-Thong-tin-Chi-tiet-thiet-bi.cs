using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class updatetableThongtinChitietthietbi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88e71cbb-e8d9-4c14-8871-f33f38a79f0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f00a8ce2-5dea-476c-b210-1cf40f5bc4b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe2a97ff-fbf9-4dbd-95cf-a07b271394aa");

            migrationBuilder.AddColumn<bool>(
                name: "DaXuat",
                table: "thong_tin_chi_tiet_thiet_bi",
                type: "bit",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DaXuat",
                table: "thong_tin_chi_tiet_thiet_bi");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "88e71cbb-e8d9-4c14-8871-f33f38a79f0c", "f29d109e-fc75-4f3c-857a-410e7efdac62", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f00a8ce2-5dea-476c-b210-1cf40f5bc4b2", "814a5c13-9d9d-4e15-8d9c-35dea5ac5e1b", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe2a97ff-fbf9-4dbd-95cf-a07b271394aa", "1f2eb35b-f8dc-4bdc-a4c8-0024ec5e74fe", "User", "USER" });
        }
    }
}
