using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("nhan_su")]
    public class NhanSuEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ma { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ten { get; set; }
        public int KhoaId { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string SDT { get; set; }
        [MaxLength(255)]
        public string DiaChi { get; set; }
        [MaxLength(255)]
        public string AccountId { get; set; }
        public bool? LaQuanLyThietBi { get; set; }
        public KhoaEntity Khoa { get; set; }
    }
}
