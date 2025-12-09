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
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Email", "Templates", "Neuuropi.svg");
            
            // Fallback path check if running locally or different structure
            if (!File.Exists(logoPath))
            {
                 logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "Neuuropi.svg");
            }

            if (File.Exists(logoPath))
            {
                var image = builder.LinkedResources.Add(logoPath);
                image.ContentId = "neuropi-logo";
                image.ContentType.MediaType = "image";
                image.ContentType.MediaSubtype = "svg+xml";
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

            // Create multipart message
            var multipart = new Multipart("mixed");

            // Add HTML body
            multipart.Add(new TextPart(TextFormat.Html)
            {
                Text = htmlBody
            });

            // Add PDF attachment
            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(new System.IO.MemoryStream(attachmentData)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = attachmentName
            };

            multipart.Add(attachment);

            message.Body = multipart;

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
