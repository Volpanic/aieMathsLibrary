using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Project2D.TankGame;
using Raylib;
using static Raylib.Raylib;

namespace Project2D.Scenes
{
    using MathClasses;
    public class GameScene : Scene
    {
        public PlayerTank player;

        public GameScene(Game _game) : base(_game)
        {
            player = new PlayerTank();
            player.Position = new Vector2(Program.GameWidth / 2, Program.GameHeight/2);
        }

        public override void Update()
        {
            player.Update();
        }

        public override void Draw()
        {
            player.Draw();
        }
    }
}
