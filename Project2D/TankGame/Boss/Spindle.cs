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

namespace Project2D.TankGame.Boss
{
    public class Spindle : Component
    {

        public int State = 0;
        public bool CanHurtPlayer = false;
        public Texture2D BulletSprite;
        Random rand = new Random();

        bool Rage = false;
        public int MaxHp = 500;
        public int HP = 500;


        public Spindle(GameScene _gameScene) : base(_gameScene)
        {
            BulletSprite = LoadTexture(Path.Combine("Resources","Sprites","spr_spin_bullet.png"));
        }

        public override void Create()
        {
            Sprite = LoadTexture(Path.Combine("Resources", "Sprites", "spr_spindle.png"));
            Dimensions = new MathClasses.Vector2(Sprite.width,Sprite.height);
            Origin = Dimensions / 2;
            Position = new MathClasses.Vector2(Program.GameWidth / 2, Program.GameHeight / 2);
        }

        public override void Update()
        {

            switch(State)
            {
                case 0:
                {
                    
                    break;
                }

                case 1:
                {
                    Attacks();
                    break;
                }
            }
        }

        public float Timer = 0;
        public int AttackPhase = 0;
        public float SpinMulti = -2;
        public int SubTimer = 0;
        public void Attacks()
        {
            Timer += Game.deltaTime;

            switch(AttackPhase)
            {
                case 0:
                {
                    Rotation += (float)(Math.PI/2) * SpinMulti * Game.deltaTime;
                    SpinMulti = Lerp(SpinMulti,5,0.08f * Game.deltaTime);

                    if(SpinMulti >= 4.9f)
                    {
                        Timer = 0;
                        AttackPhase = 1;
                        SpinMulti = 5;
                    }
                    break;
                }

                case 1: // 4 Way Shot 
                {
                    Rotation += (float)(Math.PI * 1f) * Game.deltaTime;
                    if (Timer > 15)
                    {
                        Shoot4Way(1);
                        Timer = 0;
                        SubTimer++;
                    }

                    if(SubTimer >= 16)
                    {
                        ChangePhase();
                    }

                    break;
                }

                case 2: // ShotGun
                {
                    Rotation = Lerp(Rotation,MathClasses.Vector2.Direction(Position,gameScene.player.Position) * RAD2DEG,0.08f);
                    if (Timer > 60)
                    {
                        for(int i = 0; i < rand.Next(6,12); i++)
                        {
                            SpindleBullet sb = new SpindleBullet(gameScene, BulletSprite);
                            sb.Position = Position;
                            sb.Velocity = new MathClasses.Vector2((float)Math.Cos((Rotation + rand.Next(-15,15)) * DEG2RAD), (float)Math.Sin((Rotation + rand.Next(-15, 15))  * DEG2RAD)) * (float)(rand.NextDouble() + rand.Next(2, 5));
                            gameScene.SpindleBullets.Add(sb);
                        }
                        Timer = 0;
                        SubTimer++;
                    }

                    if (SubTimer >= 8)
                    {
                        ChangePhase();
                    }

                    break;
                }

                case 3: // SIn
                {
                    Rotation = Numbers.SinWave(-45,45,2,0,Timer + (SubTimer * 15));
                    if (Timer > 15)
                    {
                        Shoot4Way(1);
                        Timer = 0;
                        SubTimer++;
                    }

                    if (SubTimer >= 8)
                    {
                        ChangePhase();
                    }

                    break;
                }

                case 4: //Lazer
                {
                    if (SubTimer == 0)
                    {
                        Rotation = Numbers.Approach(Rotation, 0, Timer / 10);
                        if(Rotation == 0)
                        {
                            SpindleBullet sb = new SpindleBullet(gameScene, BulletSprite);
                            sb.Position = Position;
                            sb.Velocity = new MathClasses.Vector2((float)Math.Cos((Rotation) * DEG2RAD), (float)Math.Sin((Rotation) * DEG2RAD));
                            gameScene.SpindleBullets.Add(sb);

                            SubTimer = 1;
                            Timer = 0;
                        }
                    }

                    if (SubTimer == 1)
                    {
                        if (Timer >= 60)
                        {
                            Timer = 0;
                            SubTimer = 2;
                        }
                    }

                    if (SubTimer == 2)
                    {
                        Rotation += (float)Math.PI / 1.5f;

                        SpindleBullet sb = new SpindleBullet(gameScene, BulletSprite);
                        sb.Position = Position;
                        sb.Velocity = new MathClasses.Vector2((float)Math.Cos((Rotation) * DEG2RAD), (float)Math.Sin((Rotation) * DEG2RAD)) * 2;
                        gameScene.SpindleBullets.Add(sb);

                        if(Timer > 240)
                        {
                            ChangePhase();
                        }

                    }


                    break;
                }
            }
        }

