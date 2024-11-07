using cafeteriaDBLocalHost;

namespace api.Interfaces
{
    public interface IFOOD_TYPE_Repository
    {
        public Task<List<FOOD_TYPE>> getAllFoodTypeAsync();
        //public Task<FOOD_TYPE> CreateFoodTypeAsync(FOOD_TYPE FoodModel);
    }
}
