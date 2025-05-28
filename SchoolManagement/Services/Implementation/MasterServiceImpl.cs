using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Master;




namespace SchoolManagement.Services.Implementation
{
    public class MasterServiceImpl:IMasterService
    {
        private readonly SchoolManagementDb _context;

        public MasterServiceImpl(SchoolManagementDb context)
        {
            _context = context;
            
        }

        public List<MasterResponseVM> GetAll()

        {
            var result = _context.Masters.Include(m => m.MasterType).Where(m => !m.IsDeleted).ToList();
            if (result != null && result.Count() > 0)
            {
                return MasterResponseVM.ToViewModelList(result);
            }
            return null;
        }


        public MasterResponseVM GetById(int id)
        {
            var result = _context.Masters.Where(m => m.Id == id && !m.IsDeleted).FirstOrDefault();
            if (result != null)
            {
                return MasterResponseVM.ToViewModel(result);
            }
            return null;
        }
        public List<MasterResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = _context.Masters.Where(m => m.TenantId == tenantId && !m.IsDeleted).ToList();
            if (result != null)
            {
                return MasterResponseVM.ToViewModelList(result);
            }
            return null;

        }

        public MasterResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _context.Masters.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (result != null)
            {
                return MasterResponseVM.ToViewModel(result);
            }
            return null;
        }
        public MasterResponseVM CreateMasterType(MasterRequestVM masterType)
        {
            var masterTypeModel = MasterRequestVM.ToModel(masterType);
            masterTypeModel.CreatedOn = DateTime.UtcNow;
            _context.Masters.Add(masterTypeModel);
            _context.SaveChanges();
            return MasterResponseVM.ToViewModel(masterTypeModel);

        }
        public MasterResponseVM UpdateMasterType(int id, int tenantId, MasterRequestVM request)
        {
            var masterType = _context.Masters.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (masterType != null)
            {
                masterType.Name = request.Name;
                //masterType.UpdatedBy = request.UpdatedBy;
                masterType.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return MasterResponseVM.ToViewModel(masterType);
            }
            return null;
        }
        public MasterResponseVM DeleteById(int id, int tenantId)
        {
            var masterType = _context.Masters.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (masterType != null)
            {
                masterType.IsDeleted = true;
                _context.SaveChanges();
                return MasterResponseVM.ToViewModel(masterType);
            }
            return null;

        }


    }
}
