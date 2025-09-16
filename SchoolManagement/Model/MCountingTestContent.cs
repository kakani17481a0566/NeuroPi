using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    /// <summary>
    /// Represents the content details for a counting test, such as the item to be counted.
    /// Maps to the "counting_test_content" table in the database.
    /// </summary>
    [Table("counting_test_content")]
    public class MCountingTestContent
    {
        /// <summary>
        /// The unique identifier for the counting test content record.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// The display label for the item being counted (e.g., "Apples").
        /// </summary>
        [Column("label")]
        public string? Label { get; set; }

        /// <summary>
        /// The shape or type of the item (e.g., "circle", "square").
        /// </summary>
        [Column("shape")]
        public string? Shape { get; set; }

        /// <summary>
        /// The correct number of items to be counted.
        /// </summary>
        [Column("count")]
        public int? Count { get; set; }

        /// <summary>
        /// The foreign key referencing the parent test.
        /// </summary>
        [Column("test_id")]
        public int? TestId { get; set; }

        /// <summary>
        /// Navigation property for the parent Test, linked by TestId.
        /// This assumes a corresponding 'MTest' model exists for the 'test' table.
        /// </summary>
        [ForeignKey("TestId")]
        public virtual MTest? Test { get; set; }
    }
}