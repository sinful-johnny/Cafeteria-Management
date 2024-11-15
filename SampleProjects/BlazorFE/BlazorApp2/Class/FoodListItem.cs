namespace Class
{
	public class FoodListItem
	{
		public int Id { get; set; }
		public List<Food>? Content { get; set; }
		public string Title { get; set; } = string.Empty;
	}
}
