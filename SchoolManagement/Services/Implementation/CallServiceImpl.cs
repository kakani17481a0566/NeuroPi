using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Call;

namespace SchoolManagement.Services.Implementation
{
    public class CallServiceImpl:ICallService
    {
        private readonly SchoolManagementDb context;
        public CallServiceImpl(SchoolManagementDb _context)
        {
            context= _context;
            
        }

        public List<CallResponseVM> GetAllEmployeeLogs(int empId,int tenantId)
        {
            var result = context.Call.Where(e => e.ContactId == empId && e.TenantId == tenantId).Include(e=>e.Stage).Include(c=>c.Contact).ToList();
            if (result != null && result.Count() > 0)
            {
                return result.Select(c => new CallResponseVM()
                {
                    Id = c.Id,
                    ContactId = c.ContactId,
                    Contact = c.Contact.Name,
                    StageId = c.StageId,
                    Stage = c.Stage.Name,
                    AudioLink = c.AudioLink,
                    Remarks = c.Remarks,
                    TenantId = c.TenantId,

                }).ToList();
            }
            return null;
        }

        public List<CallResponseVM> GetAllLogs(int tenantId)
        {
            var result = context.Call.Where(e => e.TenantId == tenantId).Include(e => e.Stage).Include(c => c.Contact).ToList();
            if (result != null && result.Count() > 0)
            {
               return result.Select(c => new CallResponseVM()
                {
                    Id = c.Id,
                    ContactId = c.ContactId,
                    Contact = c.Contact.Name,
                    StageId = c.StageId,
                    Stage = c.Stage.Name,
                    AudioLink = c.AudioLink,
                    Remarks = c.Remarks,
                    TenantId = c.TenantId,

                }).ToList();
            }
            return null;
        }
    }
}
