using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using WebAPI.Data;
using WebAPI.Models.Mail;

namespace WebAPI.Service
{
    public class SendMailService : ISendMailService
    {
        private readonly MailSettings mailSettings;
        private readonly ApplicationDbContext _context;
        public SendMailService(IOptions<MailSettings> _mailSettings, ApplicationDbContext context)
        {
            mailSettings = _mailSettings.Value;
            _context = context;
        }

        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            smtp.Disconnect(true);

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendMail(new MailContent()
            {
                To = email,
                Subject = subject,
                Body = htmlMessage
            });
        }

        public async Task SendMailThongBaoBaoDuong()
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
                        Subject = "THÔNG BÁO BẢO DƯỠNG",
                        Body = @$"<p>Dear <strong>{user.Ten}</strong></p>
                                <p>Danh sách mã thiết bị cần bảo dưỡng: <strong>{dsMaStr}</strong></p>
                                <p>Happy closing !!!</p>>"
                    };

                    await SendMail(content);
                }
            }
        }
    }
}