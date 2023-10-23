using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("phieu_nhap_xuat")]
    public class PhieuNhapXuatEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ma { get; set; }
        public DateTime NgayNhapXuat { get; set; }
        public string NhaCungCap { get; set; }
        public string NguoiDaiDien { get; set; }
        public int NhanVienId { get; set; }
        public int? SoLuong { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TongTien { get; set; }
        [MaxLength(255)]
        public string GhiChu { get; set; }
        public int LoaiPhieu { get; set; }
        public NhanSuEntity NhanVien { get; set; }
    }
}
