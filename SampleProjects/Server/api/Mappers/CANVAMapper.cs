
using api.Dtos.CANVA;
using Cafeteria_DB;

namespace api.Mappers
{
    public static class CANVAMapper
    {
        public static CANVADto ToCanvaDto(this CANVA CanvaModel)
        {
            return new CANVADto
            {
                ID_CANVA = CanvaModel.ID_CANVA,
                WIDTH = CanvaModel.WIDTH,
                HEIGHT = CanvaModel.HEIGHT
            };
        }
    }
}
