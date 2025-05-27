using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Master;
using SchoolManagement.ViewModel.MasterType;

namespace SchoolManagement.Services.Implementation
{
    //sai vardhan
    public class MasterTypeServiceImpl:IMasterTypeService
    {
        private readonly SchoolManagementDb _context;
        public MasterTypeServiceImpl(SchoolManagementDb context)
        {
            _context = context;
            
        }
        public List<MasterTypeResponseVM> GetAll()
        {
            var result=_context.MasterTypes.Where(m=>!m.IsDeleted).ToList();
            if (result != null && result.Count() > 0)
            {
                return MasterTypeResponseVM.ToViewModelList(result);
            }
            return null;
        }


        public MasterTypeResponseVM GetById(int id)
        {
            var result=_context.MasterTypes.Where(m=>m.Id == id && !m.IsDeleted).FirstOrDefault();
            if (result != null)
            {
                return MasterTypeResponseVM.ToViewModel(result);
            }
            return null;
        }
        public List<MasterTypeResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = _context.MasterTypes.Where(m => m.TenantId == tenantId && !m.IsDeleted).ToList();
            if(result!=null)
            {
                return MasterTypeResponseVM.ToViewModelList(result);
            }
            return null;

        }

        public MasterTypeResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _context.MasterTypes.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (result != null)
            {
                return MasterTypeResponseVM.ToViewModel(result);
            }
            return null;
        }
        public MasterTypeResponseVM CreateMasterType(MasterTypeRequestVM masterType)
        {
            var masterTypeModel = MasterTypeRequestVM.ToModel(masterType);
            masterTypeModel.CreatedOn = DateTime.UtcNow;
            _context.MasterTypes.Add(masterTypeModel);
            _context.SaveChanges();
            return MasterTypeResponseVM.ToViewModel(masterTypeModel);

        }
        public MasterTypeResponseVM UpdateMasterType(int id, int tenantId, MasterTypeUpdateVM request)
        {
            var masterType = _context.MasterTypes.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (masterType != null)
            {
                masterType.Name = request.Name;
                masterType.UpdatedBy = request.UpdatedBy;
                masterType.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return MasterTypeResponseVM.ToViewModel(masterType);
            }
            return null;
        }
        public MasterTypeResponseVM DeleteById(int id, int tenantId)
        {
            var masterType = _context.MasterTypes.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (masterType != null)
            {
                masterType.IsDeleted = true;
                _context.SaveChanges();
                return MasterTypeResponseVM.ToViewModel(masterType);
            }
            return null;

        }

    }
}
