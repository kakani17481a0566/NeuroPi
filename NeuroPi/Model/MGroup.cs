﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("groups")]
    public class MGroup : MBaseModel
    {
        [Key]
        [Column("group_id")]
        public int GroupId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }



        // Navigation properties
        public virtual MTenant Tenant { get; set; }
        public virtual ICollection<MGroupUser> GroupUsers { get; set; } = new List<MGroupUser>();
    }
}
