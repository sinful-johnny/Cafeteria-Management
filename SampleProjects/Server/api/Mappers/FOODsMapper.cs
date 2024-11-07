using api.Dtos.FOOD;
using cafeteriaDBLocalHost;
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

        public static FOODsOnTABLE ToFoodsOnTableDto(this V_ADMIN_FOODsOnTABLE FoodsOnTableModel)
        {
            return new FOODsOnTABLE
            {
                ID_FOOD = FoodsOnTableModel.ID_FOOD,
                ID_TABLE = FoodsOnTableModel.ID_TABLE,
                FOOD_NAME = FoodsOnTableModel.FOOD_NAME,
                FOOD_TYPENAME = FoodsOnTableModel.FOOD_TYPENAME,
                AMOUNT_LEFT = FoodsOnTableModel.AMOUNT_LEFT,
                PRICE = FoodsOnTableModel.PRICE,
                FOOD_TYPE_STATUS = FoodsOnTableModel.FOOD_TYPE_STATUS,
                IMAGE_LINK = FoodsOnTableModel.IMAGE_LINK,
                AMOUNT_IN_TABLE = FoodsOnTableModel.AMOUNT_IN_TABLE
            };
        }
    }
}
