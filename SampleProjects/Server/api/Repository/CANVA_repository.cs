using api.Data;
using api.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CafeteriaDB;

namespace EFTut.Repository
{
    public class CANVA_repository : ICanvaRepository
    {
        private readonly ApplicationDBContext _context;
        public CANVA_repository (ApplicationDBContext context)
        {
            _context = context;
        }

        public  async Task<List<CANVA>> GetAllCanvaAsync()
        {
            //var connection = _context.Database.GetDbConnection();
            var result = await _context.Canva.FromSqlRaw("EXEC DBO.SelectAllCanva").ToListAsync();
            return result;
        }

        public async Task<CANVA> CreateCanvaAsync(CANVA CanvaModel)
        {
            var widthCanva = new SqlParameter("@width", CanvaModel.WIDTH);
            var heightCanva = new SqlParameter("@height", CanvaModel.HEIGHT);

            //_context.Canva.FromSqlRaw("EXEC DBO.InsertCanva @WIDTH, @HEIGHT", widthCanva, heightCanva);
            await _context.Database.ExecuteSqlRawAsync("EXEC DBO.InsertCanva @WIDTH, @HEIGHT", widthCanva, heightCanva);
            return CanvaModel;
        }

        public async Task<bool> DeleteCanvaAsync(int id)
        {
            var idCanvaParam = new SqlParameter("@ID_CANVA", id);
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC DBO.DeleteCanva @ID_CANVA", idCanvaParam);
            return rowsAffected > 0;
        }

        public async Task<CANVA?> GetIdCanvaAsync(int id)
        {
            var idCanvaParam = new SqlParameter("@ID_CANVA", id);
            var result = await _context.Canva.FromSqlRaw("EXEC DBO.SelectIdCanva @ID_CANVA", idCanvaParam).ToListAsync();
            return result.FirstOrDefault();
        }
    }
}
