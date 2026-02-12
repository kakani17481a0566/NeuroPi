using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("students", Schema = "public")]
    public class MStudent
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string Name { get; set; } = default!;

        [Column("last_name")]
        public string? LastName { get; set; }
        
        [Column("gender")]
        public string? Gender { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }
        
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("admission_grade")]
        public string? AdmissionGrade { get; set; }
    }
}
