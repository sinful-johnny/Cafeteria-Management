﻿using api.Data;
using api.Interfaces;

using CafeteriaDB;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FOOD_TYPE_Repository : IFOOD_TYPE_Repository
    {
        private readonly ApplicationDBContext _context;
        public FOOD_TYPE_Repository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<FOOD_TYPE>> getAllFoodType()
        {
            var result = await _context.FoodType.FromSqlRaw("EXEC DBO.GetAllFoodType").ToListAsync();
            return result;
        }
    }
}