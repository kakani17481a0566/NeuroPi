using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Employee;
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.Services.Implementation
{
    public class EmployeeServiceImpl : IEmployeeService
    {
        private readonly SchoolManagementDb _context;
        public EmployeeServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public EmployeeResponseVM CreateEmployee(EmployeeRequestVM employee)
        {
            var EmployeeModel=EmployeeRequestVM.ToModel(employee);
            _context.Employees.Add(EmployeeModel);
            _context.SaveChanges();
            return EmployeeResponseVM.ToViewModel(EmployeeModel);
        }

        public EmployeeResponseVM DeleteById(int id, int tenantId)
        {
            var employee= _context.Employees.FirstOrDefault(x => x.Id == id && x.TenantId==tenantId &&  !x.IsDeleted);
            if (employee != null)
            {
                employee.IsDeleted = true;
                _context.SaveChanges();
                return EmployeeResponseVM.ToViewModel(employee);

            }
            return null;
        }

        public List<EmployeeResponseVM> GetAll()
        {
            var result=_context.Employees.ToList();
            if (result != null && result.Count>0)
            {
                return EmployeeResponseVM.ToViewModelList(result);

            }
            return null;
        }

        public List<EmployeeResponseVM> GetAllByTenantId(int tenantId)
        {
            var result=_context.Employees.Where(e=>e.TenantId== tenantId && !e.IsDeleted).ToList();
            if (result != null && result.Count>0)
            {
                return EmployeeResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public EmployeeResponseVM GetById(int id)
        {
            var result = _context.Employees.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
            if (result != null)
            {
                return EmployeeResponseVM.ToViewModel(result);
            }
            return null;
        }

        public EmployeeResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _context.Employees.FirstOrDefault(e => !e.IsDeleted && e.Id == id && e.TenantId == tenantId);
            if(result != null)
            {
               return  EmployeeResponseVM.ToViewModel(result);
            }
            return null;
        }

        public EmployeeResponseVM UpdateEmployee(int id, int tenantId, EmployeeUpdateVM masterType)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id && e.TenantId == tenantId && !e.IsDeleted);
            if (employee != null)
            {
                employee.EmpCode = masterType.EmpCode;
                _context.SaveChanges();
                return EmployeeResponseVM.ToViewModel(employee);

            }
            return null;

        }
    }
}
