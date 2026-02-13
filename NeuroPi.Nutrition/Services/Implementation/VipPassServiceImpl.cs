using NeuroPi.Nutrition.Data;
using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.VipPass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using NeuropiCommonLib.PDF;


namespace NeuroPi.Nutrition.Services.Implementation
{
    public class VipPassServiceImpl : IVipPassService
    {
        private readonly NeutritionDbContext context;

        public VipPassServiceImpl(NeutritionDbContext _context)
        {
            context = _context;
        }

        public List<MVipCarpidum> GenerateBulkPasses(VipBulkPassRequestVM request)
        {
            var passes = new List<MVipCarpidum>();

            for (int i = 0; i < request.PassCount; i++)
            {
                var pass = new MVipCarpidum
                {
                    VipName = request.VipName,
                    VipEmail = request.VipEmail,
                    VipPhone = request.VipPhone,
                    QrCode = Guid.NewGuid(),
                    PassCount = request.PassCount,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };

                context.VipCarpidum.Add(pass);
                passes.Add(pass);
            }

            context.SaveChanges();
            return passes;
        }

        public async Task<bool> SendPassesViaEmail(string vipEmail)
        {
            var passes = context.VipCarpidum
                .Where(v => v.VipEmail == vipEmail && !v.IsDeleted)
                .ToList();

            if (!passes.Any()) return false;

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(
                        "kakanimohithkrishnasai@gmail.com",
                        "vcewdxucyhcjhskp"
                    ),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("kakanimohithkrishnasai@gmail.com"),
                    Subject = "VIP Carpe Diem Invitation from My School Italy and Neuropi Ai",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(vipEmail);

                string vipName = passes.First().VipName;
                string body = GetVipPassEmailBody(vipName, passes);

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                foreach (var pass in passes)
                {
                    string qrCodeStr = pass.QrCode.ToString();
                    using var qrGenerator = new QRCodeGenerator();
                    using var qrCodeData = qrGenerator.CreateQrCode(qrCodeStr, QRCodeGenerator.ECCLevel.Q);
                    using var qrCode = new QRCode(qrCodeData);
                    using Bitmap qrCodeAsBitmap = qrCode.GetGraphic(20);
                    
                    using var memoryStream = new MemoryStream();
                    qrCodeAsBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] qrBytes = memoryStream.ToArray();

                    // Generate PDF for this single pass
                    // We pass a list containing just this one pass to reuse the existing generator
                    var singlePassList = new List<(int PassId, byte[] QrCodeBytes)> { (pass.Id, qrBytes) };
                    byte[] pdfBytes = VipPassPdfGenerator.GenerateVipPassesPdf(vipName, singlePassList);

                    // Attach with specific name: {VipName}_{PassId}.pdf
                    string cleanVipName = vipName.Replace(" ", "_");
                    mailMessage.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), $"{cleanVipName}_{pass.Id}.pdf", "application/pdf"));
                }

                mailMessage.AlternateViews.Add(htmlView);
                await smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email Error: {ex.Message}");
                return false;
            }
        }

        private string GetVipPassEmailBody(string name, List<MVipCarpidum> passes)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>VIP Invitation</title>
</head>
<body style='margin:0; padding:0; background-color:#f8f9fb; font-family:Arial, sans-serif;'>
    <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f8f9fb; padding:20px;'>
        <tr>
            <td align='center'>
                <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff; border-radius:15px; overflow:hidden; border:1px solid #ddd; box-shadow:0 10px 25px rgba(0,0,0,0.1);'>
                    <tr>
                        <td style='background:linear-gradient(135deg,#8b0000,#c21807); padding:30px; text-align:center; color:#ffffff;'>
                            <h1 style='margin:0; font-size:24px; letter-spacing:2px;'>VIP INVITATION</h1>
                            <p style='margin:10px 0 0; font-size:14px; opacity:0.9;'>Carpe Diem Celebration 2026</p>
                        </td>
                    </tr>
                    <tr>
                        <td style='padding:40px; color:#333; line-height:1.6;'>
                            <h2 style='color:#8b0000; margin-top:0;'>Honored Guest Invitation</h2>
                            <p>Dear <strong>{name}</strong>,</p>
                            <p>We are delighted to extend a special invitation to you as an <strong>Honored VIP Guest</strong> at the Carpe Diem Day Celebration by <strong>My School Italy & Neuropi Ai</strong>.</p>
                            <p>Please find your personalized VIP access passes attached to this email as a PDF document. You can print the document or present it on your mobile device at the VIP entrance for seamless entry.</p>

                            <div style='margin-top:30px; padding:20px; background:#fff5f5; border-radius:10px; text-align:center;'>
                                <p style='margin:0; font-weight:bold; color:#8b0000;'>Event Details</p>
                                <p style='margin:5px 0 0;'>Date: 28th February 2026</p>
                                <p style='margin:5px 0 0;'>Venue: <strong>Ashray Conventions</strong></p>
                                <p style='margin:2px 0 0; font-size:12px; color:#555;'>Near Hitech city Metro, Hyderabad</p>
                                <p style='margin:8px 0 0;'><a href='https://maps.app.goo.gl/twAGzoyvyfzfCdCj7' style='color:#8b0000; text-decoration:none; font-weight:bold; font-size:13px;'>📍 Open in Google Maps</a></p>
                            </div>

                            <p style='margin-top:30px;'>We look forward to welcoming you to this magical evening!</p>
                        </td>
                    </tr>
                    <tr>
                        <td style='background:#fafafa; padding:30px; text-align:center; border-top:1px solid #eee;'>
                            <p style='font-size:12px; color:#999; margin:0;'>
                                © {DateTime.Now.Year} My School Italy & Neuropi Ai<br>
                                This is an automated VIP invitation.
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }

        public List<MVipCarpidum> GetVipPasses()
        {
            return context.VipCarpidum
                .OrderByDescending(v => v.Id)
                .ToList();
        }

        public List<MVipCarpidum> GetPassesByEmail(string email)
        {
            return context.VipCarpidum
                .Where(v => v.VipEmail == email)
                .OrderByDescending(v => v.Id)
                .ToList();
        }

        public VipValidationResponseVM ValidateVipPass(Guid code)
        {
            string codeStr = code.ToString();

            var pass = context.VipCarpidum.IgnoreQueryFilters()
                .FirstOrDefault(v => v.QrCode.ToString() == codeStr);

            if (pass == null)
            {
                return new VipValidationResponseVM
                {
                    Status = "NotFound",
                    Message = "VIP Pass not found"
                };
            }

            // Check if already used/deleted
            if (pass.IsDeleted)
            {
                return new VipValidationResponseVM
                {
                    Status = "AlreadyUsed",
                    Message = "VIP Pass Already Used",
                    VipName = pass.VipName,
                    VipEmail = pass.VipEmail
                };
            }

            // Mark as used (Soft Delete)
            pass.IsDeleted = true;
            pass.UpdatedOn = DateTime.UtcNow;

            context.VipCarpidum.Update(pass);
            context.SaveChanges();

            return new VipValidationResponseVM
            {
                Status = "Valid",
                VipName = pass.VipName,
                VipEmail = pass.VipEmail,
                ValidationTime = DateTime.UtcNow,
                Message = "Access Granted"
            };
        }

        public bool DeleteVipPass(int id)
        {
            var pass = context.VipCarpidum.Find(id);
            if (pass == null) return false;

            pass.IsDeleted = true;
            pass.UpdatedOn = DateTime.UtcNow;
            context.SaveChanges();

            return true;
        }
    }
}
