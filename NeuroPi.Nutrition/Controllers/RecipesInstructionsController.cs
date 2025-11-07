using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.RecipesInstructions;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    public class RecipesInstructionsController
    {
        private readonly IRecipesInstructions _service;

        public RecipesInstructionsController(IRecipesInstructions service)
        {
            _service = service;
        }

        [HttpGet]
        public ResponseResult<List<RecipesInstructionsResponseVM>> GetRecipesInstructions()
        {
            var result = _service.GetRecipesInstructions();
            if (result == null)
            {
                return new ResponseResult<List<RecipesInstructionsResponseVM>>(HttpStatusCode.NotFound, null, "Recipes instructions not found");
            }
            return new ResponseResult<List<RecipesInstructionsResponseVM>>(HttpStatusCode.OK, result, "Recipes instructions details fetched successfully");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<RecipesInstructionsResponseVM> GetRecipesInstructionsById(int id)
        {
            var result = _service.GetRecipesInstructionsById(id);
            if (result == null)
            {
                return new ResponseResult<RecipesInstructionsResponseVM>(HttpStatusCode.NotFound, null, "Recipes instructions not found");
            }
            return new ResponseResult<RecipesInstructionsResponseVM>(HttpStatusCode.OK, result, "Recipes instructions details fetched successfully");
        }

        [HttpGet("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<RecipesInstructionsResponseVM> GetRecipesInstructionsByIdAndTenantId(int id, int tenantId)
        {
            var result = _service.GetRecipesInstructionsByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<RecipesInstructionsResponseVM>(HttpStatusCode.NotFound, null, "Recipes instructions not found");
            }
            return new ResponseResult<RecipesInstructionsResponseVM>(HttpStatusCode.OK, result, "Recipes instructions details fetched successfully");

        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<RecipesInstructionsResponseVM>> GetRecipesInstructionsByTenantId(int tenantId)
        {
            var result = _service.GetRecipesInstructionsByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<RecipesInstructionsResponseVM>>(HttpStatusCode.NotFound, null, "Recipes instructions not found");
            }
            return new ResponseResult<List<RecipesInstructionsResponseVM>>(HttpStatusCode.OK, result, "Recipes instructions details fetched successfully");
        }

        [HttpPost]
        public ResponseResult<RecipesInstructionsResponseVM> CreateRecipesInstructions([FromBody] RecipesInstructionsRequestVM vm)
        {
            var result = _service.CreateRecipesInstruction(vm);
            return new ResponseResult<RecipesInstructionsResponseVM>(HttpStatusCode.OK, result, "Recipes Instruction Created Successfully");            
        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<RecipesInstructionsResponseVM> UpdateRecipesInstructions(int id, int tenantId, RecipesInstructionsUpdateVM updateVM)
        {
            var result = _service.UpdateRecipesInstruction(id, tenantId, updateVM);
            if(result == null)
            {
                return new ResponseResult<RecipesInstructionsResponseVM>(HttpStatusCode.NotFound, null, "Recipes Instructions not found");
            }
            return new ResponseResult<RecipesInstructionsResponseVM>(HttpStatusCode.OK, result, "Recipes Instruction updated Successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteRecipesInstructions(int id, int tenantId)
        { 
            var result = _service.DeleteRecipesInstruction(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Recipes Instructions not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Recipes Instructions deleted successfully");

        }
    }
}
