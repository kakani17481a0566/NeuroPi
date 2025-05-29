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
        //Gets all master types that are not deleted
        // Developed by: Sai Vardhan
        public List<MasterTypeResponseVM> GetAll()
        {
            var result=_context.MasterTypes.Where(m=>!m.IsDeleted).ToList();
            if (result != null && result.Count() > 0)
            {
                return MasterTypeResponseVM.ToViewModelList(result);
            }
            return null;
        }

        // Gets a master type by its ID that is not deleted
        // Developed by: Sai Vardhan
        public MasterTypeResponseVM GetById(int id)
        {
            var result=_context.MasterTypes.Where(m=>m.Id == id && !m.IsDeleted).FirstOrDefault();
            if (result != null)
            {
                return MasterTypeResponseVM.ToViewModel(result);
            }
            return null;
        }
        // Gets all master types by tenant ID that are not deleted
        // Developed by: Sai Vardhan
        public List<MasterTypeResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = _context.MasterTypes.Where(m => m.TenantId == tenantId && !m.IsDeleted).ToList();
            if(result!=null)
            {
                return MasterTypeResponseVM.ToViewModelList(result);
            }
            return null;

        }

        // Gets a master type by its ID and tenant ID that is not deleted
        // Developed by: Sai Vardhan
        public MasterTypeResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _context.MasterTypes.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (result != null)
            {
                return MasterTypeResponseVM.ToViewModel(result);
            }
            return null;
        }

        // Creates a new master type and saves it to the database
        // Developed by: Sai Vardhan
        public MasterTypeResponseVM CreateMasterType(MasterTypeRequestVM masterType)
        {
            var masterTypeModel = MasterTypeRequestVM.ToModel(masterType);
            masterTypeModel.CreatedOn = DateTime.UtcNow;
            _context.MasterTypes.Add(masterTypeModel);
            _context.SaveChanges();
            return MasterTypeResponseVM.ToViewModel(masterTypeModel);

        }
        // Updates an existing master type by its ID and tenant ID
        // Developed by: Sai Vardhan
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

        // Deletes a master type by its ID and tenant ID, marking it as deleted
        // Developed by: Sai Vardhan
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
