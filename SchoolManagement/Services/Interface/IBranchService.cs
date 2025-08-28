using Microsoft.CognitiveServices.Speech.Transcription;
using SchoolManagement.ViewModel.Branch;
using SchoolManagement.ViewModel.CourseTeacher;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IBranchService
    {
        List<BranchResponseVM> GetAllBranches();

        List<BranchResponseVM> GetBranchesByTenantId(int tenantId);

        BranchResponseVM GetBranchById(int id);

        BranchResponseVM GetBranchByIdAndTenantId(int id, int tenantId);

        BranchResponseVM AddBranch(BranchRequestVM branch);

        BranchResponseVM UpdateBranch(int id, int tenantId, BranchUpdateVM branch);

        bool DeleteBranch(int id, int tenantId);

        //CourseTeacherVM GetBranchByDepartmentId(int departmentId, int userId);

        
        List<BranchDropDownOptionVm> GetBranchDropDownOptions(int tenantId);
        //bool DeleteBranch(int id,int tenantId);

        CourseTeacherVM GetBranchByDepartmentId(int userId, int tenanatId);
    }
}
