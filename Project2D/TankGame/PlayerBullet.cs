using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using System.IO;
using Raylib;
using static Raylib.Raylib;

namespace Project2D.TankGame
{
    using MathClasses;
    using Project2D.Scenes;

    public class PlayerBullet : Component
    {
        public Vector2 Velocity = new Vector2();

        private int MaxLifeTime = 120;
        private int LifeTimer = 0;

        public PlayerBullet(GameScene _gS) : base(_gS)
        {

        }

        public override void Create()
        {

        }

        public override void Update()
        {
            Position += Velocity;

            LifeTimer++;

            if(LifeTimer >= MaxLifeTime)
            {
                Active = false;
            }
        }

        public override void Draw()
        {
            DrawCircleV(new Raylib.Vector2(Position.x,Position.y),2,Color.BLACK);
        }

    }
}
