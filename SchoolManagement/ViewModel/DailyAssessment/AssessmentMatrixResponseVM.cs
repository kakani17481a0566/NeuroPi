namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class AssessmentMatrixResponseVM
    {
        public List<AssessmentMatrixHeaderVM> Headers { get; set; } = new();
        public List<AssessmentMatrixStatusVM> AssessmentStatuses { get; set; } = new();
        public List<AssessmentMatrixGradeVM> Grades { get; set; } = new();
        public List<AssessmentMatrixDataVM> Data { get; set; } = new();
    }

    public class AssessmentMatrixHeaderVM
    {
        public string Key { get; set; }    // e.g., "studentName"
        public string Label { get; set; }  // e.g., "Student Name"
    }

    public class AssessmentMatrixStatusVM
    {
        public int Code { get; set; }      // e.g., 171
        public string Name { get; set; }   // e.g., "NOT_STARTED"
    }

    public class AssessmentMatrixGradeVM
    {
        public int Id { get; set; }        // e.g., 1
        public string Name { get; set; }   // e.g., "A+"
    }

    public class AssessmentMatrixDataVM
    {
        public string RowId { get; set; }         // e.g., unique identifier
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int TimeTableId { get; set; }
        public string TimeTableName { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int AssessmentId { get; set; }
        public string AssessmentName { get; set; }

        public int AssessmentStatusCode { get; set; }
        public string AssessmentStatusName { get; set; }

        public int GradeId { get; set; }
        public string GradeName { get; set; }
    }
}
