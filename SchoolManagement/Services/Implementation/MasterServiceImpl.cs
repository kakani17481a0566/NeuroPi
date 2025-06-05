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

        // Gets all master types that are not deleted
        // Developed by: Vardhan
        public List<MasterResponseVM> GetAll()

        {
            var result = _context.Masters.Include(m => m.MasterType).Where(m => !m.IsDeleted).ToList();
            if (result != null && result.Count() > 0)
            {
                return MasterResponseVM.ToViewModelList(result);
            }
            return null;
        }

        // Gets a master type by its ID
        // Developed by: Vardhan
        public MasterResponseVM GetById(int id)
        {
            var result = _context.Masters.Include(m=>m.MasterType).FirstOrDefault(m => m.Id == id && !m.IsDeleted);
            if (result != null)
            {
                return MasterResponseVM.ToViewModel(result);
            }
            return null;
        }
        // Gets all master types for a specific tenant that are not deleted
        // Developed by: Vardhan
        public List<MasterResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = _context.Masters.Where(m => m.TenantId == tenantId && !m.IsDeleted).Include(m=>m.MasterType).ToList();
            if (result != null)
            {
                return MasterResponseVM.ToViewModelList(result);
            }
            return null;

        }
        // Gets a master type by its ID and tenant ID
        // Developed by: Vardhan
        public MasterResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _context.Masters.Include(m=>m.MasterType).FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (result != null)
            {
                return MasterResponseVM.ToViewModel(result);
            }
            return null;
        }
        // Creates a new master type and saves it to the database
        // Developed by: Vardhan
        public MasterResponseVM CreateMasterType(MasterRequestVM masterType)
        {
            var masterTypeModel = MasterRequestVM.ToModel(masterType);
            masterTypeModel.CreatedOn = DateTime.UtcNow;
            _context.Masters.Add(masterTypeModel);
            _context.SaveChanges();
            return MasterResponseVM.ToViewModel(masterTypeModel);

        }
        // Updates an existing master type by its ID and tenant ID
        // Developed by: Vardhan
        public MasterResponseVM UpdateMasterType(int id, int tenantId, MasterUpdateVM request)
        {
            var masterType = _context.Masters.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (masterType != null)
            {
                masterType.Name = request.Name;
                masterType.MasterTypeId = request.MasterTypeId;
                masterType.UpdatedBy = request.UpdatedBy;
                masterType.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return MasterResponseVM.ToViewModel(masterType);
            }
            return null;
        }
        // Deletes a master type by marking it as deleted
        // Developed by: Vardhan
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

        // Gets all master types by their master type ID for a specific tenant that are not deleted
        // Developed by: Vardhan
        public List<MasterResponseVM> GetAllByMasterTypeId(int id,int tenantId)
        {
            var result=_context.Masters.Where(m=>m.MasterTypeId == id && !m.IsDeleted && m.TenantId==tenantId).ToList();
            if(result!=null && result.Count>0)
            {
                return MasterResponseVM.ToViewModelList(result);
            }
            return null;
        }
    }
}
