using MathClasses;
using Project2D.Scenes;
using Raylib;
using System;
using System.IO;
using static Raylib.Raylib;
using rl = Raylib;

namespace Project2D.TankGame.Boss
{
    public class Spindle : Component
    {
        public int State = 0;
        public bool CanHurtPlayer = false;
        public Texture2D BulletSprite;
        Random rand = new Random();

        //Stats
        bool Rage = false;
        public int MaxHp = 300;
        public int HP = 300;


        public Spindle(GameScene _gameScene) : base(_gameScene)
        {
            //Load Texture for bullet
            BulletSprite = Sprites.GetSprite("spr_spin_bullet");
        }

        public override void Create()
        {
            //Load Sprites
            Sprite = Sprites.GetSprite("spr_spindle");
            Dimensions = new MathClasses.Vector2(Sprite.width, Sprite.height);
            Origin = Dimensions / 2;
            Position = new MathClasses.Vector2(Program.GameWidth / 2, Program.GameHeight / 2);
        }

        public override void Update()
        {
            switch (State)
            {
                case 0: // Passive
                {

                    break;
                }

                case 1: // Boss
                {
                    Attacks();
                    break;
                }

                case 2: // Death
                {
                    Timer += Game.deltaTime;

                    Rotation += Timer / 5.0f;

                    if(Timer >=  120)
                    {
                        Scale.x = Lerp(Scale.x,0,0.08f);
                        Scale.y = Lerp(Scale.y, 0, 0.08f);

                        if(Scale.x <= 0.1f)
                        {
                            gameScene.dialougeBox.Visible = true;
                            gameScene.dialougeBox.Messages.Add("You won, but i didn't make anything happen other than this.");
                            Active = false;
                        }
                    }

                    break;
                }
            }
        }

        //Attack Variables
        public float Timer = 0;
        public int AttackPhase = 0;
        public float SpinMulti = -2;
        public int SubTimer = 0;

        public void Attacks()
        {
            Timer += Game.deltaTime;

            switch (AttackPhase)
            {
                case 0: // Spin up, Only happens once at start of battle
                {
                    Rotation += (float)(Math.PI / 2) * SpinMulti * Game.deltaTime;
                    SpinMulti = Lerp(SpinMulti, 5, 0.08f * Game.deltaTime);

                    if (SpinMulti >= 4.9f)
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
                    if (Timer > (Rage? 10 : 15))
                    {
                        Shoot4Way(1);
                        Timer = 0;
                        SubTimer++;
                    }

                    if (SubTimer >= 16)
                    {
                        ChangePhase();
                    }

                    break;
                }

                case 2: // ShotGun
                {
                    Rotation = Lerp(Rotation, MathClasses.Vector2.Direction(Position, gameScene.player.Position) * RAD2DEG, 0.08f);
                    if (Timer > (Rage ? 30 : 60))
                    {
                        for (int i = 0; i < rand.Next(6, 12); i++)
                        {
                            ShootBullet(Position, Rotation + rand.Next(-8, 8),(float)(rand.NextDouble() + rand.Next(2, 3)));
                        }
                        gameScene.game.ScreenShake(10,2);
                        Timer = 0;
                        SubTimer++;
                    }

                    if (SubTimer >= 8)
                    {
                        ChangePhase();
                    }

                    break;
                }

                case 3: // SinWaveMove
                {
                    Rotation = Numbers.SinWave(-15, 15, 1, 0, Timer + (SubTimer * 15));
                    if (Timer > (Rage ? 4 : 8))
                    {
                        Shoot4Way(1f);
                        Timer = 0;
                        SubTimer++;
                    }

                    if (SubTimer >= 16)
                    {
                        ChangePhase();
                    }

                    break;
                }

                case 4: //Lazer
                {
                    if (SubTimer == 0) //Roate back to 0
                    {
                        Rotation = Numbers.Approach(Rotation, 0, Timer / 10);
                        if (Rotation == 0)
                        {
                            ShootBullet(Position, Rotation, 2);

                            if(Rage)
                            {
                                ShootBullet(Position, Rotation + 180, 2);
                            }

                            SubTimer = 1;
                            Timer = 0;
                        }
                    }

                    if (SubTimer == 1) // Wait a second
                    {
                        if (Timer >= 60)
                        {
                            Timer = 0;
                            SubTimer = 2;
                        }
                    }

                    if (SubTimer == 2) // Shoot stream
                    {
                        Rotation += (float)Math.PI + (Timer / 10); // It's in degrees, just pies a nice number i guess

                        if(Timer % 10 > 0 && Timer % 10 < 3)
                        {
                            ShootBullet(Position, Rotation, 1);

                            if (Rage)
                            {
                                ShootBullet(Position, Rotation + 180, 1);
                            }
                        }

                        if (Timer > 240)
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
            ShootBullet(Position, Rotation, Speed);
            ShootBullet(Position, Rotation + 90, Speed);
            ShootBullet(Position, Rotation + 180, Speed);
            ShootBullet(Position, Rotation + 270, Speed);
        }

        //Shoot a bullet
        public void ShootBullet(MathClasses.Vector2 NewPosition, float DegreeRotation,float Speed)
        {
            SpindleBullet sb = new SpindleBullet(gameScene, BulletSprite);
            sb.Position = NewPosition;
            sb.Velocity = new MathClasses.Vector2((float)Math.Cos((DegreeRotation ) * DEG2RAD), (float)Math.Sin((DegreeRotation) * DEG2RAD)) * Speed;
            sb.Rotation = DegreeRotation;
            gameScene.SpindleBullets.Add(sb);
        }

        public override void Draw()
        {
            DrawSelf();

            //BossHealthBar
            rl.Rectangle BossHealthBack = new rl.Rectangle(8, Program.GameHeight * 0.9f, Program.GameWidth - 16, Program.GameHeight * 0.05f);
            rl.Rectangle BossHealthFront = BossHealthBack;
            BossHealthFront.width *= ((float)HP / (float)MaxHp);

            //Make bar transparent when near bar
            int Alpha = 255;
            if (gameScene.player.Position.y > Program.GameHeight * 0.8)
            {
                Alpha = 64;
            }

            DrawRectangleRec(BossHealthBack, new Color(Color.DARKGRAY.r, Color.DARKGRAY.g, Color.DARKGRAY.b,Alpha));
            DrawRectangleRec(BossHealthFront, new Color(Color.RED.r, Color.RED.g, Color.RED.b, Alpha));
            DrawRectangleLinesEx(BossHealthBack, 1, new Color(Color.BLACK.r, Color.BLACK.g, Color.BLACK.b, Alpha));
        }

        public void ChangePhase()
        {
            SubTimer = 0;
            Timer = 0;
            int OldPhase = AttackPhase;

            while (OldPhase == AttackPhase)
            {
                AttackPhase = rand.Next(1, 5);
            }
        }

        public void SpindleHit()
        {
            if (State == 0)
            {
                gameScene.dialougeBox.Visible = true;
                gameScene.dialougeBox.Messages.Add("BOSS PROTOCOL BEGIN/0.");
                State = 1;
            }
            else if(State == 1)
            {
                HP -= 1;

                if(HP < (MaxHp/2))
                {
                    Rage = true;
                }

                if (HP <= 0)
                {
                    Timer = 0;
                    SubTimer = 0;
                    State = 2;
                }
            }
        }
    }
}
