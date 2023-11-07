using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DashBoard
{
	public class DashBoardChartDto
	{
		public string Category { get; set; }
		public int Value { get; set; }
	}

	public class ThietBiDto
	{
        public int Id { get; set; }
        [MaxLength(255)]
        public string Ma { get; set; }
        [MaxLength(255)]
        public string Ten { get; set; }
        [MaxLength(255)]
        public string MDRR { get; set; }
        [MaxLength(255)]
        public string LoaiTTBYT { get; set; }
        public string TenLoaiTTBYT { get; set; }
    }
}

