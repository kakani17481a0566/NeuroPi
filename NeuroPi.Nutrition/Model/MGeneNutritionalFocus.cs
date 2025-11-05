using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_gene_nutritional_focus")]
    public class MGeneNutritionalFocus : MBaseModel
    {
        [Column("id")]
        public  int Id  { get; set; }

        [Column("nutritional_focus_id")]
        [ForeignKey("NutritionalFocus")]
        public int NutritionalFocusId { get; set; }

        [Column("genes_id")]
        [ForeignKey("Genes")]
        public int GenesId { get; set; }

        [Column("tenant_id")]
       public int TenantId { get; set; }

        public virtual MNutritionalFocus NutritionalFocus { get; set; }
        public virtual MGenGenes Genes { get; set; }
         

      
    }
}
