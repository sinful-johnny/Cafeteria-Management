using Class;

namespace BlazorApp2.Components
{
    public static class CollisionUtils
    {
        // Rectangle-Rectangle Collision
        public static bool IsRectangleCollidingWithRectangle(RectangleTable rect1, RectangleTable rect2)
        {
            return (
                rect1.X < rect2.X + rect2.Width &&
                rect1.X + rect1.Width > rect2.X &&
                rect1.Y < rect2.Y + rect2.Height &&
                rect1.Y + rect1.Height > rect2.Y
            );
        }

        // Circle-Circle Collision
        public static bool IsCircleCollidingWithCircle(CircleTable circle1, CircleTable circle2)
        {
            var dx = circle1.X - circle2.X;
            var dy = circle1.Y - circle2.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            return distance < circle1.Radius + circle2.Radius;
        }

        // Circle-Rectangle Collision
        public static bool IsCircleCollidingWithRectangle(CircleTable circle, RectangleTable rect)
        {
            var distX = Math.Abs(circle.X - (rect.X + rect.Width / 2));
            var distY = Math.Abs(circle.Y - (rect.Y + rect.Height / 2));

            if (distX > rect.Width / 2 + circle.Radius) return false;
            if (distY > rect.Height / 2 + circle.Radius) return false;

            if (distX <= rect.Width / 2) return true;
            if (distY <= rect.Height / 2) return true;

            var dx = distX - rect.Width / 2;
            var dy = distY - rect.Height / 2;
            return dx * dx + dy * dy <= circle.Radius * circle.Radius;
        }

        // Generalized Collision Check
        public static bool CheckCollision(object shape1, object shape2)
        {
            if (shape1 is RectangleTable rect1 && shape2 is RectangleTable rect2)
            {
                return IsRectangleCollidingWithRectangle(rect1, rect2);
            }
            else if (shape1 is CircleTable circle1 && shape2 is CircleTable circle2)
            {
                return IsCircleCollidingWithCircle(circle1, circle2);
            }
            else if (shape1 is CircleTable circle && shape2 is RectangleTable rect)
            {
                return IsCircleCollidingWithRectangle(circle, rect);
            }
            else if (shape1 is RectangleTable rectangle && shape2 is CircleTable circle0)
            {
                return IsCircleCollidingWithRectangle(circle0, rectangle);
            }
            return false;
        }

        // Border Collision Detection for Y-Axis
        public static bool IsCollidingWithBorderY(object table, double canvasHeight)
        {
            if (table is RectangleTable rect)
            {
                return rect.Y < 0 || rect.Y + rect.Height > canvasHeight;
            }
            else if (table is CircleTable circle)
            {
                return circle.Y - circle.Radius < 0 || circle.Y + circle.Radius > canvasHeight;
            }
            return false;
        }

        // Border Collision Detection for X-Axis
        public static bool IsCollidingWithBorderX(object table, double canvasWidth)
        {
            if (table is RectangleTable rect)
            {
                return rect.X < 0 || rect.X + rect.Width > canvasWidth;
            }
            else if (table is CircleTable circle)
            {
                return circle.X - circle.Radius < 0 || circle.X + circle.Radius > canvasWidth;
            }
            return false;
        }
    }
}
