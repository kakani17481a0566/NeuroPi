using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("books")]
public class MBook : MBaseModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("institution_id")]  
    public int? InstitutionId { get; set; }

    [Column("books_type_id")]
    public int? BooksTypeId { get; set; }

    [Column("tenant_id")]
    public int TenantId { get; set; }

    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual MTenant Tenant { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    public virtual MUser CreatedByUser { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    public virtual MUser? UpdatedByUser { get; set; }

    [ForeignKey("ParentId")]
    public virtual MBook? Parent { get; set; }
}
