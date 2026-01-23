using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using System;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class PageSessionService : IPageSessionService
    {
        private readonly SchoolManagementDb _db;

        public PageSessionService(SchoolManagementDb db)
        {
            _db = db;
        }

        public long StartSession(string pageName, int? userId, int? tenantId, string ipAddress)
        {
            var session = new MPageSessionLog
            {
                page_name = pageName,
                user_id = userId,
                tenant_id = tenantId,
                ip_address = ipAddress,
                page_open_time = DateTime.UtcNow
            };

            _db.PageSessionLogs.Add(session);
            _db.SaveChanges();

            return session.id;
        }

        public bool EndSession(long sessionId)
        {
            var session = _db.PageSessionLogs.FirstOrDefault(s => s.id == sessionId);
            if (session != null)
            {
                session.page_close_time = DateTime.UtcNow;
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
