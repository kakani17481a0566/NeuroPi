using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Parent;

namespace SchoolManagement.Services.Implementation
{
    public class ParentServiceImpl : IParentService
    {
        public readonly SchoolManagementDb _context;
        public ParentServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ParentResponseVM AddParent(ParentRequestVM parent)
        {
            var newParent = ParentRequestVM.ToModel(parent);
            newParent.CreatedOn = DateTime.UtcNow;
            _context.Parents.Add(newParent);
            _context.SaveChanges();
            return ParentResponseVM.ToViewModel(newParent);

        }

        public bool DeleteParent(int id, int tenantId)
        {
            var parent = _context.Parents
                .FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            if (parent == null)
            {
                return false;
            }
            parent.IsDeleted = true;
            parent.UpdatedOn = DateTime.UtcNow;
            _context.Parents.Update(parent);
            _context.SaveChanges();
            return true;
        }

        public List<ParentResponseVM> GetAllParents()
        {
            var parents = _context.Parents.ToList();
            return ParentResponseVM.ToViewModelList(parents);
        }

        public ParentResponseVM GetParentById(int id)
        {
            var parent = _context.Parents.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (parent == null)
            {
                return null; 
            }
            return ParentResponseVM.ToViewModel(parent);
        }

        public ParentResponseVM GetParentByIdAndTenantId(int id, int tenantId)
        {
            var parent = _context.Parents
                .FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            if (parent == null)
            {
                return null; 
            }
            return ParentResponseVM.ToViewModel(parent);

        }

        public List<ParentResponseVM> GetParentByTenantId(int tenantId)
        {
            var parent = _context.Parents
                .Where(p => p.TenantId == tenantId && !p.IsDeleted).ToList();
            if (parent == null)
            {
                return null; 
            }
            return ParentResponseVM.ToViewModelList(parent);

        }

        public ParentResponseVM UpdateParent(int id, int tenantId, ParentUpdateVM parent)
        {
            var existingParent = _context.Parents
                .FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            if (existingParent == null)
            {
                return null;
            }
            existingParent.UserId = parent.UserId;
            existingParent.UpdatedBy = parent.UpdatedBy;
            existingParent.UpdatedOn = DateTime.UtcNow;

            _context.Parents.Update(existingParent);
            _context.SaveChanges();
            return ParentResponseVM.ToViewModel(existingParent);
        }
    }
}
