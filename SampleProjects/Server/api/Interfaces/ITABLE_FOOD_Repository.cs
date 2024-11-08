using cafeteriaDBLocalHost;

namespace api.Interfaces
{
    public interface ITABLE_FOOD_Repository
    {
        public Task<(List<V_ADMIN_TABLEInCANVA>, List<V_ADMIN_FOODsOnTABLE>)> getAllTableFoodAsync(string idCanvas);
        //public Task<string> SaveCreatedTableAsync(string idCanvas, string idShape);
    }
}
