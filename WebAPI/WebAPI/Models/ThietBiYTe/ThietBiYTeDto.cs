using System.ComponentModel.DataAnnotations;
using WebAPI.Entities;

namespace WebAPI.Models.ThietBiYTe
{
    public class ThietBiYTeDto
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
        public int SoLuong { get; set; }
    }
}
