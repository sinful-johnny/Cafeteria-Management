namespace BlazorApp1.Class
{
    using System;
    using System.Collections.Generic;

    public class CircleTable : ITable
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
        public string? ImgURL { get; set; }
        public string? ShapeName { get; set; }
        public double Radius { get; set; }

        // Constructor
        public CircleTable(int tableId, string shapeId, int x, int y, double radius, string tableStatus, string ImgURL, string ShapeName)
        {
            X = x;
            Y = y;
            Radius = radius;
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

        public bool IsMouseInRange(double x, double y)
        {
            var distance = Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y));
            return distance < Radius;
        }

        public void Draw(object context)
        {
            // Implement drawing logic using Blazor's Canvas or other graphics library
            // Placeholder for implementation
            throw new NotImplementedException();
        }
    }

}
