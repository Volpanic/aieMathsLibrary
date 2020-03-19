using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector3
    {
        public float x, y, z;

        public Vector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        //Addition
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        //Subtraction
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        //Multiplication V F
        public static Vector3 operator *(Vector3 a, float scale)
        {
            return new Vector3(a.x * scale, a.y * scale, a.z * scale);
        }

        //Multiplication F V
        public static Vector3 operator *(float scale,Vector3 a)
        {
            return new Vector3(a.x * scale, a.y * scale, a.z * scale);
        }

        public float Dot(Vector3 comp)
        {
            return ((x * comp.x) + (y * comp.y) + (z * comp.z));
        }

        public Vector3 Cross(Vector3 comp)
        {
            float xx = (y * comp.z) - (z * comp.y);
            float yy = (z * comp.x) - (x *comp.z);
            float zz = (x * comp.y) - (y * comp.x);

            return new Vector3(xx,yy,zz);
        }

    }
}
