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
    }
}
