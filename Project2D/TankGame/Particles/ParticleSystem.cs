using MathClasses;
using System;
using System.Collections.Generic;

namespace Project2D.TankGame.Particles
{
    public class ParticleSystem
    {
        protected List<Particle> PartList = new List<Particle>();

        public void AddParticleCircle(Particle part, Vector2 center, float radius, int amount, float AngleOffset = 0)
        {
            float Slice = (float)(Math.PI * 2) / (float)amount;
            for(int i = 0; i < amount; i++)
            {
                Particle pa = part.Clone();

                float Angle = ((Slice * i) + AngleOffset) % (float)Math.PI * 2;

                pa.Position = center + new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * radius;

                PartList.Add(pa);
            }
        }

        public void AddParticleCircleSpeed(Particle part, Vector2 center,float Speed, float radius, int amount, float AngleOffset = 0)
        {
            float Slice = (float)(Math.PI * 2) / (float)amount;
            for (int i = 0; i < amount; i++)
            {
                Particle pa = part.Clone();

                float Angle = ((Slice * i) + AngleOffset) % (float)Math.PI * 2;

                pa.Position = center + new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * radius;
                pa.Velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * Speed;

                PartList.Add(pa);
            }
        }

        public void AddParticle(Particle part)
        {
            PartList.Add(part);
        }

        public void Draw()
        {
            for (int i = 0; i < PartList.Count; i++)
            {
                PartList[i].Draw();

                if (!PartList[i].Active)
                {
                    PartList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
