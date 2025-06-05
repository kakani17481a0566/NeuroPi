using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Assessment
{
    public class AssessmentResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int TopicId { get; set; }

        public int AssessmentSkillId { get; set; }

        public int TenantId { get; set; }

        public int createdBy { get; set; }

        public DateTime createdOn { get; set; }

        public int? updatedBy { get; set; }
        public DateTime? updatedOn { get; set; }



        public static AssessmentResponseVM ToViewModel(MAssessment assessment)
        {
            return new AssessmentResponseVM
            {
                Id = assessment.Id,
                Name = assessment.Name,
                Description = assessment.Description,
                TopicId = assessment.TopicId,
                AssessmentSkillId = assessment.AssessmentSkillId,
                TenantId = assessment.TenantId,
                createdBy = assessment.CreatedBy,
                createdOn = assessment.CreatedOn,
                updatedBy = assessment.UpdatedBy,
                updatedOn = DateTime.UtcNow
            };
        }

        public static List<AssessmentResponseVM> ToViewModelList(List<MAssessment> assessmentList)
        {
            List<AssessmentResponseVM> result = new List<AssessmentResponseVM>();
            foreach (var assessment in assessmentList)
            {
                result.Add(ToViewModel(assessment));
            }
            return result;

        }
    }
}
