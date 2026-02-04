using Microsoft.CognitiveServices.Speech.Transcription;
using SchoolManagement.ViewModel.AcademicYear;
using SchoolManagement.ViewModel.Branch;
using SchoolManagement.ViewModel.CourseTeacher;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IAcademicYear
    {
        //List<BranchResponseVM> GetAllBranches();

        List<AcademicYearResponseVM> GetAllAcadamicYears();
        AcademicYearResponseVM GetAcademicYearById(int id);
        AcademicYearResponseVM CreateAcademicYear(AcademicYearCreateVm model);
        AcademicYearResponseVM UpdateAcademicYear(int id, AcademicYearUpdateVm model);
        bool DeleteAcademicYear(int id);


       
    }
}
