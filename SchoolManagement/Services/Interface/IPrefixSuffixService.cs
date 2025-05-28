using SchoolManagement.ViewModel.PrefixSuffix;

namespace SchoolManagement.Services.Interface
{
    public interface IPrefixSuffixService
    {
        List<PrefixSuffixResponseVM> GetAllPrefixSuffix();

        List<PrefixSuffixResponseVM> GetAllPrefixSuffixByTenantId(int tenantId);

        PrefixSuffixResponseVM GetPrefixSuffixById(int id);

        PrefixSuffixResponseVM GetPrefixSuffixByIdAndTenantId(int id, int tenantId);

        PrefixSuffixResponseVM AddPrefixSuffix(PrefixSuffixRequestVM prefixSuffixAddVM);

        PrefixSuffixResponseVM UpdatePrefixSuffix(int id, int tenantId, PrefixSuffixUpdateVM prefixSuffix);

        bool DeletePrefixSuffix(int id, int tenantId);
    }
}
