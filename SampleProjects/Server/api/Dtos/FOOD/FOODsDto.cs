using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace api.Dtos.FOOD
{
    public class FOODsDto
    {
        public string ID_FOOD { get; set; } = string.Empty;
        public string FOOD_NAME { get; set; } = string.Empty;
        public string FOOD_TYPENAME { get; set; } = string.Empty;
        public int? AMOUNT_LEFT { get; set; } = 0;
        public decimal? PRICE { get; set; } = 0;
        public string FOOD_TYPE_STATUS { get; set; } = string.Empty;
        public string IMAGE_LINK { get; set; } = string.Empty;
    }
}
