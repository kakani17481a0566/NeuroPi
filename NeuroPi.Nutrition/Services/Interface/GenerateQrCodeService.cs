using NeuroPi.Nutrition.ViewModel.QrCode;
using NeuroPi.Nutrition.Model;
using System.Collections.Generic;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface GenerateQrCodeService
    {
        //string GenerateQrCode();
        QrCodeValidationResponseVM ValidateQrCode(Guid code);

        string AddCarpidiumDetails(QrCodeRequestVM qrCode);

        List<MCarpedium> GetGuestList();
    }
}
