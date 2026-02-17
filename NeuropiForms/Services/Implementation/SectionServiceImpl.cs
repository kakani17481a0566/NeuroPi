using NeuropiForms.Data;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.Sections;
using System.Security.Cryptography.X509Certificates;

namespace NeuropiForms.Services.Implementation
{
    
    public class SectionServiceImpl : ISectionService
    {
        private readonly NeuropiFormsDbContext _context;

        public SectionServiceImpl(NeuropiFormsDbContext context)
        {
            _context = context;
        }

        public SectionsResponseVM CreateSection(SectionsRequestVM section)
        {
            var SectionModel = SectionsRequestVM.ToModel(section);
            SectionModel.CreatedOn = DateTime.UtcNow;
            SectionModel.IsDeleted = false;

            _context.Sections.Add(SectionModel);
            _context.SaveChanges();
            return SectionsResponseVM.ToViewModel(SectionModel);
        }

       

        public bool DeleteSection(int id, int tenantId)
        {
            var section = _context.Sections.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);

            if (section == null) return false;

            section.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<SectionsResponseVM> GetAllSections()
        {
            var sections = _context.Sections.Where(s => !s.IsDeleted).ToList();
            return SectionsResponseVM.ToViewModelList(sections);

        }

        public SectionsResponseVM GetSectionById(int id)
        {
            var section = _context.Sections.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if (section == null) return null;
            return SectionsResponseVM.ToViewModel(section);

        }

        public SectionsResponseVM GetSectionByIdAndTenantId(int id, int tenantId)
        {
            var section = _context.Sections.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (section == null) return null;
            return SectionsResponseVM.ToViewModel(section);

        }

        public SectionsResponseVM GetSectionByTenantId(int tenantId)
        {
            var section = _context.Sections.FirstOrDefault(s => s.TenantId == tenantId && !s.IsDeleted);
            if (section == null) return null;
            return SectionsResponseVM.ToViewModel(section);
        }

       

        public SectionsResponseVM UpdateSection(int id, int tenantId, SectionsUpdateVM section)
        {
            var existingSection = _context.Sections.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (existingSection == null) return null;

            section.Name = section.Name ?? existingSection.Name;
            section.Description = section.Description ?? existingSection.Description;
            section.IsActive = section.IsActive ?? existingSection.IsActive;
            section.Parameters = section.Parameters ?? existingSection.Parameters;
            section.Weightage = section.Weightage ?? existingSection.Weightage;
            section.AutoWeightCal = section.AutoWeightCal ?? existingSection.AutoWeightCal;
            section.MultiValues = section.MultiValues ?? existingSection.MultiValues;
            section.VersionId = section.VersionId ?? existingSection.VersionId;
            section.Max = section.Max ?? existingSection.Max;
            section.Min = section.Min ?? existingSection.Min;
            section.AppId = section.AppId ?? existingSection.AppId;

            _context.SaveChanges();
            return SectionsResponseVM.ToViewModel(existingSection);



        }
    }
}
