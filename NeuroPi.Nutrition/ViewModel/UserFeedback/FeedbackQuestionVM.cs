using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserFeedback
{
    public class FeedbackQuestionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }   // question text
        public int MastersTypeId { get; set; }
        public int TenantId { get; set; }

        public string TargetDate { get; set; }   // 🔥 ADD THIS (YYYY-MM-DD)

        public static FeedbackQuestionVM FromMaster(MNutritionMaster master, DateOnly targetDate)
        {
            if (master == null) return null;

            return new FeedbackQuestionVM
            {
                Id = master.Id,
                Name = master.Name,
                MastersTypeId = master.NutritionMasterTypeId,
                TenantId = master.TenantId,
                TargetDate = targetDate.ToString("yyyy-MM-dd")   // 🔥 SET YESTERDAY
            };
        }

        public static List<FeedbackQuestionVM> FromMasterList(List<MNutritionMaster> list, DateOnly targetDate)
        {
            return list.Select(x => FromMaster(x, targetDate)).ToList();
        }
    }
}
