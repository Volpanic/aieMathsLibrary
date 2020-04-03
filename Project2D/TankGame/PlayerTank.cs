using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Raylib;
using static Raylib.Raylib;
using MathClasses;
using Project2D.Scenes;
using Project2D.TankGame;

using rl = Raylib;

namespace Project2D.TankGame
{
    using MathClasses;
    public class PlayerTank : Component
    {
        public Vector2 Velocity = new Vector2();
        public float Speed = 0;

        //Gun
        public Texture2D GunSprite;
        public Vector2 GunOrigin;
        public float GunRotation = 0.0f;

        public Vector2 MousePos { get { return GetRelativeMousePosition(); } }

        public PlayerTank(GameScene _gS) : base(_gS)
        {

        }

        public static Vector2 GetRelativeMousePosition()
        {
            Vector2 RefMousePos = (new Vector2(GetMousePosition().x, GetMousePosition().y) / Program.GameZoom);

            return RefMousePos;
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
            PlayerMovement();
            PlayerCollision();

            GunUpdate();

        }

        public override Rectangle GetCollisionRectangle()
        {
            Rectangle rect = new Rectangle((Position.x - Origin.x) + 2, (Position.y - Origin.y) + 2,Dimensions.x-2, Dimensions.y-2);

            return rect;
        }

        public void PlayerMovement()
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

            if (KeyLeft)
            {
                Rotation -= rotSpeed;
            }

            //Movement
            if (KeyForward)
            {
                Velocity = new Vector2((float)Math.Cos(Rotation * DEG2RAD), (float)Math.Sin(Rotation * DEG2RAD));
                Speed = Numbers.Approach(Speed, 2, 0.11f);
            }

            if (KeyBackWard)
            {
                Velocity = new Vector2(-(float)Math.Cos(Rotation * DEG2RAD), -(float)Math.Sin(Rotation * DEG2RAD));
                Speed = Numbers.Approach(Speed, 2, 0.11f);
            }

            if ((KeyBackWard && KeyForward) || (!KeyBackWard && !KeyForward))
            {
                Speed = Numbers.Approach(Speed, 0, 0.11f);
            }
        }

        public void PlayerCollision()
        {
            Rectangle ColRect = GetCollisionRectangle();

            Vector2 actualVel = (Velocity * Speed) * (Game.deltaTime);

            //X Collision
            Rectangle XRect = ColRect;
            XRect.x += actualVel.x;
            //Check if touching
            if (gameScene.tileGrid.RectTileCollision(XRect))
            {
                //Move Towards wall until touching
                XRect = ColRect;
                XRect.x += (float)Math.Sign(actualVel.x);
                while (!gameScene.tileGrid.RectTileCollision(XRect))
                {
                    Position.x += (float)Math.Sign(actualVel.x);
                    XRect.x += (float)Math.Sign(actualVel.x);
                }
                Velocity.x = 0;
                actualVel.x = 0;
            }
            Position.x += actualVel.x;

            //YCollision
            ColRect = GetCollisionRectangle();

            Rectangle YRect = ColRect;
            YRect.y += actualVel.y;
            if (gameScene.tileGrid.RectTileCollision(YRect))
            {
                YRect = ColRect;
                YRect.y += (float)Math.Sign(actualVel.y);
                while (!gameScene.tileGrid.RectTileCollision(YRect))
                {
                    Position.y += (float)Math.Sign(actualVel.y);
                    YRect.y += (float)Math.Sign(actualVel.y);
                }
                Velocity.y = 0;
                actualVel.y = 0;
            }
            Position.y += actualVel.y;
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

            DrawTexturePro(GunSprite,MathMore.toRayRect(ImageRect),MathMore.toRayRect(DestRect),new Raylib.Vector2(GunOrigin.x,GunOrigin.y),GunRotation * RAD2DEG,Color.WHITE);

            DrawCircleLines((int)MousePos.x, (int)MousePos.y, 3, Color.BLACK);
        }
    }
}
