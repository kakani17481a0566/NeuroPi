// SchoolManagement.ViewModel.ItemSupplier/ItemSupplierRequestVM.cs
using System;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemSupplier
{
    public class ItemSupplierRequestVM
    {
        //public int Id { get; set; }              // optional for update
        public int ItemId { get; set; }
        public int BranchId { get; set; }
        public int TenantId { get; set; }
        public int? Adt { get; set; }            // nullable to match DB
        public int CreatedBy { get; set; }       // required on create
        public int? UpdatedBy { get; set; }      // set on update

        public MItemSupplier ToModel()
        {
            var entity = new MItemSupplier
            {
                ItemId = this.ItemId,
                BranchId = this.BranchId,
                TenantId = this.TenantId,
                Adt = this.Adt,
                CreatedBy = this.CreatedBy,
                UpdatedBy = this.UpdatedBy
            };

          

            return entity;
        }
    }
}
