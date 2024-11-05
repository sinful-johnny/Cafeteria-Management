using api.Data;
using api.Interfaces;
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
        public async Task<IActionResult> GetAllShapeType() {
            var result = await _shapeTypeRepo.GetAllShapeType();   
            return Ok(result);

        }
    }
}
