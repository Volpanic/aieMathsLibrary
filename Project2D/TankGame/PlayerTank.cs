using Project2D.Scenes;
using Raylib;
using System;
using System.IO;
using static Raylib.Raylib;

namespace Project2D.TankGame
{
    using MathClasses;
    using Project2D.TankGame.Particles;

    public class PlayerTank : Component
    {
        public Vector2 Velocity = new Vector2();
        public float Speed = 0;
        public Texture2D TredSprite;

        //Gun
        public Texture2D GunSprite;
        public Vector2 GunOrigin;
        public float GunRotation = 0.0f;
        public int BulletAmount = 1;
        float BulletTimer = 0;
        float BulletTimerMax = 10;
        float TredTimer = 0;

        public int MaxHp = 5;
        public int HP = 5;
        public bool BeenHit = false;
        public float HitTimer = 0;

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
            //Load Sprites
            Sprite = LoadTexture(Path.Combine("Resources", "Sprites", "spr_tank.png")); // Set to tank sprite
            Dimensions = new Vector2(Sprite.width, Sprite.height); // Sets sprite width and height
            Origin = Dimensions / 2; // Centers the sprite
            TredSprite = LoadTexture(Path.Combine("Resources", "Sprites", "spr_tred.png"));

            //Gun Stuff
            GunSprite = LoadTexture(Path.Combine("Resources", "Sprites", "spr_tank_gun.png")); // Load Gun Sprite
            GunOrigin = new Vector2(2, 3); //Set origin 

        }

        public override void OnDestroy()
        {
            UnloadTexture(Sprite);
            UnloadTexture(GunSprite);
            UnloadTexture(TredSprite);
        }

        public override void Update()
        {
            PlayerMovement();
            PlayerCollision();

            //Create Tred particles
            if (Speed != 0)
            {
                TredTimer += Game.deltaTime;

                if (TredTimer > 5)
                {
                    TredTimer = TredTimer % 5;
                    Vector2 tred1 = new Vector2((4) * (float)Math.Cos(Rotation), (+4) * (float)Math.Sin(Rotation));
                    Vector2 tred2 = new Vector2((-4) * (float)Math.Cos(Rotation), (-4) * (float)Math.Sin(Rotation));
                    tred1 += Position;
                    tred2 += Position;
                    gameScene.partSystem.PartList.Add(new Particle(gameScene, TredSprite, tred1, Vector2.Zero, Rotation, true, 60));
                    gameScene.partSystem.PartList.Add(new Particle(gameScene, TredSprite, tred2, Vector2.Zero, Rotation, true, 60));
                }
            }

            GunUpdate();
        }

        //Slightly smaller hitbox
        public override Rectangle GetCollisionRectangle()
        {
            Rectangle rect = new Rectangle((Position.x - Origin.x) + 2, (Position.y - Origin.y) + 2, Dimensions.x - 2, Dimensions.y - 2);

            return rect;
        }

        //Move Player
        public void PlayerMovement()
        {
            float rotSpeed = (float)Math.PI;

            bool KeyRight = IsKeyDown(KeyboardKey.KEY_D); // Rot Right
            bool KeyLeft = IsKeyDown(KeyboardKey.KEY_A); // Rot Left

            bool KeyForward = IsKeyDown(KeyboardKey.KEY_SPACE); // Move Forward
            bool KeyBackWard = IsKeyDown(KeyboardKey.KEY_X); // Move Backward
            bool KeyShift = IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT); // Slow Down

            float MaxSpeed = 2;

            if (KeyShift)
            {
                MaxSpeed = 1;
            }

            //Rotation
            if (KeyRight)
            {
                Rotation += rotSpeed * Game.deltaTime;
            }

            if (KeyLeft)
            {
                Rotation -= rotSpeed * Game.deltaTime;
            }

            //Movement
            if (KeyForward)
            {
                Velocity = new Vector2((float)Math.Cos(Rotation * DEG2RAD), (float)Math.Sin(Rotation * DEG2RAD));
                Speed = Numbers.Approach(Speed, MaxSpeed, 0.11f);
            }

