using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("items_image")]
    public class MItemsImage : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("item_id")]
        public int ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual MItems Item { get; set; }

        [Column("image")]
        public byte[]? Image { get; set; }

        [Column("img_description")]
        public string? ImgDescription { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
