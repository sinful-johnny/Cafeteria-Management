namespace Class.Services
{
	public class FoodListItemService
	{
		public List<FoodListItem> Items { get; set; } = new List<FoodListItem>();

		public void setItems(List<FoodListItem> items)
		{
			Items = items;
		}
	}
}
