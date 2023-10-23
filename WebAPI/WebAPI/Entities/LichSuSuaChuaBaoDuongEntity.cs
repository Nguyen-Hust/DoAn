using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("lich_su_sua_chua_bao_duong")]
    public class LichSuSuaChuaBaoDuongEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime NgayThucHien { get; set; }
        public int Loai { get; set; }
        public int ChiTietThietBiId { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
    }
}
