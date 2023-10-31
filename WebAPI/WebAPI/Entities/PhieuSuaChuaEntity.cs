using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("phieu_sua_chua")]
    public class PhieuSuaChuaEntity
    {
        [Key]
        public int Id { get; set; }
        public string Ma { get; set; }
        public int NhanVienId { get; set; }
        public int ChiTietThietBiId { get; set; }
        public int? TrangThai { get; set; }
        [Required]
        [MaxLength(255)]
        public string LyDo { get; set; }
        public DateTime? CreateTime { get; set; }
        public NhanSuEntity NhanVien { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
    }
}
