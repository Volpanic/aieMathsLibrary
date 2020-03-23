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

    public class PlayerTank : Component
    {
        public Vector2 Velocity = new Vector2();

        public override void Create()
        {
            Sprite = LoadTexture(Path.Combine("Resources","Sprites","spr_tank.png")); // Set to tank sprite
            Dimensions = new Vector2(Sprite.width,Sprite.height); // Sets sprite width and height
            Origin = Dimensions / 2; // Centers the sprite
        }

        public override void Update()
        {
            float rotSpeed = (float)Math.PI;

            bool KeyRight = IsKeyDown(KeyboardKey.KEY_RIGHT); // Rot Right
            bool KeyLeft = IsKeyDown(KeyboardKey.KEY_LEFT); // Rot Left

            bool KeyForward = IsKeyDown(KeyboardKey.KEY_Z); // Move Forward
            bool KeyBackWard = IsKeyDown(KeyboardKey.KEY_X); // Move Backward


            //Rotation
            if (KeyRight)
            {
                Rotation += rotSpeed;
            }

            if(KeyLeft)
            {
                Rotation -= rotSpeed;
            }

            //Movement
            if(KeyForward)
            {
                Velocity = new Vector2((float)Math.Cos(Rotation * DEG2RAD), (float)Math.Sin(Rotation * DEG2RAD));
            }

            if (KeyBackWard)
            {
                Velocity = new Vector2(-(float)Math.Cos(Rotation * DEG2RAD), -(float)Math.Sin(Rotation * DEG2RAD));
            }

            if((KeyBackWard && KeyForward) || (!KeyBackWard && !KeyForward))
            {
                Velocity.x = 0;
                Velocity.y = 0;
            }

            Position += Velocity;
        }

        public override void Draw()
        {
            DrawSelf();
        }
    }
}
