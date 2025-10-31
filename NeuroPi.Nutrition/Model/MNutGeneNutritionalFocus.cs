using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_gene_nutritional_focus")]
    public class MNutGeneNutritionalFocus
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
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public MNutNutritionalFocus NutritionalFocus { get; set; }

        public MGenGenes Genes { get; set; }

        public MTenant Tenant { get; set; }
    }
}
