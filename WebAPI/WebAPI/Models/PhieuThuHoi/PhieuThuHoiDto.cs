using WebAPI.Models.ThongTinChiTietThietBi;

namespace WebAPI.Models.PhieuThuHoi
{
    public class PhieuThuHoiDto
    {
        public int Id { get; set; }
        public string Ma { get; set; }
        public int? NhanVienId { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? TongThietBi { get; set; }
        public List<int> DanhSachThietBiId { get; set; }
        public List<ThongTinChiTietThietBiDto>? DanhSachThietBi { get; set; }
    }
}
