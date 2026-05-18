using SchoolManagement.ViewModel.EmployeeStudyPlan;

namespace SchoolManagement.Services.Interface
{
    public interface IEmployeeStudyPlanService
    {
        EmployeeStudyPlanVm CreateEmployeeStudyPlan(EmployeeStudyPlanCreateVm vm);
        bool DeleteEmployeeStudyPlan(int id, int tenantId);
        List<EmployeeStudyPlanVm> GetAllEmployeeStudyPlans(int tenantId);
        EmployeeStudyPlanDetailVm? GetEmployeeStudyPlanById(int id, int tenantId);
        List<EmployeeStudyPlanDetailVm> GetEmployeeStudyPlansByEmployeeId(int employeeDetailsId, int tenantId);
        List<EmployeeStudyPlanVm> GetEmployeeStudyPlansByPlanId(int studyPlanId, int tenantId);
    }
}
