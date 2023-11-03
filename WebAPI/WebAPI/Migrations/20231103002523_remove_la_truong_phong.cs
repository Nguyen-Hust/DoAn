using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class remove_la_truong_phong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "LaTruongKhoa",
                table: "nhan_su");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "LaTruongKhoa",
                table: "nhan_su",
                type: "bit",
                nullable: true);

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
        }
    }
}
