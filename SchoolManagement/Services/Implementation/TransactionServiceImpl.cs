using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SchoolManagement.Data;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Transaction;
using SchoolManagement.ViewModel.Transcation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace SchoolManagement.Services.Implementation
{
    public class TransactionServiceImpl : ITransactionService
    {
        private readonly string _connectionString;
        private readonly SchoolManagementDb _context;

        public TransactionServiceImpl(IConfiguration configuration, SchoolManagementDb context)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
        }

        #region Transfer

        // Transfer money between accounts
        // Developed by: Mohith
        public TransactionResultVM Transfer(TransactionRequestVM req)
        {
            if (req.Amount <= 0)
            {
                return new TransactionResultVM
                {
                    IsSuccess = false,
                    ErrorMessage = "Amount must be greater than zero."
                };
            }

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("p_debit_acc_id", req.DebitAccId, DbType.Int32);
                parameters.Add("p_credit_acc_id", req.CreditAccId, DbType.Int32);
                parameters.Add("p_amount", req.Amount, DbType.Double);
                parameters.Add("p_trx_mode_id", req.TrxModeId, DbType.Int32);
                parameters.Add("p_trx_status", req.TrxStatus, DbType.Int32);
                parameters.Add("p_tenant_id", req.TenantId, DbType.Int32);
                parameters.Add("p_created_by", req.CreatedBy, DbType.Int32);
                parameters.Add("p_trx_desc", req.TrxDesc ?? string.Empty, DbType.String);
                parameters.Add("p_debit_acc_head_id", req.DebitAccHeadId, DbType.Int32);
                parameters.Add("p_credit_acc_head_id", req.CreditAccHeadId, DbType.Int32);
                parameters.Add("p_trx_date", req.TrxDate, DbType.Date); // ✅ NEW

                const string sql = @"
            SELECT
                debit_trx_id AS DebitTrxId,
                credit_trx_id AS CreditTrxId,
                ref_trns_id AS RefTrnsId
            FROM sp_full_transaction_transfer(
                @p_debit_acc_id,
                @p_credit_acc_id,
                @p_amount,
                @p_trx_mode_id,
                @p_trx_status,
                @p_tenant_id,
                @p_created_by,
                @p_trx_desc,
                @p_debit_acc_head_id,
                @p_credit_acc_head_id,
                @p_trx_date -- ✅ Include this in the function call
            );
        ";

                var result = connection.QueryFirstOrDefault<SpTransferResult>(sql, parameters);

                if (result == null)
                {
                    return new TransactionResultVM
                    {
                        IsSuccess = false,
                        ErrorMessage = "Transaction failed or no data returned."
                    };
                }

                return new TransactionResultVM
                {
                    IsSuccess = true,
                    DebitTrxId = result.DebitTrxId,
                    CreditTrxId = result.CreditTrxId,
                    RefTrnsId = result.RefTrnsId
                };
            }
            catch (Exception ex)
            {
                return new TransactionResultVM
                {
                    IsSuccess = false,
                    ErrorMessage = $"An error occurred during transfer: {ex.Message}"
                };
            }
        }


        #endregion

        #region Querying Transactions

        public TransactionResponseVM GetByTrxId(int trxId)
        {
            var trx = _context.Transactions
                .AsNoTracking()
                .FirstOrDefault(t => t.TrxId == trxId && !t.IsDeleted);

            return trx == null ? null : MapToVM(trx);
        }

        public TransactionResponseVM GetByTrxIdAndTenantId(int trxId, int tenantId)
        {
            var trx = _context.Transactions
                .AsNoTracking()
                .FirstOrDefault(t => t.TrxId == trxId && t.TenantId == tenantId && !t.IsDeleted);

            return trx == null ? null : MapToVM(trx);
        }

        public List<TransactionResponseVM> GetByRefTrnsId(string refTrnsId)
        {
            var trxs = _context.Transactions
                .AsNoTracking()
                .Where(t => t.RefTrnsId == refTrnsId && !t.IsDeleted)
                .ToList();

            return trxs.Any() ? trxs.Select(MapToVM).ToList() : null;
        }

        #endregion

        #region Update

        public bool UpdateAmountByRefTrnsIdAndTenant(string refTrnsId, int tenantId, double newAmount, int modifiedBy)
        {
            var transactions = _context.Transactions
                .Where(t => t.RefTrnsId == refTrnsId && t.TenantId == tenantId && !t.IsDeleted)
                .ToList();

            if (!transactions.Any())
                return false;

            foreach (var trx in transactions)
            {
                trx.TrxAmount = (float)newAmount;
                trx.ModifiedOn = DateTime.UtcNow;
                trx.ModifiedBy = modifiedBy;
            }

            _context.SaveChanges();
            return true;
        }

        #endregion

        #region Helpers

        private TransactionResponseVM MapToVM(dynamic trx)
        {
            return new TransactionResponseVM
            {
                TrxId = trx.TrxId,
                AccId = trx.AccId,
                TrxAmount = trx.TrxAmount ?? 0,
                TrxDesc = trx.TrxDesc,
                RefTrnsId = trx.RefTrnsId,
                CreatedOn = trx.CreatedOn,
                TenantId = trx.TenantId,
                TrxTypeId = trx.TrxTypeId ?? 0,
                TrxModeId = trx.TrxModeId ?? 0,
                TrxStatus = trx.TrxStatus ?? 0,
                AccHeadId = trx.AccHeadId
            };
        }

        private class SpTransferResult
        {
            public int DebitTrxId { get; set; }
            public int CreditTrxId { get; set; }
            public string RefTrnsId { get; set; }
        }

        #endregion



        public ResponseResult<VTransactionTableVM> GetFormattedTransactionTable(int tenantId)
        {
            try
            {
                var transactions = (
                    from t in _context.Transactions
                    join acc in _context.Accounts on t.AccId equals acc.Id into accGroup
                    from acc in accGroup.DefaultIfEmpty()
                    join tt in _context.Masters on t.TrxTypeId equals tt.Id into ttGroup
                    from tt in ttGroup.DefaultIfEmpty()
                    join tm in _context.Masters on t.TrxModeId equals tm.Id into tmGroup
                    from tm in tmGroup.DefaultIfEmpty()
                    join ts in _context.Masters on t.TrxStatus equals ts.Id into tsGroup
                    from ts in tsGroup.DefaultIfEmpty()
                    join ah in _context.Masters on t.AccHeadId equals ah.Id into ahGroup
                    from ah in ahGroup.DefaultIfEmpty()
                    where !t.IsDeleted && t.TenantId == tenantId
                    orderby t.CreatedOn descending
                    select new
                    {
                        t.TrxId,
                        t.TrxDesc,
                        t.TrxDate,
                        t.TrxAmount,
                        t.RefTrnsId,
                        AccountId = acc ,
                        AccountName = acc.AccName,
                        AccountType = acc.AccType,
                        BankName = acc.BankName,
                        Branch = acc.Branch,
                        IFSCCode = acc.IfscCode,
                        TrxTypeName = tt.Name,
                        TrxModeName = tm.Name,
                        TrxStatusName = ts.Name,
                        AccHeadName = ah.Name,
                        t.CreatedBy,
                        t.CreatedOn,
                        t.TenantId
                    }).ToList();

                var headerLabels = new List<string>
        {
            "TrxId", "TrxDesc", "TrxDate", "TrxAmount", "RefTrnsId",
            "AccountId", "AccountName", "AccountType", "BankName", "Branch", "IFSCCode",
            "TrxType", "TrxMode", "TrxStatus", "AccHead",
            "CreatedBy", "CreatedOn", "TenantId"
        };

                var headers = headerLabels
                    .Select((label, index) => new TableHeader
                    {
                        Key = $"Col{index + 1}",
                        Label = label
                    }).ToList();

                var rows = new List<Dictionary<string, object>>();

                foreach (var row in transactions)
                {
                    var dict = new Dictionary<string, object>
                    {
                        ["Col1"] = row.TrxId,
                        ["Col2"] = row.TrxDesc,
                        ["Col3"] = row.TrxDate,
                        ["Col4"] = row.TrxAmount,
                        ["Col5"] = row.RefTrnsId,
                        ["Col6"] = row.AccountId,
                        ["Col7"] = row.AccountName,
                        ["Col8"] = row.AccountType,
                        ["Col9"] = row.BankName,
                        ["Col10"] = row.Branch,
                        ["Col11"] = row.IFSCCode,
                        ["Col12"] = row.TrxTypeName,
                        ["Col13"] = row.TrxModeName,
                        ["Col14"] = row.TrxStatusName,
                        ["Col15"] = row.AccHeadName,
                        ["Col16"] = row.CreatedBy,
                        ["Col17"] = row.CreatedOn,
                        ["Col18"] = row.TenantId
                    };

                    rows.Add(dict);
                }

                return new ResponseResult<VTransactionTableVM>(
                    HttpStatusCode.OK,
                    new VTransactionTableVM
                    {
                        Headers = headers,
                        TData = rows
                    },
                    "Transaction table fetched successfully."
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<VTransactionTableVM>(
                    HttpStatusCode.InternalServerError,
                    null,
                    $"Error: {ex.Message}"
                );
            }
        }



    }
}

