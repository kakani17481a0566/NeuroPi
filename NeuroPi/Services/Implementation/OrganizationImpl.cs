using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Organization;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class OrganizationImpl : IOrganizationService
    {
        private readonly NeuroPiDbContext _context;

        public OrganizationImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public List<OrganizationVM> GetAll()
        {
            return _context.Organizations
                .Where(o => !o.IsDeleted)
                .Select(o => new OrganizationVM
                {
                    OrganizationId = o.OrganizationId,
                    Name = o.Name,
                    ParentId = o.ParentId,
                    TenantId = o.TenantId
                }).ToList();
        }

        public OrganizationVM GetById(int id)
        {
            var org = _context.Organizations.FirstOrDefault(o => o.OrganizationId == id && !o.IsDeleted);
            if (org == null) return null;

            return new OrganizationVM
            {
                OrganizationId = org.OrganizationId,
                Name = org.Name,
                ParentId = org.ParentId,
                TenantId = org.TenantId
            };
        }

        public OrganizationVM Create(OrganizationInputVM input)
        {
            var entity = new MOrganization
            {
                Name = input.Name,
                ParentId = input.ParentId,
                TenantId = input.TenantId,
                IsDeleted = false
            };

            _context.Organizations.Add(entity);
            _context.SaveChanges();

            return new OrganizationVM
            {
                OrganizationId = entity.OrganizationId,
                Name = entity.Name,
                ParentId = entity.ParentId,
                TenantId = entity.TenantId
            };
        }

        public OrganizationVM Update(int id, OrganizationUpdateInputVM input)
        {
            var org = _context.Organizations.FirstOrDefault(o => o.OrganizationId == id && !o.IsDeleted);
            if (org == null) return null;

            org.Name = input.Name;
            org.ParentId = input.ParentId;
            org.UpdatedBy = input.UpdatedBy;
            org.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return new OrganizationVM
            {
                OrganizationId = org.OrganizationId,
                Name = org.Name,
                ParentId = org.ParentId,
                TenantId = org.TenantId
            };
        }

        public bool Delete(int id)
        {
            var org = _context.Organizations.FirstOrDefault(o => o.OrganizationId == id && !o.IsDeleted);
            if (org == null) return false;

            org.IsDeleted = true;
            org.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }


        public List<OrganizationVM> GetByTenantId(int tenantId)
        {
            return _context.Organizations
                .Where(o => o.TenantId == tenantId && !o.IsDeleted)
                .Select(o => new OrganizationVM
                {
                    OrganizationId = o.OrganizationId,
                    Name = o.Name,
                    ParentId = o.ParentId,
                    TenantId = o.TenantId
                }).ToList();
        }


    }
}
