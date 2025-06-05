using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Contact;
using SchoolManagement.ViewModel.institutions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class InstitutionServiceImpl : IInstitutionService
    {
        private readonly SchoolManagementDb _context;

        public InstitutionServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        // Get list of all institutions (excluding deleted ones)
        // Developed by: Mohith
        public List<InstitutionResponseVM> GetAll()
        {
            return _context.Institutions
                .Where(x => !x.IsDeleted)
                .Select(x => new InstitutionResponseVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    ContactId = x.ContactId,
                    TenantId = x.TenantId,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                    UpdatedOn = x.UpdatedOn,
                    UpdatedBy = x.UpdatedBy,
                    IsDeleted = x.IsDeleted
                }).ToList();
        }

        // Get institution by ID
        // Developed by: Mohith
        public InstitutionResponseVM GetById(int id)
        {
            var x = _context.Institutions.FirstOrDefault(i => i.Id == id && !i.IsDeleted);
            if (x == null) return null;

            return new InstitutionResponseVM
            {
                Id = x.Id,
                Name = x.Name,
                ContactId = x.ContactId,
                TenantId = x.TenantId,
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy,
                UpdatedOn = x.UpdatedOn,
                UpdatedBy = x.UpdatedBy,
                IsDeleted = x.IsDeleted
            };
        }

        // Create a new institution
        // Developed by: Mohith
        public InstitutionResponseVM Create(InstitutionCreateRequestVM request)
        {
            var entity = new MInstitution
            {
                Name = request.Name,
                ContactId = request.ContactId,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.Institutions.Add(entity);
            _context.SaveChanges();

            return GetById(entity.Id);
        }

        // Update an institution by ID and Tenant ID
        // Developed by: Mohith
        public InstitutionResponseVM UpdateByIdAndTenantId(int id, int tenantId, InstitutionUpdateRequestVM request)
        {
            var entity = _context.Institutions
                .FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);

            if (entity == null) return null;

            entity.Name = request.Name;
            entity.ContactId = request.ContactId;
            entity.UpdatedBy = request.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return GetById(entity.Id);
        }

        // Delete institution by ID and Tenant ID
        // Optionally delete associated contact if deleteContact is true
        // Developed by: Mohith
        public bool DeleteByIdAndTenantId(int id, int tenantId, bool deleteContact)
        {
            var institution = _context.Institutions
                .Include(i => i.Contact)
                .FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);

            if (institution == null) return false;

            institution.IsDeleted = true;

            if (deleteContact && institution.Contact != null && !institution.Contact.IsDeleted)
            {
                institution.Contact.IsDeleted = true;
            }

            _context.SaveChanges();
            return true;
        }

        // Get institution and its contact by ID and Tenant ID
        // Developed by: Mohith
        public InstitutionWithContactResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _context.Institutions
                .Include(i => i.Contact)  // Eager load contact
                .FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);

            if (entity == null) return null;

            var institutionVM = new InstitutionResponseVM
            {
                Id = entity.Id,
                Name = entity.Name,
                ContactId = entity.ContactId,
                TenantId = entity.TenantId,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy,
                UpdatedOn = entity.UpdatedOn,
                UpdatedBy = entity.UpdatedBy,
                IsDeleted = entity.IsDeleted
            };

            var contactVM = entity.Contact != null
                ? ContactResponseVM.ToViewModel(entity.Contact)
                : null;

            return new InstitutionWithContactResponseVM
            {
                Institution = institutionVM,
                Contact = contactVM
            };
        }

        // Create institution with a new contact (in a transaction)
        // Developed by: Mohith
        public InstitutionWithContactResponseVM CreateWithContact(InstitutionWithContactRequestVM request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // Step 1: Create contact
                var contactModel = ContactRequestVM.ToModel(request.Contact);
                contactModel.CreatedOn = DateTime.UtcNow;
                _context.Contacts.Add(contactModel);
                _context.SaveChanges();

                var contactResponse = ContactResponseVM.ToViewModel(contactModel);

                // Step 2: Create institution using new contact
                var institution = new MInstitution
                {
                    Name = request.InstitutionName,
                    ContactId = contactModel.Id,
                    TenantId = request.TenantId,
                    CreatedBy = request.CreatedBy,
                    CreatedOn = DateTime.UtcNow
                };

                _context.Institutions.Add(institution);
                _context.SaveChanges();

                // Commit transaction
                transaction.Commit();

                var institutionResponse = new InstitutionResponseVM
                {
                    Id = institution.Id,
                    Name = institution.Name,
                    ContactId = institution.ContactId,
                    TenantId = institution.TenantId,
                    CreatedBy = institution.CreatedBy,
                    CreatedOn = institution.CreatedOn,
                    IsDeleted = false
                };

                return new InstitutionWithContactResponseVM
                {
                    Contact = contactResponse,
                    Institution = institutionResponse
                };
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
