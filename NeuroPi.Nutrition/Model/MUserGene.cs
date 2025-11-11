using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_user_gene")]
    public class MUserGene:MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("gene_id")]
        [ForeignKey("Genes")]
        public int GeneId { get; set; }

        [Column("gene_status")]
        public string GeneStatus { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        public MGenGenes Genes { get; set; }

    }
}
