namespace Class
{
    public class FoodOnTable
    {
        public Food Food { get; set; }
        public int Amount { get; set; }

        // Constructor
        public FoodOnTable(Food food, int amount)
        {
            Food = food;
            Amount = amount;
        }
        public override string ToString()
        {
            return $"{{\n" + $" \"Food\": {{\n" + $" \"FoodId\": {Food.FoodId},\n" + $" \"FoodName\": \"{Food.FoodName}\",\n" + $" \"AmountLeft\": {Food.AmountLeft},\n" + $" \"Price\": {Food.Price},\n" + $" \"FoodTypeStatus\": \"{Food.FoodTypeStatus}\",\n" + $" \"ImageUrl\": \"{Food.ImageUrl}\"\n" + $" }},\n" + $" \"Amount\": {Amount}\n" + $"}}";
        }
    }
}
