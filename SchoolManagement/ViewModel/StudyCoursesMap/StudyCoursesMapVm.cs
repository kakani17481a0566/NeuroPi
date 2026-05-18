using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.StudyCoursesMap
{
    public class StudyCoursesMapVm
    {
        public int Id { get; set; }
        public int StudyPlanId { get; set; }
        public int StudyCoursesId { get; set; }
        public int? SeqOrd { get; set; }
        public int TenantId { get; set; }

        public static StudyCoursesMapVm ToViewModel(MStudyCoursesMap model)
        {
            return new StudyCoursesMapVm
            {
                Id = model.Id,
                StudyPlanId = model.StudyPlanId,
                StudyCoursesId = model.StudyCoursesId,
                SeqOrd = model.SeqOrd,
                TenantId = model.TenantId
            };
        }

        public static List<StudyCoursesMapVm> ToViewModelList(List<MStudyCoursesMap> models)
        {
            return models.Select(ToViewModel).ToList();
        }
    }
}
