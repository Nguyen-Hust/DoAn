using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.PhieuNhapXuat
{
	public class ChiTietPhieuNhapXuatDto
	{
        public int Id { get; set; }
        public int? PhieuNhapXuatId { get; set; }
        public int? ChiTietThietBiId { get; set; }
        public decimal? GiaTien { get; set; }
    }
}

