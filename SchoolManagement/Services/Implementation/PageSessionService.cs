using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.ViewModel.PageSession;

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

        public List<PageSessionLogDto> GetSessionLogs()
        {
            var query = from p in _db.PageSessionLogs
                        join u in _db.Users on p.user_id equals u.UserId into userJoin
                        from u in userJoin.DefaultIfEmpty()
                        orderby p.page_open_time descending
                        select new PageSessionLogDto
                        {
                            Id = p.id,
                            IpAddress = p.ip_address,
                            PageName = p.page_name,
                            FirstName = u != null ? u.FirstName : null,
                            LastName = u != null ? u.LastName : null,
                            PageOpenTime = p.page_open_time,
                            PageCloseTime = p.page_close_time,
                            TimeSpentSeconds = p.page_close_time.HasValue 
                                ? (p.page_close_time.Value - p.page_open_time).TotalSeconds 
                                : (double?)null
                        };

            return query.ToList();
        }
    }
}
