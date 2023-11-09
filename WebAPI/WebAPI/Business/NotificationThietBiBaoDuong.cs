using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using WebAPI.Data;
using WebAPI.Models.Mail;
using WebAPI.Service;

namespace WebAPI.Business
{
    public class NotificationThietBiBaoDuong
    {
        private readonly ApplicationDbContext _context;
        private readonly ISendMailService _sendMailService;
        public NotificationThietBiBaoDuong(ApplicationDbContext context, ISendMailService sendMailService)
        {
            _context = context;
            _sendMailService = sendMailService;
        }
        public async Task SendMail()
        {
            var users = _context.NhanSu.Where(_ => !string.IsNullOrEmpty(_.AccountId) && _.LaQuanLyThietBi == true).ToList();
            var thietBiYTe = await _context.ThongTinChiTietThietBi.ToListAsync();
            var items = thietBiYTe
                .Where(_ => _.NgayNhap.AddMonths(_.ThoiGianBaoDuong ?? 0) < DateTime.Now).Select(s => s.Ma).ToList();
            var dsMaStr = string.Join(", ", items);
            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    MailContent content = new MailContent
                    {
                        To = user.Email,
                        Subject = "Sửa chữa thiết bị",
                        Body = @$"<p>Dear <strong>{user.Ten}</strong></p>
                                <p>Danh sách mã thiết bị cần bảo dưỡng: <strong>{dsMaStr}</strong></p>
                                <p>Happy closing !!!</p>>"
                    };

                    await _sendMailService.SendMail(content);
                }
            }
        }
    }
}
