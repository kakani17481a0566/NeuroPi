using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemSupplier;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemSupplierController : ControllerBase
    {
        public readonly IItemSupplierService _itemSupplierService;
        
        public ItemSupplierController(IItemSupplierService itemSupplierService)
        {
            _itemSupplierService = itemSupplierService;
        }
        [HttpGet]
        public ResponseResult<List<ItemSupplierResponseVM>> GetAllItemSuppliers()
        {
            var itemSuppliers = _itemSupplierService.GetAll();
            return new ResponseResult<List<ItemSupplierResponseVM>>(System.Net.HttpStatusCode.OK, itemSuppliers, "All item suppliers fetched successfully");
        }
        [HttpGet]
        [Route("tenant/{tenantId}")]
        public ResponseResult<List<ItemSupplierResponseVM>> GetAllItemSuppliersByTenantId(int tenantId)
        {
            var itemSuppliers = _itemSupplierService.GetAllByTenantId(tenantId);
            return new ResponseResult<List<ItemSupplierResponseVM>>(System.Net.HttpStatusCode.OK, itemSuppliers, "Item suppliers for tenant fetched successfully");
        }
        [HttpGet]

        [Route("{id}")]
        public ResponseResult<ItemSupplierResponseVM> GetItemSupplierById(int id)
        {
            var itemSupplier = _itemSupplierService.GetById(id);
            if (itemSupplier == null)
            {
                return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.NotFound, null, "Item supplier not found");
            }
            return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.OK, itemSupplier, "Item supplier fetched successfully");
        }
        [HttpGet]
        [Route("{id}/tenant/{tenantId}")]
        public ResponseResult<ItemSupplierResponseVM> GetItemSupplierByIdAndTenantId(int id, int tenantId)
        {
            var itemSupplier = _itemSupplierService.GetByIdAndTenantId(id, tenantId);
            if (itemSupplier == null)
            {
                return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.NotFound, null, "Item supplier not found for the given tenant");
            }
            return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.OK, itemSupplier, "Item supplier for tenant fetched successfully");
        }
        [HttpPost]
        public ResponseResult<ItemSupplierResponseVM> CreateItemSupplier([FromBody] ItemSupplierRequestVM request)
        {
            var createdItemSupplier = _itemSupplierService.CreateItemSupplier(request);
            return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.Created, createdItemSupplier, "Item supplier created successfully");
        }

        [HttpPut]
        [Route("{id}/tenant/{tenantId}")]
        public ResponseResult<ItemSupplierResponseVM> UpdateItemSupplier(int id, int tenantId, [FromBody] ItemSupplierRequestVM request)
        {
            var updatedItemSupplier = _itemSupplierService.UpdateItemSupplier(id, tenantId, request);
            if (updatedItemSupplier == null)
            {
                return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.NotFound, null, "Item supplier not found for the given tenant");
            }
            return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.OK, updatedItemSupplier, "Item supplier updated successfully");
        }
        [HttpDelete]
        [Route("{id}/tenant/{tenantId}")]
        public ResponseResult<ItemSupplierResponseVM> DeleteItemSupplier(int id, int tenantId)
        {
            var deletedItemSupplier = _itemSupplierService.DeleteByIdAndTenantId(id, tenantId);
            if (deletedItemSupplier == null)
            {
                return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.NotFound, null, "Item supplier not found for the given tenant");
            }
            return new ResponseResult<ItemSupplierResponseVM>(System.Net.HttpStatusCode.OK, deletedItemSupplier, "Item supplier deleted successfully");
        }


    }
}
