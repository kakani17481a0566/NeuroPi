using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Contact;
using SchoolManagement.ViewModel.Institutions;
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

        public bool Delete(int id)
        {
            var entity = _context.Institutions.FirstOrDefault(i => i.Id == id && !i.IsDeleted);
            if (entity == null) return false;

            entity.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public InstitutionResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _context.Institutions
                .FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);
            if (entity == null) return null;

            return new InstitutionResponseVM
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
        }

        public InstitutionWithContactResponseVM CreateWithContact(InstitutionWithContactRequestVM request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // 1. Create the contact
                var contactModel = ContactRequestVM.ToModel(request.Contact);
                contactModel.CreatedOn = DateTime.UtcNow;
                _context.Contacts.Add(contactModel);
                _context.SaveChanges();

                var contactResponse = ContactResponseVM.ToViewModel(contactModel);

                // 2. Create the institution
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

                transaction.Commit();

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
