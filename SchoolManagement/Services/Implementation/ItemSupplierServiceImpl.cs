using System;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemSupplier;

namespace SchoolManagement.Services.Implementation
{
    public class ItemSupplierServiceImpl : IItemSupplierService
    {
        private readonly SchoolManagementDb _db;

        public ItemSupplierServiceImpl(SchoolManagementDb dbcontext)
        {
            _db = dbcontext;
        }

        public List<ItemSupplierResponseVM> GetAll()
        {
            return _db.ItemSuppliers
                      .Where(x => !x.IsDeleted)
                      .Select(ItemSupplierResponseVM.FromModel)
                      .ToList();
        }

        public List<ItemSupplierResponseVM> GetAllByTenantId(int tenantId)
        {
            return _db.ItemSuppliers
                      .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                      .Select(ItemSupplierResponseVM.FromModel)
                      .ToList();
        }

        public ItemSupplierResponseVM? GetById(int id)
        {
            var entity = _db.ItemSuppliers
                            .FirstOrDefault(x => !x.IsDeleted && x.Id == id);
            return entity == null ? null : ItemSupplierResponseVM.FromModel(entity);
        }

        public ItemSupplierResponseVM? GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ItemSuppliers
                            .FirstOrDefault(x => !x.IsDeleted && x.Id == id && x.TenantId == tenantId);
            return entity == null ? null : ItemSupplierResponseVM.FromModel(entity);
        }

        public ItemSupplierResponseVM CreateItemSupplier(ItemSupplierRequestVM request)
        {
            // If you already have a static mapper, fine; otherwise map inline:
            var model = ItemSupplierRequestVM.ToModel(request); // or map manually, see note below.

            model.CreatedOn = DateTime.UtcNow;
            // model.CreatedBy = request.CreatedBy; // if available in your VM

            _db.ItemSuppliers.Add(model);
            _db.SaveChanges();

            return ItemSupplierResponseVM.FromModel(model);
        }

        public ItemSupplierResponseVM? UpdateItemSupplier(int id, int tenantId, ItemSupplierRequestVM request)
        {
            var entity = _db.ItemSuppliers
                            .FirstOrDefault(x => !x.IsDeleted && x.Id == id && x.TenantId == tenantId);

            if (entity == null) return null;

            // Map the updatable fields from request -> entity
            // Adjust to your real fields (examples shown):
            // entity.Name = request.Name;
            // entity.ContactPerson = request.ContactPerson;
            // entity.Phone = request.Phone;
            // entity.Email = request.Email;
            // entity.Address = request.Address;
            // entity.GstNumber = request.GstNumber;
            // entity.Notes = request.Notes;

            entity.UpdatedOn = DateTime.UtcNow;
            // entity.UpdatedBy = request.UpdatedBy; // if available

            _db.SaveChanges();
            return ItemSupplierResponseVM.FromModel(entity);
        }

        public ItemSupplierResponseVM? DeleteByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ItemSuppliers
                            .FirstOrDefault(x => !x.IsDeleted && x.Id == id && x.TenantId == tenantId);

            if (entity == null) return null;

            // Soft delete (recommended)
            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;
            // entity.UpdatedBy = ...;

            _db.SaveChanges();
            return ItemSupplierResponseVM.FromModel(entity);
        }
    }
}
