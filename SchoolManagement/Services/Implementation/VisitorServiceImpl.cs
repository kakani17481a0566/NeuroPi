using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Model;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.visitors;
using SchoolManagement.Model;


namespace SchoolManagement.Services.Implementation
{
    public class VisitorServiceImpl:VisitorInterface
    {
        private readonly SchoolManagementDb context;
        public VisitorServiceImpl(SchoolManagementDb context)
        {
            this.context = context;

        }

        public VisitorResponseVM AddVisitor(VisitorRequestVM visitor)
        {
            MVisitor mvisitor = VisitorRequestVM.ToModel(visitor);
            context.Visitors.Add(mvisitor);
            context.SaveChanges();
            return VisitorResponseVM.ToViewModel(mvisitor);
        }
        public VisitorResponseVM UpdateById(int id, VisitorRequestVM visitor)
        {
            MVisitor mvisitor = context.Visitors.FirstOrDefault(v => v.id == id);
            if (mvisitor != null)
            {
                mvisitor.name = visitor.Name;
                mvisitor.address = visitor.Address;
                mvisitor.mobilenumber = visitor.mobileNumber;
                mvisitor.in_time = visitor.In_time;
                mvisitor.out_time = visitor.Out_time;
                mvisitor.purpose = visitor.Purpose;
                context.SaveChanges();
                return VisitorResponseVM.ToViewModel(mvisitor);
            }
            return null;
        }
    }
}
