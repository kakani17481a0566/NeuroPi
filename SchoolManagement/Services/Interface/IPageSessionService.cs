using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.ViewModel.PageSession;

namespace SchoolManagement.Services.Interface
{
    public interface IPageSessionService
    {
        long StartSession(string pageName, int? userId, int? tenantId, string ipAddress);
        bool EndSession(long sessionId);
        List<PageSessionLogDto> GetSessionLogs();
    }
}
