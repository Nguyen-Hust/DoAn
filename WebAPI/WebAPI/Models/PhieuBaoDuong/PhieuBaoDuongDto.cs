using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.PhieuBaoDuong
{
    public class PhieuBaoDuongDto
    {
        public int Id { get; set; }
        public int? NhanVienId { get; set; }
        public int? TrangThai { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? TongThietBi { get; set; }
        public List<int> DanhSachThietBi { get; set; }
    }
}
