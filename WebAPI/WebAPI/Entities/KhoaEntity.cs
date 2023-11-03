using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("Khoa")]
    public class KhoaEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ma { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ten { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string SDT { get; set; }
        public List<NhanSuEntity> NhanSu { get; set; }
    }
}
