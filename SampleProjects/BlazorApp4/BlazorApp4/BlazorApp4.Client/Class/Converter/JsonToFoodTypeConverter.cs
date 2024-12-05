namespace Class.Converter
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

    public class FoodWithCategory
    {
        public Food Food { get; set; }
        public string Category { get; set; }

        public FoodWithCategory(Food food, string category)
        {
            Food = food;
            Category = category;
        }

        public FoodWithCategory()
        {
        }
    }

    public static class JsonToFoodTypeConverter
    {
        public static FoodWithCategory ToFoodListItem(this FOODsDto foodsDto) {
            return new FoodWithCategory()
            {
                Food = new Food()
                {
                    FoodId = foodsDto.ID_FOOD,
                    FoodName = foodsDto.FOOD_NAME,
                    AmountLeft = foodsDto.AMOUNT_LEFT != null ? (int)foodsDto.AMOUNT_LEFT : 0,
                    Price = foodsDto.PRICE != null ? (int)foodsDto.PRICE : 0,
                    FoodTypeStatus = foodsDto.FOOD_TYPE_STATUS,
                    ImageUrl = foodsDto.IMAGE_LINK
                },
                Category = foodsDto.FOOD_TYPENAME
            };
        }
    }
}
