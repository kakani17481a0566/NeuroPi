using SchoolManagement.ViewModel.visitors;

namespace SchoolManagement.Services.Interface
{
    public interface VisitorInterface
    {
        VisitorResponseVM AddVisitor(VisitorRequestVM visitor);
        VisitorResponseVM UpdateById(int id, VisitorRequestVM requestVM);
    }
    public interface visitorInterface
    {
        VisitorResponseVM UpdateById(int id, VisitorRequestVM visitor);
    }
}

