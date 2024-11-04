using CafeteriaDB;
using System.Data;

namespace api.Interfaces
{
    public interface ICanvaRepository
    {
        public Task<List<CANVA>> GetAllCanvaAsync();
        public Task<CANVA?> GetIdCanvaAsync(int id);
        public Task<CANVA> CreateCanvaAsync(CANVA CanvaModel);
        public Task<bool> DeleteCanvaAsync(int id);
    }
}
