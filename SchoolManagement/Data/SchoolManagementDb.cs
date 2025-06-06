using Microsoft.EntityFrameworkCore;
using SchoolManagement.Model;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Data
{
    public class SchoolManagementDb : DbContext
    {
        public SchoolManagementDb(DbContextOptions<SchoolManagementDb> options) : base(options) { }

        // Existing DbSets
        public DbSet<MContact> Contacts { get; set; }
        public DbSet<MInstitution> Institutions { get; set; }
        public DbSet<MMasterType> MasterTypes { get; set; }
        public DbSet<MMaster> Masters { get; set; }
        public DbSet<MItemHeader> ItemHeaders { get; set; }
        public DbSet<MItem> Items { get; set; }
        public DbSet<MLibBookRecord> LibBookRecords { get; set; }
        public DbSet<MBook> Books { get; set; }
        public DbSet<MAccount> Accounts { get; set; }
        public DbSet<MTransaction> Transactions { get; set; }
        public DbSet<MPrefixSuffix> PrefixSuffix { get; set; }
        public DbSet<MUtilitiesList> UtilitiesList { get; set; }

        // New School Management DbSets
        public DbSet<MSubject> subjects { get; set; }
        public DbSet<MCourse> Courses { get; set; }
        public DbSet<MCourseSubject> course_subject { get; set; }
        public DbSet<MTopic> Topics { get; set; }
        public DbSet<MWeek> Weeks { get; set; }
        public DbSet<MPublicHoliday> PublicHolidays { get; set; }
        public DbSet<MTimeTable> TimeTables { get; set; }
        public DbSet<MPeriod> Periods { get; set; }
        public DbSet<MTimeTableDetail> TimeTableDetails { get; set; }
        public DbSet<MAssessmentSkill> AssessmentSkills { get; set; }
        public DbSet<MWorksheet> Worksheets { get; set; }
        public DbSet<MAssessment> Assessments { get; set; }
        public DbSet<MGrade> Grades { get; set; }
        public DbSet<MParent> Parents { get; set; }
        public DbSet<MStudent> Students { get; set; }
        public DbSet<MParentStudent> ParentStudents { get; set; }
        public DbSet<MTimeTableTopic> TimeTableTopics { get; set; }
        public DbSet<MTimeTableWorksheet> TimeTableWorksheets { get; set; }

        public DbSet<MDailyAssessment> DailyAssessments { get; set; }

        public DbSet<MTerm> Terms { get; set; }


        // views

        //public DbSet<DailyTeachingSchedule> DailyTeachingSchedules { get; set; }

        public DbSet<MVwComprehensiveTimeTable> VwComprehensiveTimeTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the MVwComprehensiveTimeTable as a database view
            // and specify that it has no primary key.
            modelBuilder.Entity<MVwComprehensiveTimeTable>().HasNoKey().ToView("vw_comprehensive_time_table");

            // You would add other model configurations here if needed,
            // for example, fluent API configurations for relationships or table mappings
            // if not using conventions.

            // Example for MContact if it requires configuration (assuming NeuroPi models don't auto-configure)
            // modelBuilder.Entity<MContact>().ToTable("contact"); // If table name is different from DbSet name
        }






    }
}