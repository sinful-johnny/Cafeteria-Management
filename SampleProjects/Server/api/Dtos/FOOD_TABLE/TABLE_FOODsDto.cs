
using api.Dtos.FOOD;
using cafeteriaDBLocalHost;

namespace api.Dtos.FOOD_TABLE
{
    public class TABLE_FOODsDto
    {
        public double? X_COORDINATE { get; set; } = 0;
        public double? Y_COORDINATE { get; set; } = 0;
        public string SHAPE_TYPENAME { get; set; } = string.Empty;
        public double? WIDTH { get; set; } = 0;
        public double? HEIGHT { get; set; } = 0;
        public double? RADIUS { get; set; } = 0;
        public List<FOODsOnTABLE> FOODS { get; set; } = new List<FOODsOnTABLE>();
        public string TABLE_STATUS { get; set; } = string.Empty;
        public string ID_TABLE { get; set; } = string.Empty;
        public string ID_CANVA { get; set; } = string.Empty;
    }
}
