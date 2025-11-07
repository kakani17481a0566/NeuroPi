using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserMealType
{
    public class UserMealTypeResponseVM
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MealTypeId { get; set; }

        public string PreferredMealTime { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static UserMealTypeResponseVM ToViewModel(MUserMealType model)
        {
            return new UserMealTypeResponseVM
            {
                Id = model.Id,
                UserId = model.UserId,
                MealTypeId = model.MealTypeId,
                PreferredMealTime = model.PreferredMealTime,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn,

            };
        }

        public static List<UserMealTypeResponseVM> ToViewModelList(List<MUserMealType> modelList)
        {
            return modelList.Select(model => ToViewModel(model)).ToList();
        }
    }
}