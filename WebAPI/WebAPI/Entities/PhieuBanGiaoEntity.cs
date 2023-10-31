using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("phieu_ban_giao")]
    public class PhieuBanGiaoEntity
    {
        [Key]
        public int Id { get; set; }
        public string Ma { get; set; }
        public int NhanVienId { get; set; }
        public int NhanVienNhan { get; set; }
        public DateTime? CreateTime { get; set; }
        public List<ChiTietPhieuBanGiaoEntity> ChiTietPhieuBanGiao { get; set; }
    }
}
