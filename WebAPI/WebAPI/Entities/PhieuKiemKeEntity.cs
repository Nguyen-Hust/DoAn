using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("phieu_kiem_ke")]
    public class PhieuKiemKeEntity
    {
        [Key]
        public int Id { get; set; }
        public int NhanVienId { get; set; }
        public int? PhongBanId { get; set; }
        public NhanSuEntity NhanVien { get; set; }
        public KhoaEntity PhongBan { get; set; }
    }
}
