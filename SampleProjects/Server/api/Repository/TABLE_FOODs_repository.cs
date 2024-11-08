using api.Data;
using api.Dtos.FOOD_TABLE;
using api.Interfaces;
using cafeteriaDBLocalHost;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TABLE_FOODs_repository : ITABLE_FOOD_Repository
    {
        private readonly ApplicationDBContext _context;
        public TABLE_FOODs_repository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<(List<V_ADMIN_TABLEInCANVA>, List<V_ADMIN_FOODsOnTABLE>)> getAllTableFoodAsync(string idCanvas)
        {
            var resultTables = await _context.V_TableInCanva
                .FromSqlRaw("EXEC DBO.sp_ADMIN_TABLEInCANVA @ID_CANVA={0}", idCanvas)
                .ToListAsync();
            var resultFoods = await _context.V_FoodsOnTable
                .FromSqlRaw($"EXEC DBO.sp_ADMIN_FOODsOnTABLE")
                .ToListAsync();

            return (resultTables, resultFoods);
        }
        public async Task<List<TABLE_FOODsDto>> SaveCreatedTableAsync(List<TABLE_FOODsDto> TablesInCanvaDto)
        {
            _context.Database.ExecuteSqlRaw(
                    """
                    TRUNCATE TABLE FOOD_TABLE;
                    DELETE FROM CAFETERIA_TABLE;
                    """);
            foreach (var TableInCanvaDto in TablesInCanvaDto)
            {
                await _context.Database.ExecuteSqlRawAsync(
                """
                EXEC DBO.sp_ADMIN_SAVE_TABLEinCANVA
                        @X_COORDINATE = {0}, @Y_COORDINATE = {1}, @TABLE_STATUS = {2}, 
                        @ID_TABLE = {3}, @ID_SHAPE = {4}, @ID_CANVA = {5}
                """,
                TableInCanvaDto.x ?? 0,
                TableInCanvaDto.y ?? 0,
                TableInCanvaDto.tableStatus,
                TableInCanvaDto.tableId,
                TableInCanvaDto.shapeId,
                TableInCanvaDto.ID_CANVA);

                foreach (var food in TableInCanvaDto.foods)
                {
                    _ = await _context.Database.ExecuteSqlRawAsync(
                    """
                    EXEC DBO.sp_ADMIN_SAVE_FOODonTABLE
                            @ID_TABLE = {0}, @ID_FOOD = {1}, 
                            @AMOUNT_IN_TABLE = {2}
                    """,
                    TableInCanvaDto.tableId,
                    food.food.foodId,
                    food.amount);
                }
            }

            return TablesInCanvaDto;
        }
    }
}
