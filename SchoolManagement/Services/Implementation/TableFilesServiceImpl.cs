using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TableFile;
using SchoolManagement.ViewModel.TableFiles;

namespace SchoolManagement.Services.Implementation
{
    public class TableFilesServiceImpl : ITableFilesService
    {
        private readonly SchoolManagementDb _db;

        public TableFilesServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public TimetableAttachmentVM Create(TimetableAttachmentCreateVM vm)
        {
            var entity = new MTableFiles
            {
                CourseId = vm.CourseRefId,
                TimeTableId = vm.TimeTableId,
                Name = vm.Name,
                Link = vm.Link,
                Type = vm.Type,
                TenantId = vm.TenantId,

                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _db.TableFiles.Add(entity);
            _db.SaveChanges();

            return ToVM(entity);
        }

        public TimetableAttachmentVM Update(int id, TimetableAttachmentUpdateVM vm, int tenantId)
        {
            var entity = _db.TableFiles
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return null;

            entity.CourseId = vm.CourseRefId;
            entity.TimeTableId = vm.TimeTableId;
            entity.Name = vm.Name;
            entity.Link = vm.Link;
            entity.Type = vm.Type;

            entity.UpdatedBy = vm.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();

            return ToVM(entity);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _db.TableFiles
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();
            return true;
        }

        public TimetableAttachmentVM GetById(int id, int tenantId)
        {
            var entity = _db.TableFiles
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            return entity == null ? null : ToVM(entity);
        }

        public List<TimetableAttachmentVM> GetAll(int tenantId)
        {
            return _db.TableFiles
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .AsNoTracking()
                .ToList()
                .Select(ToVM)
                .ToList();
        }

        public List<TimetableAttachmentVM> GetByCourseAndTimeTable(int courseId, int? timeTableId, int tenantId)
        {
            var query = _db.TableFiles
                .Where(x => x.CourseId == courseId &&
                            x.TenantId == tenantId &&
                            !x.IsDeleted);

            if (timeTableId.HasValue)
                query = query.Where(x => x.TimeTableId == timeTableId);

            return query.AsNoTracking()
                        .ToList()
                        .Select(ToVM)
                        .ToList();
        }

        private static TimetableAttachmentVM ToVM(MTableFiles x)
        {
            return new TimetableAttachmentVM
            {
                Id = x.Id,
                CourseRefId = x.CourseId,
                TimeTableId = x.TimeTableId,
                Name = x.Name,
                Link = x.Link,
                Type = x.Type,
                TenantId = x.TenantId
            };
        }
    }
}
