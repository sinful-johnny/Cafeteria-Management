namespace Class
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using System;
    using System.Collections.Generic;

    public interface ITable
    {
        public double X { get; set; }
        public double Y { get; set; }
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
        public Task Draw(IJSRuntime JS, IJSObjectReference canvasContext);

        public bool IsMouseInRange(double x, double y);

        public void SetIsHovered(bool value);
    }
}
