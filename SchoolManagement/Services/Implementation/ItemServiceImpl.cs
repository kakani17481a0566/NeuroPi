using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Services.Interface;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Item;
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.Services.Implementation
{
    public class ItemServiceImpl : IItemService
    {
        public SchoolManagementDb _context;

        public object ItemMV { get; private set; }

        public ItemServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public List<ItemVM> GetAll()
        {
            return _context.Items
                .Where(i => i!.IsDeleted)
                .Select(i => new ItemVM
                {
                    Id = i.Id,
                    ItemHeaderId = i.ItemHeaderId,
                    BookCondition = i.BookCondition,
                    Status = i.Status,
                    PurchasedOn = i.PurchasedOn,
                    TenantId = i.TenantId,


                }).ToList();
        }
        public List<ItemVM> GetAllByTenantId(int TenantId)
        {
            return _context.Items
                .Where(i => i!.IsDeleted && i.TenantId == TenantId)
                .Select(i => new ItemVM
                {
                    Id = i.Id,
                    ItemHeaderId = i.ItemHeaderId,
                    BookCondition = i.BookCondition,
                    Status = i.Status,
                    PurchasedOn = i.PurchasedOn,
                    TenantId = i.TenantId,


                }).ToList();
        }
        public ItemVM GetById(int Id)

        {
            var i = _context.Items.FirstOrDefault(i => i!.IsDeleted && i.Id == Id);
            if (i == null) return null;
            return new ItemVM
            {
                Id = i.Id,
                ItemHeaderId = i.ItemHeaderId,
                BookCondition = i.BookCondition,
                Status = i.Status,
                PurchasedOn = i.PurchasedOn,
                TenantId = i.TenantId,
            };


        }
        public ItemVM GetByIdAndTenantId(int Id, int TenantId)

        {
            var i = _context.Items.FirstOrDefault(i => i!.IsDeleted && i.Id == Id && i.TenantId == TenantId);

            if (i == null) return null;
            return new ItemVM
            {
                Id = i.Id,
                ItemHeaderId = i.ItemHeaderId,
                BookCondition = i.BookCondition,
                Status = i.Status,
                PurchasedOn = i.PurchasedOn,
                TenantId = i.TenantId,
            };


        }
        public ItemVM UpdateByIdAndTenantId(int Id, int TenantId, UpdateItemVM request)
        {
            var entity = _context.Items.FirstOrDefault(i => i.Id == Id && i.TenantId == TenantId && !i.IsDeleted);
            if (entity != null) return null;
            entity.BookCondition = request.BookCondition;
            entity.Status = request.Status;
            entity.ItemHeaderId = request.ItemHeaderId;

            _context.SaveChanges();
            return new ItemVM { Id = Id, ItemHeaderId = request.ItemHeaderId, BookCondition = request.BookCondition, Status = request.Status };
        }

        public ItemVM DeleteByIdAndTenantId(int Id, int TenantId)
        {
            var Item = _context.Items.FirstOrDefault(i => i.Id == Id && i.TenantId == TenantId && !i.IsDeleted);
            if (Item == null) return null;
            Item.IsDeleted = true;
            _context.Items.Update(Item);
            _context.SaveChanges();
            return true;

            
        }
        
        public ItemVM createwithItem(ItemRequestVM Item)
        {
            var ItemModel = new MItem() { ItemHeaderId=Item.ItemHeaderId,TenantId=Item.TenantId,BookCondition=Item.BookCondition, Status=Item.Status,PurchasedOn=Item.PurchasedOn,CreatedBy=Item.CreatedBy};
            ItemModel.CreatedOn = DateTime.UtcNow;
            _context.Items.Add(ItemModel);
            _context.SaveChanges();
            return new ItemVM() {Id=ItemModel.Id, ItemHeaderId = ItemModel.ItemHeaderId, TenantId = ItemModel.TenantId, BookCondition = ItemModel.BookCondition, Status = ItemModel.Status, PurchasedOn = ItemModel.PurchasedOn  };
        }


        private ItemVM GetById(object id)
        {
            throw new NotImplementedException();
        }


        List<ItemVM> IItemService.GetAll()
        {
            throw new NotImplementedException();
        }




    }

    public interface IItemService
    {
        List<ItemVM> GetAll();
    }
}

