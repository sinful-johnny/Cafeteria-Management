﻿using CafeteriaDB;

namespace api.Interfaces
{
    public interface IFOOD_TYPE_Repository
    {
        public Task<List<FOOD_TYPE>> getAllFoodType();
    }
}