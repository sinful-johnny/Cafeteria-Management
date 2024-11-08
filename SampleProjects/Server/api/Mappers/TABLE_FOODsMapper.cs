using api.Dtos.FOOD;
using api.Dtos.FOOD_TABLE;
using cafeteriaDBLocalHost;
using System.Runtime.CompilerServices;

namespace api.Mappers
{
    public static class TABLE_FOODsMapper
    {
        public static TABLE_FOODsDto ToTablesInCanvaDto(this V_ADMIN_TABLEInCANVA TablesInCanvaModel, List<FOODsOnTABLE> FoodsOnTable)
        {
            return new TABLE_FOODsDto
            {
                X_COORDINATE = TablesInCanvaModel.X_COORDINATE,
                Y_COORDINATE = TablesInCanvaModel.Y_COORDINATE,
                SHAPE_TYPENAME = TablesInCanvaModel.SHAPE_TYPENAME,
                WIDTH = TablesInCanvaModel.WIDTH,
                HEIGHT = TablesInCanvaModel.HEIGHT,
                RADIUS = TablesInCanvaModel.RADIUS,
                FOODS = FoodsOnTable.ToList(),
                TABLE_STATUS = TablesInCanvaModel.TABLE_STATUS,
                ID_TABLE = TablesInCanvaModel.ID_TABLE,
                ID_CANVA = TablesInCanvaModel.ID_CANVA,
            };
        }

        //public static CAFETERTIA_TABLE FromCreatedTableToTable(this CreateTABLEDto )
    }
}
