using System;
using System.Collections.Generic;

namespace MathClasses
{
    public class CollisionShape
    {
        public int Sides = 4;
        public float Rotation = 0f;
        public Vector2 Position = Vector2.Zero;
        public float Radius = 16;

        public CollisionShape(int _sides, Vector2 _pos, float _rot = 0.0f, float _radius = 8.0f)
        {
            Sides = _sides;
            Position = _pos;
            Rotation = _rot;
            Radius = _radius;

            ResetPoints();
        }

        public List<Vector2> Points = new List<Vector2>();

        public void ResetPoints()
        {
            Points = new List<Vector2>();

            float Interval = (float)(Math.PI * 2) / Sides;
            for (int i = 0; i < Sides; i++)
            {
                float AngleA = (Interval * i) + Rotation;
                Vector2 PointA = Position + new Vector2((float)Math.Cos(AngleA) * Radius, (float)Math.Sin(AngleA) * Radius);

                Points.Add(PointA);

            }
        }

        public static bool Intersect(CollisionShape Shape1, CollisionShape Shape2)
        {
            Shape1.ResetPoints();
            Shape2.ResetPoints();
            CollisionShape S1 = Shape1;
            CollisionShape S2 = Shape2;

            for (int shape = 0; shape < 2; shape++)
            {
                if (shape == 1)
                {
                    S1 = Shape2;
                    S2 = Shape1;
                }

                for (var a = 0; a < S1.Points.Count; a++)
                {
                    int b = (a + 1) % S1.Points.Count;
                    Vector2 axisProj = new Vector2(-(S1.Points[b].y - S1.Points[a].y), (S1.Points[b].x - S1.Points[a].x));

                    //Work out min and Max
                    float min_r1 = int.MaxValue;
                    float max_r1 = -int.MaxValue;
                    for (var p = 0; p < S1.Points.Count; p++)
                    {
                        float q = (S1.Points[p].x * axisProj.x + S1.Points[p].y * axisProj.y);
                        min_r1 = Math.Min(min_r1, q);
                        max_r1 = Math.Max(max_r1, q);
                    }

                    float min_r2 = int.MaxValue;
                    float max_r2 = -int.MaxValue;
                    for (var p = 0; p < S2.Points.Count; p++)
                    {
                        float q = (S2.Points[p].x * axisProj.x + S2.Points[p].y * axisProj.y);
                        min_r2 = Math.Min(min_r2, q);
                        max_r2 = Math.Max(max_r2, q);
                    }

                    if (!(max_r2 >= min_r1 && max_r1 >= min_r2))
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}