using Microsoft.EntityFrameworkCore;
using NeuropiForms.Models;

namespace NeuropiForms.Data
{
    public class NeuropiFormsDbContext : DbContext
    {
        public NeuropiFormsDbContext(DbContextOptions<NeuropiFormsDbContext> options) : base(options)
        {
        }

        public DbSet<MComplianceMaster> ComplianceMasters { get; set; }
        public DbSet<MComplianceStatus> ComplianceStatuses { get; set; }
        public DbSet<MGroupModel> Groups { get; set; }
        public DbSet<MListModel> Lists { get; set; }
        public DbSet<MSection> Sections { get; set; }
        public DbSet<MSectionGroup> SectionGroups { get; set; }
        public DbSet<MField> Fields { get; set; }
        public DbSet<MSectionField> SectionFields { get; set; }
        public DbSet<MForm> Forms { get; set; }
        public DbSet<MFormSectionMapping> FormSectionMappings { get; set; }
        public DbSet<MFormVersion> FormVersions { get; set; }
        public DbSet<MSoapService> SoapServices { get; set; }
        public DbSet<MDatabaseResource> DatabaseResources { get; set; }
        public DbSet<MFormSepTable> FormSepTables { get; set; }
        public DbSet<MFormSubmission> FormSubmissions { get; set; }
        public DbSet<MSubmissionFieldValue> SubmissionFieldValues { get; set; }
        public DbSet<MSubmissionSectionValue> SubmissionSectionValues { get; set; }
    }
}
