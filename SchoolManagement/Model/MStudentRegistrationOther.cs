using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    public class MStudentRegistrationOther
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("registration_id")]
        public int Registration_Id { get; set; }

        [Column("language_adults_home")]
        public string Language_Adults_Home { get; set; }

        [Column("language_most_used_with_child")]
        public string Language_Most_Used_With_Child { get; set; }

        [Column("can_read_english")]
        public bool Can_Read_English { get; set; }

        [Column("read_skill_id")]
        public int Read_Skill_Id { get; set; }

        [Column("can_write_english")]
        public bool Can_Write_English { get; set; }

        [Column("write_skill_id")]
        public int Write_Skill_Id { get; set; }

        [Column("signature")]
        public string Signature { get; set; }

        [Column("date_of_joining")]
        public DateOnly Date_Of_Joining { get; set; }

        [Column("tenant_id")]
        public int Tenant_Id { get; set; }


    }
}
