using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NeuroPi.UserManagment.Model;
using SchoolManagement.Model;
using SchoolManagement.ViewModel.ItemSupplier;
using SchoolManagement.ViewModel.VwTermPlanDetails;
using static SchoolManagement.Model.MTableFiles;

namespace SchoolManagement.Data
{
    public class SchoolManagementDb : DbContext
    {
        internal object visitor;

        public SchoolManagementDb(DbContextOptions<SchoolManagementDb> options) : base(options) { }

        public DbSet<MTenant> Tenants { get; set; }
        public DbSet<MUser> Users { get; set; }  // Add if missing



        // Existing DbSets
        public DbSet<MContact> Contacts { get; set; }
        public DbSet<MInstitution> Institutions { get; set; }
        public DbSet<MMasterType> MasterTypes { get; set; }
        public DbSet<MMaster> Masters { get; set; }
        public DbSet<MItemHeader> ItemHeaders { get; set; }
        public DbSet<MItems> Items { get; set; }
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

        public DbSet<MStudentAttendance> StudentAttendance { get; set; }

        public DbSet<MCourseTeacher> CourseTeachers { get; set; }
        public DbSet<MVisitor> Visitors { get; set; }
        public DbSet<MPhoneLog> PhoneLogs { get; set; }
        public DbSet<MSupplier> Supplier { get; set; }
        public DbSet<MSupplierBranch> SupplierBranches { get; set; }
        public DbSet<MItemSupplier> ItemSuppliers { get; set; }
        public DbSet<MOrders> Orders { get; set; }

        public DbSet<MItems> items { get; set; }

        public DbSet<MItem> item { get; set; }

        public DbSet<MItemCategory> ItemCategory { get; set; }

        public DbSet<MItemLocation> ItemLocation { get; set; }

        public DbSet<MItemBranch> ItemBranch { get; set; }

        public DbSet<MOrderItem> OrderItem { get; set; }



        // views
        public DbSet<MVwComprehensiveTimeTable> VwComprehensiveTimeTables { get; set; }
        public DbSet<MVwTermPlanDetails> VwTermPlanDetails { get; set; }

        public DbSet<MVTimeTable> VTimeTable { get; set; }

        public DbSet<MVTermTable> VTermTable { get; set; }

        public DbSet<MImage> images { get; set; }

        public DbSet<MTestContent> TestContent { get; set; }

        public DbSet<MTest> test { get; set; }
        public DbSet<MTestResult> TestResult { get; set; }




        //Registrationform

        public DbSet<MStudentRegistration> StudentRegistrations { get; set; } = null!;


        public DbSet<MStudentsEnquiry> StudentsEnquiries { get; set; }


        public DbSet<MStudentCourse> StudentCourses { get; set; }

        public DbSet<MCountingTestContent> CountingTestContents { get; set; }


        public DbSet<MItemsGroup> ItemsGroup { get; set; }
        public DbSet<MItemsImage> ItemsImages { get; set; }

        public DbSet<MGenere> generes {  get; set; }



        //fee
        public DbSet<MFeeStructure> FeeStructures { get; set; }
        public DbSet<MFeePackage> FeePackages { get; set; }
        public DbSet<MFeePackageMaster> FeePackageMasters { get; set; }

        public DbSet<MCorporate> Corporates { get; set; } = null!;

        public DbSet<MPostTransactionMaster> postTransactionMasters{ get; set; }
        public DbSet<MLibraryTransaction> LibraryTransaction { get; set; }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Always call base first

            // ✅ 1. Apply UTC conversion to all DateTime properties
            var dateTimeProps = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));

            foreach (var prop in dateTimeProps)
            {
                prop.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                ));
            }



            // ✅ 2. Configure views
            modelBuilder.Entity<MVwComprehensiveTimeTable>()
                .HasNoKey()
                .ToView("vw_comprehensive_time_table");

            modelBuilder.Entity<MVwTermPlanDetails>()
                .HasNoKey()
                .ToView("vw_term_plan_details");

            modelBuilder.Entity<MVTimeTable>()
                .HasNoKey()
                .ToView("v_time_table");

            modelBuilder.Entity<MVTermTable>()
                .HasNoKey()
                .ToView("v_term_table");


            modelBuilder.Entity<MItemsGroup>()
                .HasOne(g => g.SetItem)
                .WithMany()
                .HasForeignKey(g => g.SetItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MItemsGroup>()
                .HasOne(g => g.Item)
                .WithMany()
                .HasForeignKey(g => g.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MItemsGroup>()
                .HasOne(g => g.Tenant)
                .WithMany()
                .HasForeignKey(g => g.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 ItemsImage FK relationships
            modelBuilder.Entity<MItemsImage>()
                .HasOne(i => i.Item)
                .WithMany()
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MItemsImage>()
                .HasOne(i => i.Tenant)
                .WithMany()
                .HasForeignKey(i => i.TenantId)
                .OnDelete(DeleteBehavior.Cascade);


            // Map + indexes for student_registration
            modelBuilder.Entity<MStudentRegistration>(entity =>
            {
                entity.ToTable("student_registration");

                // Ensure DATE (not timestamp) for these two
                entity.Property(e => e.RegDate).HasColumnType("date");
                entity.Property(e => e.StuDob).HasColumnType("date");

                // Unique per-tenant reg_number for active rows (matches DB index)
                entity.HasIndex(e => new { e.TenantId, e.RegNumber })
                      .HasDatabaseName("uq_sr_reg_number_per_tenant")
                      .IsUnique()
                      .HasFilter("is_deleted = false");
            });



        }




    }
}