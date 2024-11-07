using cafeteriaDBLocalHost;

namespace api.Interfaces
{
    public interface ISHAPE_TYPE_Repository
    {
        public Task<List<SHAPE_TYPE>> GetAllShapeTypeAsync();
    }
}
