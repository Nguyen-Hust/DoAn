using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class Build_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e1fcb6d-4057-4155-aa03-7f2cd46698fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd1d265a-a80c-4f0d-b146-b6f191186b80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dac166fc-24d8-4c39-b8b9-45667a25e8ea");

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TruongKhoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "thong_bao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MetaData = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thong_bao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "nhan_su",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LaTruongKhoa = table.Column<bool>(type: "bit", nullable: true),
                    LaQuanLyThietBi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhan_su", x => x.Id);
                    table.ForeignKey(
                        name: "FK_nhan_su_Khoa_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thong_bao_nguoi_duong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThongBaoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    TimeRead = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thong_bao_nguoi_duong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_thong_bao_nguoi_duong_thong_bao_ThongBaoId",
                        column: x => x.ThongBaoId,
                        principalTable: "thong_bao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phieu_kiem_ke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    PhongBanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_kiem_ke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phieu_kiem_ke_Khoa_PhongBanId",
                        column: x => x.PhongBanId,
                        principalTable: "Khoa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_phieu_kiem_ke_nhan_su_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "nhan_su",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phieu_nhap_xuat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgayNhapXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NhaCungCap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LoaiPhieu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_nhap_xuat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phieu_nhap_xuat_nhan_su_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "nhan_su",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thong_tin_chi_tiet_thiet_bi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ThietBiId = table.Column<int>(type: "int", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    XuatXu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NamSX = table.Column<int>(type: "int", nullable: true),
                    HangSanXuat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TinhTrang = table.Column<int>(type: "int", nullable: true),
                    KhoaId = table.Column<int>(type: "int", nullable: true),
                    NhanVienId = table.Column<int>(type: "int", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GiaTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ThoiGianBaoDuong = table.Column<int>(type: "int", nullable: true),
                    ThietBiYTeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thong_tin_chi_tiet_thiet_bi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_thong_tin_chi_tiet_thiet_bi_Khoa_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_thong_tin_chi_tiet_thiet_bi_nhan_su_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "nhan_su",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_thong_tin_chi_tiet_thiet_bi_thiet_bi_y_te_ThietBiYTeId",
                        column: x => x.ThietBiYTeId,
                        principalTable: "thiet_bi_y_te",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_phieu_kiem_ke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuKiemKeId = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chi_tiet_phieu_kiem_ke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chi_tiet_phieu_kiem_ke_phieu_kiem_ke_PhieuKiemKeId",
                        column: x => x.PhieuKiemKeId,
                        principalTable: "phieu_kiem_ke",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chi_tiet_phieu_kiem_ke_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_phieu_nhap_xuat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuNhapXuatId = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false),
                    GiaTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chi_tiet_phieu_nhap_xuat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chi_tiet_phieu_nhap_xuat_phieu_nhap_xuat_PhieuNhapXuatId",
                        column: x => x.PhieuNhapXuatId,
                        principalTable: "phieu_nhap_xuat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chi_tiet_phieu_nhap_xuat_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lich_su_sua_chua_bao_duong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayThucHien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Loai = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lich_su_sua_chua_bao_duong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lich_su_sua_chua_bao_duong_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phieu_bao_duong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_bao_duong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phieu_bao_duong_nhan_su_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "nhan_su",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_phieu_bao_duong_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phieu_sua_chua",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    ChiTietThietBiId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_sua_chua", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phieu_sua_chua_nhan_su_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "nhan_su",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_phieu_sua_chua_thong_tin_chi_tiet_thiet_bi_ChiTietThietBiId",
                        column: x => x.ChiTietThietBiId,
                        principalTable: "thong_tin_chi_tiet_thiet_bi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_kiem_ke_ChiTietThietBiId",
                table: "chi_tiet_phieu_kiem_ke",
                column: "ChiTietThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_kiem_ke_PhieuKiemKeId",
                table: "chi_tiet_phieu_kiem_ke",
                column: "PhieuKiemKeId");

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_nhap_xuat_ChiTietThietBiId",
                table: "chi_tiet_phieu_nhap_xuat",
                column: "ChiTietThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_nhap_xuat_PhieuNhapXuatId",
                table: "chi_tiet_phieu_nhap_xuat",
                column: "PhieuNhapXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_lich_su_sua_chua_bao_duong_ChiTietThietBiId",
                table: "lich_su_sua_chua_bao_duong",
                column: "ChiTietThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_nhan_su_KhoaId",
                table: "nhan_su",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_bao_duong_ChiTietThietBiId",
                table: "phieu_bao_duong",
                column: "ChiTietThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_bao_duong_NhanVienId",
                table: "phieu_bao_duong",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_kiem_ke_NhanVienId",
                table: "phieu_kiem_ke",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_kiem_ke_PhongBanId",
                table: "phieu_kiem_ke",
                column: "PhongBanId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_nhap_xuat_NhanVienId",
                table: "phieu_nhap_xuat",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_sua_chua_ChiTietThietBiId",
                table: "phieu_sua_chua",
                column: "ChiTietThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_sua_chua_NhanVienId",
                table: "phieu_sua_chua",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_thong_bao_nguoi_duong_ThongBaoId",
                table: "thong_bao_nguoi_duong",
                column: "ThongBaoId");

            migrationBuilder.CreateIndex(
                name: "IX_thong_tin_chi_tiet_thiet_bi_KhoaId",
                table: "thong_tin_chi_tiet_thiet_bi",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_thong_tin_chi_tiet_thiet_bi_NhanVienId",
                table: "thong_tin_chi_tiet_thiet_bi",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_thong_tin_chi_tiet_thiet_bi_ThietBiYTeId",
                table: "thong_tin_chi_tiet_thiet_bi",
                column: "ThietBiYTeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chi_tiet_phieu_kiem_ke");

            migrationBuilder.DropTable(
                name: "chi_tiet_phieu_nhap_xuat");

            migrationBuilder.DropTable(
                name: "lich_su_sua_chua_bao_duong");

            migrationBuilder.DropTable(
                name: "phieu_bao_duong");

            migrationBuilder.DropTable(
                name: "phieu_sua_chua");

            migrationBuilder.DropTable(
                name: "thong_bao_nguoi_duong");

            migrationBuilder.DropTable(
                name: "phieu_kiem_ke");

            migrationBuilder.DropTable(
                name: "phieu_nhap_xuat");

            migrationBuilder.DropTable(
                name: "thong_tin_chi_tiet_thiet_bi");

            migrationBuilder.DropTable(
                name: "thong_bao");

            migrationBuilder.DropTable(
                name: "nhan_su");

            migrationBuilder.DropTable(
                name: "Khoa");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e1fcb6d-4057-4155-aa03-7f2cd46698fb", "50d8823d-0b43-4092-8c09-d92d64c65ffa", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd1d265a-a80c-4f0d-b146-b6f191186b80", "f2921fc8-c94b-4ddd-94c4-6e45fce5f2b3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dac166fc-24d8-4c39-b8b9-45667a25e8ea", "f7012c75-c2e4-489a-a5c5-bf7a3ead8c97", "User", "USER" });
        }
    }
}
