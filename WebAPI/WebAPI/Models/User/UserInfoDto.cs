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
    }
}
