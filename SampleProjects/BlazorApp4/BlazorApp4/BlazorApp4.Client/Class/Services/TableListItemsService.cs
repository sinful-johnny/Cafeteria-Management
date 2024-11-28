namespace Class.Services
{
    public class TableListItemsService
    {
        public List<ShapeListItem> Items { get; set; } = new List<ShapeListItem>();

        public void setItems(List<ShapeListItem> items)
        {   
            Items = items; 
        }
    }
}
