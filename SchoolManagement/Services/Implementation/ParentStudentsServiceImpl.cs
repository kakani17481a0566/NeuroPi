using SchoolManagement.Data;
using SchoolManagement.Services.Interface;

namespace SchoolManagement.Services.Implementation
{
    public class ParentStudentsServiceImpl : SchoolManagement.Services.Interface.IParentStudentsService
    {
        private readonly SchoolManagement.Data.SchoolManagementDb _db;

        public ParentStudentsServiceImpl(SchoolManagement.Data.SchoolManagementDb db)
        {
            _db = db;
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM Create(SchoolManagement.ViewModel.ParentStudents.ParentStudentRequestVM request)
        {
            var entity = new SchoolManagement.Model.MParentStudent
            {
                ParentId = request.ParentId,
                StudentId = request.StudentId,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = System.DateTime.UtcNow
            };

            _db.ParentStudents.Add(entity);
            _db.SaveChanges();

            return MapToResponse(entity);
        }

        public System.Collections.Generic.List<SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM> GetAll()
        {
            return _db.ParentStudents
                .Where(x => !x.IsDeleted)
                .Select(MapToResponse)
                .ToList();
        }

        public System.Collections.Generic.List<SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM> GetAllByTenantId(int tenantId)
        {
            return _db.ParentStudents
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Select(MapToResponse)
                .ToList();
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM GetById(int id)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return entity == null ? null : MapToResponse(entity);
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity == null ? null : MapToResponse(entity);
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM UpdateByIdAndTenantId(int id, int tenantId, SchoolManagement.ViewModel.ParentStudents.ParentStudentUpdateVM request)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.ParentId = request.ParentId;
            entity.StudentId = request.StudentId;
            entity.UpdatedBy = request.UpdatedBy;
            entity.UpdatedOn = System.DateTime.UtcNow;

            _db.SaveChanges();
            return MapToResponse(entity);
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM DeleteByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.IsDeleted = true;
            entity.UpdatedOn = System.DateTime.UtcNow;
            _db.SaveChanges();

            return MapToResponse(entity);
        }

        private SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM MapToResponse(SchoolManagement.Model.MParentStudent model)
        {
            return new SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM
            {
                Id = model.Id,
                ParentId = model.ParentId,
                StudentId = model.StudentId,
                TenantId = model.TenantId
            };
        }
    }
}
