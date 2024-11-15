namespace Class.Services
{
    public class TableListItemsService
    {
        public List<TableListItem> Items { get; set; } = new List<TableListItem>();

        public void setItems(List<TableListItem> items)
        {
            Items = items; 
        }
    }
}
