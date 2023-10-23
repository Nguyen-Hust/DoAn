using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("thong_bao_nguoi_duong")]
    public class ThongBaoNguoiDungEntity
    {
        [Key]
        public int Id { get; set; }
        public int ThongBaoId { get; set; }
        public string UserId { get; set; }
        public int? Status { get; set; }
        public DateTime? TimeRead { get; set; }
        public ThongBaoEntity ThongBao { get; set; }
    }
}
