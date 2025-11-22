using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserFeedback;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class UserFeedbackServiceImpl : IUserFeedback
    {
        private readonly NeutritionDbContext _context;

        public UserFeedbackServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        /* -------------------------------------------------------------
           Helper: Convert Model → ResponseVM
        ------------------------------------------------------------- */
        private static UserFeedbackResponseVM ToResponseVM(MUserFeedback model)
        {
            if (model == null) return null;

            return new UserFeedbackResponseVM
            {
                Id = model.Id,
                UserId = model.UserId,
                FeedbackTypeId = model.FeedbackTypeId,
                FeedbackTypeName = model.FeedbackType?.Name,
                FeedbackText = model.FeedbackText,
                Date = model.Date,
                TenantId = model.TenantId,
                CreatedOn = model.CreatedOn,
                CreatedBy = model.CreatedBy,
                UpdatedOn = model.UpdatedOn,
                UpdatedBy = model.UpdatedBy
            };
        }

        /* -------------------------------------------------------------
           CREATE
        ------------------------------------------------------------- */
        public UserFeedbackResponseVM CreateUserFeedback(UserFeedbackRequestVM vm)
        {
            var model = UserFeedbackRequestVM.ToModel(vm);

            _context.UserFeedback.Add(model);
            _context.SaveChanges();

            // Load feedback type name
            _context.Entry(model).Reference(x => x.FeedbackType).Load();

            return ToResponseVM(model);
        }

        /* -------------------------------------------------------------
           UPDATE
        ------------------------------------------------------------- */
        public UserFeedbackResponseVM UpdateUserFeedback(int id, int tenantId, UserFeedbackUpdateVM vm)
        {
            var existing = _context.UserFeedback
                .Include(f => f.FeedbackType)
                .FirstOrDefault(f => f.Id == id && f.TenantId == tenantId && !f.IsDeleted);

            if (existing == null)
                return null;

            vm.UpdateModel(existing);

            _context.SaveChanges();

            return ToResponseVM(existing);
        }

        /* -------------------------------------------------------------
           DELETE (soft delete)
        ------------------------------------------------------------- */
        public bool DeleteUserFeedback(int id, int tenantId)
        {
            var entity = _context.UserFeedback
                .FirstOrDefault(f => f.Id == id && f.TenantId == tenantId && !f.IsDeleted);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        /* -------------------------------------------------------------
           GET ALL
        ------------------------------------------------------------- */
        public List<UserFeedbackResponseVM> GetAllUserFeedbacks()
        {
            return _context.UserFeedback
                .Where(f => !f.IsDeleted)
                .Include(f => f.FeedbackType)
                .Select(f => ToResponseVM(f))
                .ToList();
        }

        /* -------------------------------------------------------------
           GET BY ID
        ------------------------------------------------------------- */
        public UserFeedbackResponseVM GetUserFeedbackById(int id)
        {
            var entity = _context.UserFeedback
                .Include(f => f.FeedbackType)
                .FirstOrDefault(f => f.Id == id && !f.IsDeleted);

            return ToResponseVM(entity);
        }

        /* -------------------------------------------------------------
           GET BY ID + TENANT
        ------------------------------------------------------------- */
        public UserFeedbackResponseVM GetUserFeedbackByIdAndTenantId(int id, int tenantId)
        {
            var entity = _context.UserFeedback
                .Include(f => f.FeedbackType)
                .FirstOrDefault(f => f.Id == id && f.TenantId == tenantId && !f.IsDeleted);

            return ToResponseVM(entity);
        }

        /* -------------------------------------------------------------
           GET ALL BY TENANT
        ------------------------------------------------------------- */
        public List<UserFeedbackResponseVM> GetUserFeedbacksByTenantId(int tenantId)
        {
            return _context.UserFeedback
                .Where(f => f.TenantId == tenantId && !f.IsDeleted)
                .Include(f => f.FeedbackType)
                .Select(f => ToResponseVM(f))
                .ToList();
        }

        public List<FeedbackQuestionVM> GetFeedbackQuestions(int userId, int tenantId)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var yesterday = today.AddDays(-1);

            bool hasSubmitted = _context.UserFeedback
                .Any(f => f.UserId == userId
                       && f.TenantId == tenantId
                       && f.Date == yesterday
                       && !f.IsDeleted);

            if (hasSubmitted)
                return new List<FeedbackQuestionVM>();

            var masterType = _context.NutritionMasterTypes
                .FirstOrDefault(t => t.Name.ToLower() == "previous_day_feedback_type"
                                  && t.TenantId == tenantId
                                  && !t.IsDeleted);

            if (masterType == null)
                return new List<FeedbackQuestionVM>();

            var masters = _context.NutritionMasters
                .Where(m => m.NutritionMasterTypeId == masterType.Id
                         && m.TenantId == tenantId
                         && !m.IsDeleted)
                .ToList();

            return FeedbackQuestionVM.FromMasterList(masters, yesterday);  // 🔥 PASS DATE
        }



        public bool SaveUserFeedback(int userId, int tenantId, SaveFeedbackVM model)
        {
            if (!DateOnly.TryParse(model.Date, out var parsedDate))
                return false;

            var existing = _context.UserFeedback
                .FirstOrDefault(f =>
                    f.UserId == userId &&
                    f.TenantId == tenantId &&
                    f.FeedbackTypeId == model.FeedbackTypeId &&
                    f.Date == parsedDate &&
                    !f.IsDeleted
                );

            if (existing != null)
                return false;

            var entity = new MUserFeedback
            {
                UserId = userId,
                TenantId = tenantId,
                FeedbackTypeId = model.FeedbackTypeId,
                FeedbackText = model.FeedbackText,
                Date = parsedDate,        // safely parsed
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.UserFeedback.Add(entity);
            _context.SaveChanges();
            return true;
        }



    }
}
