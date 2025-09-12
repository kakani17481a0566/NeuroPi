using NeuroPi.UserManagment.Model;
using SchoolManagement.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("course")]
public class MCourse : MBaseModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Required]
    [Column("tenant_id")]
    [ForeignKey("Tenant")]
    public int TenantId { get; set; }

    public virtual MTenant Tenant { get; set; }

    public virtual ICollection<MSubject> Subjects { get; set; } = new List<MSubject>();
    public virtual ICollection<MStudentCourse> StudentCourses { get; set; } = new List<MStudentCourse>();
}
