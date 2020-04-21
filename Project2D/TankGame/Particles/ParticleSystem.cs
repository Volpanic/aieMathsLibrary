using System.Collections.Generic;

namespace Project2D.TankGame.Particles
{
    public class ParticleSystem
    {
        public List<Particle> PartList = new List<Particle>();

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
