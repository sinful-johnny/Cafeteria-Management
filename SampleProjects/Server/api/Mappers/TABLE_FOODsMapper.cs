using api.Dtos.FOOD;
using api.Dtos.FOOD_TABLE;
using cafeteriaDBLocalHost;
using System.Runtime.CompilerServices;

namespace api.Mappers
{
    public static class TABLE_FOODsMapper
    {
        public static TABLE_FOODsDto ToTablesInCanvaDto(this V_ADMIN_TABLEInCANVA TablesInCanvaModel, List<FOODwithAmount> FoodsOnTable)
        {
            return new TABLE_FOODsDto
            {
                x = TablesInCanvaModel.X_COORDINATE,
                y = TablesInCanvaModel.Y_COORDINATE,
                width = TablesInCanvaModel.WIDTH,
                height = TablesInCanvaModel.HEIGHT,
                radius = TablesInCanvaModel.RADIUS,
                foods = FoodsOnTable.ToList(),
                tableStatus = TablesInCanvaModel.TABLE_STATUS,
                tableId = TablesInCanvaModel.ID_TABLE,
                shapeId = TablesInCanvaModel.ID_SHAPE,
                ID_CANVA = TablesInCanvaModel.ID_CANVA,
            };
        }

        public static CAFETERIA_TABLE FromCreatedTableToTable(this CreateTABLEDto createTableDto)
        {
            return new CAFETERIA_TABLE
            {
                ID_CANVA = createTableDto.ID_CANVA,
                ID_SHAPE = createTableDto.ID_SHAPE
            };
        }
    }
}
