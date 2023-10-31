using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.PhieuSuaChua
{
    public class PhieuSuaChuaDto
    {
        public int Id { get; set; }
        public string Ma { get; set; }
        public int NhanVienId { get; set; }
        public int ChiTietThietBiId { get; set; }
        public int? TrangThai { get; set; }
        public string LyDo { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
