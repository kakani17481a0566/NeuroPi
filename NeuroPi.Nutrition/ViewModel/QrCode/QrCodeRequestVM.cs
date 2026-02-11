using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.QrCode
{
    public class QrCodeRequestVM
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Gmail { get; set; }

        public string StudentName { get; set; }

        public static MCarpedium ToModel(QrCodeRequestVM qrCodeRequest)
        {
            return new MCarpedium
            {
                Name = qrCodeRequest.Name,
                Gender = qrCodeRequest.Gender,
                StudentName = qrCodeRequest.StudentName,
                gmail = qrCodeRequest.Gmail,
                QrCode = Guid.NewGuid()

            };
        }

    }
}
