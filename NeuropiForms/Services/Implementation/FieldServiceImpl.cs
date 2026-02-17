using NeuropiForms.Data;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.Fields;

namespace NeuropiForms.Services.Implementation
{
    public class FieldServiceImpl : IFieldService

    {
        private readonly NeuropiFormsDbContext _context;

        public FieldServiceImpl(NeuropiFormsDbContext context)
        {
            _context = context;
        }

        public FieldsResponseVM CreateField(FieldsRequestVM field)
        {
            var newField = FieldsRequestVM.ToModel(field);
            newField.CreatedOn = DateTime.UtcNow;
            _context.Fields.Add(newField);
            _context.SaveChanges();
            return FieldsResponseVM.ToViewModel(newField);

        }

        public bool DeleteField(int id, int tenantId)
        {
            var field = _context.Fields.FirstOrDefault(f => f.Id == id && f.TenantId == tenantId && !f.IsDeleted);
            if (field != null)
                return false;

            field.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<FieldsResponseVM> GetAllFields()
        {
           var fields = _context.Fields.Where(f => !f.IsDeleted).ToList();
            return FieldsResponseVM.ToViewModelList(fields);
        }

        public FieldsResponseVM GetFieldById(int id)
        {
            var field = _context.Fields.FirstOrDefault(f => f.Id == id && !f.IsDeleted);
            if(field == null)
                return null;
            return FieldsResponseVM.ToViewModel(field);
        }

        public FieldsResponseVM GetFieldByIdAndTenantId(int id, int tenantId)
        {
            var field = _context.Fields.FirstOrDefault(f => f.Id == id && f.TenantId == tenantId && !f.IsDeleted);
            if (field == null)
                return null;
            return FieldsResponseVM.ToViewModel(field);
        }

        public FieldsResponseVM GetFieldByTenantId(int tenantId)
        {
            var field = _context.Fields.FirstOrDefault(f => f.TenantId == tenantId && !f.IsDeleted);
            if (field == null)
                return null;
            return FieldsResponseVM.ToViewModel(field);
        }

        public FieldsResponseVM UpdateField(int id, int tenantId, FieldsUpdateVM field)
        {
            var existingField = _context.Fields.FirstOrDefault(f => f.Id == id && f.TenantId == tenantId && !f.IsDeleted);
            if (existingField == null)
                return null;
            existingField.Name = field.Name;
            existingField.Question = field.Question;
            existingField.ControlId = field.ControlId;
            existingField.ControlTypeId = field.ControlTypeId;
            existingField.OptionsTypeId = field.OptionsTypeId;
            existingField.OptionName = field.OptionName;
            existingField.OptionJson = field.OptionJson;
            existingField.ValidationRules = field.ValidationRules;
            existingField.Placeholder = field.Placeholder;
            existingField.HelpText = field.HelpText;
            existingField.IsActive = field.IsActive;
            existingField.Weightage = field.Weightage;
            existingField.Max = field.Max;
            existingField.Min = field.Min;
            existingField.DefaultValue = field.DefaultValue;
            existingField.DatatypeId = field.DatatypeId;
            existingField.IsCalculated = field.IsCalculated;
            existingField.Formula = field.Formula;
            existingField.AppId = field.AppId;
            existingField.TenantId = field.TenantId;
            existingField.UpdatedBy = field.UpdatedBy;
            existingField.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return FieldsResponseVM.ToViewModel(existingField);

        }
    }
}