            if (KeyBackWard)
            {
                Velocity = new Vector2(-(float)Math.Cos(Rotation * DEG2RAD), -(float)Math.Sin(Rotation * DEG2RAD));
                Speed = Numbers.Approach(Speed, MaxSpeed, 0.11f);
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

            HitTimer = Numbers.Approach(HitTimer, 0, 1);
            if(HitTimer == 0)
            {
                BeenHit = false;
            }
        }

        public override void Draw()
        {
            if (!BeenHit || HitTimer % 5 == 0)
            {
                DrawSelf();
                GunDraw();
            }
            

            //Draw HealthBar
            Rectangle PlayerHealthBack = new Rectangle(8, 8, 96, Program.GameHeight * 0.05f);
            Rectangle PlayerHealthFront = new Rectangle(PlayerHealthBack.x, PlayerHealthBack.y, PlayerHealthBack.width, PlayerHealthBack.height);
            HP = (int)Clamp(HP,0,MaxHp);
            PlayerHealthFront.width *= ((float)HP / (float)MaxHp);

            int Alpha = 255;
            DrawRectangleRec(MathMore.toRayRect(PlayerHealthBack), new Color(Color.DARKGRAY.r, Color.DARKGRAY.g, Color.DARKGRAY.b, Alpha));
            DrawRectangleRec(MathMore.toRayRect(PlayerHealthFront), new Color(Color.GREEN.r, Color.GREEN.g, Color.GREEN.b, Alpha));
            DrawRectangleLinesEx(MathMore.toRayRect(PlayerHealthBack), 1, new Color(Color.BLACK.r, Color.BLACK.g, Color.BLACK.b, Alpha));
        }

        public void HurtPlayer()
        {
            if(!BeenHit)
            {
                BeenHit = true;
                HP -= 1;
                HitTimer = 60;
            }
        }

        ////////////////
        public void GunUpdate()
        {
            //Rotate gun towards mouse
            GunRotation = Vector2.Direction(Position, MousePos);

            bool BulletShoot = IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON);
            float Speed = 5;

            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) BulletTimer = BulletTimerMax;

            BulletTimer += Game.deltaTime;

            //Shoot
            if (BulletTimer >= BulletTimerMax && BulletShoot)
            {
                BulletTimer = BulletTimer % BulletTimerMax;

                float dirCone = 0 + (((float)Math.PI / 32.0f) * BulletAmount);
                dirCone = Math.Min(dirCone, (float)Math.PI * 0.25f);
                for (int b = 0; b < BulletAmount; b++)
                {
                    PlayerBullet pb = new PlayerBullet(gameScene);

                    float newGunRotation = (GunRotation - (dirCone / 2) + (dirCone / (float)BulletAmount * b));
                    pb.Velocity = new Vector2((float)Math.Cos(newGunRotation), (float)Math.Sin(newGunRotation)) * Speed;

                    //Set pos to be partway through the barrel
                    pb.Position = Position + (new Vector2((float)Math.Cos(newGunRotation), (float)Math.Sin(newGunRotation)) * 6);

                    gameScene.PlayerBullets.Add(pb);
                }
            }
        }

        public void GunDraw()
        {
            Rectangle ImageRect = new Rectangle(0, 0, GunSprite.width, GunSprite.height); // Image rectangle (Whole Image)
            Rectangle DestRect = new Rectangle(Position.x, Position.y, ImageRect.width, ImageRect.height); // World Rect

            DrawTexturePro(GunSprite, MathMore.toRayRect(ImageRect), MathMore.toRayRect(DestRect), new Raylib.Vector2(GunOrigin.x, GunOrigin.y), GunRotation * RAD2DEG, Color.WHITE);

            DrawCircleLines((int)MousePos.x, (int)MousePos.y, 3, Color.BLACK);
        }
    }
}
