using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("chi_tiet_phieu_ban_giao")]
    public class ChiTietPhieuBanGiaoEntity
    {
        [Key]
        public int Id { get; set; }
        public int PhieuBanGiaoId { get; set; }
        public int ChiTietThietBiId { get; set; }
        public PhieuBanGiaoEntity PhieuBanGiao { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
    }
}
