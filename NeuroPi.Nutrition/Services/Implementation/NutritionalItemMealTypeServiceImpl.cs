using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionalItemMealType;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class NutritionalItemMealTypeServiceImpl : INutritionalItemMealType
    {
        private readonly NeutritionDbContext _context;

        public  NutritionalItemMealTypeServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public NutritionalItemMealTypeResponseVM CreateNutrionalItemMealType(NutritionalItemMealTypeRequestVM vm)
        {
            var newNIMealType = NutritionalItemMealTypeRequestVM.ToModel(vm);
            _context.NutritionalItemMealType.Add(newNIMealType);
            _context.SaveChanges();
            return NutritionalItemMealTypeResponseVM.ToViewModel(newNIMealType);
        }

        public bool DeleteNutritionalItemMealType(int id, int tenantId)
        {
            var existingNIMealType = _context.NutritionalItemMealType.FirstOrDefault(mt => mt.Id == id && mt.TenantId == tenantId && !mt.IsDeleted);
            if (existingNIMealType == null)
            {
                return false;
            }
            existingNIMealType.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<NutritionalItemMealTypeResponseVM> GetNutritionalItemMealType()
        {
            var NIMealType = _context.NutritionalItemMealType.Where(mt=> !mt.IsDeleted).ToList();   
            if( NIMealType == null )
            {
                return null;
            }
            return NutritionalItemMealTypeResponseVM.ToViewModelList( NIMealType );

        }

        public NutritionalItemMealTypeResponseVM GetNutritionalItemMealTypeById(int id)
        {
            var NIMealType = _context.NutritionalItemMealType.FirstOrDefault( mt => mt.Id==id && !mt.IsDeleted);
            if(NIMealType == null )
            {
                return null;
            }
            return NutritionalItemMealTypeResponseVM.ToViewModel( NIMealType );
        }

        public NutritionalItemMealTypeResponseVM GetNutritionalItemMealTypeByIdAndTenantId(int id, int tenantId)
        {
            var NIMealType = _context.NutritionalItemMealType.FirstOrDefault(mt => mt.Id == id&& mt.TenantId== tenantId && !mt.IsDeleted);
            if (NIMealType == null)
            {
                return null;
            }
            return NutritionalItemMealTypeResponseVM.ToViewModel(NIMealType);


        }

        public List<NutritionalItemMealTypeResponseVM> GetNutritionalItemMealTypeByTenantId(int tenantId)
        {
            var NIMealType = _context.NutritionalItemMealType.Where(mt=>mt.TenantId==tenantId && !mt.IsDeleted).ToList();
            if( NIMealType == null )
            {
                return null;
            }
            return NutritionalItemMealTypeResponseVM.ToViewModelList( NIMealType );
        }

        public NutritionalItemMealTypeResponseVM UpdateNutritionalMealType(int id, int tenantId, NutritionalItemMealTypeUpdateVM vm)
        {
            var NIMealType = _context.NutritionalItemMealType.FirstOrDefault(mt => mt.Id == id && mt.TenantId == tenantId && !mt.IsDeleted);
            if (NIMealType == null)
            {
                return null;
            
            }
            NIMealType.NutritionalItemId = vm.NutritionalItemId;
            NIMealType.MealTypeId = vm.MealTypeId;
            NIMealType.UpdatedBy = vm.UpdatedBy;
            NIMealType.UpdatedOn = vm.UpdatedOn;
            _context.SaveChanges();
            return NutritionalItemMealTypeResponseVM.ToViewModel(NIMealType);

        }
    }
}
