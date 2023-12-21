using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models.ThongTinChiTietThietBi;

namespace WebAPI.Models.ExportImport
{
	public class ExportDetailNhapXuatDto
	{
        public string Ma { get; set; }
        public string NgayNhapXuat { get; set; }
        public string NhaCungCap { get; set; }
        public string NguoiDaiDien { get; set; }
        public int NhanVienId { get; set; }
        public int? SoLuong { get; set; }
        public decimal? TongTien { get; set; }
        public string GhiChu { get; set; }
        public string LoaiPhieu { get; set; }
        public List<ExportThongTinThietBiDetailDto> ExportThongTinThietBiDetailDtos { get; set; }
    }

    public class ExportThongTinThietBiDetailDto
    {
        public string Ma { get; set; }
        public int ThietBiYTeId { get; set; }
        public string NgayNhap { get; set; }
        public string XuatXu { get; set; }
        public int? NamSX { get; set; }
        public string HangSanXuat { get; set; }
        public string? TinhTrang { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public decimal? GiaTien { get; set; }
        public int? ThoiGianBaoDuong { get; set; }
    }
}

