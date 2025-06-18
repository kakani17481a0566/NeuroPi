using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Model;
using SchoolManagement.Model;
using SchoolManagement.ViewModel.VwTermPlanDetails;
using static SchoolManagement.Model.MTableFiles;

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

        public DbSet<MBranch> Branches { get; set; }
        public DbSet<MEmployee> Employees { get; set; }



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

        public DbSet<MTableFiles> TableFiles { get; set; }

        public DbSet<MTimeTableAssessment> TimeTableAssessments { get; set; }



        // views
        public DbSet<MVwComprehensiveTimeTable> VwComprehensiveTimeTables { get; set; }
        public DbSet<MVwTermPlanDetails> VwTermPlanDetails { get; set; }

        public DbSet<MVTimeTable> VTimeTable { get; set; }

        public DbSet<MVTermTable> VTermTable { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MVwComprehensiveTimeTable>().HasNoKey().ToView("vw_comprehensive_time_table");

            modelBuilder.Entity<MVwTermPlanDetails>().HasNoKey().ToView("vw_term_plan_details");

            modelBuilder.Entity<MVTimeTable>().HasNoKey().ToView("v_time_table");

            modelBuilder.Entity<MVTermTable>().HasNoKey().ToView("v_term_table");

            //modelBuilder.Entity<MTimeTableAssessment>().ToTable("time_table_assessments");

        }
    }
}