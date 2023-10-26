using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.LoaiThietBi
{
    public class LoaiThietBiDto
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Ma { get; set; }
        [MaxLength(255)]
        public string Ten { get; set; }
    }
}
