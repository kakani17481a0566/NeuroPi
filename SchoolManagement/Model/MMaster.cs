using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;


namespace SchoolManagement.Model
{
    [Table("masters")]
    public class MMaster : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("masters_type_id")]
        public int? MasterTypeId { get; set; }

        [ForeignKey(nameof(MasterTypeId))]
        public virtual MMasterType MasterType { get; set; }

        [Column("code")]
        public string? Code { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
