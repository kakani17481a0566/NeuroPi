using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.Model;

namespace NeuroPi.Models
{
    [Table("tenants")]
    public class MTenant : MBaseModel  // Inherit from MBaseModel to include audit fields
    {
        // Primary key for the tenant
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Auto-incremented primary key
        [Column("tenant_id")] // Maps to the 'tenant_id' column in the database
        public int TenantId { get; set; }

        // Name of the tenant, required field with max length constraint
        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        // Navigation property to the creator user (foreign key to MUser)
        [ForeignKey("CreatedBy")]
        public virtual MUser Creator { get; set; }

        // Navigation property to the updater user (foreign key to MUser)
        [ForeignKey("UpdatedBy")]
        public virtual MUser Updater { get; set; }

        // Collection of users associated with the tenant
        public virtual ICollection<MUser> Users { get; set; }

        // If needed, other navigation properties or additional business logic can be added here
    }
}
