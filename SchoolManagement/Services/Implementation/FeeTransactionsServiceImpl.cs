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
                // 🔹 Check for existing transactions to avoid duplicates
                var studentIds = finalInsert.Select(f => f.StudentId).Distinct().ToList();
                var existingTransactions = _db.FeeTransactions
                    .Where(ft => studentIds.Contains(ft.StudentId) && !ft.IsDeleted)
                    .Select(ft => new { ft.StudentId, ft.FeeStructureId, ft.TrxDate })
                    .ToList();

                // Filter out transactions that already exist
                var transactionsToInsert = finalInsert
                    .Where(ft => !existingTransactions.Any(et =>
                        et.StudentId == ft.StudentId &&
                        et.FeeStructureId == ft.FeeStructureId &&
                        et.TrxDate.Date == ft.TrxDate.Date))
                    .ToList();

                if (transactionsToInsert.Any())
                {
                    _db.FeeTransactions.AddRange(transactionsToInsert);
                }
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



        public ResponseResult<FeeReportSummaryVM> GetStudentFeeReport(int tenantId, int studentId)
        {
            // 🔹 Query with joins and project directly into FeeReportTransactionVM
            var transactions = (from ft in _db.FeeTransactions
                                join s in _db.Students on ft.StudentId equals s.Id
                                join fs in _db.FeeStructures on ft.FeeStructureId equals fs.Id
                                join fp in _db.FeePackages
                                      on new { ft.FeeStructureId, s.CourseId, ft.TenantId }
                                   equals new { FeeStructureId = fp.FeeStructureId, fp.CourseId, fp.TenantId }
                                      into fpJoin
                                from fp in fpJoin.DefaultIfEmpty()
                                join m in _db.Masters on fp.PaymentPeriod equals m.Id into mJoin
                                from m in mJoin.DefaultIfEmpty()
                                where ft.TenantId == tenantId && ft.StudentId == studentId && !ft.IsDeleted && ft.TrxDate <= DateTime.UtcNow
                                orderby ft.TrxDate
                                select new FeeReportTransactionVM
                                {
                                    Id = ft.Id,
                                    TenantId = ft.TenantId,
                                    FeeStructureId = ft.FeeStructureId,
                                    FeeStructureName = fs.Name,
                                    StudentId = ft.StudentId,
                                    TrxDate = ft.TrxDate,
                                    TrxMonth = ft.TrxDate.ToString("MMM"),
                                    TrxYear = ft.TrxDate.Year.ToString(),
                                    TrxType = ft.TrxType,
                                    TrxName = ft.TrxName,
                                    Debit = ft.Debit,
                                    Credit = ft.Credit,
                                    TrxStatus = ft.TrxStatus,
                                    PaymentType = m.Name ?? "Annual"
                                })
                                .GroupBy(t => t.Id) // Group by transaction ID to remove duplicates
                                .Select(g => g.First()) // Take first from each group
                                .OrderBy(t => t.TrxDate)
                                .ToList();

            if (!transactions.Any())
            {
                return new ResponseResult<FeeReportSummaryVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "No transactions found for this student."
                );
            }

            // 🔹 Calculate summary from the projected list
            var totalFee = transactions.Sum(t => t.Debit);
            var totalPaid = transactions.Sum(t => t.Credit);
            var pendingFee = totalFee - totalPaid;

            // 🔹 For Student & Course info, re-query once (lightweight)
            var student = _db.Students
                .Include(s => s.Course)
                .FirstOrDefault(s => s.Id == studentId && s.TenantId == tenantId);

            var summaryVM = new FeeReportSummaryVM
            {
                StudentId = student?.Id ?? studentId,
                StudentName = student?.Name ?? string.Empty,
                CourseId = student?.CourseId ?? 0,
                CourseName = student?.Course?.Name ?? string.Empty,
                TotalFee = totalFee,
                TotalPaid = totalPaid,
                PendingFee = pendingFee,
                Transactions = transactions
            };

            return new ResponseResult<FeeReportSummaryVM>(
                HttpStatusCode.OK,
                summaryVM,
                "Fee report retrieved successfully."
            );
        }



        public ResponseResult<List<FeeReportTransactionVM>> GetRecentTransactions(
            int tenantId, 
            int branchId, 
            int courseId, 
            int limit)
        {
            // Get recent transactions for students in the specified course
            var transactions = (from ft in _db.FeeTransactions
                                join s in _db.Students on ft.StudentId equals s.Id
                                join fs in _db.FeeStructures on ft.FeeStructureId equals fs.Id
                                join fp in _db.FeePackages
                                      on new { ft.FeeStructureId, s.CourseId, ft.TenantId }
                                   equals new { FeeStructureId = fp.FeeStructureId, fp.CourseId, fp.TenantId }
                                      into fpJoin
                                from fp in fpJoin.DefaultIfEmpty()
                                join m in _db.Masters on fp.PaymentPeriod equals m.Id into mJoin
                                from m in mJoin.DefaultIfEmpty()
                                where ft.TenantId == tenantId 
                                   && s.CourseId == courseId 
                                   && s.BranchId == branchId
                                   && !ft.IsDeleted 
                                   && ft.TrxDate <= DateTime.UtcNow
                                orderby ft.TrxDate descending
                                select new FeeReportTransactionVM
                                {
                                    Id = ft.Id,
                                    TenantId = ft.TenantId,
                                    FeeStructureId = ft.FeeStructureId,
                                    FeeStructureName = fs.Name,
                                    StudentId = ft.StudentId,
                                    TrxDate = ft.TrxDate,
                                    TrxMonth = ft.TrxDate.ToString("MMM"),
                                    TrxYear = ft.TrxDate.Year.ToString(),
                                    TrxType = ft.TrxType,
                                    TrxName = ft.TrxName,
                                    Debit = ft.Debit,
                                    Credit = ft.Credit,
                                    TrxStatus = ft.TrxStatus,
                                    PaymentType = m.Name ?? "Annual"
                                })
                                .Take(limit)
                                .ToList();

            if (!transactions.Any())
            {
                return new ResponseResult<List<FeeReportTransactionVM>>(
                    HttpStatusCode.NotFound,
                    new List<FeeReportTransactionVM>(),
                    "No recent transactions found."
                );
            }

            return new ResponseResult<List<FeeReportTransactionVM>>(
                HttpStatusCode.OK,
                transactions,
                $"Retrieved {transactions.Count} recent transactions."
            );
        }

        public ResponseResult<int> AddPayment(AddPaymentRequest request)
        {
            try
            {
                var transaction = new MFeeTransactions
                {
                    TenantId = request.TenantId,
                    FeeStructureId = request.FeeStructureId, // Provided by frontend
                    StudentId = request.StudentId,
                    TrxDate = request.TrxDate,
                    TrxType = "credit",
                    TrxName = $"Payment Received - {request.PaymentMode}",
                    Debit = 0,
                    Credit = request.Amount,
                    TrxStatus = "Completed",
                    TrxId = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    UpdatedOn = DateTime.UtcNow,
                    UpdatedBy = request.CreatedBy,
                    IsDeleted = false
                };

                _db.FeeTransactions.Add(transaction);
                var result = _db.SaveChanges();

                return new ResponseResult<int>(
                    HttpStatusCode.OK,
                    result,
                    $"Payment of ₹{request.Amount} recorded successfully."
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<int>(
                    HttpStatusCode.InternalServerError,
                    0,
                    $"Error recording payment: {ex.Message}"
                );
            }
        }

        public ResponseResult<int> AddBill(AddBillRequest request)
        {
            try
            {
                // Try to find fee structure by name if not provided
                int feeStructureId = request.FeeStructureId ?? 0;
                
                if (feeStructureId == 0 && !string.IsNullOrEmpty(request.FeeStructureName))
                {
                    var feeStructure = _db.FeeStructures
                        .FirstOrDefault(fs => fs.TenantId == request.TenantId && 
                                             fs.Name == request.FeeStructureName);
                    
                    if (feeStructure != null)
                    {
                        feeStructureId = feeStructure.Id;
                    }
                }

                var transaction = new MFeeTransactions
                {
                    TenantId = request.TenantId,
                    FeeStructureId = feeStructureId,
                    StudentId = request.StudentId,
                    TrxDate = request.TrxDate,
                    TrxType = "debit",
                    TrxName = !string.IsNullOrEmpty(request.Description) 
                        ? request.Description 
                        : request.FeeStructureName,
                    Debit = request.Amount,
                    Credit = 0,
                    TrxStatus = "Pending",
                    TrxId = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    UpdatedOn = DateTime.UtcNow,
                    UpdatedBy = request.CreatedBy,
                    IsDeleted = false
                };

                _db.FeeTransactions.Add(transaction);
                var result = _db.SaveChanges();

                return new ResponseResult<int>(
                    HttpStatusCode.OK,
                    result,
                    $"Bill of ₹{request.Amount} added successfully."
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<int>(
                    HttpStatusCode.InternalServerError,
                    0,
                    $"Error adding bill: {ex.Message}"
                );
            }
        }
    }
}
