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

    public class PlayerTank : Component
    {
        public Vector2 Velocity = new Vector2();


        //Gun
        public Texture2D GunSprite;
        public Vector2 GunOrigin;
        public float GunRotation = 0.0f;

        public Vector2 MousePos { get { return new Vector2(GetMousePosition().x, GetMousePosition().y) / Program.GameZoom; } }

        public PlayerTank(GameScene _gS) : base(_gS)
        {

        }

        public override void Create()
        {
            Sprite = LoadTexture(Path.Combine("Resources","Sprites","spr_tank.png")); // Set to tank sprite
            Dimensions = new Vector2(Sprite.width,Sprite.height); // Sets sprite width and height
            Origin = Dimensions / 2; // Centers the sprite

            //Gun Stuff
            GunSprite = LoadTexture(Path.Combine("Resources", "Sprites", "spr_tank_gun.png")); // Load Gun Sprite
            GunOrigin = new Vector2(2, 3); //Set origin 
        }

        public override void OnDestroy()
        {
            UnloadTexture(Sprite);
            UnloadTexture(GunSprite);
        }

        public override void Update()
        {
            float rotSpeed = (float)Math.PI;

            bool KeyRight = IsKeyDown(KeyboardKey.KEY_D); // Rot Right
            bool KeyLeft = IsKeyDown(KeyboardKey.KEY_A); // Rot Left

            bool KeyForward = IsKeyDown(KeyboardKey.KEY_SPACE); // Move Forward
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

            GunUpdate();
        }

        public override void Draw()
        {
            DrawSelf();

            GunDraw();
        }

        ////////////////
        public void GunUpdate()
        {
            GunRotation = Vector2.Direction(Position,MousePos);

            bool BulletShoot = IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON);
            float Speed = 5;

            if(BulletShoot)
            {
                PlayerBullet pb = new PlayerBullet(gameScene);
                pb.Velocity = new Vector2((float)Math.Cos(GunRotation),(float)Math.Sin(GunRotation)) * Speed;

                //Set pos to be partway through the barrel
                pb.Position = Position + (new Vector2((float)Math.Cos(GunRotation), (float)Math.Sin(GunRotation)) * 6);

                gameScene.PlayerBullets.Add(pb);
            }
        }

        public void GunDraw()
        {
            Rectangle ImageRect = new Rectangle(0,0,GunSprite.width,GunSprite.height); // Image rectangle (Whole Image)
            Rectangle DestRect = new Rectangle(Position.x,Position.y,ImageRect.width,ImageRect.height); // World Rect

            DrawTexturePro(GunSprite,ImageRect,DestRect,new Raylib.Vector2(GunOrigin.x,GunOrigin.y),GunRotation * RAD2DEG,Color.WHITE);

            DrawCircleLines((int)MousePos.x, (int)MousePos.y, 3, Color.BLACK);
        }
    }
}
