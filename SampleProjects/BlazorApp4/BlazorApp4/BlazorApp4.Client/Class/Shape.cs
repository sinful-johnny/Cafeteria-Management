namespace Class
{
    public class Shape
    {
        public string ShapeId { get; set; }
        public string ImgURL { get; set; }
        public string ShapeName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Radius { get; set; }
        public string ShapeType { get; set; }

        public Shape(string shapeId, string imgURL, string shapeName, int width, int height, double radius, string shapeType)
        {
            ShapeId = shapeId;
            ImgURL = imgURL;
            ShapeName = shapeName;
            Width = width;
            Height = height;
            Radius = radius;
            ShapeType = shapeType;
        }

        public Shape()
        {
        }

        public override string ToString() { 
            return $"{{\n" + $" \"ShapeId\": \"{ShapeId}\",\n" + $" \"ImgURL\": \"{ImgURL}\",\n" + $" \"ShapeName\": \"{ShapeName}\",\n" + $" \"Width\": {Width},\n" + $" \"Height\": {Height},\n" + $" \"Radius\": {Radius}\n" + $"}}"; 
        }
    }

}
