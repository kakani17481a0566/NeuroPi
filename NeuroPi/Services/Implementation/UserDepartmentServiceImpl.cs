using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.UserDepartment;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class UserDepartmentServiceImpl : IUserDepartmentService
    {
        private readonly NeuroPiDbContext _context;
        public UserDepartmentServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public UserDepartmentCreateVM CreateUserDepartment(UserDepartmentRequestVM input)
        {
            var userDepartment = UserDepartmentRequestVM.ToModel(input);
            _context.UserDepartments.Add(userDepartment);
            _context.SaveChanges();

            return new UserDepartmentCreateVM
            {

                UserDeptId = userDepartment.UserDeptId,
                UserId = userDepartment.UserId,
                DepartmentId = userDepartment.DepartmentId,
                TenantId = userDepartment.TenantId,
                CreatedBy = (int)userDepartment.CreatedBy

            };



        }

        public List<UserDepartmentResponseVM> GetAllUserDepartments()
        {
            var result = _context.UserDepartments.Where(r => !r.IsDeleted).ToList();
            if (result != null)
            {
                return UserDepartmentResponseVM.ToViewModelList(result);
            }
            return null;


        }

        public UserDepartmentResponseVM GetUserDepartmentById(int id)
        {
            var result = _context.UserDepartments.FirstOrDefault(t => t.UserDeptId == id);
            if (result != null)
            {
                return UserDepartmentResponseVM.ToViewModel(result);
            }
            return null;

        }

        public List<UserDepartmentResponseVM> GetUserDepartmentsByTenantId(int tenantId)
        {
            var result = _context.UserDepartments.Where(t => t.TenantId == tenantId && !t.IsDeleted).ToList();
            if (result != null)
            {
                return UserDepartmentResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public UserDepartmentResponseVM GetUserDepartmentByIdAndTenantId(int id, int tenantId)
        {
            var result = _context.UserDepartments.FirstOrDefault(t => t.UserDeptId == id && t.TenantId == tenantId);
            if (result != null)
            {
                return UserDepartmentResponseVM.ToViewModel(result);
            }
            return null;
        }


        public UserDepartmentResponseVM UpdateUserDepartmentByUserDeptIdAndTenantId(int id, int tenantId, UserDepartmentUpdateVM input)
        {
            var userDepartment = _context.UserDepartments.FirstOrDefault(t => t.UserDeptId == id && t.TenantId == tenantId);
            if (userDepartment == null)
            {
                return null;
            }
            userDepartment.UserId = input.UserId;
            userDepartment.DepartmentId = input.DepartmentId;

            userDepartment.UpdatedBy = input.UpdatedBy;
            userDepartment.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return UserDepartmentResponseVM.ToViewModel(userDepartment);

        }



        public bool DeleteUserDepartmentByUserDeptIdAndTenantId(int id, int tenantId)
        {
            var userDepartment = _context.UserDepartments.FirstOrDefault(t => t.UserDeptId == id && t.TenantId == tenantId && !t.IsDeleted);
            if (userDepartment == null)
            {
                return false;
            }
            userDepartment.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
    }
}

