namespace SchoolManagement.ViewModel.CountingTestContent
{
    /// <summary>
    /// Response model for returning counting test content details from the API.
    /// </summary>
    public class CountingTestContentRespounceVM
    {
        /// <summary>
        /// The unique identifier for the counting test content record.
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// The title of the parent test (optional, included for response convenience).
        /// </summary>
        public string? TestTitle { get; set; }
    }
}
