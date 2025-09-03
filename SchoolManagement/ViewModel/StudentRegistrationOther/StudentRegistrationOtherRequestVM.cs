using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.StudentRegistrationOther
{
    public class StudentRegistrationOtherRequestVM
    {
        public int Registration_Id { get; set; }

        public string Language_Adults_Home { get; set; }

        public string Language_Most_Used_With_Child { get; set; }

        public bool Can_Read_English { get; set; }

        public int Read_Skill_Id { get; set; }

        public bool Can_Write_English { get; set; }

        public int Write_Skill_Id { get; set; }

        public string Signature { get; set; }

        public DateOnly Date_Of_Joining { get; set; }

        public int Tenant_Id { get; set; }
    }
}
