namespace SchoolManagement.ViewModel.Items
{
    // ----------------------------------------------------------------------
    // Response DTO for Individual Items
    // ----------------------------------------------------------------------
    public class ItemsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int ItemQuantity { get; set; }
    }

    // ----------------------------------------------------------------------
    // Wrapper DTO for Items + Filters
    // ----------------------------------------------------------------------
    public class ItemsFilterResponse
    {
        public List<ItemsResponse> Items { get; set; } = new();

        // ✅ Dynamic filters to support frontend toolbar filtering
        public ItemsFilters Filters { get; set; } = new();
    }

    // ----------------------------------------------------------------------
    // Filters Object
    // ----------------------------------------------------------------------
    public class ItemsFilters
    {
        public Dictionary<int, string> Categories { get; set; } = new(); // categoryId : categoryName
        public List<string> StatusList { get; set; } = new();            // "Available", "Not Available"
        public List<int> PriceList { get; set; } = new();                // Unique price values
        public List<int> Sizes { get; set; } = new();                    // Available sizes
    }
}
