using System.Threading.Tasks;

namespace NeuropiCommonLib.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlBody);
        Task SendEmailWithAttachmentAsync(string toEmail, string subject, string htmlBody, byte[] attachmentData, string attachmentName);
    }
}
