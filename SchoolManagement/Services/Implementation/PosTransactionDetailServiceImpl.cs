using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PosTransactionDetail;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class PosTransactionDetailServiceImpl : IPosTransactionDetailService
    {
        private readonly SchoolManagementDb _context;

        public PosTransactionDetailServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

       

       

        public PosTransactionDetailResponseVM GetAllDetailsByMasterTransactionId(int masterTransactionId)
        {
            var master = _context.PosTransactionMaster
                .Include(m => m.student)
                    .ThenInclude(s => s.Branch)
                .Include(m => m.student)
                    .ThenInclude(s => s.Course)
                .FirstOrDefault(m => m.Id == masterTransactionId);

            if (master == null)
                return null;

            var items = _context.PosTransactionDetails
                .Include(d => d.Item)
                .Where(d => d.MasterTransactionId == masterTransactionId)
                .Select(d => new Item
                {
                    ItemId = d.ItemId,
                    ItemName = d.Item.Name,
                    UnitPrice = d.UnitPrice,
                    Quantity = d.Quantity,
                    GstPercentage = d.GstPercentage,
                    GstValue = d.GstValue
                })
                .ToList();


            var response = new PosTransactionDetailResponseVM
            {
                MasterTransactionId = master.Id,
                StudentId = master.student.Id,
                StudentName = master.student.Name,
                BranchName = master.student.Branch.Name,
                CourseName = master.student.Course.Name,
                items = items
            };

            return response;
        }

       
    }
}
