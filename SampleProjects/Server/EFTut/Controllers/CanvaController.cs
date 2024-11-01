using EFTut.Data;
using EFTut.Interfaces;
using EFTut.Mappers;
using EFTut.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFTut.Controllers
{
    [Route("api/canva")]
    [ApiController]
    public class CanvaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICanvaRepository _canvaRepo;
        public CanvaController(ApplicationDBContext context, ICanvaRepository canvaRepo)
        {
            _canvaRepo = canvaRepo;
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCanva()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var canvaModels = await this._canvaRepo.GetAllCanvaAsync();
            var canvaDtos = canvaModels.Select(c => c.ToCanvaDto());
            return Ok(canvaDtos);
        }
    }
}
