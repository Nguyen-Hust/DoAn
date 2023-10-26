using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class insert_loai_thiet_bi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2520e0f2-3f29-4c0d-b670-00f2d0c5aae3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a619a45-d66f-4655-bd3e-1f094e69a97c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "909be55c-d1a4-4418-ae30-6992b737cbf5");

            migrationBuilder.CreateTable(
                name: "loai_thiet_bi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loai_thiet_bi", x => x.Id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loai_thiet_bi");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2520e0f2-3f29-4c0d-b670-00f2d0c5aae3", "1d60eff9-00a2-4d4e-8960-c423147e9434", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a619a45-d66f-4655-bd3e-1f094e69a97c", "20c6a5e2-702f-4641-896d-ac5a46d0a114", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "909be55c-d1a4-4418-ae30-6992b737cbf5", "d0beef8f-81fd-4490-8e4c-4523d9f1358b", "Admin", "ADMIN" });
        }
    }
}
