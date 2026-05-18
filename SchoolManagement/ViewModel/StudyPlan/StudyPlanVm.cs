using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.StudyPlan
{
    public class StudyPlanVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TenantId { get; set; }

        public static StudyPlanVm ToViewModel(MStudyPlan model)
        {
            return new StudyPlanVm
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                TenantId = model.TenantId
            };
        }

        public static List<StudyPlanVm> ToViewModelList(List<MStudyPlan> models)
        {
            return models.Select(ToViewModel).ToList();
        }
    }
}
