using System;
namespace WebAPI.Models.ExportImport
{
	public class ExportPhieuNhapXuatDto
	{
        public string Ma { get; set; }
        public string NgayNhapXuat { get; set; }
        public string NhaCungCap { get; set; }
        public string NguoiDaiDien { get; set; }
        public string NhanVien { get; set; }
        public int? SoLuong { get; set; }
        public decimal? TongTien { get; set; }
        public string LoaiPhieu { get; set; }
        public string GhiChu { get; set; }
    }
}

