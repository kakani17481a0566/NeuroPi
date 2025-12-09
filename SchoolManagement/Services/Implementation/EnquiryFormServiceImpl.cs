using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.EnquiryForm;
using NeuropiCommonLib.Email;
using System.IO;

namespace SchoolManagement.Services.Implementation
{
    public class EnquiryFormServiceImpl : IEnquiryFormService
    {
        private readonly SchoolManagementDb _context;
        private readonly IEmailService _emailService;

        public EnquiryFormServiceImpl(SchoolManagementDb context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // ------------------------------------------------------
        // CREATE
        // ------------------------------------------------------
        public async Task<EnquiryFormResponseVM> Create(EnquiryFormRequestVM request, int createdBy)
        {
            var now = DateTime.UtcNow;

            var model = new MEnquiryForm
            {
                CompanyName = request.CompanyName,
                ContactPerson = request.ContactPerson,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                IsAgreed = request.IsAgreed,
                DigitalSignature = request.DigitalSignature,
                TenantId = request.TenantId,

                // Timestamps
                CreatedBy = createdBy,
                CreatedOn = now,
                AgreedOn = request.IsAgreed ? now : null,  // ✔ correct agreed time

                IsDeleted = false
            };

            _context.EnquiryForms.Add(model);
            _context.SaveChanges();

            // ---------------------------------------
            // GENERATE LINK
            // ---------------------------------------
            string ndaLink = "#";
            if (!string.IsNullOrEmpty(request.NdaBaseUrl))
            {
                // Ensure no trailing slash on base, and construct: {base}/{uuid}/nda
                ndaLink = $"{request.NdaBaseUrl.TrimEnd('/')}/{model.Uuid}/nda";
            }

            // ---------------------------------------
            // SEND EMAIL
            // ---------------------------------------
            try
            {
                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), 
                                                   "EmailTemplates", "NdaEmail.html");

                if (!File.Exists(templatePath))
                {
                    Console.WriteLine($"EMAIL ERROR: Template file not found at {templatePath}");
                }
                else
                {
                    string html = File.ReadAllText(templatePath);

                    html = html.Replace("{{ContactPerson}}", model.ContactPerson)
                               .Replace("{{NdaLink}}", ndaLink);

                    Console.WriteLine($"Sending email to: {model.Email}");
                    Console.WriteLine($"NDA Link in email: {ndaLink}");

                    await _emailService.SendEmailAsync(
                        model.Email,
                        "NeuroPi – NDA for Our Continued Collaboration",
                        html
                    );

                    Console.WriteLine("EMAIL SENT SUCCESSFULLY");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EMAIL ERROR: {ex.Message}");
                Console.WriteLine($"EMAIL ERROR STACK TRACE: {ex.StackTrace}");
            }

            var response = ToResponse(model);
            response.Link = ndaLink != "#" ? ndaLink : null;
            
            return response;
        }

        // ------------------------------------------------------
        // GET BY UUID
        // ------------------------------------------------------
        public EnquiryFormResponseVM GetByUuid(Guid uuid, int tenantId)
        {
            var model = _context.EnquiryForms
                .FirstOrDefault(x => x.Uuid == uuid &&
                                     x.TenantId == tenantId &&
                                     !x.IsDeleted);

            return model == null ? null : ToResponse(model);
        }

        // ------------------------------------------------------
        // GET ALL
        // ------------------------------------------------------
        public List<EnquiryFormResponseVM> GetAll(int tenantId)
        {
            return _context.EnquiryForms
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedOn)
                .Select(ToResponse)
                .ToList();
        }

