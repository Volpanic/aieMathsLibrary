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

        public static Vector4 operator *(Vector4 a, Matrix4 b) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            return new Vector4(0, 0, 0, 0);
        }

        public static Vector4 operator *(Matrix4 b, Vector4 a) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            return new Vector4(0, 0, 0, 0);
        }

        //Multiplication 
        public static Matrix4 operator *(Matrix4 a, Matrix4 b) //Definatly wrong, will fix when i'm not a huge idiot.(might be a while...).
        {
            return new Matrix4();
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
