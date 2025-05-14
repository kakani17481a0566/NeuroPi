using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.ViewModel.AuditLog;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IAuditLogService
    {
        AuditLogResponseVM AddAuditLog(AuditRequestVM auditRequestVM);
    }
}
