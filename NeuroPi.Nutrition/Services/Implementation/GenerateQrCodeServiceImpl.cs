using NeuroPi.Nutrition.Data;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using NeuroPi.Nutrition.ViewModel.QrCode;
using QRCoder;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class GenerateQrCodeServiceImpl : GenerateQrCodeService
    {
        public readonly NeutritionDbContext context;
        public GenerateQrCodeServiceImpl(NeutritionDbContext _context)
        {
            context = _context;
        }

        public string GenerateQrCode(string gmail, string name, string studentName, string gender, string qrcode)
        {
            var inputData = qrcode;
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(inputData, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrCodeData);
            using Bitmap qrCodeAsBitmap = qrCode.GetGraphic(20);
            using var memoryStream = new MemoryStream();
            qrCodeAsBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            string base64String = Convert.ToBase64String(memoryStream.ToArray());
            sendEmail(gmail, name, studentName, gender, base64String);
            return "data:image/png;base64," + base64String;
        }

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
                <tr>
                    <td><strong>Venue</strong></td>
                    <td>
                        <strong>Ashray Conventions</strong><br/>
                        Near Hitech city Metro, Hyderabad<br/>
                        <a href='https://maps.app.goo.gl/twAGzoyvyfzfCdCj7' style='color:#8b0000; font-size:11px;'>View on Google Maps</a>
                    </td>
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
                    "info@neuropi.ai",
                    "wblollhzrtfyzmlm"
                ),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("info@neuropi.ai"),
                Subject = "Carpe Diem Invitation from My School Italy and Neuropi Ai",
                IsBodyHtml = true
            };

            if (!string.IsNullOrWhiteSpace(email))
            {
                mailMessage.To.Add(email);
            }
            else
            {
                // handle missing email if necessary
                return "email missing";
            }

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
            string codeStr = code.ToString();

            // 1. Try to find in Carpidum (regular student passes) first
            var query = from c in context.Carpidum.IgnoreQueryFilters()
                        join s in context.Students.AsNoTracking() on c.StudentId equals s.Id into students
                        from subStudent in students.DefaultIfEmpty()

                        join co in context.Courses.AsNoTracking() on subStudent.CourseId equals co.Id into courses
                        from subCourse in courses.DefaultIfEmpty()

                        join b in context.Branches.AsNoTracking() on subStudent.BranchId equals b.Id into branches
                        from subBranch in branches.DefaultIfEmpty()

                        where c.QrCode == codeStr
                        select new { c, subStudent, subCourse, subBranch };

            var result = query.FirstOrDefault();

            if (result != null)
            {
                var record = result.c;

                if (record.IsDeleted)
                {
                    return new QrCodeValidationResponseVM
                    {
                        Status = "AlreadyUsed",
                        Message = "QR Code Already Used",
                        StudentName = result.subStudent != null ? (result.subStudent.Name + " " + (result.subStudent.LastName ?? "")).Trim() : "Unknown",
                        VisitorName = !string.IsNullOrEmpty(record.GuardianName) ? record.GuardianName : record.ParentType,
                    };
                }

                // Internal Mark as Used
                record.IsDeleted = true;
                record.UpdatedOn = DateTime.UtcNow;

                context.Carpidum.Update(record);
                context.SaveChanges();

                var student = result.subStudent;
                var course = result.subCourse;
                var branch = result.subBranch;

                return new QrCodeValidationResponseVM
                {
                    Status = "Valid",
                    Message = "Access Granted",
                    StudentName = student != null ? (student.Name + " " + (student.LastName ?? "")).Trim() : "Unknown",
                    VisitorName = !string.IsNullOrEmpty(record.GuardianName) ? record.GuardianName : record.ParentType,
                    Gender = "",
                    CourseName = course?.Name,
                    BranchName = branch?.Name,
                    Batch = student?.AdmissionGrade,
                    ValidationTime = DateTime.Now
                };
            }

            // 2. Fallback: Check VIP Pass Table
            var vipPass = context.VipCarpidum.IgnoreQueryFilters()
                .FirstOrDefault(v => v.QrCode == code); // Direct Guid comparison

            if (vipPass != null)
            {
                if (vipPass.IsDeleted)
                {
                    return new QrCodeValidationResponseVM
                    {
                        Status = "AlreadyUsed",
                        Message = "VIP Pass Already Used",
                        VisitorName = vipPass.VipName,
                        StudentName = "VIP Access"
                    };
                }

                vipPass.IsDeleted = true;
                vipPass.UpdatedOn = DateTime.UtcNow;

                context.VipCarpidum.Update(vipPass);
                context.SaveChanges();

                return new QrCodeValidationResponseVM
                {
                    Status = "Valid",
                    Message = "Access Granted (VIP)",
                    VisitorName = vipPass.VipName,
                    StudentName = "VIP Access",
                    CourseName = "VIP",
                    BranchName = "VIP",
                    Batch = "VIP",
                    ValidationTime = DateTime.Now
                };
            }

            return new QrCodeValidationResponseVM { Status = "NotFound", Message = "QR Code not found" };
        }

        public string AddCarpidiumDetails(QrCodeRequestVM qrCode)
        {
            // Limit removed - allow unlimited passes per student

            // ToModel returns MCarpidum (CommonLib)
            var carpidumModel = QrCodeRequestVM.ToModel(qrCode);

            // Use request VM properties for email, as carpidumModel doesn't have them
            string qrcodeGenerated = GenerateQrCode(
                carpidumModel.Email,
                carpidumModel.GuardianName, // Mapped from Name
                qrCode.StudentName, // Direct from VM
                qrCode.Gender,      // Direct from VM
                carpidumModel.QrCode
            );

            context.Carpidum.Add(carpidumModel);
            context.SaveChanges();

            return qrcodeGenerated;
        }

        public List<MCarpidum> GetGuestList()
        {
            var query = from c in context.Carpidum
                        join s in context.Students on c.StudentId equals s.Id into students
                        from subStudent in students.DefaultIfEmpty()

                        // Join Course
                        join co in context.Courses on subStudent.CourseId equals co.Id into courses
                        from subCourse in courses.DefaultIfEmpty()

                        // Join Branch
                        join b in context.Branches on subStudent.BranchId equals b.Id into branches
                        from subBranch in branches.DefaultIfEmpty()

                        orderby c.Id descending
                        select new { c, subStudent, subCourse, subBranch };

            var list = query.ToList();

            return list.Select(x => {
                var carpidum = x.c;
                if (x.subStudent != null)
                {
                    carpidum.StudentName = (x.subStudent.Name + " " + (x.subStudent.LastName ?? "")).Trim();
                    carpidum.Gender = ""; // User requested to hide gender
                    carpidum.Batch = x.subStudent.AdmissionGrade;
                }
                else
                {
                    carpidum.StudentName = "Unknown";
                    carpidum.Gender = "";
                }

                carpidum.CourseName = x.subCourse?.Name;
                carpidum.BranchName = x.subBranch?.Name;

                return carpidum;
            }).ToList();
        }
        public List<MCarpidum> GetPassesByStudentId(int studentId)
        {
            var query = from c in context.Carpidum
                        join s in context.Students on c.StudentId equals s.Id into students
                        from subStudent in students.DefaultIfEmpty()

                        // Join Course
                        join co in context.Courses on subStudent.CourseId equals co.Id into courses
                        from subCourse in courses.DefaultIfEmpty()

                        // Join Branch
                        join b in context.Branches on subStudent.BranchId equals b.Id into branches
                        from subBranch in branches.DefaultIfEmpty()

                        where c.StudentId == studentId && !c.IsDeleted
                        orderby c.CreatedOn descending
                        select new { c, subStudent, subCourse, subBranch };

            var list = query.ToList();

            return list.Select(x =>
            {
                var carpidum = x.c;
                if (x.subStudent != null)
                {
                    carpidum.StudentName = (x.subStudent.Name + " " + (x.subStudent.LastName ?? "")).Trim();
                    carpidum.Gender = ""; // User requested to hide gender
                    carpidum.Batch = x.subStudent.AdmissionGrade;
                }
                else
                {
                    carpidum.StudentName = "Unknown";
                    carpidum.Gender = "";
                }

                carpidum.CourseName = x.subCourse?.Name;
                carpidum.BranchName = x.subBranch?.Name;

                return carpidum;
            }).ToList();
        }

        public bool DeletePass(int id)
        {
            var pass = context.Carpidum.FirstOrDefault(c => c.Id == id);
            if (pass == null)
            {
                return false;
            }

            // Soft delete - mark as deleted
            pass.IsDeleted = true;
            pass.UpdatedOn = DateTime.UtcNow;
            context.SaveChanges();
            return true;
        }
    }
}
