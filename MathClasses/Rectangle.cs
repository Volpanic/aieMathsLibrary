namespace MathClasses
{
    public class Rectangle
    {
        public float x = 0;
        public float y = 0;

        public float width = 1;
        public float height = 1;

        public float Right { get { return x + width; } }
        public float Bottem { get { return y + height; } }

        public Rectangle(Vector2 topLeft, Vector2 size)
        {
            x = topLeft.x;
            y = topLeft.y;

            width = size.x;
            height = size.y;
        }

        public Rectangle(float _x, float _y, float _width, float _height)
        {
            x = _x;
            y = _y;

            width = _width;
            height = _height;
        }

        public bool CollidingWith(Rectangle other)
        {
            return (x < other.Right && Right > other.x && y < other.Bottem && Bottem > other.y);
        }

        public bool PointInRect(Vector2 point)
        {
            return ((point.x > x && point.x < Right) && (point.y > y && point.y < Bottem));
        }

        public static bool RectCollision(Rectangle rect1, Rectangle rect2)
        {
            return rect1.CollidingWith(rect2);
        }
    }
}
