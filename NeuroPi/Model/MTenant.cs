using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.Model;

namespace NeuroPi.Models  // Ensure this is the only namespace with MTenant definition
{
    [Table("tenants")]
    public class MTenant : MBaseModel  // Inheriting audit fields from MBaseModel
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
        [ForeignKey("CreatedBy")]  // Explicitly mapping to the 'CreatedBy' column in the MUser table
        public virtual MUser Creator { get; set; }

        // Navigation property to the updater user (foreign key to MUser)
        [ForeignKey("UpdatedBy")]  // Explicitly mapping to the 'UpdatedBy' column in the MUser table
        public virtual MUser Updater { get; set; }

        // Collection of users associated with the tenant (one-to-many relationship)
        public virtual ICollection<MUser> Users { get; set; } = new List<MUser>();  // Initialize to prevent null reference

        // If needed, other navigation properties or additional business logic can be added here
    }
}
