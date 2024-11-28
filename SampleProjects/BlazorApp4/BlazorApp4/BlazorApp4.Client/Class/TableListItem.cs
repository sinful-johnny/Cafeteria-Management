namespace Class
{
	public class ShapeListItem
	{
        public int Id { get; set; }
        public List<Shape>? Content { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
