namespace SchoolManagement.ViewModel.Subject
{
    public class VStudentPerformanceChartVM
    {
        public List<string> Headers { get; set; } = new(); // Example: ["Student", "PSED[A1]", "CLL[B3]", ...]
        public List<List<string>> TData { get; set; } = new(); // Each row corresponds to student + grades
    }
}
