using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Models.NhanVien;
using WebAPI.Models.Notification;
using WebAPI.Models.Shared;
using WebAPI.Models.ThietBiYTe;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("get-count")]
        public async Task<CommonResultDto<int>> GetCountNotificationUnRead()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = _context.ThongBaoNguoiDung
                .Include(x => x.ThongBao)
                .Where(m => m.UserId == userId)
                .Where(m => !m.TimeRead.HasValue)
                .Count();
            return new CommonResultDto<int>(result);
        }

        [HttpPost]
        [Route("get-list")]
        public async Task<CommonResultDto<PagedResultDto<NotificationDto>>> GetListNotificationMobileAsync(SearchListDto input)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var result = _context.ThongBaoNguoiDung
                    .Include(x => x.ThongBao)
                    .Where(m => m.UserId == userId)
                    .Select(n => new NotificationDto
                    {
                        Subject = n.ThongBao.Subject,
                        Message = n.ThongBao.Message,
                        SendTime = n.ThongBao.SendTime,
                        IsRead = n.TimeRead.HasValue ? true : false
                    }).OrderByDescending(_ => _.SendTime).ToList();
                var totalCount = result.Count;
                var items = result
                    .Skip(input.SkipCount ?? 0)
                    .Take(input.MaxResultCount ?? 1000).ToList();
                return new CommonResultDto<PagedResultDto<NotificationDto>> (new PagedResultDto<NotificationDto>
                {
                    Items = items,
                    TotalCount = totalCount
                });
            }
            catch (Exception ex)
            {
                return new CommonResultDto<PagedResultDto<NotificationDto>>(ex.Message);
            }
        }

        [HttpPost]
        [Route("read-all")]
        public async Task<CommonResultDto<bool>> ReadAll()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var notifications = _context.ThongBaoNguoiDung.Where(_ => _.UserId == userId).ToList();
            foreach (var notification in notifications) 
            {
                notification.Status = 1;
                notification.TimeRead = DateTime.Now;
            }
            _context.UpdateRange(notifications);
            await _context.SaveChangesAsync();
            return new CommonResultDto<bool>(true);
        }
    }
}
