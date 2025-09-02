    using System;
    using SchoolManagement.Model;

    namespace SchoolManagement.ViewModel.ItemSupplier
    {
        public class ItemSupplierResponseVM
        {
            public int Id { get; set; }
            public int ItemId { get; set; }
            public int BranchId { get; set; }
            public int TenantId { get; set; }
            public int? Adt { get; set; }          // ✅ fixed type

            public DateTime CreatedOn { get; set; }
            public int CreatedBy { get; set; }
            public DateTime? UpdatedOn { get; set; }
            public int? UpdatedBy { get; set; }
            public bool IsDeleted { get; set; }

            // STATIC mapper: Entity -> Response
            public static ItemSupplierResponseVM FromModel(MItemSupplier m)
            {
                if (m == null) return null;

                return new ItemSupplierResponseVM
                {
                    Id = m.Id,
                    ItemId = m.ItemId,
                    BranchId = m.BranchId,
                    TenantId = m.TenantId,
                    Adt = m.Adt,
                    CreatedOn = m.CreatedOn,
                    CreatedBy = m.CreatedBy,
                    UpdatedOn = m.UpdatedOn,
                    UpdatedBy = m.UpdatedBy,
                    IsDeleted = m.IsDeleted
                };
            }
        }
    }
