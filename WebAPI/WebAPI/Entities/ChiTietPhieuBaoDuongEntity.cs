using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("chi_tiet_phieu_bao_duong")]
    public class ChiTietPhieuBaoDuongEntity
    {
        [Key]
        public int Id { get; set; }
        public int PhieuBaoDuongId { get; set; }
        public int ChiTietThietBiId { get; set; }
        public PhieuBaoDuongEntity PhieuBaoDuong { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
    }
}