        // ------------------------------------------------------
        // UPDATE
        // ------------------------------------------------------
        public async Task<EnquiryFormResponseVM> Update(Guid uuid, EnquiryFormUpdateVM request, int tenantId, int updatedBy)
        {
            var model = _context.EnquiryForms
                .FirstOrDefault(x => x.Uuid == uuid &&
                                     x.TenantId == tenantId &&
                                     !x.IsDeleted);

            if (model == null)
                return null;

            var now = DateTime.UtcNow;

            // Update fields
            model.CompanyName = request.CompanyName;
            model.ContactPerson = request.ContactPerson;
            model.ContactNumber = request.ContactNumber;
            model.Email = request.Email;
            model.DigitalSignature = request.DigitalSignature;

            // ✔ Only set AgreedOn when user newly agrees (false → true)
            bool isNewlyAgreed = !model.IsAgreed && request.IsAgreed;
            if (isNewlyAgreed)
            {
                model.AgreedOn = now;
            }

            model.IsAgreed = request.IsAgreed;

            // Update metadata
            model.UpdatedBy = updatedBy;
            model.UpdatedOn = now;

            _context.SaveChanges();

            // ✔ Send confirmation email when NDA is signed
            if (isNewlyAgreed)
            {
                try
                {
                    // TODO: Generate PDF of signed NDA document
                    // For now, send email confirmation without PDF
                    
                    string confirmationSubject = "NDA Signed Successfully - NeuroPi";
                    string confirmationBody = $@"
                        <html>
                        <body style='font-family: Arial, sans-serif;'>
                            <h2>NDA Signed Successfully</h2>
                            <p>Dear {model.ContactPerson},</p>
                            <p>Thank you for signing the Non-Disclosure Agreement with NeuroPi Tech Private Limited.</p>
                            <p><strong>Details:</strong></p>
                            <ul>
                                <li>Company: {model.CompanyName}</li>
                                <li>Signed by: {model.ContactPerson}</li>
                                <li>Email: {model.Email}</li>
                                <li>Date Signed: {model.AgreedOn?.ToString("MMMM dd, yyyy")}</li>
                            </ul>
                            <p>A copy of this signed NDA has been recorded in our system.</p>
                            <p><em>Note: PDF copy will be attached in the next update.</em></p>
                            <br/>
                            <p>Best regards,<br/>
                            NeuroPi Team</p>
                        </body>
                        </html>";

                    // Send to the signer
                    await _emailService.SendEmailAsync(
                        model.Email,
                        confirmationSubject,
                        confirmationBody
                    );

                    // Send notification to NeuroPi admin (info@neuropi.ai)
                    string adminNotification = $@"
                        <html>
                        <body style='font-family: Arial, sans-serif;'>
                            <h2>New NDA Signed</h2>
                            <p><strong>Details:</strong></p>
                            <ul>
                                <li>Company: {model.CompanyName}</li>
                                <li>Contact Person: {model.ContactPerson}</li>
                                <li>Email: {model.Email}</li>
                                <li>Phone: {model.ContactNumber}</li>
                                <li>Date Signed: {model.AgreedOn?.ToString("MMMM dd, yyyy HH:mm:ss")} UTC</li>
                                <li>UUID: {model.Uuid}</li>
                            </ul>
                        </body>
                        </html>";

                    await _emailService.SendEmailAsync(
                        "info@neuropi.ai",
                        $"NDA Signed: {model.CompanyName}",
                        adminNotification
                    );

                    Console.WriteLine($"Confirmation emails sent for NDA signing: {model.CompanyName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"EMAIL ERROR (NDA Confirmation): {ex.Message}");
                    // Don't fail the update if email fails
                }
            }

            return ToResponse(model);
        }

        // ------------------------------------------------------
        // SOFT DELETE
        // ------------------------------------------------------
        public bool Delete(Guid uuid, int tenantId, int deletedBy)
        {
            var model = _context.EnquiryForms
                .FirstOrDefault(x => x.Uuid == uuid &&
                                     x.TenantId == tenantId &&
                                     !x.IsDeleted);

            if (model == null)
                return false;

            model.IsDeleted = true;
            model.UpdatedBy = deletedBy;
            model.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        // ------------------------------------------------------
        // HELPER: Convert Model → Response VM
        // ------------------------------------------------------
        private EnquiryFormResponseVM ToResponse(MEnquiryForm model)
        {
            return new EnquiryFormResponseVM
            {
                Uuid = model.Uuid,
                CompanyName = model.CompanyName,
                ContactPerson = model.ContactPerson,
                ContactNumber = model.ContactNumber,
                Email = model.Email,
                IsAgreed = model.IsAgreed,
                DigitalSignature = model.DigitalSignature,
                AgreedOn = model.AgreedOn,
                TenantId = model.TenantId,

                CreatedOn = model.CreatedOn,
                CreatedBy = model.CreatedBy,
                UpdatedOn = model.UpdatedOn,
                UpdatedBy = model.UpdatedBy
            };
        }
    }
}
