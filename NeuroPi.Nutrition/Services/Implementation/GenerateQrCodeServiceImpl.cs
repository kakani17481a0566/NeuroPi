using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.QrCode;
using QRCoder;
using System.Drawing;
using System.Net;
using System.Net.Mail;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class GenerateQrCodeServiceImpl : GenerateQrCodeService
    {
        public readonly NeutritionDbContext context;
        public GenerateQrCodeServiceImpl(NeutritionDbContext _context)
        {
            context = _context;
            
        }
        public string GenerateQrCode(string gmail,string name,string studentName,string gender,Guid qrcode)
        {           
                    var inputData = qrcode.ToString();
                    using var qrGenerator = new QRCodeGenerator();
                    using var qrCodeData = qrGenerator.CreateQrCode(inputData, QRCodeGenerator.ECCLevel.Q);
                    using var qrCode = new QRCode(qrCodeData);
                    using Bitmap qrCodeAsBitmap = qrCode.GetGraphic(20);
                    using var memoryStream = new MemoryStream();
                    qrCodeAsBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    string base64String = Convert.ToBase64String(memoryStream.ToArray());
                    sendEmail(gmail,name,studentName, gender, base64String);
                    return "data:image/png;base64," + base64String;
        }

        //        public string sendEmail(string email,string name,string studentName,string gender,string base64)
        //        {
        //            var smtpClient = new SmtpClient("smtp.gmail.com")
        //            {
        //                Port = 587,
        //                Credentials = new NetworkCredential("kakanimohithkrishnasai@gmail.com", "vcewdxucyhcjhskp"),
        //                EnableSsl = true,
        //            };

        //            var mailMessage = new MailMessage
        //            {
        //                From = new MailAddress("kakanimohithkrishnasai@gmail.com"),
        //                Subject = "Carpedium Invitation from My School Italy and Neuropi Ai",
        //                Body = GetVisitorPassEmailBody(name, studentName, gender, base64),
        //                IsBodyHtml = true,
        //            };
        //            mailMessage.To.Add(email);

        //            smtpClient.Send(mailMessage);
        //            return "success";
        //        }
        public static string GetVisitorPassEmailBody(
        string name,
        string studentName,
        string gender)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
<meta charset='UTF-8'>
<title>Carpedium Day Invitation</title>
</head>

<body style='margin:0; padding:0; background:linear-gradient(135deg,#f8f9fb,#eef2f7); font-family:Arial, Helvetica, sans-serif;'>

<table width='100%' cellpadding='0' cellspacing='0'>
<tr>
<td align='center'>

<table cellpadding='0' cellspacing='0'
       style='
       width:9.5cm;
       height:12.5cm;
       background:#ffffff;
       border-radius:15px;
       overflow:hidden;
       border:1px solid #ddd;
       box-shadow:0 10px 25px rgba(0,0,0,0.15);
       '>

    <!-- Header -->
    <tr>
        <td style='background:linear-gradient(135deg,#8b0000,#c21807); padding:15px; text-align:center; color:#ffffff;'>
            <h1 style='margin:0; font-size:20px; letter-spacing:1px;'>CARPEDIUM CELEBRATION 2026</h1>
            <p style='margin:6px 0 0; font-size:13px;'>My School Italy & Neuropi</p>
        </td>
    </tr>

    <!-- Decorative Lights -->
    <tr>
        <td style='text-align:center; background:#fff5f5; padding:6px; font-size:14px; color:#c21807;'>
            ✨ 🎉 ✨ 🎊 ✨ 🎉 ✨
        </td>
    </tr>

    <!-- Body -->
    <tr>
        <td style='padding:15px; font-size:13px;'>

            <h2 style='margin:0 0 8px; font-size:16px; color:#8b0000; text-align:center;'>
                You Are Cordially Invited!
            </h2>

            <p style='margin:0 0 12px; color:#555; text-align:center;'>
                We are delighted to invite you to the <strong>Carpedium Day Celebration</strong><br>
                at <strong>My School Italy & Neuropi</strong>.
            </p>

            <table width='100%' cellpadding='6' cellspacing='0'
                   style='border:1px solid #eee; border-radius:6px;'>
                <tr>
                    <td><strong>Guest Name</strong></td>
                    <td>{name}</td>
                </tr>
                <tr>
                    <td><strong>Student Name</strong></td>
                    <td>{studentName}</td>
                </tr>
                <tr>
                    <td><strong>Event Date</strong></td>
                    <td>28 <sup> th </sup> Feburary  2026</td>
                </tr>
            </table>

            <!-- QR Code -->
            <div style='text-align:center; margin:15px 0;'>
                <p style='margin:6px 0; font-weight:bold; color:#8b0000;'>Scan for Entry</p>
                <img src='cid:qrCode'
                     width='100'
                     height='100'
                     alt='QR Code'
                     style='border:2px solid #eee; border-radius:8px;' />
            </div>

            <p style='text-align:center; color:#777; font-size:12px;'>
                Join us for a magical evening filled with performances, joy, and celebration!
            </p>

        </td>
    </tr>

    <!-- Celebration Image Section -->
  

    <!-- Footer with Logos -->
    <tr>
        <td style='background:#fafafa; padding:20px; text-align:center;'>

            <img src='https://hitex-hyderabad.myschoolitaly.com/wp-content/uploads/2025/03/The-Neuroscientific-European-Childcare-PDF_12-x-4-ft_Backside.png'
                 alt='School Logo'
                 height='40'
                 style='margin:0 10px;' />

            <img src='https://neuropi.ai/images/N.png'
                 alt='Partner Logo'
                 height='40'
                 style='margin:0 10px;' />

            <p style='font-size:12px; color:#999; margin-top:10px;'>
                © {DateTime.Now.Year} My School Italy & Neuropi
            </p>

        </td>
    </tr>

</table>

</td>
</tr>
</table>

</body>
</html>
"

       ;
        }


        public string sendEmail(
    string email,
    string name,
    string studentName,
    string gender,
    string base64)
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
                Subject = "Carpe Diem Invitation from My School Italy and Neuropi Ai",
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            string body = GetVisitorPassEmailBody(
                name,
                studentName,
                gender
            );

            AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(
                    body,
                    null,
                    "text/html"
                );

            byte[] qrBytes = Convert.FromBase64String(base64);

            LinkedResource qrImage = new LinkedResource(
                new MemoryStream(qrBytes),
                "image/png"
            );

            qrImage.ContentId = "qrCode";
            qrImage.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            htmlView.LinkedResources.Add(qrImage);
            mailMessage.AlternateViews.Add(htmlView);
            smtpClient.Send(mailMessage);

            return "success";
        }

        public QrCodeValidationResponseVM ValidateQrCode(Guid code)
        {
            var record = context.Carpedium.FirstOrDefault(c => c.QrCode == code);

            if (record == null)
            {
                return new QrCodeValidationResponseVM { Status = "NotFound", Message = "QR Code not found" };
            }

            if (record.IsDeleted)
            {
                return new QrCodeValidationResponseVM
                {
                    Status = "AlreadyUsed",
                    Message = "QR Code already used",
                    StudentName = record.StudentName,
                    VisitorName = record.Name,
                    Gender = record.Gender,
                    ValidationTime = DateTime.Now
                };
            }

            // Valid case
            record.IsDeleted = true;
            context.SaveChanges();

            return new QrCodeValidationResponseVM
            {
                Status = "Valid",
                Message = "Access Granted",
                StudentName = record.StudentName,
                VisitorName = record.Name,
                Gender = record.Gender,
                ValidationTime = DateTime.Now
            };
        }

        public string AddCarpidiumDetails(QrCodeRequestVM qrCode)
        {
            var response = context.Carpedium.Where(c => !c.IsDeleted && c.StudentName.Trim().ToLower() == qrCode.StudentName.Trim().ToLower()).ToList();
            if (response.Count() != 2)
            {
                var carpediumModel = QrCodeRequestVM.ToModel(qrCode);
                string qrcode = GenerateQrCode(carpediumModel.gmail, carpediumModel.Name, carpediumModel.StudentName, carpediumModel.Gender, carpediumModel.QrCode);
                context.Add(carpediumModel);
                context.SaveChanges();
                return qrcode;
            }
            return "The Student had already reached maximum passes if needed kindly contact My School Italy";
        }
    }
}
