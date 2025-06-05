using NeuroPi.UserManagment.Model;

namespace SchoolManagement.ViewModel.PrefixSuffix
{
    public class PrefixSuffixRequestVM
    {
        public  string? prefix { get; set; }

        public string? suffix { get; set; }

        public int length { get; set; }

        public int tenantId { get; set; }

        public int createdBy { get; set; }


        public static MPrefixSuffix ToModel(PrefixSuffixRequestVM requestVM)
        {
            return new MPrefixSuffix()
            {
                Prefix = requestVM.prefix,
                Suffix = requestVM.suffix,
                Length = requestVM.length,
                TenantId = requestVM.tenantId,
                CreatedBy = requestVM.createdBy,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
