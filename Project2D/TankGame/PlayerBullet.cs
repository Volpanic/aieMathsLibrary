﻿using Raylib;
using static Raylib.Raylib;

namespace Project2D.TankGame
{
    using MathClasses;
    using Project2D.Scenes;

    public class PlayerBullet : Component
    {
        public Vector2 Velocity = new Vector2();

        private int MaxLifeTime = 120;
        private float LifeTimer = 0;

        public PlayerBullet(GameScene _gS) : base(_gS)
        {

        }

        public override void Create()
        {

        }

        public override void Update()
        {
            //XCheck
            if (gameScene.tileGrid.RectTileCollision(new Rectangle(Position.x - 2 + (Velocity * Game.deltaTime).x, Position.y - 2, 2, 2)))
            {
                Active = false;
            }

            //YCheck
            if (gameScene.tileGrid.RectTileCollision(new Rectangle(Position.x - 2, Position.y - 2 + (Velocity * Game.deltaTime).y, 2, 2)))
            {
                Active = false;
            }

            Position += (Velocity * Game.deltaTime);

            LifeTimer += Game.deltaTime;

            if (LifeTimer >= MaxLifeTime)
            {
                Active = false;
            }
        }

        public override void Draw()
        {
            //Draw Bullet
            DrawCircleV(new Raylib.Vector2(Position.x, Position.y), 2, Color.BLACK);
        }



    }
}
