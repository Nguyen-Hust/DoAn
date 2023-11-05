using WebAPI.Models.Mail;

namespace WebAPI.Service
{
    public interface ISendMailService
    {
        Task SendMail(MailContent mailContent);
        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task SendMailThongBaoBaoDuong();
    }
}
