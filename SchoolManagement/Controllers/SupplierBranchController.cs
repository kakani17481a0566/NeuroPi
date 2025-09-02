using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Supplier_Branch;


namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierBranchController : ControllerBase
    {
        public readonly ISupplierBranchService _supplierBranchService;
         public SupplierBranchController(ISupplierBranchService supplierBranchService)

        {
            _supplierBranchService = supplierBranchService;
            
            
        }
        [HttpGet]
        public ResponseResult<List<SupplierBranchVM>> GetAllSupplierBranches()
        {
            var supplierBranches = _supplierBranchService.GetAllSupplierBranches();
            return new ResponseResult<List<SupplierBranchVM>>(HttpStatusCode.OK, supplierBranches, "All supplier branches fetched successfully");
        }
        [HttpGet]
        [Route("{id}")]
        public ResponseResult<SupplierBranchVM> GetSupplierBranchById(int id)
        {
            var supplierBranch = _supplierBranchService.GetSupplierBranchById(id);
            if (supplierBranch == null)
            {
                return new ResponseResult<SupplierBranchVM>(HttpStatusCode.NotFound, null, "Supplier branch not found");
            }
            return new ResponseResult<SupplierBranchVM>(HttpStatusCode.OK, supplierBranch, "Supplier branch fetched successfully");
        }
        [HttpPost]
        public ResponseResult<SupplierBranchVM> CreateSupplierBranch([FromBody] SupplierBranchVM supplierBranch)
        {
            var createdSupplierBranch = _supplierBranchService.CreateSupplierBranch(supplierBranch);
            return new ResponseResult<SupplierBranchVM>(HttpStatusCode.Created, createdSupplierBranch, "Supplier branch created successfully");
        }
        [HttpPost]
        [Route("{id}")]
        public ResponseResult<SupplierBranchVM> UpdateSupplierBranch(int id, [FromBody] SupplierBranchVM supplierBranch)
        {
            var updatedSupplierBranch = _supplierBranchService.UpdateSupplierBranch(id, supplierBranch);
            if (updatedSupplierBranch == null)
            {
                return new ResponseResult<SupplierBranchVM>(HttpStatusCode.NotFound, null, "Supplier branch not found");
            }
            return new ResponseResult<SupplierBranchVM>(HttpStatusCode.OK, updatedSupplierBranch, "Supplier branch updated successfully");
        }
        [HttpDelete]
        [Route("{id}")]
        public ResponseResult<bool> DeleteSupplierBranch(int id)
        {
            var isDeleted = _supplierBranchService.DeleteSupplierBranch(id);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Supplier branch not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Supplier branch deleted successfully");
        }
    }

}
