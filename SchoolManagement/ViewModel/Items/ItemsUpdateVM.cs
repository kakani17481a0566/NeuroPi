namespace SchoolManagement.ViewModel.Items
{
    public class ItemsUpdateVM
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? Depth { get; set; }

        public string? Description { get; set; }
        public string? ItemCode { get; set; }
        public bool IsGroup { get; set; } = false;

        // Audit
        public int UpdatedBy { get; set; }
    }
}
