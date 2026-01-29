
using SchoolManagement.ViewModel.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("lib_transaction")]
    public class MLibraryTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        // Foreign key to Students
        [Column("student_id")]
        public int? StudentId { get; set; }

        [ForeignKey("StudentId")]
        public MStudent Student { get; set; }
        // Foreign key to ItemHeader (Books
        [Column("book_id")]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public MLibraryBookTitle Book { get; set; }
        [Column("check_in")]
        public DateOnly CheckIn { get; set; }
        [Column("check_out")]
        public DateOnly? CheckOut { get; set; }

        [Column("check_in_by")]
        public int CheckInBy { get; set; }

        [Column("check_out_by")]
        public int? CheckOutBy { get; set; }

        [Column("status")]
        public string Status { get; set; }
        //public MStudent Student { get; set; }


    }
}
