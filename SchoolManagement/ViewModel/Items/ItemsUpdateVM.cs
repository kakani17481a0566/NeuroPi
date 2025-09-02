namespace SchoolManagement.ViewModel.Items
{
    public class ItemsUpdateVM
    {
        public int CategoryId { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Depth { get; set; }

        public string Name { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
