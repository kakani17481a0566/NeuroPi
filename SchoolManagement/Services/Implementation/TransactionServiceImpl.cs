using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Transaction;
using SchoolManagement.ViewModel.Transcation;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

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

                var parameters = new DynamicParameters();
                parameters.Add("p_debit_acc_id", req.DebitAccId, DbType.Int32);
                parameters.Add("p_credit_acc_id", req.CreditAccId, DbType.Int32);
                parameters.Add("p_amount", req.Amount, DbType.Double);
                parameters.Add("p_trx_type_id", req.TrxTypeId, DbType.Int32);
                parameters.Add("p_trx_mode_id", req.TrxModeId, DbType.Int32);
                parameters.Add("p_trx_status", req.TrxStatus, DbType.Int32);
                parameters.Add("p_tenant_id", req.TenantId, DbType.Int32);
                parameters.Add("p_created_by", req.CreatedBy, DbType.Int32);

                string sql = @"
                    SELECT * FROM sp_full_transaction_transfer(
                        @p_debit_acc_id,
                        @p_credit_acc_id,
                        @p_amount,
                        @p_trx_type_id,
                        @p_trx_mode_id,
                        @p_trx_status,
                        @p_tenant_id,
                        @p_created_by
                    );
                ";

                var result = connection.QueryFirstOrDefault<(int debitTrxId, int creditTrxId, string refTrnsId)>(
                    sql,
                    parameters);

                if (result == default)
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
                    DebitTrxId = result.debitTrxId,
                    CreditTrxId = result.creditTrxId,
                    RefTrnsId = result.refTrnsId
                };
            }
            catch (Exception ex)
            {
                return new TransactionResultVM
                {
                    IsSuccess = false,
                    ErrorMessage = "An error occurred: " + ex.Message
                };
            }
        }

        public TransactionResponseVM GetByTrxId(int trxId)
        {
            var trx = _context.Transactions
                .AsNoTracking()
                .FirstOrDefault(t => t.TrxId == trxId && !t.IsDeleted);

            if (trx == null)
                return null;

            return MapToVM(trx);
        }

        public TransactionResponseVM GetByTrxIdAndTenantId(int trxId, int tenantId)
        {
            var trx = _context.Transactions
                .AsNoTracking()
                .FirstOrDefault(t => t.TrxId == trxId && t.TenantId == tenantId && !t.IsDeleted);

            if (trx == null)
                return null;

            return MapToVM(trx);
        }

        public List<TransactionResponseVM> GetByRefTrnsId(string refTrnsId)
        {
            var trxs = _context.Transactions
                .AsNoTracking()
                .Where(t => t.RefTrnsId == refTrnsId && !t.IsDeleted)
                .ToList();

            if (trxs == null || trxs.Count == 0)
                return null;

            return trxs.Select(MapToVM).ToList();
        }

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

        public bool UpdateAmountByRefTrnsIdAndTenant(string refTrnsId, int tenantId, double newAmount, int modifiedBy)
        {
            var transactions = _context.Transactions
                .Where(t => t.RefTrnsId == refTrnsId && t.TenantId == tenantId && !t.IsDeleted)
                .ToList();

            if (transactions == null || transactions.Count == 0)
                return false;

            foreach (var trx in transactions)
            {
                trx.TrxAmount = (float)newAmount;
                trx.ModifiedOn = DateTime.UtcNow;
                trx.ModifiedBy = modifiedBy;  // Set who modified
            }

            _context.SaveChanges();
            return true;
        }
    }
}
