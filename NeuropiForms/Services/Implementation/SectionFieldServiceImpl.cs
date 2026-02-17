using Microsoft.EntityFrameworkCore;
using NeuropiForms.Data;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.SectionField; 

namespace NeuropiForms.Services.Implementation
{
    public class SectionFieldServiceImpl : ISectionFieldService
    {
        private readonly NeuropiFormsDbContext _context;

        public SectionFieldServiceImpl(NeuropiFormsDbContext context)
        {
            _context = context;
        }

        public SectionFieldResponseVM CreateSectionField(SectionFieldRequestVM requestVM)
        {
            var sectionField = SectionFieldRequestVM.ToModel(requestVM);
            _context.SectionFields.Add(sectionField);
            _context.SaveChanges();
            return SectionFieldResponseVM.ToViewModel(sectionField);

        }

        public bool DeleteSectionField(int id, int tenantId)
        {
            var sectionField = _context.SectionFields.FirstOrDefault(x => x.Id == id && x.TenantId==tenantId);
            if (sectionField == null)
            {
                return false;
            }
            sectionField.IsDeleted = true;
            _context.SectionFields.Update(sectionField);
            _context.SaveChanges();
            return true;

        }

        public List<SectionFieldResponseVM> GetAllSectionFields()
        {
            var sectionFields = _context.SectionFields.Where(sf => !sf.IsDeleted).ToList();
            return SectionFieldResponseVM.ToViewModelList(sectionFields);


        }

        public SectionFieldResponseVM GetById(int id)
        {
            var sectionField = _context.SectionFields.FirstOrDefault(sf => sf.Id == id && !sf.IsDeleted);
            if (sectionField == null)
            {
                return null;
            }
            return SectionFieldResponseVM.ToViewModel(sectionField);
        }

        public SectionFieldResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var sectionField = _context.SectionFields.FirstOrDefault(sf => sf.Id == id && sf.TenantId == tenantId && !sf.IsDeleted);
            if (sectionField == null)
            {
                return null;
            }
            return SectionFieldResponseVM.ToViewModel(sectionField);
        }

        public SectionFieldResponseVM GetByTenantId(int tenantId)
        {
            var sectionField = _context.SectionFields.FirstOrDefault(sf => sf.TenantId == tenantId && !sf.IsDeleted);
            if (sectionField == null)
            {
                return null;
            }
            return SectionFieldResponseVM.ToViewModel(sectionField);
        }

        public SectionFieldResponseVM UpdateSectionField(int id, int tenantId, SectionFieldUpdateVM updateVM)
        {
            var existingSectionField = _context.SectionFields.FirstOrDefault(sf => sf.Id == id && sf.TenantId == tenantId && !sf.IsDeleted);
            if (existingSectionField == null)
            {
                return null;
            }
            var sectionField = existingSectionField;
            sectionField.SectionId = updateVM.SectionId ?? sectionField.SectionId;
            sectionField.FieldId = updateVM.FieldId ?? sectionField.FieldId;
            sectionField.DisplayOrder = updateVM.DisplayOrder ?? sectionField.DisplayOrder;
            sectionField.IsRequired = updateVM.IsRequired ?? sectionField.IsRequired;
            sectionField.CustomLabel = updateVM.CustomLabel ?? sectionField.CustomLabel;
            sectionField.VersionId = updateVM.VersionId ?? sectionField.VersionId;
            sectionField.AppId = updateVM.AppId ?? sectionField.AppId;
            sectionField.TenantId = updateVM.TenantId ?? sectionField.TenantId;
            sectionField.UpdatedBy = updateVM.UpdatedBy ?? sectionField.UpdatedBy;
            sectionField.UpdatedOn = updateVM.UpdatedOn ?? sectionField.UpdatedOn;

            _context.SectionFields.Update(sectionField);
            _context.SaveChanges();
            return SectionFieldResponseVM.ToViewModel(sectionField);
        }
    }
}
