using Microsoft.AspNetCore.Components.Forms;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.QrCode;
using QRCoder;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using static QRCoder.PayloadGenerator;
using static System.Net.Mime.MediaTypeNames;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class GenerateQrCodeServiceImpl : GenerateQrCodeService
    {
        public readonly NeutritionDbContext context;
        public GenerateQrCodeServiceImpl(NeutritionDbContext _context)
        {
            context = _context;
            
        }
        public string GenerateQrCode()
        {
            var result = context.Carpedium.Where(c => !c.IsDeleted).ToList();
            if (result != null)
            {
                foreach (var c in result)
                {
                    var inputData = c.QrCode.ToString();
                    using var qrGenerator = new QRCodeGenerator();
                    using var qrCodeData = qrGenerator.CreateQrCode(inputData, QRCodeGenerator.ECCLevel.Q);
                    using var qrCode = new QRCode(qrCodeData);
                    using Bitmap qrCodeAsBitmap = qrCode.GetGraphic(20);
                    using var memoryStream = new MemoryStream();
                    qrCodeAsBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    string base64String = Convert.ToBase64String(memoryStream.ToArray());
                    sendEmail("sai.gone@neuropi.ai", c.Name,c.StudentName,c.Gender,base64String);
                    return "data:image/png;base64," + base64String;
                }
            }
            return "No data found";
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
    <title>Visitor Pass</title>
</head>

<body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

<table width='100%' cellpadding='0' cellspacing='0'>
<tr>
<td align='center'>

<!-- PASS CONTAINER -->
<table cellpadding='0' cellspacing='0'
       style='
       width:9.5cm;
       height:12.5cm;
       background:#ffffff;
       border-radius:10px;
       overflow:hidden;
       border:1px solid #ddd;
       '>

    <!-- Header -->
    <tr>
        <td style='background:#8b0000; padding:10px; text-align:center; color:#ffffff;'>
            <h1 style='margin:0; font-size:18px;'>VISITOR PASS</h1>
            <p style='margin:4px 0 0; font-size:12px;'>My School Italy & Neuropi</p>
        </td>
    </tr>

    <!-- Body -->
    <tr>
        <td style='padding:12px; font-size:12px;'>

            <h2 style='margin:0 0 6px; font-size:14px; color:#333;'>Carpe Diem</h2>

            <p style='margin:0 0 10px; color:#555;'>
                Welcome to <strong>My School Italy & Neuropi</strong>
            </p>

            <table width='100%' cellpadding='4' cellspacing='0'
                   style='border:1px solid #ddd;'>
                <tr>
                    <td><strong>Name</strong></td>
                    <td>{name}</td>
                </tr>
                <tr>
                    <td><strong>Gender</strong></td>
                    <td>{gender}</td>
                </tr>
                <tr>
                    <td><strong>Student</strong></td>
                    <td>{studentName}</td>
                </tr>
            </table>

            <!-- QR Code -->
            <div style='text-align:center; margin:10px 0;'>
                <p style='margin:4px 0; font-weight:bold;'>Scan QR</p>
                <img src='cid:qrCode'
                     width='90'
                     height='90'
                     alt='QR Code'
                     style='border:1px solid #ccc;' />
            </div>

        </td>
    </tr>

    <!-- Celebration Image Section -->
    <tr>
        <td style='padding:0;'>
            <img src='https://images.unsplash.com/photo-1546410531-bb4caa6b424d'
                 alt='School Celebration'
                 style='width:100%; height:auto; display:block;' />
        </td>
    </tr>

                        <!-- Footer with Logos -->
                        <tr>
                            <td style='background:#f0f0f0; padding:20px; text-align:center;'>

                                <img src='https://hitex-hyderabad.myschoolitaly.com/wp-content/uploads/2025/03/The-Neuroscientific-European-Childcare-PDF_12-x-4-ft_Backside.png'
                                     alt='School Logo'
                                     height='40'
                                     style='margin:0 10px;' />

                                <img src='https://neuropi.ai/images/N.png'
                                     alt='Partner Logo'
                                     height='40'
                                     style='margin:0 10px;' />

                                <p style='font-size:12px; color:#777; margin-top:10px;'>
                                    © {DateTime.Now.Year} Italy & Neuropiai School
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

        public string ValidateQrCode(Guid code)
        {
            var result= context.Carpedium.Where(c=>!c.IsDeleted).FirstOrDefault(c=>c.QrCode==code);
            if (result != null)
            {
                result.IsDeleted = true;
                context.SaveChanges();
                return "validated";

            }
            return "already validated";
        }

        public string AddCarpidiumDetails(QrCodeRequestVM qrCode)
        {
            var carpediumModel=QrCodeRequestVM.ToModel(qrCode);
            context.Add(carpediumModel);
            context.SaveChanges();
            return "saved";
        }
    }
}
