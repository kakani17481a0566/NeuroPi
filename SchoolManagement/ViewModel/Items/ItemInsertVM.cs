namespace SchoolManagement.ViewModel.Items
{
    /// <summary>
    /// Request payload for creating a new Item with optional child group items
    /// </summary>
    public class ItemInsertVM
    {
        // 🔹 Main Item (items table)
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int TenantId { get; set; }          // only here (not in child)

        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? Depth { get; set; }
        public int? Size { get; set; }
        public int? ParentItemId { get; set; }

        public string? Description { get; set; }
        public string? ItemCode { get; set; }

        // If true → this item will act as a parent/group
        public bool IsGroup { get; set; } = false;

        // ✅ Audit fields only at parent level
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        // 🔹 Grouped Child Items (items_group table)
        public List<ItemGroupInsertVM> GroupItems { get; set; } = new();
    }

    /// <summary>
    /// Represents a single child item belonging to a group (items_group table)
    /// </summary>
    public class ItemGroupInsertVM
    {
        public int ItemId { get; set; }       // child item id
        public int? Quantity { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? FixedPrice { get; set; }

    }
}
