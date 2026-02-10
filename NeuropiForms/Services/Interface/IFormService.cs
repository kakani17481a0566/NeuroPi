using NeuroPi.CommonLib.Model;
using NeuropiForms.Models;
using System.Collections.Generic;

namespace NeuropiForms.Services.Interface
{
    public interface IFormService
    {
        ResponseResult<IEnumerable<MForm>> GetAllForms();
        ResponseResult<MForm?> GetFormById(int id);
        ResponseResult<MForm> CreateForm(MForm form);
        ResponseResult<MForm?> UpdateForm(int id, MForm form);
        ResponseResult<bool> DeleteForm(int id);
    }
}
