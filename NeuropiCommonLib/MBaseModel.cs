using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.CommonLib.Model
{
    public abstract class MBaseModel
    {


        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column("created_by")]

        public int CreatedBy { get; set; }

        [Column("updated_on")]
        public DateTime? UpdatedOn { get; set; }


        [Column("updated_by")]

        public int? UpdatedBy { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;





    }
}
