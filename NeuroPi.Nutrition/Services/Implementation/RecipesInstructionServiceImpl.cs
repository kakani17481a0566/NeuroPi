using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.RecipesInstructions;
using System.Net;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class RecipesInstructionServiceImpl : IRecipesInstructions
    {
        private readonly NeutritionDbContext _context;

        public RecipesInstructionServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public RecipesInstructionsResponseVM CreateRecipesInstruction(RecipesInstructionsRequestVM request)
        {
            var Instruction = RecipesInstructionsRequestVM.ToModel(request);
            Instruction.CreatedOn= DateTime.UtcNow;
            _context.RecipesInstructions.Add(Instruction);
            _context.SaveChanges();
            return RecipesInstructionsResponseVM.ToViewModel(Instruction);

        }

        public bool DeleteRecipesInstruction(int id, int tenantId)
        {
            var Instruction = _context.RecipesInstructions.FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);
            if (Instruction == null)
            { 
                return false;
            
            }
            Instruction.IsDeleted = true;
            _context.SaveChanges();
            return true;

        }

        public List<RecipesInstructionsResponseVM> GetRecipesInstructions()
        {
            var InstructionList = _context.RecipesInstructions.Where(i => !i.IsDeleted).ToList();
            if (InstructionList == null)
            {
                return null;
            }
            return RecipesInstructionsResponseVM.ToViewModelList(InstructionList);
        }

        public RecipesInstructionsResponseVM GetRecipesInstructionsById(int id)
        {
            var Instruction =_context.RecipesInstructions.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (Instruction == null)
            {
                return null;
            }
            return RecipesInstructionsResponseVM.ToViewModel(Instruction);
        }

        public RecipesInstructionsResponseVM GetRecipesInstructionsByIdAndTenantId(int id, int tenantId)
        {
            var Instruction = _context.RecipesInstructions.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if(Instruction == null)
            {
                return null;
            }
            return RecipesInstructionsResponseVM.ToViewModel(Instruction);
        }

        public List<RecipesInstructionsResponseVM> GetRecipesInstructionsByTenantId(int tenantId)
        {
            var Instruction = _context.RecipesInstructions.Where(x => x.TenantId == tenantId && !x.IsDeleted).ToList();
            if (Instruction == null)
            {
                return null;
            }
            return RecipesInstructionsResponseVM.ToViewModelList(Instruction);
        }

        public RecipesInstructionsResponseVM UpdateRecipesInstruction(int id, int tenantId, RecipesInstructionsUpdateVM request)
        {
            var Instruction = _context.RecipesInstructions.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (Instruction == null)
            {
                return null;
            }
            return RecipesInstructionsResponseVM.ToViewModel(Instruction);
        }
    }
}
