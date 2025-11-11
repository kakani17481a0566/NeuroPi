using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserGene;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class UserGeneServiceImpl : IUserGene
    {
        private readonly NeutritionDbContext _context;

        public UserGeneServiceImpl(NeutritionDbContext nutritionDbContext)
        {
            _context = nutritionDbContext;
        }

        public UserGeneResponseVM CreateUserGene(UserGeneRequestVM UserGeneRequestVM)
        {
            var UserGene = UserGeneRequestVM.ToModel(UserGeneRequestVM);
            UserGene.CreatedOn = DateTime.UtcNow;
            _context.UserGene.Add(UserGene);
            _context.SaveChanges();
            return UserGeneResponseVM.ToViewModel(UserGene);

        }

        public bool DeleteUserGene(int id, int tenantId)
        {
            var UserGene = _context.UserGene
                .FirstOrDefault(uf => uf.Id == id && uf.TenantId == tenantId && !uf.IsDeleted);
            if (UserGene == null)
            {
                return false;
            }
            UserGene.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<UserGeneResponseVM> GetUserGene()
        {
            var UserGene = _context.UserGene
                .Where(uf => !uf.IsDeleted)
                .ToList();
            if (UserGene == null || UserGene.Count == 0)
            {
                return new List<UserGeneResponseVM>();
            }
            return UserGeneResponseVM.ToViewModelList(UserGene);
        }

        public UserGeneResponseVM GetUserGeneById(int id)
        {
            var UserGene = _context.UserGene
                .FirstOrDefault(uf => uf.Id == id && !uf.IsDeleted);
            if (UserGene != null)
            {
                return UserGeneResponseVM.ToViewModel(UserGene);
            }
            return null;
        }

        public UserGeneResponseVM GetUserGeneByIdAndTenantId(int id, int tenantId)
        {
            var UserGene = _context.UserGene
                .FirstOrDefault(uf => uf.Id == id && uf.TenantId == tenantId && !uf.IsDeleted);
            if (UserGene != null)
            {
                return UserGeneResponseVM.ToViewModel(UserGene);
            }
            return null;
        }

        public List<UserGeneResponseVM> GetUserGeneByTenantId(int tenantId)
        {
            var UserGene = _context.UserGene
                .Where(uf => uf.TenantId == tenantId && !uf.IsDeleted)
                .ToList();
            if (UserGene == null || UserGene.Count == 0)
            {
                return new List<UserGeneResponseVM>();
            }
            return UserGeneResponseVM.ToViewModelList(UserGene);

        }

        public UserGeneResponseVM UpdateUserGene(int id, int tenatId, UserGeneUpdateVM UserGeneRequestVM)
        {
            var existingGene = _context.UserGene
                .FirstOrDefault(uf => uf.Id == id && uf.TenantId == tenatId && !uf.IsDeleted);
            if (existingGene == null)
            {
                return null;
            }

            existingGene.UserId = UserGeneRequestVM.UserId;
            existingGene.GeneId = UserGeneRequestVM.GeneId;
            existingGene.GeneStatus = UserGeneRequestVM.GeneStatus;
            existingGene.Notes = UserGeneRequestVM.Notes;
            existingGene.UpdatedBy = UserGeneRequestVM.UpdatedBy;
            existingGene.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return UserGeneResponseVM.ToViewModel(existingGene);
        }


    }
}
