using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    public class MStudentRegistrationMedical
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("registration_id")]
        public int Registration_Id { get; set; }

        [Column("any_allergy")]
        public bool Any_Allergy { get; set; }

        [Column("what_allergy_id")]
        public int What_Allergy_Id { get; set; }

        [Column("other_allergy_text")]
        public string Other_Allergy_Text { get; set; }

        [Column("medical_kit")]
        public bool Medical_Kit { get; set; }

        [Column("serious_medical_conditions")]
        public string Serious_Medical_Conditions { get; set; }

        [Column("serious_medical_info")]
        public string Serious_Medical_Info { get; set; }

        [Column("tenant_id")]
        public int Tenant_Id { get; set; }


    }
}
