using Project2D.TankGame.Particles;
using Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2D.Scenes
{
    public abstract class Scene
    {

        protected Game game;
        public Camera2D camera;
        public ParticleSystem partSystem;

        public Scene(Game _game)
        {
            game = _game;
            partSystem = new ParticleSystem();
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
