using NeuroPi.UserManagment.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("contact")]
    public class MContact : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name", TypeName = "varchar(100)")]
        public string? Name { get; set; }

        [Column("pri_number", TypeName = "varchar(20)")]
        public string? PriNumber { get; set; }

        [Column("sec_number", TypeName = "varchar(20)")]
        public string? SecNumber { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        public string? Email { get; set; }

        [Column("address_1", TypeName = "varchar(200)")]
        public string? Address1 { get; set; }

        [Column("address_2", TypeName = "varchar(200)")]
        public string? Address2 { get; set; }

        [Column("state", TypeName = "varchar(50)")]
        public string? State { get; set; }

        [Column("city", TypeName = "varchar(50)")]
        public string? City { get; set; }

        [Column("pincode", TypeName = "varchar(20)")]
        public string? Pincode { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("contact_person", TypeName = "varchar(150)")]
        public string? ContactPerson { get; set; }

        [Column("contact_type", TypeName = "varchar(150)")]
        public string? ContactType { get; set; }

        [Column("qualification", TypeName = "varchar(150)")]
        public string? Qualification { get; set; }

        [Column("profession", TypeName = "varchar(150)")]
        public string? Profession { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("relationship_id")]
        public int? RelationshipId { get; set; }

        // ----------------------------
        // Navigation Properties
        // ----------------------------
        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(UserId))]
        public virtual MUser? User { get; set; }

        [ForeignKey(nameof(RelationshipId))]
        public virtual MMaster? Relationship { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }

    }
}
