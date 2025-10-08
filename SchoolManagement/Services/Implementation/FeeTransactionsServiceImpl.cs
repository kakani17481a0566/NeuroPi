using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.FeeTransactions;
using System.Net;

namespace SchoolManagement.Services.Implementation
{
    public class FeeTransactionsServiceImpl : IFeeTransactions
    {
        private readonly SchoolManagementDb _db;

        public FeeTransactionsServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public ResponseResult<int> BuildFeeTransactions(BuildFeeTransactionsRequest request)
        {
            var insertedTransactions = new List<MFeeTransactions>();
            var skippedStudents = new List<int>(); // collect students with null join date

            // 🔹 1. Get students based on request filter
            var students = _db.Students
                .Where(s => !s.IsDeleted && s.TenantId == request.TenantId)
                .Where(s => !request.StudentId.HasValue || s.Id == request.StudentId.Value)
                .Where(s => !request.CourseId.HasValue || s.CourseId == request.CourseId.Value)
                .ToList();

            if (!students.Any())
                return new ResponseResult<int>(HttpStatusCode.NotFound, 0, "No students found for criteria.");

            // 🔹 2. Get Fee Structures + Terms (common for tenant)
            var feeStructures = _db.FeeStructures
                .Where(fs => fs.TenantId == request.TenantId)
                .ToList();

            var terms = _db.Terms
                .Where(t => !t.IsDeleted && t.TenantId == request.TenantId)
                .ToList();

            var academicYearEnd = terms.Any()
                ? terms.Max(t => t.EndDate)
                : DateTime.UtcNow.AddMonths(12);

            foreach (var student in students)
            {
                if (!student.DateOfJoining.HasValue)
                {
                    skippedStudents.Add(student.Id);
                    continue; // skip instead of failing whole batch
                }

                var joinDateTime = student.DateOfJoining.Value.ToDateTime(TimeOnly.MinValue);

                // 🔹 Get only relevant fee packages for this course
                var feePackages = _db.FeePackages
                    .Where(fp => fp.TenantId == request.TenantId && fp.CourseId == student.CourseId)
                    // ✅ Always include mandatory fees; include optional only if requested
                    .Where(fp => fp.FeeType == 0 || request.IncludeOptionalFeeStructures.Contains(fp.FeeStructureId))
                    .Include(fp => fp.PaymentPeriodMaster)
                    .ToList();

                // -----------------------
                // 3. Onetime
                // -----------------------
                foreach (var pkg in feePackages.Where(fp => fp.PaymentPeriodMaster?.Name == "Onetime"))
                {
                    var fee = feeStructures.FirstOrDefault(fs => fs.Id == pkg.FeeStructureId);
                    if (fee != null)
                        insertedTransactions.Add(BuildTransaction(student, fee, joinDateTime, request.CreatedBy));
                }

                // -----------------------
                // 4. Annual
                // -----------------------
                var annualFees = feePackages.Where(fp => fp.PaymentPeriodMaster?.Name == "Annual");
                if (annualFees.Any())
                {
                    foreach (var pkg in annualFees)
                    {
                        var fee = feeStructures.FirstOrDefault(fs => fs.Id == pkg.FeeStructureId);
                        if (fee != null)
                            insertedTransactions.Add(BuildTransaction(student, fee, joinDateTime, request.CreatedBy));
                    }
                }
                else
                {
                    var fee = feeStructures.FirstOrDefault(fs => fs.Name == "Annual Fee");
                    if (fee != null)
                        insertedTransactions.Add(BuildTransaction(student, fee, joinDateTime, request.CreatedBy));
                }

                // -----------------------
                // 5. Monthly
                // -----------------------
                foreach (var pkg in feePackages.Where(fp => fp.PaymentPeriodMaster?.Name == "Monthly"))
                {
                    var fee = feeStructures.FirstOrDefault(fs => fs.Id == pkg.FeeStructureId);
                    if (fee == null) continue;

                    var startDate = new DateTime(joinDateTime.Year, joinDateTime.Month, 1);
                    for (var dt = startDate; dt <= academicYearEnd; dt = dt.AddMonths(1))
                    {
                        var trxDate = dt < joinDateTime ? joinDateTime : dt;
                        insertedTransactions.Add(BuildTransaction(student, fee, trxDate, request.CreatedBy));
                    }
                }

                // -----------------------
                // 6. Term
                // -----------------------
                foreach (var pkg in feePackages.Where(fp => fp.PaymentPeriodMaster?.Name == "Term"))
                {
                    var fee = feeStructures.FirstOrDefault(fs => fs.Id == pkg.FeeStructureId);
                    if (fee == null) continue;

                    foreach (var term in terms.Where(t => joinDateTime <= t.EndDate))
                    {
                        var trxDate = term.StartDate < joinDateTime ? joinDateTime : term.StartDate;
                        insertedTransactions.Add(BuildTransaction(student, fee, trxDate, request.CreatedBy));
                    }
                }
            }

            // 🔹 Deduplicate
            var finalInsert = insertedTransactions
                .GroupBy(tx => new { tx.StudentId, tx.FeeStructureId, tx.TrxDate })
                .Select(g => g.First())
                .ToList();

            if (finalInsert.Any())
            {
                _db.FeeTransactions.AddRange(finalInsert);
            }

            var count = _db.SaveChanges();

            // Build message with skipped students info
            var msg = $"{count} fee transactions inserted successfully.";
            if (skippedStudents.Any())
            {
                msg += $" Skipped {skippedStudents.Count} students (no DateOfJoining): {string.Join(",", skippedStudents)}.";
            }

            return new ResponseResult<int>(
                HttpStatusCode.OK,
                count,
                msg
            );
        }

        private MFeeTransactions BuildTransaction(MStudent student, MFeeStructure fee, DateTime trxDate, int createdBy)
        {
            return new MFeeTransactions
            {
                TenantId = student.TenantId,
                FeeStructureId = fee.Id,
                StudentId = student.Id,
                TrxDate = trxDate,
                TrxType = "debit",
                TrxName = $"{fee.Name} Fee",
                Debit = fee.Amount,
                Credit = 0,
                TrxStatus = "Pending",
                TrxId = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                CreatedBy = createdBy,
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = createdBy,
                IsDeleted = false
            };
        }
    }
}
