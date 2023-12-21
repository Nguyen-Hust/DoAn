using System;
namespace WebAPI.Models.ThongTinChiTietThietBi
{
	public class ThongTinThietBiReaderDto
	{
        public string Ma { get; set; }
        public int ThietBiYTeId { get; set; }
        public DateTime NgayNhap { get; set; }
        public string? XuatXu { get; set; }
        public int? NamSX { get; set; }
        public string? HangSanXuat { get; set; }
        public string? TinhTrang { get; set; }
        public int? KhoaId { get; set; }
        public int? NhanVienId { get; set; }
        public string? Serial { get; set; }
        public string? Model { get; set; }
        public decimal? GiaTien { get; set; }
        public int? ThoiGianBaoDuong { get; set; }
    }
}

