using Class.Converter;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Class.Services
{
    public class TableListItemsService : INotifyPropertyChanged
    {
        private List<ShapeListItem> _items = new List<ShapeListItem>();

        public List<ShapeListItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void setItems(List<ShapeListItem> items)
        {
            Items = items;
        }

        public void SetItemsFromJson(string json)
        {
            var shapeDtos = JsonSerializer.Deserialize<List<ShapeDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            });

            if (shapeDtos != null)
            {
                var shapes = new List<Shape>();
                foreach (var dto in shapeDtos)
                {
                    shapes.Add(dto.ToShape());
                }

                // Group shapes into ShapeListItem if needed
                Items = new List<ShapeListItem>
            {
                new ShapeListItem { Id = 1, Content = shapes, Title = "Tables" }
            };
            }
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
