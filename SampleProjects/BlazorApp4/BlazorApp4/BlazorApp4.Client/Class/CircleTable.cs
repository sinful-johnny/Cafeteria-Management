namespace Class
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using System;
    using System.Collections.Generic;

    public class CircleTable : ITable
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsHovered { get; set; }
        public int TableId { get; set; }
        public bool IsSelected { get; set; }
        public string? Color { get; set; } = "green";
        public string? TableStatus { get; set; } // "locked", "unlocked", "occupied"
        public string? ShapeId { get; set; }
        public List<FoodOnTable> Foods { get; set; } = new List<FoodOnTable>();
        public string? ImgURL { get; set; }
        public string? ShapeName { get; set; }
        public double Radius { get; set; }

        // Constructor
        public CircleTable(int tableId, string shapeId, double x, double y, double radius, string tableStatus)
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

        public async Task Draw(IJSRuntime JS, IJSObjectReference canvasContext)
        {
            // Implement drawing logic using Blazor's Canvas or other graphics library
            // Placeholder for implementation
            if (Foods.Count > 0)
            {
                Color = "red";
            }
            else
            {
                Color = "green";
            }

            if (JS != null)
            {
                await JS.InvokeVoidAsync("canvasInterop.drawCircle", canvasContext, X, Y, Radius, Color);
            }
        }
        public ITable Clone()
        {
            return new CircleTable(TableId, ShapeId ?? "", X, Y, Radius, TableStatus ?? "");
        }
    }

}
