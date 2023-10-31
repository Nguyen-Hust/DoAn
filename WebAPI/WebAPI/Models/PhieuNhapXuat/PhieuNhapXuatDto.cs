using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.ThongTinChiTietThietBi;

namespace WebAPI.Models.PhieuNhapXuat
{
	public class PhieuNhapXuatDto
	{
        public int Id { get; set; }
        public string Ma { get; set; }
        public DateTime NgayNhapXuat { get; set; }
        public string NhaCungCap { get; set; }
        public string NguoiDaiDien { get; set; }
        public int NhanVienId { get; set; }
        public int? SoLuong { get; set; }
        public decimal? TongTien { get; set; }
        [MaxLength(255)]
        public string GhiChu { get; set; }
        public int LoaiPhieu { get; set; }
        public List<ThongTinChiTietThietBiDto> ThongTinChiTietThietBiDtos { get; set; }
    }
}

