using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.Fields
{
    public class FieldsResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Question { get; set; }

        public string ControlId { get; set; }


        public int? ControlTypeId { get; set; }

        public int? OptionsTypeId { get; set; }

        public string OptionName { get; set; }

        public string OptionJson { get; set; }

        public string ValidationRules { get; set; }


        public string Placeholder { get; set; }

        public string HelpText { get; set; }


        public bool? IsActive { get; set; } = true;

        public double? Weightage { get; set; }

        public int? Max { get; set; }

        public int? Min { get; set; }

        public string DefaultValue { get; set; }

        public int? DatatypeId { get; set; }

        public bool? IsCalculated { get; set; }

        public string Formula { get; set; }

        public int? AppId { get; set; }

        public int? TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static FieldsResponseVM ToViewModel(MField field)
        {
            if (field == null) return null;
            return new FieldsResponseVM
            {
                Id = field.Id,
                Name = field.Name,
                Question = field.Question,
                ControlId = field.ControlId,
                ControlTypeId = field.ControlTypeId,
                OptionsTypeId = field.OptionsTypeId,
                OptionName = field.OptionName,
                OptionJson = field.OptionJson,
                ValidationRules = field.ValidationRules,
                Placeholder = field.Placeholder,
                HelpText = field.HelpText,
                IsActive = field.IsActive,
                Weightage = field.Weightage,
                Max = field.Max,
                Min = field.Min,
                DefaultValue = field.DefaultValue,
                DatatypeId = field.DatatypeId,
                IsCalculated = field.IsCalculated,
                Formula = field.Formula,
                AppId = field.AppId,
                TenantId = field.TenantId,
                CreatedBy = field.CreatedBy,
                CreatedOn = field.CreatedOn,
                UpdatedBy = field.UpdatedBy,
                UpdatedOn = field.UpdatedOn
            };
        }

        public static List<FieldsResponseVM> ToViewModelList(List<MField> fields)
        {
            return fields.Select(f => ToViewModel(f)).ToList();
        }

    }
}
