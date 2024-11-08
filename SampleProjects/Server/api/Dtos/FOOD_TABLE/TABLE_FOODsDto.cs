
using api.Dtos.FOOD;
using cafeteriaDBLocalHost;

namespace api.Dtos.FOOD_TABLE
{
    public class TABLE_FOODsDto
    {
        public double? x { get; set; } = 0;
        public double? y { get; set; } = 0;
        public double? width { get; set; } = 0;
        public double? height { get; set; } = 0;
        public double? radius { get; set; } = 0;
        public List<FOODwithAmount> foods { get; set; } = new List<FOODwithAmount>();
        public string tableStatus { get; set; } = string.Empty;
        public string tableId { get; set; } = string.Empty;
        public string shapeId { get; set; } = string.Empty;
        public string ID_CANVA { get; set; } = string.Empty;
    }
}
