using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_nutritional_focus")]
    public class MNutNutritionalFocus : MBaseModel
    {
        [Column("id")]
        public int Id { get; set;}

        [Column("code")]
        public string Code { get; set;}

        [Column("name")]
        public string Name { get; set;}

        [Column("description")]
        public string Description { get; set;}

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set;}
        
        public MTenant Tenant { get; set;}

    }
}
