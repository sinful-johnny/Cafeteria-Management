using api.Dtos.FOOD_TABLE;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/TableFoods")]
    [ApiController]
    public class TableFoodsController : Controller
    {
        private readonly ITABLE_FOOD_Repository _tablefoodRepo;
        public TableFoodsController(ITABLE_FOOD_Repository tablefoodRepo)
        {
            this._tablefoodRepo = tablefoodRepo;
        }

        [HttpGet("{idCanva}")]
        public async Task<IActionResult> GetAllShapeType([FromRoute]string idCanva)
        {
            var ShapeTypeModels = await _tablefoodRepo.getAllTableFoodAsync(idCanva);

            //var TablesInCanvaDto = ShapeTypeModels.Item1.Select(stm => stm.ToTablesInCanvaDto()).ToList();
            var FoodsOnTableDto = ShapeTypeModels.Item2.Select(fot => fot.ToFoodsOnTableDto()).ToList();
            var TablesInCanvaDto = ShapeTypeModels.Item1.Select(stm => stm.ToTablesInCanvaDto(FoodsOnTableDto)).ToList();

            foreach (var table in TablesInCanvaDto)
            {
                // Find all FoodsOnTableDto items that match the current table's ID_TABLE
                var matchingFoods = FoodsOnTableDto
                    .Where(food => food.ID_TABLE == table.ID_TABLE)
                    .ToList();

                // Assuming each TablesInCanvaDto has a List<FoodsOnTableDto> to store related foods
                table.FOODS = matchingFoods; // Assign the matching foods to the current table
            }

            return Ok(TablesInCanvaDto);
        }

        //[HttpPost("{idCanva}, {idShape}")]
        //public async Task<IActionResult> SaveCreatedTable([FromRoute] string idCanva, [FromRoute] string idShape)
        //{

        //}
    }
}
