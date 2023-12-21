using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.User
{
    public class UserInfoDto
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public int? KhoaId { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string Role { get; set; }
        public int? NhanVienId { get; set; }
    }
}
