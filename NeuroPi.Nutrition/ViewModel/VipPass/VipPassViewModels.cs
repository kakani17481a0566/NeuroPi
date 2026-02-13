using System;

namespace NeuroPi.Nutrition.ViewModel.VipPass
{
    public class VipBulkPassRequestVM
    {
        public string VipName { get; set; }
        public string VipEmail { get; set; }
        public string? VipPhone { get; set; }
        public int PassCount { get; set; } = 1;
        public bool SendEmail { get; set; } = true;
    }

    public class VipValidationResponseVM
    {
        public string Status { get; set; } // "Valid", "AlreadyUsed", "NotFound", "Error"
        public string VipName { get; set; }
        public string VipEmail { get; set; }
        public DateTime? ValidationTime { get; set; }
        public string Message { get; set; }
    }
}
