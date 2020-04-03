using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Raylib.Raylib;
using MathClasses;
using Project2D.Scenes;

using rl = Raylib;

namespace Project2D.TankGame
{
    public static class MathMore
    {
        public static rl.Vector2 toVector2(Vector2 original)
        { 
            return new rl.Vector2(original.x, original.y);
        }

        public static rl.Color toRayColour(Colour original)
        {
            return new rl.Color(original.GetRed(),original.GetGreen(),original.GetBlue(),original.GetAlpha());
        }

        public static rl.Rectangle toRayRect(Rectangle rect)
        {
            return new rl.Rectangle(rect.x,rect.y,rect.width,rect.height);
        }
    }
}
