using System;
namespace WebAPI.Models.ExportImport
{
	public class ExportPhieuBaoDuongDto
	{
        public string Ma { get; set; }
        public string NhanVien { get; set; }
        public int? TrangThai { get; set; }
        public string CreateTime { get; set; }
        public int? TongThietBi { get; set; }
    }
}

