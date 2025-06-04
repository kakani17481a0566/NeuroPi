using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Master;
using SchoolManagement.ViewModel.PublicHoliday;

namespace SchoolManagement.Services.Implementation
{
    public class PublicHolidayServiceImpl:IPublicHolidayService
    {
        private readonly SchoolManagementDb _context;
        public PublicHolidayServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public PublicHolidayResponseVM CreatePublicHoliday(PublicHolidayRequestVM publicHolidayRequest)
        {
            var publicHolidayModel = PublicHolidayRequestVM.ToModel(publicHolidayRequest);
            publicHolidayModel.CreatedOn = DateTime.UtcNow;
            _context.PublicHolidays.Add(publicHolidayModel);
            _context.SaveChanges();
            return PublicHolidayResponseVM.ToViewModel(publicHolidayModel);
        }

        public PublicHolidayResponseVM DeleteById(int id, int tenantId)
        {
            var publicHoliday = _context.PublicHolidays.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (publicHoliday != null)
            {
                publicHoliday.IsDeleted = true;
                _context.SaveChanges();
                return PublicHolidayResponseVM.ToViewModel(publicHoliday);
            }
            return null;
        }

        public List<PublicHolidayResponseVM> GetAll()
        {
            var result= _context.PublicHolidays.Where(h=>!h.IsDeleted).ToList();
            if (result != null && result.Count() > 0)
            {
                return PublicHolidayResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public List<PublicHolidayResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = _context.PublicHolidays.Where(p => p.TenantId == tenantId).ToList();
            if (result != null && result.Count() > 0)
            {
                return PublicHolidayResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public PublicHolidayResponseVM GetById(int id)
        {
            var result = _context.PublicHolidays.FirstOrDefault(m => m.Id == id && !m.IsDeleted);
            if (result != null)
            {
                return PublicHolidayResponseVM.ToViewModel(result);
            }
            return null;
        }

        public PublicHolidayResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _context.PublicHolidays.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (result != null)
            {
                return PublicHolidayResponseVM.ToViewModel(result);
            }
            return null;
        }



        public PublicHolidayResponseVM UpdatePublicHoliday(int id, int tenantId, PublicHolidayRequestVM  publicHolidayRequest)
        {
            var publicHoliday = _context.PublicHolidays.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (publicHoliday != null)
            {
                publicHoliday.Name = publicHolidayRequest.Name;
                publicHoliday.TenantId = tenantId;
                publicHoliday.Date= publicHolidayRequest.Date;
                _context.SaveChanges();
                return PublicHolidayResponseVM.ToViewModel(publicHoliday);
            }
            return null;
        }
    }
}
