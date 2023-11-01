using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("chi_tiet_phieu_thu_hoi")]
    public class ChiTietPhieuThuHoiEntity
    {
        [Key]
        public int Id { get; set; }
        public int PhieuThuHoiId { get; set; }
        public int ChiTietThietBiId { get; set; }
        public PhieuThuHoiEntity PhieuThuHoi { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
    }
}
