using api.Dtos.FOOD;
using CafeteriaDB;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Mappers
{
    public static class FOODsMapper
    {
        public static FOODsDto ToFoodsDto(this FOOD_TYPE FoodModel)
        {
            return new FOODsDto
            {
                ID_FOOD = FoodModel.ID_FOOD,
                FOOD_NAME = FoodModel.FOOD_NAME,
                FOOD_TYPENAME = FoodModel.FOOD_TYPENAME,
                AMOUNT_LEFT = FoodModel.AMOUNT_LEFT,
                PRICE = FoodModel.PRICE,
                FOOD_TYPE_STATUS = FoodModel.FOOD_TYPE_STATUS,
                IMAGE_LINK = FoodModel.IMAGE_LINK,
            };
        }

        public static FOODwithAmount ToFoodsOnTableDto(this V_ADMIN_FOODsOnTABLE FoodsOnTableModel)
        {
            var Food = new FOODOnTABLE
            {
                foodId = FoodsOnTableModel.ID_FOOD,
                ID_TABLE = FoodsOnTableModel.ID_TABLE,
                foodName = FoodsOnTableModel.FOOD_NAME,
                amount_left = FoodsOnTableModel.AMOUNT_LEFT,
                price = FoodsOnTableModel.PRICE,
                foodTypeStatus = FoodsOnTableModel.FOOD_TYPE_STATUS,
                imageURL = FoodsOnTableModel.IMAGE_LINK
            };

            return new FOODwithAmount
            {
                food = Food,
                amount = FoodsOnTableModel.AMOUNT_IN_TABLE
            };
        }
    }
}
