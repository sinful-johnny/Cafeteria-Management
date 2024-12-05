using Class.Converter;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Class.Services
{
	public class FoodListItemService : INotifyPropertyChanged
    {
		private List<FoodListItem> _items = new List<FoodListItem>();

        public List<FoodListItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void SetItemsFromJson(string json)
        {
            var foodDtos = JsonSerializer.Deserialize<List<FOODsDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (foodDtos != null)
            {
                var foodsWithCategory = foodDtos.Select(f => f.ToFoodWithCategory()).ToList();

                var categories = foodsWithCategory.Select(f => f.Category)
                                        .Distinct()
                                        .ToList();
                int categoryIndex = 0;
                List<FoodListItem> foodListItems = new List<FoodListItem>();
                foreach(var category in categories)
                {
                    List<FoodWithCategory> foodInCategory = [.. foodsWithCategory.FindAll(f => f.Category == category)];
                    foodListItems.Add(
                        new FoodListItem()
                        {
                            Id = categoryIndex++,
                            Title = category,
                            Content = foodInCategory.Select(f => f.Food).ToList()
                        }
                    );
                }

                Items = foodListItems;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void setItems(List<FoodListItem> items)
		{
			Items = items;
		}

        public override string ToString() { 
            return JsonSerializer.Serialize(Items, new JsonSerializerOptions { WriteIndented = true }); 
        }
    }
}
