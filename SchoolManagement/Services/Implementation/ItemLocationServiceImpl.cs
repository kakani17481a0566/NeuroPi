using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemLocation;

namespace SchoolManagement.Services.Implementation
{
    public class ItemLocationServiceImpl : IItemLocationService
    {
        private readonly SchoolManagementDb _context;
            public ItemLocationServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public ItemLocationResponseVM CreateItemLocation(ItemLocationRequestVM itemLocationRequestVM)
        {
            var newItemLocation = new Model.MItemLocation
            {
                Name = itemLocationRequestVM.Name,
                BranchId = itemLocationRequestVM.BranchId,
                TenantId = itemLocationRequestVM.TenantId,
                CreatedBy = itemLocationRequestVM.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };
            _context.ItemLocation.Add(newItemLocation);
            _context.SaveChanges();
            return ItemLocationResponseVM.ToViewModel(newItemLocation);

        }

        public bool DeleteItemLocation(int id, int tenantId)
        {
            var itemLocation = _context.ItemLocation.FirstOrDefault(il => !il.IsDeleted && il.Id == id && il.TenantId == tenantId);
            if (itemLocation == null) return false;
            itemLocation.IsDeleted = true;
            itemLocation.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }

        public ItemLocationResponseVM GetItemLocationById(int branchId)
        {
            var itemLocation = _context.ItemLocation.FirstOrDefault(il => !il.IsDeleted && il.BranchId == branchId);
            if (itemLocation == null) return null;
            return new ItemLocationResponseVM
            {
                Id = itemLocation.Id,
                Name = itemLocation.Name,
                BranchId = itemLocation.BranchId,
                TenantId = itemLocation.TenantId,
                CreatedBy = itemLocation.CreatedBy,
                CreatedOn = itemLocation.CreatedOn,
                UpdatedBy = itemLocation.UpdatedBy,
                UpdatedOn = itemLocation.UpdatedOn
            };
        }

        public  List<ItemLocationResponseVM> GetItemLocationById(int branchId, int tenantId)
        {
            return _context.ItemLocation.Where(il =>!il.IsDeleted && il.BranchId==branchId && il.TenantId == tenantId)
                .Select(il => new ItemLocationResponseVM
                {
                    Id = il.Id,
                    Name = il.Name,
                    BranchId = il.BranchId,
                    TenantId = il.TenantId,
                    CreatedBy = il.CreatedBy,
                    CreatedOn = il.CreatedOn,
                    UpdatedBy = il.UpdatedBy,
                    UpdatedOn = il.UpdatedOn
                }).ToList();


        }

        public List<ItemLocationResponseVM> GetItemLocations()
        {
            return _context.ItemLocation.Where(il => !il.IsDeleted).Select(il => new ItemLocationResponseVM
            {
                Id = il.Id,
                Name = il.Name,
                BranchId = il.BranchId,
                TenantId = il.TenantId,
                CreatedBy = il.CreatedBy,
                CreatedOn = il.CreatedOn,
                UpdatedBy = il.UpdatedBy,
                UpdatedOn = il.UpdatedOn
            }).ToList();
        }

        public List<ItemLocationResponseVM> ItemLocationByTenantId(int tenantId)
        {
            return _context.ItemLocation.Where(il => !il.IsDeleted && il.TenantId == tenantId).Select(il => new ItemLocationResponseVM
            {
                Id = il.Id,
                Name = il.Name,
                BranchId = il.BranchId,
                TenantId = il.TenantId,
                CreatedBy = il.CreatedBy,
                CreatedOn = il.CreatedOn,
                UpdatedBy = il.UpdatedBy,
                UpdatedOn = il.UpdatedOn
            }).ToList();
        }

        public ItemLocationResponseVM UpdateItemLocation(int id, int tenantId, ItemLocationUpdateVM itemLocationUpdateVM)
        {
            var exsitingItemLocation = _context.ItemLocation.FirstOrDefault(il => !il.IsDeleted && il.Id == id && il.TenantId == tenantId);
            if (exsitingItemLocation == null) return null;
            exsitingItemLocation.Name = itemLocationUpdateVM.Name;
            exsitingItemLocation.BranchId = itemLocationUpdateVM.BranchId;
            exsitingItemLocation.UpdatedBy = itemLocationUpdateVM.UpdatedBy;
            exsitingItemLocation.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return ItemLocationResponseVM.ToViewModel(exsitingItemLocation);

        }

        
    }
}
