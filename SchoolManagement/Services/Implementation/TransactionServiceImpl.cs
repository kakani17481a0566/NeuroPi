using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Transaction;
using SchoolManagement.ViewModel.Transcation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

            // Validate account IDs
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
                parameters.Add("p_trx_desc", req.TrxDesc, DbType.String);
                parameters.Add("p_debit_acc_head_id", req.DebitAccHeadId, DbType.Int32);
                parameters.Add("p_credit_acc_head_id", req.CreditAccHeadId, DbType.Int32);

                string sql = @"
                    SELECT
                        debit_trx_id AS DebitTrxId,
                        credit_trx_id AS CreditTrxId,
                        ref_trns_id AS RefTrnsId
                    FROM sp_full_transaction_transfer(
                        @p_debit_acc_id,
                        @p_credit_acc_id,
                        @p_amount,
                        @p_trx_type_id,
                        @p_trx_mode_id,
                        @p_trx_status,
                        @p_tenant_id,
                        @p_created_by,
                        @p_trx_desc,
                        @p_debit_acc_head_id,
                        @p_credit_acc_head_id
                    );
                ";

                // Open the connection and execute the stored procedure
                
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
                    ErrorMessage = "An error occurred: " + ex.Message
                };
            }
        }

        // Get transaction by transaction ID
        // Developed by: Mohith
        public TransactionResponseVM GetByTrxId(int trxId)
        {
            var trx = _context.Transactions
                .AsNoTracking()
                .FirstOrDefault(t => t.TrxId == trxId && !t.IsDeleted);

            if (trx == null)
                return null;

            return MapToVM(trx);
        }

        // Get transaction by transaction ID and tenant ID
        // Developed by: Mohith
        public TransactionResponseVM GetByTrxIdAndTenantId(int trxId, int tenantId)
        {
            var trx = _context.Transactions
                .AsNoTracking()
                .FirstOrDefault(t => t.TrxId == trxId && t.TenantId == tenantId && !t.IsDeleted);

            if (trx == null)
                return null;

            return MapToVM(trx);
        }

        // Get transactions by reference transaction ID
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

        // Update transaction amount by reference transaction ID and tenant ID
        // Developed by: Mohith
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
                trx.ModifiedBy = modifiedBy;
            }

            _context.SaveChanges();
            return true;
        }


        // Helper method to map dynamic transaction object to TransactionResponseVM
        // Developed by: Mohith
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

        // Private class to hold the result of the stored procedure call
        private class SpTransferResult
        {
            public int DebitTrxId { get; set; }
            public int CreditTrxId { get; set; }
            public string RefTrnsId { get; set; }
        }
    }
}
