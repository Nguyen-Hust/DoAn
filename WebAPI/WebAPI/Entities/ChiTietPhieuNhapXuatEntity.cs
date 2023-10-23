using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("chi_tiet_phieu_nhap_xuat")]
    public class ChiTietPhieuNhapXuatEntity
    {
        [Key]
        public int Id { get; set; }
        public int PhieuNhapXuatId { get; set; }
        public int ChiTietThietBiId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? GiaTien { get; set; }
        public PhieuNhapXuatEntity PhieuNhapXuat { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
    }
}
