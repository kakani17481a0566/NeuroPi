using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuroPi.Services.Implementation
{
    public class TeamServiceImpl : ITeamService
    {
        private readonly NeuroPiDbContext _context;

        public TeamServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        // Get all non-deleted teams
        public List<MTeam> GetAllTeams()
        {
            return _context.Teams
                .Where(t => !t.IsDeleted)
                .ToList();
        }

        // Get a non-deleted team by ID
        public MTeam GetTeamById(int id)
        {
            return _context.Teams
                .FirstOrDefault(t => t.TeamId == id && !t.IsDeleted);
        }

        // Create a new team
        public MTeam CreateTeam(MTeam team)
        {
            team.IsDeleted = false;
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team;
        }

        // Update an existing team
        public MTeam UpdateTeam(int id, MTeam updatedTeam)
        {
            var existing = _context.Teams.FirstOrDefault(t => t.TeamId == id && !t.IsDeleted);
            if (existing == null) return null;

            existing.Name = updatedTeam.Name;
            existing.TenantId = updatedTeam.TenantId;
            existing.UpdatedBy = updatedTeam.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return existing;
        }

        // Soft delete a team using IsDeleted
        public bool SoftDeleteTeam(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.TeamId == id && !t.IsDeleted);
            if (team == null) return false;

            team.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
    }
}
