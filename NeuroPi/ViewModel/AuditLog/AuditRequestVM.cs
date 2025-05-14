

using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.AuditLog
{
    public class AuditRequestVM
    {

       
        public int? UserId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public int RecordId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public int TenantId { get; set; }

        public static MAuditLog ToModel(AuditRequestVM requestVM)
        {
            return new MAuditLog()
            {
                UserId = requestVM.UserId,
                Action = requestVM.Action,
                Entity = requestVM.Entity,
                RecordId = requestVM.RecordId,
                OldValues = requestVM.OldValues,
                NewValues = requestVM.NewValues,
                Timestamp = requestVM.Timestamp,
                TenantId = requestVM.TenantId

            };
        }

    }
}
