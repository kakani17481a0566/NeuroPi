using NeuroPi.Nutrition.ViewModel.QrCode;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface GenerateQrCodeService
    {
        string GenerateQrCode();
        string ValidateQrCode(Guid code);

        string AddCarpidiumDetails(QrCodeRequestVM qrCode);
    }
}
