using Microsoft.EntityFrameworkCore;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Department;

namespace NeuroPi.Services.Implementation
{
    public class DepartmentServiceImpl:IDepartmentService
    {

        private readonly NeuroPiDbContext _context;

        public DepartmentServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        

        public List<DepartmentResponseVM> GetAllDepartments()
        {
            var result=_context.Departments.Include(d=>d.Organization)
                                            .Include(d=>d.Tenant)
                                            .ToList();
            if (result != null)
            {
                return DepartmentResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public DepartmentResponseVM GetDepartmentById(int id)
        {
            var result = _context.Departments.Include(d => d.Organization)
                                           .Include(d => d.Tenant)
                                           .FirstOrDefault(d => d.DepartmentId == id);
            if(result != null)
            {
                return DepartmentResponseVM.ToViewModel(result);
            }
            return null;
        }

        public bool DeleteById(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);
            if (department == null) { return false; }

            _context.Departments.Remove(department);
            _context.SaveChanges();
            return true;
        }

        public DepartmentResponseVM AddDepartment(DepartmentRequestVM department)
        {
            if (department != null)
            {
                _context.Departments.Add(DepartmentRequestVM.ToModel(department));
                _context.SaveChanges();
                return new DepartmentResponseVM()
                {
                    TenantId = department.TenantId,
                    TenantName = department.Name
                };
            }
            return null;
        }

        public DepartmentResponseVM UpdateDepartment(int id, DepartmentRequestVM department)
        {
            MDepartment existedDepartment = _context.Departments.FirstOrDefault(d=>d.DepartmentId==id);
            if (existedDepartment != null) {
                existedDepartment.TenantId = department.TenantId;
                existedDepartment.Name = department.Name;
                existedDepartment.HeadUserId= department.HeadUserId;
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
