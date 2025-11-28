using System;
using System.Linq;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserGamesStatus;
using NeuroPi.CommonLib.Model.Nutrition; // contains MUserGamesStatus + NutritionMasters

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class UserGamesStatusesImpl : IUserGamesStatuses
    {
        private readonly NeutritionDbContext _context;

        public UserGamesStatusesImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        // =========================================================
        //  SAVE (INSERT / UPDATE)
        // =========================================================
        public UserGameStatusVM SaveUserGameStatus(SaveUserGameStatusVM model)
        {
            // Normalize activity date to UTC-only date
            model.ActivityDate = DateTime.SpecifyKind(model.ActivityDate.Date, DateTimeKind.Utc);

            var existing = _context.UserGamesStatuses
                .FirstOrDefault(x =>
                    x.UserId == model.UserId &&
                    x.NutTimetableId == model.TimetableId &&
                    x.ActivityDate == model.ActivityDate &&
                    x.TenantId == model.TenantId
                );

            if (existing == null)
            {
                var row = new MUserGamesStatus
                {
                    UserId = model.UserId,
                    NutTimetableId = model.TimetableId,
                    ActivityDate = model.ActivityDate,
                    StatusId = model.StatusId,
                    RecordingUrl = model.RecordingUrl,
                    TenantId = model.TenantId,

                    // FIX: Always use UTC
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = model.UserId
                };

                _context.UserGamesStatuses.Add(row);
                _context.SaveChanges();

                return MapToVm(row);
            }
            else
            {
                // FIX: Always use UTC
                existing.UpdatedOn = DateTime.UtcNow;
                existing.UpdatedBy = model.UserId;

                existing.StatusId = model.StatusId;
                existing.RecordingUrl = model.RecordingUrl;

                _context.SaveChanges();

                return MapToVm(existing);
            }
        }





        // =========================================================
        //  GET (FOR UI)
        // =========================================================
        public UserGameStatusVM? GetUserGameStatus(int userId, int timetableId, int tenantId)
        {
            var existing = _context.UserGamesStatuses
                .FirstOrDefault(x =>
                    x.UserId == userId &&
                    x.NutTimetableId == timetableId &&
                    x.TenantId == tenantId
                );

            if (existing == null)
                return null;

            return MapToVm(existing);
        }

        // =========================================================
        //  HELPER MAP ENTITY → VM
        // =========================================================
        private UserGameStatusVM MapToVm(MUserGamesStatus entity)
        {
            var statusId = entity.StatusId ?? 0;

            return new UserGameStatusVM
            {
                Id = entity.Id,
                UserId = entity.UserId ?? 0,
                TimetableId = entity.NutTimetableId ?? 0,
                ActivityDate = entity.ActivityDate ?? DateTime.MinValue,
                StatusId = statusId,
                StatusCode = GetStatusCode(statusId),
                StatusName = GetStatusName(statusId),
                RecordingUrl = entity.RecordingUrl
            };
        }

        // =========================================================
        //  STATUS LOOKUP HELPERS
        // =========================================================
        private string? GetStatusCode(int? statusId)
        {
            if (statusId == null || statusId == 0) return null;

            return _context.NutritionMasters
                .Where(x => x.Id == statusId)
                .Select(x => x.Code)
                .FirstOrDefault();
        }

        private string? GetStatusName(int? statusId)
        {
            if (statusId == null || statusId == 0) return null;

            return _context.NutritionMasters
                .Where(x => x.Id == statusId)
                .Select(x => x.Name)
                .FirstOrDefault();
        }
    }
}
