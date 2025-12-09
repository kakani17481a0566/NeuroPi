using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace NeuropiCommonLib.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                _config["EmailSettings:FromName"],
                _config["EmailSettings:FromEmail"]
            ));

            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };

            // Embed NeuroPi Logo
            // Note: Adjust path as necessary for your deployment
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Email", "Templates", "Neurologi.png");
            
            // Fallback path check if running locally or different structure
            if (!File.Exists(logoPath))
            {
                 logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "Neurologi.png");
            }

            if (File.Exists(logoPath))
            {
                var image = builder.LinkedResources.Add(logoPath);
                image.ContentId = "neuropi-logo";
                image.ContentType.MediaType = "image";
                image.ContentType.MediaSubtype = "png";
            }

            message.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _config["EmailSettings:SmtpHost"],
                int.Parse(_config["EmailSettings:SmtpPort"]),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                _config["EmailSettings:SmtpUser"],
                _config["EmailSettings:SmtpPassword"]
            );

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendEmailWithAttachmentAsync(string toEmail, string subject, string htmlBody, byte[] attachmentData, string attachmentName)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                _config["EmailSettings:FromName"],
                _config["EmailSettings:FromEmail"]
            ));

            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };

            // 1. Embed NeuroPi Logo
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Email", "Templates", "Neurologi.png");
            
            // Fallback path check
            if (!File.Exists(logoPath))
            {
                 logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "Neurologi.png");
            }

            if (File.Exists(logoPath))
            {
                var image = builder.LinkedResources.Add(logoPath);
                image.ContentId = "neuropi-logo";
                image.ContentType.MediaType = "image";
                image.ContentType.MediaSubtype = "png";
            }

            // 2. Add PDF Attachment
            builder.Attachments.Add(attachmentName, attachmentData, ContentType.Parse("application/pdf"));

            message.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _config["EmailSettings:SmtpHost"],
                int.Parse(_config["EmailSettings:SmtpPort"]),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                _config["EmailSettings:SmtpUser"],
                _config["EmailSettings:SmtpPassword"]
            );

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
