using api.Dtos.FOOD_TABLE;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllShapeType()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string idCanva = "Easter Egg";

            var ShapeTypeModels = await _tablefoodRepo.getAllTableFoodAsync(idCanva);

            //var TablesInCanvaDto = ShapeTypeModels.Item1.Select(stm => stm.ToTablesInCanvaDto()).ToList();
            var FoodsOnTableDto = ShapeTypeModels.Item2.Select(fot => fot.ToFoodsOnTableDto()).ToList();
            var TablesInCanvaDto = ShapeTypeModels.Item1.Select(stm => stm.ToTablesInCanvaDto(FoodsOnTableDto)).ToList();

            foreach (var table in TablesInCanvaDto)
            {
                var matchingFoods = FoodsOnTableDto
                    .Where(fot => fot.food.ID_TABLE == table.tableId)
                    .ToList();

                table.foods = matchingFoods; // Assign the matching foods to the current table
            }

            return Ok(TablesInCanvaDto);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SaveCreatedTable([FromBody] List<TABLE_FOODsDto> TablesInCanvaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tablefoodRepo.SaveCreatedTableAsync(TablesInCanvaDto);

            return Created(string.Empty, new { message = "Tables successfully created.", count = TablesInCanvaDto.Count });
        }
    }
}
