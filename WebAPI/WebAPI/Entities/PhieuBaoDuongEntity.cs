using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("phieu_bao_duong")]
    public class PhieuBaoDuongEntity
    {
        [Key]
        public int Id { get; set; }
        public int NhanVienId { get; set; }
        public int ChiTietThietBiId { get; set; }
        public int? TrangThai { get; set; }
        public NhanSuEntity NhanVien { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
    }
}
