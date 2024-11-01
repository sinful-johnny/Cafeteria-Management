
using EFTut.Dtos.CANVA;
using Cafeteria_DB;

namespace EFTut.Mappers
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
