using api.Data;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/ShapeType")]
    [ApiController]
    public class ShapeTypeController : ControllerBase
    {
        private readonly ISHAPE_TYPE_Repository _shapeTypeRepo;
        public ShapeTypeController(ISHAPE_TYPE_Repository shapeTypeRepo)
        {
            this._shapeTypeRepo = shapeTypeRepo;
        }

        [HttpGet]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllShapeType() {
            var ShapeTypeModels = await _shapeTypeRepo.GetAllShapeTypeAsync();
            var ShapeTypeDtos = ShapeTypeModels.Select(f => f.ToShapeDto());

            return Ok(ShapeTypeDtos);
        }
    }
}
