using Microsoft.EntityFrameworkCore;

namespace NeuropiForms.Data
{
    public class NeuropiFormsDbContext : DbContext
    {
        public NeuropiFormsDbContext(DbContextOptions<NeuropiFormsDbContext> options) : base(options)
        {
        }

        public DbSet<Models.MComplintensMaster> ComplintensMasters { get; set; }
        public DbSet<Models.MComplintensStatus> ComplintensStatuses { get; set; }
        public DbSet<Models.MGroupModel> Groups { get; set; }
        public DbSet<Models.MListModel> Lists { get; set; }
        public DbSet<Models.MSection> Sections { get; set; }
        public DbSet<Models.MSectionGroup> SectionGroups { get; set; }
        public DbSet<Models.MField> Fields { get; set; }
        public DbSet<Models.MSectionField> SectionFields { get; set; }
        public DbSet<Models.MForm> Forms { get; set; }
        public DbSet<Models.MFormSectionMapping> FormSectionMappings { get; set; }
        public DbSet<Models.MFormVersion> FormVersions { get; set; }
        public DbSet<Models.MSoapService> SoapServices { get; set; }
        public DbSet<Models.MDatabaseResource> DatabaseResources { get; set; }
        public DbSet<Models.MFormSepTable> FormSepTables { get; set; }
        public DbSet<Models.MFormSubmission> FormSubmissions { get; set; }
        public DbSet<Models.MSubmissionFieldValue> SubmissionFieldValues { get; set; }
        public DbSet<Models.MSubmissionSectionValue> SubmissionSectionValues { get; set; }
    }
}
