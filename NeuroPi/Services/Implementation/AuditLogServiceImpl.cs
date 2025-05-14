using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.AuditLog;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class AuditLogServiceImpl:IAuditLogService
    {
        private readonly NeuroPiDbContext _context;
        public AuditLogServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
            
        }

        public AuditLogResponseVM AddAuditLog(AuditRequestVM auditRequestVM)
        {
            var AuditModel=AuditRequestVM.ToModel(auditRequestVM);
            _context.AuditLogs.Add(AuditModel);
            int result=_context.SaveChanges();
            if (result > 0)
            {
                return new AuditLogResponseVM()
                {
                    Id=AuditModel.AuditId,
                    UserId=AuditModel.UserId,
                    TenantId=AuditModel.TenantId,
                    OldValues=AuditModel.OldValues,
                    NewValues=AuditModel.NewValues,
                    Entity=AuditModel.Entity,
                    Action=AuditModel.Action,
                    Timestamp=AuditModel.Timestamp,
                    RecordId =AuditModel.RecordId,
                };
            }
            return null;
            
        }


    }
}
