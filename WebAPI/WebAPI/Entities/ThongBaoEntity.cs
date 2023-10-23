using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("thong_bao")]
    public class ThongBaoEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(255)]
        public string Message { get; set; }
        [MaxLength(255)]
        public string MetaData { get; set; }
        public DateTime SendTime { get; set; }
    }
}
