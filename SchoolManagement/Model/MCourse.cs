﻿using NeuroPi.UserManagment.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
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
        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }

        // Add this navigation property:
        public virtual ICollection<MSubject> Subjects { get; set; }
    }
}
