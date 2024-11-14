namespace BlazorApp1.Class
{
    using System;
    using System.Collections.Generic;

    public interface ITable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHovered { get; set; }
        public int TableId { get; set; }
        public bool IsSelected { get; set; }
        public string? Color { get; set; }
        public string? TableStatus { get; set; } // "locked", "unlocked", "occupied"
        public string? ShapeId { get; set; }
        public string? ImgURL { get; set; }

        public string? ShapeName { get; set; }
        public List<FoodOnTable> Foods { get; set; }

        // Method signatures without implementation
        public void Draw(object context)
        {
            throw new NotImplementedException();
        }

        public bool IsMouseInRange(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void SetIsHovered(bool value)
        {
            throw new NotImplementedException();
        }
    }
}
