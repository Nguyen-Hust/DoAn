using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("thiet_bi_y_te")]
    public class ThietBiYTeEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ma { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ten { get; set; }
        [Required]
        [MaxLength(255)]
        public string MDRR { get; set; }
        [Required]
        [MaxLength(255)]
        public string LoaiTTBYT { get; set; }
        public int SoLuong { get; set; }
        public List<ThongTinChiTietThietBiEntity> ThongTinChiTietThietBi { get; }
    }
}
