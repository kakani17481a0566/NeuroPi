using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("vw_employee_question_answers")]
    public class MVwEmployeeQuestionAnswers
    {
        [Column("employee_id")]
        [Required]
        public int EmployeeId { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("employee_code")]
        [Required]
        public string EmployeeCode { get; set; } = string.Empty;

        [Column("q_ctg_id")]
        public int? QCtgId { get; set; }

        [Column("q_order_id")]
        public int? QOrderId { get; set; }

        [Column("question")]
        public string? Question { get; set; }

        [Column("answer")]
        public string? Answer { get; set; }

        [Column("tenant_id")]
        [Required]
        public int TenantId { get; set; }
    }
}
