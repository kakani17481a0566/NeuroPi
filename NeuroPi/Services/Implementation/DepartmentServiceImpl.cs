using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Department;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class DepartmentServiceImpl : IDepartmentService
    {
        private readonly NeuroPiDbContext _context;

        public DepartmentServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        // Get all departments (excluding soft-deleted ones)
        public List<DepartmentResponseVM> GetAllDepartments()
        {
            var result = _context.Departments
                                 .Include(d => d.Organization)
                                 .Include(d => d.Tenant)
                                 .Where(d => !d.IsDeleted)  // Exclude soft-deleted departments
                                 .ToList();
            if (result != null)
            {
                return DepartmentResponseVM.ToViewModelList(result);
            }
            return null;
        }

        // Get department by ID (only if not soft-deleted)
        public DepartmentResponseVM GetDepartmentById(int id)
        {
            var result = _context.Departments
                                 .Include(d => d.Organization)
                                 .Include(d => d.Tenant)
                                 .FirstOrDefault(d => d.DepartmentId == id && !d.IsDeleted);  // Exclude soft-deleted departments
            if (result != null)
            {
                return DepartmentResponseVM.ToViewModel(result);
            }
            return null;
        }

        // Soft delete a department (set IsDeleted to true)
        public bool DeleteById(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id && !d.IsDeleted);  // Ensure it's not already soft-deleted
            if (department == null)
            {
                return false;
            }

            // Perform soft delete by setting IsDeleted to true
            department.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        // Add a new department
        public DepartmentResponseVM AddDepartment(DepartmentRequestVM department)
        {
            if (department != null)
            {
                var departmentModel = DepartmentRequestVM.ToModel(department);
                departmentModel.IsDeleted = false; // Ensure the department is not soft-deleted by default
                _context.Departments.Add(departmentModel);
                _context.SaveChanges();

                return new DepartmentResponseVM()
                {
                    TenantId = department.TenantId,
                    TenantName = department.Name
                };
            }
            return null;
        }

        // Update an existing department
        public DepartmentResponseVM UpdateDepartment(int id, DepartmentRequestVM department)
        {
            var existedDepartment = _context.Departments.FirstOrDefault(d => d.DepartmentId == id && !d.IsDeleted);  // Ensure the department is not soft-deleted
            if (existedDepartment != null)
            {
                existedDepartment.TenantId = department.TenantId;
                existedDepartment.Name = department.Name;
                existedDepartment.HeadUserId = department.HeadUserId;
                existedDepartment.OrganizationId = department.OrganizationId;

                _context.SaveChanges();
                return new DepartmentResponseVM()
                {
                    Id = existedDepartment.DepartmentId,
                    Name = department.Name,
                    TenantId = department.TenantId,
                    HeadUserId = department.HeadUserId,
                    OrganizationId = department.OrganizationId,
                };
            }
            return null;
        }
    }
}
