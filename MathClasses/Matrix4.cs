using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix4
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16;

        public Matrix4()
        {
            m1 = 1; m2 = 0; m3 = 0; m4 = 0;
            m5 = 0; m6 = 1; m7 = 0; m8 = 0;
            m9 = 0; m10= 0; m11= 1; m12= 0;
            m13= 0; m14= 0; m15= 0; m16= 1;
        }

        public Matrix4(float _m1 = 0, float _m2 = 0, float _m3 = 0, float _m4 = 0, float _m5 = 0, float _m6 = 0, float _m7 = 0, float _m8 = 0, float _m9 = 0,
            float _m10 = 0, float _m11 = 0, float _m12 = 0, float _m13 = 0, float _m14 = 0, float _m15 = 0, float _m16 = 0)
        {
            m1 = _m1; m2 = _m2; m3 = _m3; m4 = _m4;
            m5 = _m5; m6 = _m6; m7 = _m7; m8 = _m8;
            m9 = _m9; m10 = _m10; m11 = _m11; m12 = _m12;
            m13 = _m13; m14 = _m14; m15 = _m15; m16 = _m16;
        }

        public void Set(float _m1 = 0, float _m2 = 0, float _m3 = 0, float _m4 = 0, float _m5 = 0, float _m6 = 0, float _m7 = 0, float _m8 = 0, float _m9 = 0,
            float _m10 = 0, float _m11 = 0, float _m12 = 0, float _m13 = 0, float _m14 = 0, float _m15 = 0, float _m16 = 0)
        {
            m1 = _m1; m2 = _m2; m3 = _m3; m4 = _m4;
            m5 = _m5; m6 = _m6; m7 = _m7; m8 = _m8;
            m9 = _m9; m10 = _m10; m11 = _m11; m12 = _m12;
            m13 = _m13; m14 = _m14; m15 = _m15; m16 = _m16;
        }

        public static Vector4 operator *(Vector4 vec, Matrix4 b) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            b.m1 *= vec.x;
            b.m5 *= vec.y;
            b.m9 *= vec.z;
            b.m13 *= vec.w;

            b.m2 *= vec.x;
            b.m6 *= vec.y;
            b.m10 *= vec.z;
            b.m14 *= vec.w;

            b.m3 *= vec.x;
            b.m7 *= vec.y;
            b.m11 *= vec.z;
            b.m15 *= vec.w;

            b.m4 *= vec.x;
            b.m8 *= vec.y;
            b.m12 *= vec.z;
            b.m16 *= vec.w;

            return new Vector4((b.m1 + b.m5 + b.m9 + b.m13), (b.m2 + b.m6 + b.m10 + b.m14), (b.m3 + b.m7 + b.m11 + b.m15), (b.m4 + b.m8 + b.m12 + b.m16));
        }

        public static Vector4 operator *(Matrix4 b, Vector4 vec) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            b.m1 *= vec.x;
            b.m5 *= vec.y;
            b.m9 *= vec.z;
            b.m13 *= vec.w;

            b.m2 *= vec.x;
            b.m6 *= vec.y;
            b.m10 *= vec.z;
            b.m14 *= vec.w;

            b.m3 *= vec.x;
            b.m7 *= vec.y;
            b.m11 *= vec.z;
            b.m15 *= vec.w;

            b.m4 *= vec.x;
            b.m8 *= vec.y;
            b.m12 *= vec.z;
            b.m16 *= vec.w;

            return new Vector4((b.m1 + b.m5 + b.m9 + b.m13),(b.m2 + b.m6 + b.m10 + b.m14),(b.m3 + b.m7 + b.m11 + b.m15),(b.m4 + b.m8 + b.m12 + b.m16));
        }

        //Multiplication 
        public static Matrix4 operator *(Matrix4 b, Matrix4 a) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            Matrix4 mat = new Matrix4();
            float n1 = (a.m1 * b.m1) + (a.m2 * b.m5) + (a.m3 * b.m9)  + (a.m4 * b.m13);
            float n2 = (a.m1 * b.m2) + (a.m2 * b.m6) + (a.m3 * b.m10) + (a.m4 * b.m14);
            float n3 = (a.m1 * b.m3) + (a.m2 * b.m7) + (a.m3 * b.m11) + (a.m4 * b.m15);
            float n4 = (a.m1 * b.m4) + (a.m2 * b.m8) + (a.m3 * b.m12) + (a.m4 * b.m16);

            float n5 = (a.m5 * b.m1) + (a.m6 * b.m5) + (a.m7 * b.m9) + (a.m8 * b.m13);
            float n6 = (a.m5 * b.m2) + (a.m6 * b.m6) + (a.m7 * b.m10) + (a.m8 * b.m14);
            float n7 = (a.m5 * b.m3) + (a.m6 * b.m7) + (a.m7 * b.m11) + (a.m8 * b.m15);
            float n8 = (a.m5 * b.m4) + (a.m6 * b.m8) + (a.m7 * b.m12) + (a.m8 * b.m16);

            float n9 = (a.m9 * b.m1) + (a.m10 * b.m5) + (a.m11 * b.m9) + (a.m12 * b.m13);
            float n10 = (a.m9 * b.m2) + (a.m10 * b.m6) + (a.m11 * b.m10) + (a.m12 * b.m14);
            float n11 = (a.m9 * b.m3) + (a.m10 * b.m7) + (a.m11 * b.m11) + (a.m12 * b.m15);
            float n12 = (a.m9 * b.m4) + (a.m10 * b.m8) + (a.m11 * b.m12) + (a.m12 * b.m16);

            float n13 = (a.m13 * b.m1) + (a.m14 * b.m5) + (a.m15 * b.m9) + (a.m16 * b.m13);
            float n14 = (a.m13 * b.m2) + (a.m14 * b.m6) + (a.m15 * b.m10) + (a.m16 * b.m14);
            float n15 = (a.m13 * b.m3) + (a.m14 * b.m7) + (a.m15 * b.m11) + (a.m16 * b.m15);
            float n16 = (a.m13 * b.m4) + (a.m14 * b.m8) + (a.m15 * b.m12) + (a.m16 * b.m16);

            mat.Set(n1, n2, n3, n4, n5, n6, n7, n8, n9,n10,n11,n12,n13,n14,n15,n16);

            return mat;
        }

        public void SetRotateX(float radians)
        {
            Set(1, 0, 0,0,
                0, (float)Math.Cos(radians), (float)Math.Sin(radians),0,
                0, (float)-Math.Sin(radians), (float)Math.Cos(radians),0,
                0,0,0,1);
        }

        public void SetRotateY(float radians)
        {
            Set((float)Math.Cos(radians), 0, (float)-Math.Sin(radians),0,
                0, 1, 0, 0,
                (float)Math.Sin(radians), 0, (float)Math.Cos(radians),0,
                0,0,0,1);
        }

        public void SetRotateZ(float radians)
        {
            Set((float)Math.Cos(radians), (float)Math.Sin(radians), 0,0,
                (float)-Math.Sin(radians), (float)Math.Cos(radians), 0,0,
                0, 0, 1,0,
                0,0,0,1);
        }

    }
}
