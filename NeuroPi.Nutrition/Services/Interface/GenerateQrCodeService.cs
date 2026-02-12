using NeuroPi.Nutrition.Model;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.ViewModel.QrCode;
using System.Collections.Generic;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface GenerateQrCodeService
    {
        string GenerateQrCode(string gmail, string name, string studentName, string gender, string qrcode);
        QrCodeValidationResponseVM ValidateQrCode(Guid code);
        string AddCarpidiumDetails(QrCodeRequestVM qrCode);
        List<MCarpidum> GetGuestList();
    }
}
