namespace SchoolManagement.ViewModel.PrefixSuffix
{
    public class PrefixSuffixUpdateVM
    {
       
        public string? Prefix { get; set; } = null!;
        public string? Suffix { get; set; }
        public int Length { get; set; }

        
        public int UpdatedBy { get; set; }

    }
}
