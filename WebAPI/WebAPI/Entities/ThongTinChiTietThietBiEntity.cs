using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("thong_tin_chi_tiet_thiet_bi")]
    public class ThongTinChiTietThietBiEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ma { get; set; }
        public int ThietBiYTeId { get; set; }
        public DateTime NgayNhap { get; set; }
        [MaxLength(255)]
        public string XuatXu { get; set; }
        public int? NamSX { get; set; }
        [MaxLength(255)]
        public string HangSanXuat { get; set; }
        public string? TinhTrang { get; set; }
        public int? KhoaId { get; set; }
        public int? NhanVienId { get; set; }
        [MaxLength(255)]
        public string Serial { get; set; }
        [MaxLength(255)]
        public string Model { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? GiaTien { get; set; }
        public int? ThoiGianBaoDuong { get; set; }
        public bool? DaXuat { get; set; }
        public ThietBiYTeEntity ThietBiYTe { get; set; }
        public KhoaEntity Khoa { get; set; }
        public NhanSuEntity NhanVien { get; set; }
    }
}
