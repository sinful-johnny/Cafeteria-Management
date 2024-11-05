using api.Data;
using api.Interfaces;
using CafeteriaDB;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SHAPE_TYPE_Repository : ISHAPE_TYPE_Repository
    {
        private readonly ApplicationDBContext _context;
        public SHAPE_TYPE_Repository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<SHAPE_TYPE>> GetAllShapeType()
        {
            var result = await _context.ShapeType.FromSqlRaw("EXEC DBO.GetAllShapeTypes").ToListAsync();
            return result;
        }
    }
}
