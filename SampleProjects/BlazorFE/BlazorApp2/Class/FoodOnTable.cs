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
    }

}
