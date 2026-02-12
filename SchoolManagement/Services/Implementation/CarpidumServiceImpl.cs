using SchoolManagement.Data;
using CommonLibModel = NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Carpidum;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Services.Implementation
{
    public class CarpidumServiceImpl : ICarpidumService
    {
        private readonly SchoolManagementDb _context;

        public CarpidumServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<CarpidumVM> GetAll(int tenantId)
        {
            var query = from c in _context.Carpidum
                        join s in _context.Students on c.StudentId equals s.Id
                        where c.TenantId == tenantId && !c.IsDeleted
                        select new { c, s };

            return query.Select(x => new CarpidumVM
            {
                Id = x.c.Id,
                StudentId = x.c.StudentId,
                StudentName = x.s.Name + " " + (x.s.LastName ?? ""),
                ParentType = x.c.ParentType,
                GuardianName = x.c.GuardianName,
                QrCode = x.c.QrCode,
                Email = x.c.Email,
                MobileNumber = x.c.MobileNumber,
                CreatedOn = x.c.CreatedOn,
                TenantId = x.c.TenantId
            }).ToList();
        }

        public CarpidumVM? GetById(int id, int tenantId)
        {
            var query = from c in _context.Carpidum
                        join s in _context.Students on c.StudentId equals s.Id
                        where c.Id == id && c.TenantId == tenantId && !c.IsDeleted
                        select new { c, s };

            var result = query.FirstOrDefault();

            if (result == null) return null;

            return new CarpidumVM
            {
                Id = result.c.Id,
                StudentId = result.c.StudentId,
                StudentName = result.s.Name + " " + (result.s.LastName ?? ""),
                ParentType = result.c.ParentType,
                GuardianName = result.c.GuardianName,
                QrCode = result.c.QrCode,
                Email = result.c.Email,
                MobileNumber = result.c.MobileNumber,
                CreatedOn = result.c.CreatedOn,
                TenantId = result.c.TenantId
            };
        }

        public List<CarpidumVM> GetByStudentId(int studentId, int tenantId)
        {
            var query = from c in _context.Carpidum
                        join s in _context.Students on c.StudentId equals s.Id
                        where c.StudentId == studentId && c.TenantId == tenantId && !c.IsDeleted
                        select new { c, s };

            return query.Select(x => new CarpidumVM
            {
                Id = x.c.Id,
                StudentId = x.c.StudentId,
                StudentName = x.s.Name + " " + (x.s.LastName ?? ""),
                ParentType = x.c.ParentType,
                GuardianName = x.c.GuardianName,
                QrCode = x.c.QrCode,
                Email = x.c.Email,
                MobileNumber = x.c.MobileNumber,
                CreatedOn = x.c.CreatedOn,
                TenantId = x.c.TenantId
            }).ToList();
        }

        public CarpidumVM? Create(CarpidumRequestVM request, out string message)
        {
            // valid parent type
            var validParentTypes = new List<string> { "FATHER", "MOTHER", "GRANDFATHER", "GRANDMOTHER", "GUARDIAN", "OTHERS" };
            if (!validParentTypes.Contains(request.ParentType.ToUpper()))
            {
                message = "Invalid Parent Type";
                return null;
            }

            // Check if student exists
            var student = _context.Students.FirstOrDefault(s => s.Id == request.StudentId && !s.IsDeleted);
            if (student == null)
            {
                message = "Student not found";
                return null;
            }

            // check unique QrCode
            bool qrExists = _context.Carpidum.Any(x => x.QrCode == request.QrCode && !x.IsDeleted);
            if (qrExists)
            {
                message = "QR Code already exists";
                return null;
            }

            // Check unique constraint: StudentId + ParentType + GuardianName
            bool duplicateRole = _context.Carpidum.Any(x => 
                x.StudentId == request.StudentId && 
                x.ParentType == request.ParentType && 
                x.GuardianName == request.GuardianName && 
                !x.IsDeleted);

            if (duplicateRole)
            {
                message = "Guardian with this role and name already exists for this student";
                return null;
            }

            var entity = new CommonLibModel.MCarpidum
            {
                StudentId = request.StudentId,
                ParentType = request.ParentType,
                GuardianName = request.GuardianName,
                QrCode = request.QrCode,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                TenantId = request.TenantId,
                CreatedOn = DateTime.UtcNow
            };

            _context.Carpidum.Add(entity);
            _context.SaveChanges();

            message = "Guardian added successfully";
            return GetById(entity.Id, request.TenantId);
        }

        public CarpidumVM? Update(int id, CarpidumRequestVM request, out string message)
        {
            var entity = _context.Carpidum.FirstOrDefault(x => x.Id == id && x.TenantId == request.TenantId && !x.IsDeleted);
            if (entity == null)
            {
                message = "Record not found";
                return null;
            }

            // Check unique QrCode if changed
            if (entity.QrCode != request.QrCode)
            {
                bool qrExists = _context.Carpidum.Any(x => x.QrCode == request.QrCode && !x.IsDeleted);
                if (qrExists)
                {
                    message = "QR Code already exists";
                    return null;
                }
            }

            // Check unique constraint if changed
            if (entity.StudentId != request.StudentId || entity.ParentType != request.ParentType || entity.GuardianName != request.GuardianName)
            {
                bool duplicateRole = _context.Carpidum.Any(x => 
                    x.Id != id &&
                    x.StudentId == request.StudentId && 
                    x.ParentType == request.ParentType && 
                    x.GuardianName == request.GuardianName && 
                    !x.IsDeleted);

                if (duplicateRole)
                {
                    message = "Guardian with this role and name already exists for this student";
                    return null;
                }
            }

            entity.StudentId = request.StudentId;
            entity.ParentType = request.ParentType;
            entity.GuardianName = request.GuardianName;
            entity.QrCode = request.QrCode;
            entity.Email = request.Email;
            entity.MobileNumber = request.MobileNumber;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.Carpidum.Update(entity);
            _context.SaveChanges();

            message = "Guardian updated successfully";
            return GetById(entity.Id, request.TenantId);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _context.Carpidum.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return false;

            // Soft delete
            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;
            
            _context.Carpidum.Update(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
