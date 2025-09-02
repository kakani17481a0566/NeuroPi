using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            var list = _db.ItemSuppliers
                          .AsNoTracking()
                          .Where(x => !x.IsDeleted)
                          .ToList();

            return list.Select(ItemSupplierResponseVM.FromModel).ToList();
        }

        public List<ItemSupplierResponseVM> GetAllByTenantId(int tenantId)
        {
            var list = _db.ItemSuppliers
                          .AsNoTracking()
                          .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                          .ToList();

            return list.Select(ItemSupplierResponseVM.FromModel).ToList();
        }

        public ItemSupplierResponseVM? GetById(int id)
        {
            var entity = _db.ItemSuppliers
                            .AsNoTracking()
                            .FirstOrDefault(x => !x.IsDeleted && x.Id == id);

            return ItemSupplierResponseVM.FromModel(entity);
        }

        public ItemSupplierResponseVM? GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ItemSuppliers
                            .AsNoTracking()
                            .FirstOrDefault(x => !x.IsDeleted && x.Id == id && x.TenantId == tenantId);

            return ItemSupplierResponseVM.FromModel(entity);
        }

        public ItemSupplierResponseVM CreateItemSupplier(ItemSupplierRequestVM request)
        {
            var model = request.ToModel();
            model.CreatedOn = DateTime.UtcNow;

            _db.ItemSuppliers.Add(model);
            _db.SaveChanges();

            return ItemSupplierResponseVM.FromModel(model);
        }

        public ItemSupplierResponseVM? UpdateItemSupplier(int id, int tenantId, ItemSupplierRequestVM request)
        {
            var entity = _db.ItemSuppliers
                            .FirstOrDefault(x => !x.IsDeleted && x.Id == id && x.TenantId == tenantId);

            if (entity == null) return null;

            // update allowed fields
            entity.ItemId = request.ItemId;
            entity.BranchId = request.BranchId;
            entity.Adt = request.Adt;
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = request.UpdatedBy;

            _db.SaveChanges();
            return ItemSupplierResponseVM.FromModel(entity);
        }

        public ItemSupplierResponseVM? DeleteByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ItemSuppliers
                            .FirstOrDefault(x => !x.IsDeleted && x.Id == id && x.TenantId == tenantId);

            if (entity == null) return null;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();
            return ItemSupplierResponseVM.FromModel(entity);
        }
    }
}
