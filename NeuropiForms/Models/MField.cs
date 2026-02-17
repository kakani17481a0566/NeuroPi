using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("field")]
    public class MField : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("question")]
        public string? Question { get; set; }

        [Column("control_id")]
        public string? ControlId { get; set; }

        [Column("control_type_id")]
        public int? ControlTypeId { get; set; }

        [Column("options_type_id")]
        public int? OptionsTypeId { get; set; }

        [Column("option_name")]
        public string? OptionName { get; set; }

        [Column("option_json", TypeName = "jsonb")]
        public string? OptionJson { get; set; }

        [Column("validation_rules")]
        public string? ValidationRules { get; set; }

        [Column("placeholder")]
        public string? Placeholder { get; set; }

        [Column("help_text")]
        public string? HelpText { get; set; }

        [Column("is_active")]
        public bool? IsActive { get; set; } = true;

        [Column("weightage")]
        public double? Weightage { get; set; }

        [Column("max")]
        public int? Max { get; set; }

        [Column("min")]
        public int? Min { get; set; }

        [Column("defaultvalue")]
        public string? DefaultValue { get; set; }

        [Column("datatype_id")]
        public int? DatatypeId { get; set; }

        [Column("is_calucalted")]
        public bool? IsCalculated { get; set; }

        [Column("formula")]
        public string? Formula { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
