namespace Class
{
	public class TableListItem
	{
        public int Id { get; set; }
        public List<ITable>? Content { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
