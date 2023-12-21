using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        protected readonly IConfiguration Configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() },
                                                        new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() },
                                                        new IdentityRole { Name = "Manager", NormalizedName = "Manager".ToUpper() });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        }

        public DbSet<ThietBiYTeEntity> ThietBiYTe { get; set; }
        public DbSet<ChiTietPhieuKiemKeEntity> ChiTietPhieuKiemKe { get; set; }
        public DbSet<ChiTietPhieuNhapXuatEntity> ChiTietPhieuNhapXuat { get; set; }
        public DbSet<KhoaEntity> Khoa { get; set; }
        public DbSet<LichSuSuaChuaBaoDuongEntity> LichSuSuaChuaBaoDuong { get; set; }
        public DbSet<NhanSuEntity> NhanSu { get; set; }
        public DbSet<PhieuBaoDuongEntity> PhieuBaoDuong { get; set; }
        public DbSet<PhieuKiemKeEntity> PhieuKiemKe { get; set; }
        public DbSet<PhieuNhapXuatEntity> PhieuNhapXuat { get; set; }
        public DbSet<PhieuSuaChuaEntity> PhieuSuaChua { get; set; }
        public DbSet<ThongBaoEntity> ThongBao { get; set; }
        public DbSet<ThongBaoNguoiDungEntity> ThongBaoNguoiDung { get; set; }
        public DbSet<ThongTinChiTietThietBiEntity> ThongTinChiTietThietBi { get; set; }
        public DbSet<LoaiThietBiEntity> LoaiThietBi { get; set; }
        public DbSet<ChiTietPhieuBaoDuongEntity> ChiTietPhieuBaoDuong { get; set; }
        public DbSet<PhieuBanGiaoEntity> PhieuBanGiao { get; set; }
        public DbSet<ChiTietPhieuBanGiaoEntity> ChiTietPhieuBanGiao { get; set; }
        public DbSet<PhieuThuHoiEntity> PhieuThuHoi { get; set; }
        public DbSet<ChiTietPhieuThuHoiEntity> ChiTietPhieuThuHoi { get; set; }
        public DbSet<LichSuBanGiaoThuHoiEntity> LichSuBanGiaoThuHoi { get; set; }
    }
}
