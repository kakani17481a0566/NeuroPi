namespace NeuropiForms.ViewModels.Fields
{
    public class FieldsUpdateVM
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

        public int UpdatedBy { get; set; }
    
        public DateTime UpdatedOn { get; set; }
    }
}
