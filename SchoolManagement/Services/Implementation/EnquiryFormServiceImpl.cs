using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.EnquiryForm;

namespace SchoolManagement.Services.Implementation
{
    public class EnquiryFormServiceImpl : IEnquiryFormService
    {
        private readonly SchoolManagementDb _context;

        public EnquiryFormServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        // ------------------------------------------------------
        // CREATE
        // ------------------------------------------------------
        public EnquiryFormResponseVM Create(EnquiryFormRequestVM request, int createdBy)
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

            return ToResponse(model);
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
        public EnquiryFormResponseVM Update(Guid uuid, EnquiryFormUpdateVM request, int tenantId, int updatedBy)
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
            if (!model.IsAgreed && request.IsAgreed)
            {
                model.AgreedOn = now;
            }

            model.IsAgreed = request.IsAgreed;

            // Update metadata
            model.UpdatedBy = updatedBy;
            model.UpdatedOn = now;

            _context.SaveChanges();

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
