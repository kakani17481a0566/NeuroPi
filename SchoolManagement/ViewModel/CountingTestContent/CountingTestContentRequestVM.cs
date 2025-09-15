namespace SchoolManagement.ViewModel.CountingTestContent
{
    /// <summary>
    /// Request model for creating or updating counting test content.
    /// </summary>
    public class CountingTestContentRequestVM
    {
        /// <summary>
        /// The display label for the item being counted (e.g., "Apples").
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        /// The shape or type of the item (e.g., "circle", "square").
        /// </summary>
        public string? Shape { get; set; }

        /// <summary>
        /// The correct number of items to be counted.
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// The ID of the parent test this content belongs to.
        /// </summary>
        public int? TestId { get; set; }
    }
}