        public void Shoot4Way(float Speed)
        {
            //MathClasses.Vector2 vel = new MathClasses.Vector2((float)Math.Cos((float)Rotation),(float)Math.Sin((float)Rotation))*2.0f;

            SpindleBullet sb = new SpindleBullet(gameScene,BulletSprite);
            sb.Position = Position;
            sb.Velocity = new MathClasses.Vector2((float)Math.Cos(Rotation * DEG2RAD), (float)Math.Sin(Rotation * DEG2RAD)) * Speed;

            SpindleBullet sb1 = new SpindleBullet(gameScene, BulletSprite);
            sb1.Position = Position;
            sb1.Velocity = new MathClasses.Vector2((float)Math.Cos((Rotation + 180) * DEG2RAD), (float)Math.Sin((Rotation+180) * DEG2RAD)) * Speed;

            SpindleBullet sb2 = new SpindleBullet(gameScene, BulletSprite);
            sb2.Position = Position;
            sb2.Velocity = new MathClasses.Vector2((float)Math.Cos((Rotation + 270) * DEG2RAD), (float)Math.Sin((Rotation + 270) * DEG2RAD)) * Speed;

            SpindleBullet sb3 = new SpindleBullet(gameScene, BulletSprite);
            sb3.Position = Position;
            sb3.Velocity = new MathClasses.Vector2((float)Math.Cos((Rotation + 90) * DEG2RAD), (float)Math.Sin((Rotation + 90) * DEG2RAD)) * Speed;

            gameScene.SpindleBullets.Add(sb);
            gameScene.SpindleBullets.Add(sb1);
            gameScene.SpindleBullets.Add(sb2);
            gameScene.SpindleBullets.Add(sb3);
        }

        public override void Draw()
        {
            DrawSelf();

            //BossHealthBar
            rl.Rectangle BossHealthBack = new rl.Rectangle(8,Program.GameHeight * 0.9f,Program.GameWidth - 16, Program.GameHeight * 0.05f);
            rl.Rectangle BossHealthFront = BossHealthBack;
            BossHealthFront.width *= ((float)HP / (float)MaxHp);

            DrawRectangleRec(BossHealthBack, Color.DARKGRAY);
            DrawRectangleRec(BossHealthFront, Color.RED);
            DrawRectangleLinesEx(BossHealthBack, 1, Color.BLACK);
        }

        public override void OnDestroy()
        {
            UnloadTexture(Sprite);
        }

        public void ChangePhase()
        {
            SubTimer = 0;
            Timer = 0;
            int OldPhase = AttackPhase;

            while(OldPhase == AttackPhase)
            {
                AttackPhase = rand.Next(1,5);
            }
        }

        public void SpindleHit()
        {
            if(State == 0)
            {
                gameScene.dialougeBox.Visible = true;
                gameScene.dialougeBox.Messages.Add("/BOSS PROTOCOL BEGIN/0.");
                State = 1;
            }
            else
            {
                HP -= 1;
            }
        }
    }
}
