using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.EmployeeStudyPlan
{
    public class EmployeeStudyPlanVm
    {
        public int Id { get; set; }
        public int EmployeeDetailsId { get; set; }
        public int StudyPlanId { get; set; }
        public int TenantId { get; set; }

        public static EmployeeStudyPlanVm ToViewModel(MEmployeeStudyPlan model)
        {
            return new EmployeeStudyPlanVm
            {
                Id = model.Id,
                EmployeeDetailsId = model.EmployeeDetailsId,
                StudyPlanId = model.StudyPlanId,
                TenantId = model.TenantId
            };
        }

        public static List<EmployeeStudyPlanVm> ToViewModelList(List<MEmployeeStudyPlan> models)
        {
            return models.Select(ToViewModel).ToList();
        }
    }
}
