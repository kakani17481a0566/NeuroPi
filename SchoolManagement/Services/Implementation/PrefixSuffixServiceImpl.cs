using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PrefixSuffix;

namespace SchoolManagement.Services.Implementation
{
    public class PrefixSuffixServiceImpl : IPrefixSuffixService
    {
        private readonly SchoolManagementDb _dbContext;
        public PrefixSuffixServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public PrefixSuffixResponseVM AddPrefixSuffix(PrefixSuffixRequestVM prefixSuffixAddVM)
        {
            var newPrefixSuffix = PrefixSuffixRequestVM.ToModel(prefixSuffixAddVM);
            newPrefixSuffix.CreatedOn = DateTime.UtcNow;
            _dbContext.PrefixSuffix.Add(newPrefixSuffix);
            _dbContext.SaveChanges();
            return PrefixSuffixResponseVM.ToViewModel(newPrefixSuffix);

        }

        public bool DeletePrefixSuffix(int id, int tenantId)
        {
            var prefixSuffix = _dbContext.PrefixSuffix
                .FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            if (prefixSuffix == null)
            {
                return false;
            }
            prefixSuffix.IsDeleted = true;
            prefixSuffix.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;

        }

        public List<PrefixSuffixResponseVM> GetAllPrefixSuffix()
        {
            return PrefixSuffixResponseVM.ToViewModelList(_dbContext.PrefixSuffix
                .Where(p => !p.IsDeleted)
                .ToList()
                );

        }

        public List<PrefixSuffixResponseVM> GetAllPrefixSuffixByTenantId(int tenantId)
        {
            return PrefixSuffixResponseVM.ToViewModelList(_dbContext.PrefixSuffix
                .Where(p => p.TenantId == tenantId && !p.IsDeleted)
                .ToList());
        }

        public PrefixSuffixResponseVM GetPrefixSuffixById(int id)
        {
            var prefixSufffix = _dbContext.PrefixSuffix
                .FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (prefixSufffix == null)
            {
                return null;
            }
            return PrefixSuffixResponseVM.ToViewModel(prefixSufffix);
        }

        public PrefixSuffixResponseVM GetPrefixSuffixByIdAndTenantId(int id, int tenantId)
        {
            var prefixSuffix = _dbContext.PrefixSuffix
                .FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            if (prefixSuffix == null)
            {
                return null;
            }
            return PrefixSuffixResponseVM.ToViewModel(prefixSuffix);

        }

        public PrefixSuffixResponseVM UpdatePrefixSuffix(int id, int tenantId, PrefixSuffixUpdateVM prefixSuffix)
        {
            var ExistingPrefixSuffix = _dbContext.PrefixSuffix
                .FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            if (ExistingPrefixSuffix == null)
            {
                return null;
            }
            ExistingPrefixSuffix.Prefix = prefixSuffix.Prefix;
            ExistingPrefixSuffix.Suffix = prefixSuffix.Suffix;
            ExistingPrefixSuffix.Length = prefixSuffix.Length;
            ExistingPrefixSuffix.UpdatedBy = prefixSuffix.UpdatedBy;
            ExistingPrefixSuffix.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return PrefixSuffixResponseVM.ToViewModel(ExistingPrefixSuffix);


        }
    }
}
