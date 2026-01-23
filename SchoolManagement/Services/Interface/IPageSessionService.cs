using System.Threading.Tasks;

namespace SchoolManagement.Services.Interface
{
    public interface IPageSessionService
    {
        long StartSession(string pageName, int? userId, int? tenantId, string ipAddress);
        bool EndSession(long sessionId);
    }
}
