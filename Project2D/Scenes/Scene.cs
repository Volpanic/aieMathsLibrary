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

        public Scene(Game _game)
        {
            game = _game;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
