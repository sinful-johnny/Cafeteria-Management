namespace Class
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using System;
    using System.Collections.Generic;

    public class RectangleTable : ITable
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsHovered { get; set; }
        public int TableId { get; set; }
        public bool IsSelected { get; set; }
        public string? Color { get; set; }
        public string? TableStatus { get; set; } // "locked", "unlocked", "occupied"
        public string? ShapeId { get; set; }
        public List<FoodOnTable> Foods { get; set; } = new List<FoodOnTable>();

        public double Width { get; set; }
        public double Height { get; set; }
        public string? ImgURL { get; set; }
        public string? ShapeName { get; set; }

        // Constructor
        public RectangleTable(int tableId, string shapeId, double x, double y, double width, double height, string tableStatus)
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

        public bool IsMouseInRange(double x, double y)
        {
            return x > X && x < X + Width && y > Y && y < Y + Height;
        }

        public async Task Draw(IJSRuntime JS, IJSObjectReference canvasContext)
        {
            // Implement drawing logic using Blazor's Canvas or other graphics library
            // Placeholder for implementation
            if(JS != null)
            {
                await JS.InvokeVoidAsync("canvasInterop.drawRect", canvasContext, X, Y, Width, Height, "green");
            }
        }
        public ITable Clone()
        {
            return new RectangleTable(TableId, ShapeId ?? "", X, Y, Width, Height, TableStatus ?? "");
        }
    }
}
