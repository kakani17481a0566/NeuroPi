using NeuroPi.CommonLib.Model;

namespace NeuroPi.Nutrition.ViewModel.QrCode
{
    public class QrCodeRequestVM
    {
        public string Name { get; set; } // Guardian Name
        public string Gender { get; set; }
        public string Gmail { get; set; } // Email
        public string StudentName { get; set; }
        
        // New required fields
        public int StudentId { get; set; }
        public string ParentType { get; set; } = "OTHERS"; // Default
        public int TenantId { get; set; } 

        public static MCarpidum ToModel(QrCodeRequestVM qrCodeRequest)
        {
            return new MCarpidum
            {
                GuardianName = qrCodeRequest.Name, 
                Email = qrCodeRequest.Gmail,
                QrCode = Guid.NewGuid().ToString(),
                TenantId = qrCodeRequest.TenantId,
                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
                StudentId = qrCodeRequest.StudentId,
                ParentType = qrCodeRequest.ParentType
            };
        }
    }
}
