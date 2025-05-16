using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Department;
using System;
using System.Collections.Generic;
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
            var departments = _context.Departments
                .Include(d => d.Organization)
                .Include(d => d.Tenant)
                .Where(d => d.IsDeleted == false) 
                .ToList();

            return DepartmentResponseVM.ToViewModelList(departments);
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

        public List<DepartmentResponseVM> GetDepartmentsByTenantId(int tenantId)
        {
            var departments = _context.Departments
                .Include(d => d.Organization)
                .Include(d => d.Tenant)
                .Where(d => d.TenantId == tenantId && !d.IsDeleted)
                .ToList();

            return departments.Select(d => DepartmentResponseVM.ToViewModel(d)).ToList();
        }

        public DepartmentResponseVM AddDepartment(DepartmentCreateVM request)
        {
          

            var departmentModel = DepartmentCreateVM.ToModel(request);
            departmentModel.CreatedOn = DateTime.UtcNow;
            departmentModel.IsDeleted = false;

            _context.Departments.Add(departmentModel);
            _context.SaveChanges();

            return DepartmentResponseVM.ToViewModel(departmentModel);
        }

        public DepartmentResponseVM UpdateDepartment(int id, int tenantId, DepartmentUpdateVM request)
        {
            var department = _context.Departments
                .FirstOrDefault(d => d.DepartmentId == id && d.TenantId == tenantId && !d.IsDeleted);

            if (department == null) return null;

            // Update only allowed fields
            department.Name = request.Name;
            department.OrganizationId = request.OrganizationId;
            department.HeadUserId = request.HeadUserId;
            department.UpdatedOn = DateTime.UtcNow;
            department.UpdatedBy = request.UpdatedBy ?? 0;

            _context.SaveChanges();

            return DepartmentResponseVM.ToViewModel(department);
        }

        public bool DeleteById(int id, int tenantId, int deletedBy)
        {
            var department = _context.Departments
                .FirstOrDefault(d => d.DepartmentId == id && d.TenantId == tenantId && !d.IsDeleted);

            if (department == null) return false;

            department.IsDeleted = true;
            _context.SaveChanges();

            return true;
        }
    }
}
