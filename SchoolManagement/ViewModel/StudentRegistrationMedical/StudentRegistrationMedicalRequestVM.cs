using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.StudentRegistration
{
    public class StudentRegistrationMedicalRequestVM
    {
        public int Registration_Id { get; set; }

        public bool Any_Allergy { get; set; }

        public int What_Allergy_Id { get; set; }

        public string Other_Allergy_Text { get; set; }

        public bool Medical_Kit { get; set; }

        public string Serious_Medical_Conditions { get; set; }

        public string Serious_Medical_Info { get; set; }

        public int Tenant_Id { get; set; }
    }
}
