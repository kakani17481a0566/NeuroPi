using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.StudyPlanSteps
{
    public class StudyPlanStepsVm
    {
        public int Id { get; set; }
        public int StudyPlanId { get; set; }
        public int? SeqOrd { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TenantId { get; set; }

        public static StudyPlanStepsVm ToViewModel(MStudyPlanSteps model)
        {
            return new StudyPlanStepsVm
            {
                Id = model.Id,
                StudyPlanId = model.StudyPlanId,
                SeqOrd = model.SeqOrd,
                Name = model.Name,
                Description = model.Description,
                TenantId = model.TenantId
            };
        }

        public static List<StudyPlanStepsVm> ToViewModelList(List<MStudyPlanSteps> models)
        {
            return models.Select(ToViewModel).ToList();
        }
    }
}
