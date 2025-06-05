using NeuroPi.UserManagment.Model;

namespace SchoolManagement.ViewModel.PrefixSuffix
{
    public class PrefixSuffixResponseVM
    {
        public int Id { get; set; }
        public string? Prefix { get; set; } 
        public string? Suffix { get; set; }
        public int Length { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static PrefixSuffixResponseVM ToViewModel(MPrefixSuffix prefixSuffix)
        {
            return new PrefixSuffixResponseVM
            {
                Id = prefixSuffix.Id,
                Prefix = prefixSuffix.Prefix,
                Suffix = prefixSuffix.Suffix,
                Length = prefixSuffix.Length,
                TenantId = prefixSuffix.TenantId,
                CreatedBy = prefixSuffix.CreatedBy,
                CreatedOn = prefixSuffix.CreatedOn,
                UpdatedBy = prefixSuffix.UpdatedBy,
                UpdatedOn = prefixSuffix.UpdatedOn
            };
        }
       public static List<PrefixSuffixResponseVM> ToViewModelList(List<MPrefixSuffix> prefixSuffixList)
        {
            List<PrefixSuffixResponseVM> result = new List<PrefixSuffixResponseVM>();
            foreach (var prefixSuffix in prefixSuffixList)
            {
                result.Add(ToViewModel(prefixSuffix));
            }
            return result;
        }
    }
}