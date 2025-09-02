using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Supplier;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        public readonly SupplierInterface _supplierService;
        public SupplierController(SupplierInterface supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpGet]
        public ResponseResult<List<SupplierVM>> GetAllSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return new ResponseResult<List<SupplierVM>>(HttpStatusCode.OK, suppliers, "All suppliers fetched successfully");
        }
        [HttpGet("{id}")]

        public ResponseResult<SupplierVM> GetSupplierById(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                return new ResponseResult<SupplierVM>(HttpStatusCode.NotFound, null, "Supplier not found");
            }
            return new ResponseResult<SupplierVM>(HttpStatusCode.OK, supplier, "Supplier fetched successfully");
        }

        [HttpPost]
        public ResponseResult<SupplierVM> CreateSupplier([FromBody] SupplierVM supplier)
        {
            var createdSupplier = _supplierService.CreateSupplier(supplier);
            return new ResponseResult<SupplierVM>(HttpStatusCode.Created, createdSupplier, "Supplier created successfully");
        }
        [HttpPut("{id}")]
        public ResponseResult<SupplierVM> UpdateSupplier(int id, [FromBody] SupplierVM supplier)
        {
            var updatedSupplier = _supplierService.UpdateSupplier(id, supplier);
            if (updatedSupplier == null)
            {
                return new ResponseResult<SupplierVM>(HttpStatusCode.NotFound, null, "Supplier not found");
            }
            return new ResponseResult<SupplierVM>(HttpStatusCode.OK, updatedSupplier, "Supplier updated successfully");
        }
        [HttpDelete("{id}")]
        public ResponseResult<bool> DeleteSupplier(int id)
        {
            var isDeleted = _supplierService.DeleteSupplier(id);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Supplier not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Supplier deleted successfully");
        }

    }
}
