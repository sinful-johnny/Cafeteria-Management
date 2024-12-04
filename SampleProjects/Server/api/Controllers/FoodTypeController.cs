using api.Data;
using api.Interfaces;
using api.Mappers;
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
        //[Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetAllFoodType()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var FoodTypeModels = await _foodTypeRepo.getAllFoodTypeAsync();
            var FoodTypeDtos = FoodTypeModels.Select(f => f.ToFoodsDto());

            return Ok(FoodTypeDtos);
        }
    }
}
