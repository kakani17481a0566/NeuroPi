using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.Fields
{
    public class FieldsRequestVM
    {
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


        public static MField ToModel(FieldsRequestVM vm)
        {
            return new MField
            {
                Name = vm.Name,
                Question = vm.Question,
                ControlId = vm.ControlId,
                ControlTypeId = vm.ControlTypeId,
                OptionsTypeId = vm.OptionsTypeId,
                OptionName = vm.OptionName,
                OptionJson = vm.OptionJson,
                ValidationRules = vm.ValidationRules,
                Placeholder = vm.Placeholder,
                HelpText = vm.HelpText,
                IsActive = vm.IsActive,
                Weightage = vm.Weightage,
                Max = vm.Max,
                Min = vm.Min,
                DefaultValue = vm.DefaultValue,
                DatatypeId = vm.DatatypeId,
                IsCalculated = vm.IsCalculated,
                Formula = vm.Formula,
                AppId = vm.AppId,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = vm.CreatedOn
            };
        }
    }
}
