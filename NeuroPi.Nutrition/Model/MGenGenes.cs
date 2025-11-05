using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("gen_genes")]
    public class MGenGenes : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        [Required(ErrorMessage = "code is mandatory.")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

  
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual ICollection<MGeneNutritionalFocus> NutritionalFocus { get; set; } = new List<MGeneNutritionalFocus>();



    }
}
