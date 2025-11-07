using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserMealType
{
    public class UserMealTypeRequestVM
    {
        public int UserId { get; set; }

        public int MealTypeId { get; set; }

        public string PreferredMealTime { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MUserMealType ToModel(UserMealTypeRequestVM viewModel)
        {
            return new MUserMealType
            {
                UserId = viewModel.UserId,
                MealTypeId = viewModel.MealTypeId,
                PreferredMealTime = viewModel.PreferredMealTime,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
            };
        }
    }
}
