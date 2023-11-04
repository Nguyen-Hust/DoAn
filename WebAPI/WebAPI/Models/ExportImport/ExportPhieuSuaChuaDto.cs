using System;
namespace WebAPI.Models.ExportImport
{
	public class ExportPhieuSuaChuaDto
	{
        public string Ma { get; set; }
        public string NhanVien { get; set; }
        public string ChiTietThietBi { get; set; }
        public int? TrangThai { get; set; }
        public string LyDo { get; set; }
        public string CreateTime { get; set; }
    }


    public class ExportPhieuSuaDto
    {
        public string Ma { get; set; }
        public string NhanVien { get; set; }
        public string ChiTietThietBi { get; set; }
        public int? TrangThai { get; set; }
        public string LyDo { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}

