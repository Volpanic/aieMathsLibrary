using MathClasses;

using rl = Raylib;

namespace Project2D.TankGame
{
    public static class MathMore // For converting between mine and raylibs
    {
        public static rl.Vector2 toVector2(Vector2 original)
        {
            return new rl.Vector2(original.x, original.y);
        }

        public static rl.Color toRayColour(Colour original)
        {
            return new rl.Color(original.GetRed(), original.GetGreen(), original.GetBlue(), original.GetAlpha());
        }

        public static rl.Rectangle toRayRect(Rectangle rect)
        {
            return new rl.Rectangle(rect.x, rect.y, rect.width, rect.height);
        }
    }
}
