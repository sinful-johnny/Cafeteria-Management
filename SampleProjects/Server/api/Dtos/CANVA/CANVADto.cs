using Cafeteria_DB;

namespace api.Dtos.CANVA
{
    public class CANVADto
    {
        public string ID_CANVA { get; set; } = string.Empty;
        public Nullable<double> WIDTH { get; set; }
        public Nullable<double> HEIGHT { get; set; }
    }
}
