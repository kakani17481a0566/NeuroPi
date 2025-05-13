using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Department;
using NeuroPi.UserManagment.Model;
using System.Linq;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class DepartmentServiceImpl : IDepartmentService
    {
        private readonly NeuroPiDbContext _context;

        public DepartmentServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public List<DepartmentResponseVM> GetAllDepartments()
        {
            var result = _context.Departments
                .Include(d => d.Organization)
                .Include(d => d.Tenant)
                .Where(d => !d.IsDeleted)
                .ToList();

            return DepartmentResponseVM.ToViewModelList(result);
        }

        public DepartmentResponseVM GetDepartmentById(int id)
        {
            var department = _context.Departments
                .Include(d => d.Organization)
                .Include(d => d.Tenant)
                .FirstOrDefault(d => d.DepartmentId == id && !d.IsDeleted);

            return department != null ? DepartmentResponseVM.ToViewModel(department) : null;
        }

        public DepartmentResponseVM GetDepartmentByIdAndTenantId(int id, int tenantId)
        {
            var department = _context.Departments
                .Include(d => d.Organization)
                .Include(d => d.Tenant)
                .FirstOrDefault(d => d.DepartmentId == id && d.TenantId == tenantId && !d.IsDeleted);

            return department != null ? DepartmentResponseVM.ToViewModel(department) : null;
        }

        public bool DeleteById(int id, int deletedBy)
        {
            var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id && !d.IsDeleted);
            if (department == null) return false;

            department.IsDeleted = true;
            department.UpdatedOn = DateTime.UtcNow;
            department.UpdatedBy = deletedBy;

            _context.SaveChanges();
            return true;
        }

        // **AddDepartment Implementation**
        public DepartmentResponseVM AddDepartment(DepartmentCreateVM request)
        {
            // Convert the DepartmentCreateVM to MDepartment (the model)
            var model = DepartmentCreateVM.ToModel(request);

            // Set the creation information
            model.CreatedOn = DateTime.UtcNow;
            model.CreatedBy = request.CreatedBy ?? 0;
            model.IsDeleted = false;

            // Add the department to the database
            _context.Departments.Add(model);
            _context.SaveChanges();

            // Get the newly added department and map it to the response view model
            var department = _context.Departments
                .Include(d => d.Organization)
                .Include(d => d.Tenant)
                .FirstOrDefault(d => d.DepartmentId == model.DepartmentId);

            return department != null ? DepartmentResponseVM.ToViewModel(department) : null;
        }

        // **UpdateDepartment Implementation**
        public DepartmentResponseVM UpdateDepartment(int id, DepartmentUpdateVM request)
        {
            var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id && !d.IsDeleted);
            if (department == null) return null;

            // Update the department fields with the request data
            department.Name = request.Name;
            department.TenantId = request.TenantId;
            department.OrganizationId = request.OrganizationId;
            department.HeadUserId = request.HeadUserId;
            department.UpdatedOn = DateTime.UtcNow;
            department.UpdatedBy = request.UpdatedBy ?? 0;

            // Save the changes to the database
            _context.SaveChanges();

            // Get the updated department and map it to the response view model
            var updatedDepartment = _context.Departments
                .Include(d => d.Organization)
                .Include(d => d.Tenant)
                .FirstOrDefault(d => d.DepartmentId == id);

            return updatedDepartment != null ? DepartmentResponseVM.ToViewModel(updatedDepartment) : null;
        }

        public List<DepartmentResponseVM> GetDepartmentsByTenantId(int tenantId)
        {
            var result = _context.Departments
                .Include(d => d.Organization)
                .Include(d => d.Tenant)
                .Where(d => d.TenantId == tenantId && !d.IsDeleted)
                .ToList();

            return DepartmentResponseVM.ToViewModelList(result);
        }
    }
}
