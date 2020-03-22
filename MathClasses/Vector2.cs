﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector2
    {
        public static Vector2 Zero = new Vector2(0, 0);

        public float x, y;

        public Vector2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        #region Operators

        //Addition
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        //Subtraction
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        //Multiplication V F
        public static Vector2 operator *(Vector2 a, float scale)
        {
            return new Vector2(a.x * scale, a.y * scale);
        }

        //Multiplication F V
        public static Vector2 operator *(float scale, Vector2 a)
        {
            return new Vector2(a.x * scale, a.y * scale);
        }

        //Division F V
        public static Vector2 operator /(float divis, Vector2 a)
        {
            return new Vector2(a.x / divis, a.y / divis);
        }

        //Division V F
        public static Vector2 operator /(Vector2 a, float divis)
        {
            return new Vector2(a.x / divis, a.y / divis);
        }

        #endregion// End Operators

        //Dot product
        public float Dot(Vector2 comp)
        {
            return ((x * comp.x) + (y * comp.y));
        }

        //Magnitude
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        //Normalize
        public void Normalize()
        {
            float mag = Magnitude();
            x /= mag;
            y /= mag;
        }
    }
}
