using api.Dtos.SHAPE;
using CafeteriaDB;

namespace api.Mappers
{
    public static class SHAPEMapper
    {
        public static SHAPEDto ToShapeDto(this SHAPE_TYPE ShapeModel)
        {
            return new SHAPEDto
            {
                ID_SHAPE = ShapeModel.ID_SHAPE,
                SHAPE_TYPENAME = ShapeModel.SHAPE_TYPENAME,
                WIDTH = ShapeModel.WIDTH,
                HEIGHT = ShapeModel.HEIGHT,
                RADIUS = ShapeModel.RADIUS,
            };
        }
    }
}
