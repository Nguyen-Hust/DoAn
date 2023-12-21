using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data;
using WebAPI.Models.Authentication;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;
using WebAPI.Models.User;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        [Route("abcd")]
        public string Get2()
        {
            return "TEst";
        }

        [HttpGet]
        [Route("abc")]
        public async Task<string> Get()
        {
            try
            {
                var thietBiYTe = await _context.ThietBiYTe.ToListAsync();
                if (thietBiYTe.Count > 0)
                {
                    var totalCount = thietBiYTe.Count;
                    var items = thietBiYTe.Select(_ => new ThietBiYTeDto
                    {
                        Id = _.Id,
                        Ten = _.Ten,
                        Ma = _.Ma,
                        LoaiTTBYT = _.LoaiTTBYT,
                        MDRR = _.MDRR,
                        SoLuong = _.SoLuong,
                    }).ToList();
                    return "abc";
                }
                return "abc";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("get-user-info")]
        public async Task<UserInfoDto> GetUserInfo()
        {

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var nhanVien = _context.NhanSu.FirstOrDefault(_ => _.AccountId == userId);
            return new UserInfoDto
            {
                Email = nhanVien?.Email,
                KhoaId = nhanVien?.KhoaId,
                Ma = nhanVien?.Ma,
                SDT = nhanVien?.SDT,
                Ten = nhanVien?.Ten,
                Role = nhanVien == null ? "admin" : (nhanVien.LaQuanLyThietBi == true ? "manager" : "user"),
                NhanVienId = nhanVien?.Id
            };
        }

        [HttpPost]
        [Route("register")]
        public async Task<CommonResultDto<RegisterModel>> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new CommonResultDto<RegisterModel>( "User already exists!" );
            var nhanSu = await _context.NhanSu.FindAsync(model.NhanVienId);
            if(nhanSu == null)
            {
                return new CommonResultDto<RegisterModel>("Not found" );
            }
            if (!string.IsNullOrEmpty(nhanSu.AccountId))
            {
                return new CommonResultDto<RegisterModel>("Nhân sự đã có tài khoản" );
            }
            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new CommonResultDto<RegisterModel>("User creation failed! Please check user details and try again." );
            nhanSu.AccountId = user.Id.ToString();
            _context.NhanSu.Update(nhanSu);
            await _context.SaveChangesAsync();
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            return new CommonResultDto<RegisterModel>(model);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<CommonResultDto<RegisterModel>> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new CommonResultDto<RegisterModel>("User already exists!");
            var nhanSu = await _context.NhanSu.FindAsync(model.NhanVienId);
            if (nhanSu == null)
            {
                return new CommonResultDto<RegisterModel>("Not found");
            }
            if (!string.IsNullOrEmpty(nhanSu.AccountId))
            {
                return new CommonResultDto<RegisterModel>("Nhân sự đã có tài khoản");
            }
            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new CommonResultDto<RegisterModel>("User creation failed! Please check user details and try again.");
            nhanSu.AccountId = user.Id.ToString();
            _context.NhanSu.Update(nhanSu);
            await _context.SaveChangesAsync();
            await _userManager.AddToRoleAsync(user, UserRoles.Manager);
            return new CommonResultDto<RegisterModel>(model);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
