namespace BlazorApp1.Class
{
    using System;
    using System.Collections.Generic;

    public class RectangleTable : ITable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHovered { get; set; }
        public int TableId { get; set; }
        public bool IsSelected { get; set; }
        public string? Color { get; set; }
        public string? TableStatus { get; set; } // "locked", "unlocked", "occupied"
        public string? ShapeId { get; set; }
        public List<FoodOnTable> Foods { get; set; } = new List<FoodOnTable>();

        public int Width { get; set; }
        public int Height { get; set; }
        public string? ImgURL { get; set; }
        public string? ShapeName { get; set; }

        // Constructor
        public RectangleTable(int tableId, string shapeId, int x, int y, int width, int height, string tableStatus, string ImgURL, string ShapeName)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Foods = new List<FoodOnTable>();
            TableStatus = tableStatus;
            TableId = tableId;
            ShapeId = shapeId;
            this.ImgURL = ImgURL;
            this.ShapeName = ShapeName;
        }

        public void SetIsHovered(bool value)
        {
            IsHovered = value;
        }

        public bool IsMouseInRange(int x, int y)
        {
            return x > X && x < X + Width && y > Y && y < Y + Height;
        }

        public void Draw(object context)
        {
            // Implement drawing logic using Blazor's Canvas or other graphics library
            // Placeholder for implementation
            throw new NotImplementedException();
        }
    }
}
