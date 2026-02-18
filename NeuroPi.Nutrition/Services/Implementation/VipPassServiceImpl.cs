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
                .Where(v => v.VipEmail == vipEmail && !v.IsDeleted && !v.EmailSent)
                .ToList();

            if (!passes.Any()) 
            {
                Console.WriteLine($"SendPassesViaEmail: No passes found for email {vipEmail}");
                return false;
            }

            Console.WriteLine($"SendPassesViaEmail: Found {passes.Count} passes for {vipEmail}. Preparing email batches...");

            try
            {
                using var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(
                        "kakanimohithkrishnasai@gmail.com",
                        "vcewdxucyhcjhskp"
                    ),
                    EnableSsl = true,
                };

                // Split passes into chunks of 10
                var passBatches = passes.Chunk(10).ToList();
                int totalBatches = passBatches.Count;
                int currentBatch = 1;

                foreach (var batch in passBatches)
                {
                    Console.WriteLine($"SendPassesViaEmail: Processing batch {currentBatch} of {totalBatches}...");

                    using var mailMessage = new MailMessage
                    {
                        From = new MailAddress("kakanimohithkrishnasai@gmail.com"),
                        Subject = totalBatches > 1 
                            ? $"VIP Carpe Diem Invitation from My School Italy and Neuropi Ai (Part {currentBatch}/{totalBatches})"
                            : "VIP Carpe Diem Invitation from My School Italy and Neuropi Ai",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(vipEmail);

                    string vipName = batch.First().VipName;
                    string body = GetVipPassEmailBody(vipName, batch.ToList());

                    mailMessage.Body = body;
                    // Add AlternateView for HTML body
                    var htmlView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    mailMessage.AlternateViews.Add(htmlView);

                    foreach (var pass in batch)
                    {
                        Console.WriteLine($"SendPassesViaEmail: Generating PDF for pass {pass.Id}...");
                        string qrCodeStr = pass.QrCode.ToString();
                        using var qrGenerator = new QRCodeGenerator();
                        using var qrCodeData = qrGenerator.CreateQrCode(qrCodeStr, QRCodeGenerator.ECCLevel.Q);
                        using var qrCode = new QRCode(qrCodeData);
                        using Bitmap qrCodeAsBitmap = qrCode.GetGraphic(5);
                        
                        using var memoryStream = new MemoryStream();
                        qrCodeAsBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] qrBytes = memoryStream.ToArray();

                        // Generate PDF for this single pass
                        var singlePassList = new List<(int PassId, byte[] QrCodeBytes)> { (pass.Id, qrBytes) };
                        byte[] pdfBytes = VipPassPdfGenerator.GenerateVipPassesPdf(vipName, singlePassList);

                        // Create stream for attachment - will be disposed by MailMessage
                        var pdfStream = new MemoryStream(pdfBytes); 
                        
                        // Attach with specific name: {VipName}_{PassId}.pdf
                        // Sanitize filename to remove invalid chars
                        string cleanVipName = string.Join("_", vipName.Split(Path.GetInvalidFileNameChars()));
                        cleanVipName = cleanVipName.Replace(" ", "_");
                        
                        mailMessage.Attachments.Add(new Attachment(pdfStream, $"{cleanVipName}_{pass.Id}.pdf", MediaTypeNames.Application.Pdf));
                    }

                    Console.WriteLine($"SendPassesViaEmail: Sending batch {currentBatch} email via SMTP...");
                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine($"SendPassesViaEmail: Batch {currentBatch} sent successfully.");

                    // Update EmailSent flag for this batch
                    foreach (var pass in batch)
                    {
                        pass.EmailSent = true;
                        pass.UpdatedOn = DateTime.UtcNow;
                    }
                    context.SaveChanges();

                    currentBatch++;
                }

                return true;
            }
            catch (Exception ex)
            {
                // Better logging if possible, but keeping console for now
                Console.WriteLine($"SendPassesViaEmail Error: {ex.ToString()}");
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
    <title>VIP Invitation - Carpe Diem 2026</title>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Playfair+Display:wght@400;700&family=Lato:wght@300;400;700&display=swap');
    </style>
</head>
<body style='margin:0; padding:0; background-color:#f0f2f5; font-family:""Lato"", Arial, sans-serif; -webkit-font-smoothing: antialiased;'>
    
    <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f0f2f5; padding:40px 10px;'>
        <tr>
            <td align='center'>
                
                <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff; max-width:600px; border-radius:4px; overflow:hidden; box-shadow:0 15px 35px rgba(0,32,74,0.15); border-top: 5px solid #d4af37;'>
                    
                    <tr>
                        <td style='background-color:#00204a; padding:40px 30px; text-align:center; background-image: linear-gradient(180deg, #00204a 0%, #00152e 100%);'>
                            <p style='color:#d4af37; font-size:11px; text-transform:uppercase; letter-spacing:3px; margin:0 0 10px 0; font-weight:700;'>My School Italy & Neuropi AI</p>
                            <h1 style='margin:0; font-family:""Playfair Display"", ""Times New Roman"", serif; font-size:32px; letter-spacing:1px; color:#ffffff; font-weight:400;'>VIP INVITATION</h1>
                            <p style='margin:10px 0 0; font-size:14px; color:#aab8c2; letter-spacing:1px;'>Carpe Diem Celebration 2026</p>
                        </td>
                    </tr>

                    <tr>
                        <td style='padding:50px 40px; color:#444444; line-height:1.8;'>
                            
                            <h2 style='font-family:""Playfair Display"", ""Times New Roman"", serif; color:#00204a; margin-top:0; font-size:24px; text-align:center;'>Honored Guest</h2>
                            
                            <p style='text-align:center; font-size:16px; margin-bottom:30px;'>Dear <strong>{name}</strong>,</p>
                            
                            <p style='text-align:center; margin-bottom:30px; color:#555;'>
                                We are pleased to extend this exclusive invitation to you as a distinguished guest. Join us for an evening of excellence and innovation at the annual celebration hosted by <strong>My School Italy</strong> and <strong>Neuropi Ai</strong>.
                            </p>

                            <div style='background-color:#f8f9fa; border:1px solid #e1e4e8; border-left:4px solid #d4af37; padding:25px; margin:20px 0;'>
                                <table width='100%' cellpadding='0' cellspacing='0'>
                                    <tr>
                                        <td width='50%' valign='top' style='padding-right:15px; border-right:1px solid #e1e4e8;'>
                                            <p style='margin:0; font-size:10px; text-transform:uppercase; color:#888; letter-spacing:1px;'>Date</p>
                                            <p style='margin:5px 0 0; font-weight:bold; color:#00204a; font-size:16px;'>28 February 2026</p>
                                        </td>
                                        <td width='50%' valign='top' style='padding-left:15px;'>
                                            <p style='margin:0; font-size:10px; text-transform:uppercase; color:#888; letter-spacing:1px;'>Venue</p>
                                            <p style='margin:5px 0 0; font-weight:bold; color:#00204a; font-size:16px;'>Ashray Conventions</p>
                                            <p style='margin:2px 0 0; font-size:12px; color:#666;'>Near Hitech City Metro, Hyderabad</p>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <p style='font-size:14px; color:#666; text-align:center; margin-top:30px;'>
                                <em>Your personalized VIP access pass is attached as a PDF. Please present this at the priority entrance.</em>
                            </p>

                            <div style='text-align:center; margin-top:35px;'>
                                <a href='https://maps.app.goo.gl/twAGzoyvyfzfCdCj7' style='background-color:#00204a; color:#d4af37; text-decoration:none; padding:12px 30px; font-weight:bold; font-size:13px; letter-spacing:1px; border-radius:2px; display:inline-block;'>VIEW LOCATION MAP</a>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td style='background-color:#f4f4f4; padding:25px; text-align:center; border-top:1px solid #eaeaea;'>
                            <p style='font-size:11px; color:#888; margin:0; line-height:1.5;'>
                                © {DateTime.Now.Year} My School Italy & Neuropi Ai.<br>
                                Excellence in Education & Innovation.
                            </p>
                        </td>
                    </tr>
                </table>
                
                <table height='40' width='100%'><tr><td></td></tr></table>
            </td>
        </tr>
    </table>
</body>
</html>";
;
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
