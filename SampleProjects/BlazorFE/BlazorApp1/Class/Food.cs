﻿namespace BlazorApp1.Class
{
    public class Food
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int AmountLeft { get; set; }
        public decimal Price { get; set; }
        public string FoodTypeStatus { get; set; }
        public string ImageUrl { get; set; }

        // Constructor
        public Food(int foodId, string foodName, int amountLeft, decimal price, string foodTypeStatus, string imageUrl)
        {
            FoodId = foodId;
            FoodName = foodName;
            AmountLeft = amountLeft;
            Price = price;
            FoodTypeStatus = foodTypeStatus;
            ImageUrl = imageUrl;
        }
    }

}