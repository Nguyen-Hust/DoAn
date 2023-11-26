using System;
namespace WebAPI.Models.LichSuBanGIao
{
	public class LichSuBanGiaoDto
	{
        public int Id { get; set; }
        public DateTime NgayThucHien { get; set; }
        public int NhanVienId { get; set; }
        public int ChiTietThietBiId { get; set; }
    }
}

