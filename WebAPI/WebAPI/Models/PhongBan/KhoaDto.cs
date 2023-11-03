using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Entities;
namespace WebAPI.Models.PhongBan
{
	public class KhoaDto 
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ma { get; set; }
        [Required]
        [MaxLength(255)]
        public string Ten { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string SDT { get; set; }
    }
}

