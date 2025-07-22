public class TopicDetailVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    // ✅ Resolved names
    public string SubjectName { get; set; }
    public string CourseName { get; set; }
    public string TopicTypeName { get; set; }
    public string TenantName { get; set; }

    // ✅ Optional raw IDs (for filtering, dropdowns, etc.)
    public int? SubjectId { get; set; }
    public int? CourseId { get; set; }
}
