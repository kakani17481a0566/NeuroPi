using SchoolManagement.ViewModel.TableFile;
using SchoolManagement.ViewModel.TableFiles;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ITableFilesService
    {
        TimetableAttachmentVM Create(TimetableAttachmentCreateVM vm);
        TimetableAttachmentVM Update(int id, TimetableAttachmentUpdateVM vm, int tenantId);
        bool Delete(int id, int tenantId);

        TimetableAttachmentVM GetById(int id, int tenantId);
        List<TimetableAttachmentVM> GetAll(int tenantId);
        List<TimetableAttachmentVM> GetByCourseAndTimeTable(int courseId, int? timeTableId, int tenantId);
    }
}
