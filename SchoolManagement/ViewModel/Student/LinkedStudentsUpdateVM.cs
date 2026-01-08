
namespace SchoolManagement.ViewModel.Student
{
    public class LinkedStudentsUpdateVM
    {
        public int UserId { get; set; }
        public List<int> StudentIds { get; set; } = new List<int>();
    }
}
