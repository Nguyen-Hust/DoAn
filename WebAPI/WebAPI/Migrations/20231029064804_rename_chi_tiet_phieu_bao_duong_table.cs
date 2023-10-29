using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class rename_chi_tiet_phieu_bao_duong_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuBaoDuongEntity_phieu_bao_duong_PhieuBaoDuongId",
                table: "ChiTietPhieuBaoDuongEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuBaoDuongEntity_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                table: "ChiTietPhieuBaoDuongEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietPhieuBaoDuongEntity",
                table: "ChiTietPhieuBaoDuongEntity");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e3deba-0d92-49d2-abb1-cc583c26742c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f63ea4a-3176-4042-b007-8f2dd14e893c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e92c112e-12db-4eed-8c49-fda2b2527dea");

            migrationBuilder.RenameTable(
                name: "ChiTietPhieuBaoDuongEntity",
                newName: "chi_tiet_phieu_bao_duong");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietPhieuBaoDuongEntity_PhieuBaoDuongId",
                table: "chi_tiet_phieu_bao_duong",
                newName: "IX_chi_tiet_phieu_bao_duong_PhieuBaoDuongId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietPhieuBaoDuongEntity_ChiTietThietBiId",
                table: "chi_tiet_phieu_bao_duong",
                newName: "IX_chi_tiet_phieu_bao_duong_ChiTietThietBiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chi_tiet_phieu_bao_duong",
                table: "chi_tiet_phieu_bao_duong",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_chi_tiet_phieu_bao_duong_phieu_bao_duong_PhieuBaoDuongId",
                table: "chi_tiet_phieu_bao_duong",
                column: "PhieuBaoDuongId",
                principalTable: "phieu_bao_duong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chi_tiet_phieu_bao_duong_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                table: "chi_tiet_phieu_bao_duong",
                column: "ChiTietThietBiId",
                principalTable: "thong_tin_chi_tiet_thiet_bi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chi_tiet_phieu_bao_duong_phieu_bao_duong_PhieuBaoDuongId",
                table: "chi_tiet_phieu_bao_duong");

            migrationBuilder.DropForeignKey(
                name: "FK_chi_tiet_phieu_bao_duong_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                table: "chi_tiet_phieu_bao_duong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chi_tiet_phieu_bao_duong",
                table: "chi_tiet_phieu_bao_duong");

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

            migrationBuilder.RenameTable(
                name: "chi_tiet_phieu_bao_duong",
                newName: "ChiTietPhieuBaoDuongEntity");

            migrationBuilder.RenameIndex(
                name: "IX_chi_tiet_phieu_bao_duong_PhieuBaoDuongId",
                table: "ChiTietPhieuBaoDuongEntity",
                newName: "IX_ChiTietPhieuBaoDuongEntity_PhieuBaoDuongId");

            migrationBuilder.RenameIndex(
                name: "IX_chi_tiet_phieu_bao_duong_ChiTietThietBiId",
                table: "ChiTietPhieuBaoDuongEntity",
                newName: "IX_ChiTietPhieuBaoDuongEntity_ChiTietThietBiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietPhieuBaoDuongEntity",
                table: "ChiTietPhieuBaoDuongEntity",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60e3deba-0d92-49d2-abb1-cc583c26742c", "c40724cf-7f91-47a9-97df-3b2f8c960b2e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f63ea4a-3176-4042-b007-8f2dd14e893c", "d47efc37-3327-4300-86a2-30b638c3cc24", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e92c112e-12db-4eed-8c49-fda2b2527dea", "15f9f314-fd34-42bf-bc5c-5eb4b76d3706", "Manager", "MANAGER" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuBaoDuongEntity_phieu_bao_duong_PhieuBaoDuongId",
                table: "ChiTietPhieuBaoDuongEntity",
                column: "PhieuBaoDuongId",
                principalTable: "phieu_bao_duong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuBaoDuongEntity_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                table: "ChiTietPhieuBaoDuongEntity",
                column: "ChiTietThietBiId",
                principalTable: "thong_tin_chi_tiet_thiet_bi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
