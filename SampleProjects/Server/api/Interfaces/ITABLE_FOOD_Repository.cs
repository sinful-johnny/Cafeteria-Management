using api.Dtos.FOOD_TABLE;
using CafeteriaDB;

namespace api.Interfaces
{
    public interface ITABLE_FOOD_Repository
    {
        public Task<(List<V_ADMIN_TABLEInCANVA>, List<V_ADMIN_FOODsOnTABLE>)> getAllTableFoodAsync(string idCanvas);
        public Task<List<TABLE_FOODsDto>> SaveCreatedTableAsync(List<TABLE_FOODsDto> TablesInCanvaDto);
    }
}
