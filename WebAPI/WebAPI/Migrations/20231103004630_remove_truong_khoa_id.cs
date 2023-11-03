using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class remove_truong_khoa_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "170b88af-a817-4276-a88f-fb94c46a8527");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b1f8545-9f77-4ec8-a8c8-535101b6b379");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df3be87d-4dae-4300-89ec-e88cffc493dd");

            migrationBuilder.DropColumn(
                name: "TruongKhoaId",
                table: "Khoa");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TruongKhoaId",
                table: "Khoa",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "170b88af-a817-4276-a88f-fb94c46a8527", "0df20b9c-9755-4e92-a1e0-f4efbdc83ee5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b1f8545-9f77-4ec8-a8c8-535101b6b379", "a2e7a8a5-8afb-4b29-87b7-1860881d9e4c", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df3be87d-4dae-4300-89ec-e88cffc493dd", "440eeb81-e803-4bd6-bee4-92ae005c28db", "User", "USER" });
        }
    }
}
