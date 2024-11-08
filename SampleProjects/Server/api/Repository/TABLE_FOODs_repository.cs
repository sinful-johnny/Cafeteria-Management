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
        //public async Task<string> SaveCreatedTableAsync(string idCanvas, string idShape)
        //{
        //    var resultTables = await _context.V_TableInCanva
        //        .FromSqlRaw("EXEC DBO.sp_ADMIN_TABLEInCANVA @ID_CANVA={0}", idCanvas)
        //        .ToListAsync();
        //    var resultFoods = await _context.V_FoodsOnTable
        //        .FromSqlRaw($"EXEC DBO.sp_ADMIN_FOODsOnTABLE")
        //        .ToListAsync();
        //    return (resultTables, resultFoods);
        //}
    }
}
