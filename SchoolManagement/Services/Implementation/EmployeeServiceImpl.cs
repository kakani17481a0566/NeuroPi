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
            throw new NotImplementedException();
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

        //public List<EmployeeResponseVM> GetAllByMasterTypeId(int id, int tenantId)
        //{
        //    throw new NotImplementedException();
        //}

        public List<EmployeeResponseVM> GetAllByTenantId(int tenantId)
        {
            var result=_context.Employees.Where(e=>e.TenantId== tenantId).ToList();
            if (result != null && result.Count>0)
            {
                return EmployeeResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public EmployeeResponseVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public EmployeeResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            throw new NotImplementedException();
        }

        public EmployeeResponseVM UpdateEmployee(int id, int tenantId, MasterUpdateVM masterType)
        {
            throw new NotImplementedException();
        }
    }
}
