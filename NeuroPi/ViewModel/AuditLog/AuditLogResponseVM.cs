using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.AuditLog
{
    public class AuditLogResponseVM
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public int RecordId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public int TenantId { get; set; }

        public static AuditLogResponseVM ToViewModel(MAuditLog auditLog)
        {
            return new AuditLogResponseVM
            {
                UserId = auditLog.UserId,
                Action = auditLog.Action,
                Entity = auditLog.Entity,
                NewValues = auditLog.NewValues,
                Timestamp = auditLog.Timestamp,
                TenantId = auditLog.TenantId,
                RecordId = auditLog.RecordId,
                OldValues = auditLog.OldValues,
            };
        }

        public static List<AuditLogResponseVM> ToViewModelList(List<MAuditLog> auditLogList)
        {
            List<AuditLogResponseVM> result = new List<AuditLogResponseVM>();
            foreach (var auditLog in auditLogList)
            {
                result.Add(ToViewModel(auditLog));
            }
            return result;
        }
    }
}
