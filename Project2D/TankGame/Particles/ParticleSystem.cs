using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2D.Scenes;
using Raylib;
using static Raylib.Raylib;

namespace Project2D.TankGame.Particles
{
    public class ParticleSystem
    {
        public List<Particle> PartList = new List<Particle>();

        public void Draw()
        {
            for(int i = 0; i < PartList.Count; i ++)
            {
                PartList[i].Draw();

                if(!PartList[i].Active)
                {
                    PartList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
