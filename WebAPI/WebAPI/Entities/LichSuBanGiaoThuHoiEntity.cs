using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("lich_su_ban_giao_thu_hoi")]
	public class LichSuBanGiaoThuHoiEntity
	{
        [Key]
        public int Id { get; set; }
        public DateTime NgayThucHien { get; set; }
        public int NhanVienId { get; set; }
        public int ChiTietThietBiId { get; set; }
        public ThongTinChiTietThietBiEntity ChiTietThietBi { get; set; }
    }
}

