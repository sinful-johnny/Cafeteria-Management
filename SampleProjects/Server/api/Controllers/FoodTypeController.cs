using api.Data;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/FoodType")]
    [ApiController]
    public class FoodTypeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IFOOD_TYPE_Repository _foodTypeRepo;
        public FoodTypeController(ApplicationDBContext context, IFOOD_TYPE_Repository foodTypeRepo)
        {
            _foodTypeRepo = foodTypeRepo;
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllFoodType()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var FoodTypes = await _foodTypeRepo.getAllFoodType();

            return Ok(FoodTypes);
        }
    }
}
