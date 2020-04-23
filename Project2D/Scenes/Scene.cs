using Project2D.TankGame.Particles;
using Raylib;

namespace Project2D.Scenes
{
    public abstract class Scene
    {

        public Game game;
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

        public virtual void EndScene()
        {

        }
    }
}
