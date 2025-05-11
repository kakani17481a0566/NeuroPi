using System.Collections.Generic;
using System.Linq;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Organization;

namespace NeuroPi.Services.Implementation
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
            return _context.Organizations.Select(o => new OrganizationVM
            {
                OrganizationId = o.OrganizationId,
                Name = o.Name,
                ParentId = o.ParentId,
                TenantId = o.TenantId
            }).ToList();
        }

        public OrganizationVM GetById(int id)
        {
            var org = _context.Organizations.FirstOrDefault(o => o.OrganizationId == id);
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
                TenantId = input.TenantId
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
            var org = _context.Organizations.FirstOrDefault(o => o.OrganizationId == id);
            if (org == null)
            {
                return null; 
            }

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
            var org = _context.Organizations.FirstOrDefault(o => o.OrganizationId == id);
            if (org == null) return false;

            _context.Organizations.Remove(org);
            _context.SaveChanges();
            return true;
        }
    }
}
