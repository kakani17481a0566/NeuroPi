using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("tablefiles")]
    public class MTableFles
    {
    
  
       
            [Key]
            [Column("id")]
            public int Id { get; set; }

            [Required]
            [Column("course_id")]
            public int CourseId { get; set; }

            [Column("name")]
            [StringLength(255)]
            public string? Name { get; set; }

            [Column("link", TypeName = "text")]
            public string? Link { get; set; }

            [Column("type")]
            [StringLength(255)]
            public string? Type { get; set; }

            [Column("tenant_id")]
            public int? TenantId { get; set; }

            [Required]
            [Column("created_on")]
            public DateTime CreatedOn { get; set; }

            [Required]
            [Column("created_by")]
            public int CreatedBy { get; set; }

            [Column("updated_on")]
            public DateTime? UpdatedOn { get; set; }

            [Column("updated_by")]
            public int? UpdatedBy { get; set; }

            [Required]
            [Column("is_deleted")]
            public bool IsDeleted { get; set; }
        
    }
}
