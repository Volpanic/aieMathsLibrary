using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector4
    {
        public float x, y, z, w;

        public Vector4(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }

        #region Operators

        //Addition
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        //Subtraction
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        //Multiplication V F
        public static Vector4 operator *(Vector4 a, float scale)
        {
            return new Vector4(a.x * scale, a.y * scale, a.z * scale, a.w * scale);
        }

        //Multiplication F V
        public static Vector4 operator *(float scale, Vector4 a)
        {
            return new Vector4(a.x * scale, a.y * scale, a.z * scale, a.w * scale);
        }

        //Division F V
        public static Vector4 operator /(float divis, Vector4 a)
        {
            return new Vector4(a.x / divis, a.y / divis, a.z / divis, a.w / divis);
        }

        //Division V F
        public static Vector4 operator /(Vector4 a, float divis)
        {
            return new Vector4(a.x / divis, a.y / divis, a.z / divis, a.w / divis);
        }

        #endregion//End Operators
        
        //Dot product
        public float Dot(Vector4 comp)
        {
            return ((x * comp.x) + (y * comp.y) + (z * comp.z) + (w *comp.w));
        }

        //Cross
        public Vector4 Cross(Vector4 comp)
        {
            Vector3 temp = new Vector3(comp.x, comp.y, comp.z).Cross(new Vector3(comp.x,comp.y,comp.z));
            return new Vector4(temp.x,temp.y,temp.z,0);
        }

        //Magnitude
        public float Magnitude()
        {
            return (float)(Math.Sqrt((x * x) + (y * y) + (z * z) + (w * w)));
        }

    }
}
