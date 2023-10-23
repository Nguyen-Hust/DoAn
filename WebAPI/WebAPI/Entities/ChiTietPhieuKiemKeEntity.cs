using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("chi_tiet_phieu_kiem_ke")]
    public class ChiTietPhieuKiemKeEntity
    {
        [Key]
        public int Id { get; set; }
        public int PhieuKiemKeId { get; set; }
        public int ChiTietThietBiId { get; set; }
        public int TrangThai { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
        public PhieuKiemKeEntity PhieuKiemKe { get; set; }
    }
}
