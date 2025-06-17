using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TimeTableAssessment
{
    public class TimeTableAssessmentResponseVM
    {
        public int Id { get; set; }
        public int TimeTableId { get; set; }
        public int AssessmentId { get; set; }

        public string Status { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public static TimeTableAssessmentResponseVM ToViewModel(MTimeTableAssessment model)
        {
            if (model == null) return null;
            return new TimeTableAssessmentResponseVM
            {
                Id = model.Id,
                TimeTableId = model.TimeTableId,
                AssessmentId = model.AssessmentId,
                Status = model.Status,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<TimeTableAssessmentResponseVM> ToViewModelList(List<MTimeTableAssessment> models)
        {
            if (models == null || !models.Any()) return new List<TimeTableAssessmentResponseVM>();
            return models.Select(ToViewModel).ToList();
        }
    }
}
