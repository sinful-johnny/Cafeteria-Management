using api.Data;
using api.Interfaces;
using CafeteriaDB;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FOOD_repository : IFOOD_TYPE_Repository
    {
        private readonly ApplicationDBContext _context;
        public FOOD_repository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<FOOD_TYPE>> getAllFoodTypeAsync()
        {
            var result = await _context.FoodType.FromSqlRaw("EXEC DBO.GetAllFoodType").ToListAsync();
            return result;
        }

        //public async Task<FOOD_TYPE> CreateFoodTypeAsync(FOOD_TYPE FoodModel)
        //{
        //    await _context.FoodType.AddAsync(FoodModel);
        //    await _context.SaveChangesAsync();
        //    return FoodModel;
        //}
    }
}
