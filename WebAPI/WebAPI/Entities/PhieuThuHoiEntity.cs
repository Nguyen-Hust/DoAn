using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("phieu_thu_hoi")]
    public class PhieuThuHoiEntity
    {
        [Key]
        public int Id { get; set; }
        public string Ma { get; set; }
        public int NhanVienId { get; set; }
        public DateTime? CreateTime { get; set; }
        public List<ChiTietPhieuThuHoiEntity> ChiTietPhieuThuHoi { get; set; }
    }
}
