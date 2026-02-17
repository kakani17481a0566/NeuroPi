using NeuropiForms.Data;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.SectionGroup;

namespace NeuropiForms.Services.Implementation
{
    public class SectionGroupServiceImpl : ISectionGroupService
    {
        private readonly NeuropiFormsDbContext _context;

        public SectionGroupServiceImpl (NeuropiFormsDbContext context)
        {
            _context = context;
        }

        public SectionGroupResponseVM CreateSectionGroup(SectionGroupRequestVM request)
        {
            var sectionGroup = SectionGroupRequestVM.ToModel(request);
            _context.SectionGroups.Add(sectionGroup);
            _context.SaveChanges();
            return SectionGroupResponseVM.ToViewModel(sectionGroup);

        }

        public bool DeleteSectionGroup(int id, int tenantId)
        {
            var sectionGroup = _context.SectionGroups.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (sectionGroup == null)
            {
                return false;

            }
            sectionGroup.IsDeleted = true;
            _context.SaveChanges();
            return true;

        }

        public SectionGroupResponseVM GetById(int id)
        {
            var sectionGroup = _context.SectionGroups.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if (sectionGroup == null)
            {
                return null;
            }
            return SectionGroupResponseVM.ToViewModel(sectionGroup);
        }

        public SectionGroupResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var sectionGroup = _context.SectionGroups.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (sectionGroup == null)
            {
                return null;
            }
            return SectionGroupResponseVM.ToViewModel(sectionGroup);

        }

        public SectionGroupResponseVM GetByTenantId(int tenantId)
        {
            var sectionGroup = _context.SectionGroups.FirstOrDefault(s => s.TenantId == tenantId && !s.IsDeleted);
            if (sectionGroup == null)
            {
                return null;
            }
            return SectionGroupResponseVM.ToViewModel(sectionGroup);

        }

        public List<SectionGroupResponseVM> GetSectionGroups()
        {
            var sectionGroups = _context.SectionGroups.Where(s => !s.IsDeleted).ToList();
            return SectionGroupResponseVM.ToViewModelList(sectionGroups);

        }

        public SectionGroupResponseVM Update(int id, int tenantId, SectionGroupUpdateVM update)
        {
            var sectionGroup = _context.SectionGroups.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (sectionGroup == null)
            {
                return null;
            }
            sectionGroup.GroupId = update.GroupId ?? sectionGroup.GroupId;
            sectionGroup.SectionId = update.SectionId ?? sectionGroup.SectionId;
            sectionGroup.AppId = update.AppId ?? sectionGroup.AppId;
            sectionGroup.UpdatedBy = update.UpdatedBy ?? sectionGroup.UpdatedBy;
            sectionGroup.UpdatedOn = update.UpdatedOn ?? sectionGroup.UpdatedOn;
            _context.SaveChanges();
            return SectionGroupResponseVM.ToViewModel(sectionGroup);

        }
    }
}
