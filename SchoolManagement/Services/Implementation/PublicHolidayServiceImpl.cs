using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PublicHoliday;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class PublicHolidayServiceImpl : IPublicHolidayService
    {
        private readonly SchoolManagementDb _context;

        public PublicHolidayServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<PublicHolidayResponseVM> GetAll()
        {
            var data = _context.PublicHolidays.Where(h => !h.IsDeleted).ToList();
            return data.Count > 0 ? PublicHolidayResponseVM.ToViewModelList(data) : null;
        }

        public List<PublicHolidayResponseVM> GetAllByTenantId(int tenantId)
        {
            var data = _context.PublicHolidays.Where(h => h.TenantId == tenantId && !h.IsDeleted).ToList();
            return data.Count > 0 ? PublicHolidayResponseVM.ToViewModelList(data) : null;
        }

        public PublicHolidayResponseVM GetById(int id)
        {
            var holiday = _context.PublicHolidays.FirstOrDefault(h => h.Id == id && !h.IsDeleted);
            return holiday != null ? PublicHolidayResponseVM.ToViewModel(holiday) : null;
        }

        public PublicHolidayResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var holiday = _context.PublicHolidays.FirstOrDefault(h => h.Id == id && h.TenantId == tenantId && !h.IsDeleted);
            return holiday != null ? PublicHolidayResponseVM.ToViewModel(holiday) : null;
        }

        public PublicHolidayResponseVM CreatePublicHoliday(PublicHolidayRequestVM request)
        {
            var model = PublicHolidayRequestVM.ToModel(request);
            model.CreatedOn = DateTime.UtcNow;
            _context.PublicHolidays.Add(model);
            _context.SaveChanges();
            return PublicHolidayResponseVM.ToViewModel(model);
        }

        public PublicHolidayResponseVM UpdatePublicHoliday(int id, int tenantId, PublicHolidayRequestVM request)
        {
            var holiday = _context.PublicHolidays.FirstOrDefault(h => h.Id == id && h.TenantId == tenantId && !h.IsDeleted);
            if (holiday == null) return null;

            holiday.Name = request.Name;
            holiday.Date = request.Date;
            _context.SaveChanges();

            return PublicHolidayResponseVM.ToViewModel(holiday);
        }

        public PublicHolidayResponseVM DeleteById(int id, int tenantId)
        {
            var holiday = _context.PublicHolidays.FirstOrDefault(h => h.Id == id && h.TenantId == tenantId && !h.IsDeleted);
            if (holiday == null) return null;

            holiday.IsDeleted = true;
            holiday.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return PublicHolidayResponseVM.ToViewModel(holiday);
        }
    }
}
