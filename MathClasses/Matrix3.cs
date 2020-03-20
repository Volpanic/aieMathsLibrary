using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix3
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9;

        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }

        public Matrix3(float _m1 = 0, float _m2 = 0, float _m3 = 0, float _m4 = 0, float _m5 = 0, float _m6 = 0, float _m7 = 0, float _m8 = 0, float _m9 = 0)
        {
            m1 = _m1; m2 = _m2; m3 = _m3;
            m4 = _m4; m5 = _m5; m6 = _m6;
            m7 = _m7; m8 = _m8; m9 = _m9;
        }

        public void Set(float _m1 = 0, float _m2 = 0, float _m3 = 0, float _m4 = 0, float _m5 = 0, float _m6 = 0, float _m7 = 0, float _m8 = 0, float _m9 = 0)
        {
            m1 = _m1; m2 = _m2; m3 = _m3;
            m4 = _m4; m5 = _m5; m6 = _m6;
            m7 = _m7; m8 = _m8; m9 = _m9;
        }

        public void SetScale(float _x, float _y , float _z)
        {
            m1 = _x; m2 = 0; m3 = 0;
            m4 = 0; m5 = _y; m6 = 0;
            m7 = 0; m8 = 0; m9 = _z;
        }

        public void SetScale(Vector3 sca)
        {
            m1 = sca.x; m2 = 0; m3 = 0;
            m4 = 0; m5 = sca.y; m6 = 0;
            m7 = 0; m8 = 0; m9 = sca.z;
        }

        //Multiplication 
        public static Matrix3 operator *(Matrix3 b, Matrix3 a) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            Matrix3 mat = new Matrix3();
            float n1 = (a.m1 * b.m1) + (a.m2 * b.m4) + (a.m3 * b.m7);
            float n2 = (a.m1 * b.m2) + (a.m2 * b.m5) + (a.m3 * b.m8);
            float n3 = (a.m1 * b.m3) + (a.m2 * b.m6) + (a.m3 * b.m9);

            float n4 = (a.m4 * b.m1) + (a.m5 * b.m4) + (a.m6 * b.m7);
            float n5 = (a.m4 * b.m2) + (a.m5 * b.m5) + (a.m6 * b.m8);
            float n6 = (a.m4 * b.m3) + (a.m5 * b.m6) + (a.m6 * b.m9);

            float n7 = (a.m7 * b.m1) + (a.m8 * b.m4) + (a.m9 * b.m7);
            float n8 = (a.m7 * b.m2) + (a.m8 * b.m5) + (a.m9 * b.m8);
            float n9 = (a.m7 * b.m3) + (a.m8 * b.m6) + (a.m9 * b.m9);

            mat.Set(n1,n2,n3,n4,n5,n6,n7,n8,n9);

            return mat;
        }

        public static Vector3 operator *(Vector3 vec, Matrix3 b) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            b.m1 *= vec.x;
            b.m4 *= vec.y;
            b.m7 *= vec.z;

            b.m2 *= vec.x;
            b.m5 *= vec.y;
            b.m8 *= vec.z;

            b.m3 *= vec.x;
            b.m6 *= vec.y;
            b.m9 *= vec.z;

            return new Vector3(b.m1 + b.m4 + b.m7, b.m2 + b.m5 + b.m8, b.m3 + b.m6 + b.m9);
        }

        public static Vector3 operator *(Matrix3 b, Vector3 vec) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            b.m1 *= vec.x;
            b.m4 *= vec.y;
            b.m7 *= vec.z;

            b.m2 *= vec.x;
            b.m5 *= vec.y;
            b.m8 *= vec.z;

            b.m3 *= vec.x;
            b.m6 *= vec.y;
            b.m9 *= vec.z;

            return new Vector3(b.m1 + b.m4 + b.m7, b.m2 + b.m5 + b.m8, b.m3 + b.m6 + b.m9);
        }

        public void SetRotateX(float radians)
        {
            Set(1, 0, 0,
                0, (float)Math.Cos(radians), (float)Math.Sin(radians),
                0, (float)-Math.Sin(radians), (float)Math.Cos(radians));
        }

        public void SetRotateY(float radians)
        {
            Set((float)Math.Cos(radians), 0, (float)-Math.Sin(radians),
                0,1,0,
                (float)Math.Sin(radians), 0, (float)Math.Cos(radians));
        }

        public void SetRotateZ(float radians)
        {
            Set((float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                (float)-Math.Sin(radians), (float)Math.Cos(radians), 0,
                0, 0, 1);
        }

    }
}