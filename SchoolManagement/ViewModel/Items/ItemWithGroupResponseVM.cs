namespace SchoolManagement.ViewModel.Items
{
    /// <summary>
    /// Response VM representing an Item and its group children (if any).
    /// </summary>
    public class ItemWithGroupResponseVM
    {
        // 🔹 Parent item details
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ItemCode { get; set; }
        public bool IsGroup { get; set; }
        public int TenantId { get; set; }

        // ✅ Add CategoryId so service code compiles
        public int CategoryId { get; set; }

        // 🔹 Group children (if IsGroup == true)
        public List<GroupChildVM> Children { get; set; } = new();
    }

    /// <summary>
    /// Child item entry inside a group
    /// </summary>
    public class GroupChildVM
    {
        // ✅ Rename to ItemId for clarity
        public int ItemId { get; set; }              // Child item ID
        public string Name { get; set; } = string.Empty; // Child item name
        public int Quantity { get; set; }
        public decimal? FixedPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}
