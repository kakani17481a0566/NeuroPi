using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.ViewModel.VipPass;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IVipPassService
    {
        List<MVipCarpidum> GenerateBulkPasses(VipBulkPassRequestVM request);
        Task<bool> SendPassesViaEmail(string vipEmail);
        List<MVipCarpidum> GetVipPasses();
        List<MVipCarpidum> GetPassesByEmail(string email);
        VipValidationResponseVM ValidateVipPass(Guid code);
        bool DeleteVipPass(int id);
    }
}
