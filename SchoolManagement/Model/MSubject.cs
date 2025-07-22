using NeuroPi.UserManagment.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("subjects")]
    public class MSubject : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Column("code")]
        public string Code { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("tenant_id")]
        [ForeignKey(nameof(Tenant))]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }

        [Column("course_id")]
        [ForeignKey(nameof(Course))]
        public int? CourseId { get; set; }

        public virtual MCourse Course { get; set; }

        // --- Add these navigation properties below ---
        public virtual ICollection<MTopic> Topics { get; set; } = new List<MTopic>();
        public virtual ICollection<MTimeTableDetail> TimeTableDetails { get; set; } = new List<MTimeTableDetail>();
    }
}
