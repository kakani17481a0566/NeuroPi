using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Model;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Branch;
using SchoolManagement.ViewModel.CourseTeacher;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class BranchServiceImpl : IBranchService
    {
        private readonly SchoolManagementDb _context;
        public BranchServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public BranchResponseVM AddBranch(BranchRequestVM branch)
        {
            var newBranch = BranchRequestVM.ToModel(branch);
            newBranch.CreatedOn = DateTime.UtcNow;
            _context.Branches.Add(newBranch);
            _context.SaveChanges();
            return BranchResponseVM.ToViewModel(newBranch);

        }

        public bool DeleteBranch(int id, int tenantId)
        {
            var branch = _context.Branches
                .FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);
            if (branch == null)
            {
                return false;
            }
            branch.IsDeleted = true;
            branch.UpdatedOn = DateTime.UtcNow;
            _context.Branches.Update(branch);
            _context.SaveChanges();
            return true;
        }

        public List<BranchResponseVM> GetAllBranches()
        {
            var branches = _context.Branches
                .Where(b => !b.IsDeleted)
                .ToList();
            return BranchResponseVM.ToViewModelList(branches);
        }



        public BranchResponseVM GetBranchById(int id)
        {
            var branch = _context.Branches
                .FirstOrDefault(b => b.Id == id && !b.IsDeleted);
            if (branch == null)
            {
                return null;
            }
            return BranchResponseVM.ToViewModel(branch);
        }

        public BranchResponseVM GetBranchByIdAndTenantId(int id, int tenantId)
        {
            var branch = _context.Branches
                .FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);
            if (branch == null)
            {
                return null;
            }
            return BranchResponseVM.ToViewModel(branch);

        }

        public List<BranchResponseVM> GetBranchesByTenantId(int tenantId)
        {
            var branch = _context.Branches
                 .Where(b => b.TenantId == tenantId && !b.IsDeleted)
                 .ToList();
            return branch.Select(BranchResponseVM.ToViewModel).ToList();
        }

        public BranchResponseVM UpdateBranch(int id, int tenantId, BranchUpdateVM branch)
        {
            var existingBranch = _context.Branches
                .FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);
            if (existingBranch == null)
            {
                return null;
            }
            existingBranch.Name = branch.Name;
            existingBranch.Contact = branch.Contact;
            existingBranch.Address = branch.Address;
            existingBranch.Pincode = branch.Pincode;
            existingBranch.District = branch.District;
            existingBranch.State = branch.State;
            existingBranch.UpdatedBy = branch.UpdatedBy;
            existingBranch.UpdatedOn = DateTime.UtcNow;
            _context.Branches.Update(existingBranch);
            _context.SaveChanges();
            return BranchResponseVM.ToViewModel(existingBranch);
        }
        //sai vardhan
        public CourseTeacherVM GetBranchByDepartmentId(int departmentId, int userId)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

            // Get branch by department
            var branch = _context.Branches
                .FirstOrDefault(b => b.DepartmentId == departmentId && !b.IsDeleted);

            if (branch == null)
                return null;

            var courseTeacherVM = new CourseTeacherVM
            {
                branchId = branch.Id
            };

            // Get course-teacher mappings
            var courseTeachers = _context.CourseTeachers
                .Where(ct => ct.BranchId == branch.Id && ct.TeacherId == userId && !ct.IsDeleted)
                .Include(ct => ct.Course)
                .ToList();

            // Map courses
            courseTeacherVM.courses = courseTeachers
                .Where(ct => ct.Course != null) // avoid null reference
                .Select(ct => new Course
                {
                    id = ct.Course.Id,
                    name = ct.Course.Name
                })
                .ToList();

            // Get current week
            var currentWeek = _context.Weeks
                .FirstOrDefault(w => w.StartDate <= today && w.EndDate >= today && !w.IsDeleted);

            courseTeacherVM.weekId = currentWeek?.Id ?? 0;
            courseTeacherVM.termId = currentWeek?.TermId ?? 0;

            return courseTeacherVM;
        }

    
    
    }
}
