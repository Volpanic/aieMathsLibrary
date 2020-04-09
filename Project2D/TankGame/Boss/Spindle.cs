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
        public Spindle(GameScene _gameScene) : base(_gameScene)
        {
            BulletSprite = LoadTexture(Path.Combine("Resources","Sprites","spr_spin_bullet.png"));
        }

        public override void Create()
        {
            Sprite = LoadTexture(Path.Combine("Resources", "Sprites", "spr_spindle.png"));
            Dimensions = new MathClasses.Vector2(Sprite.width,Sprite.height);
            Origin = Dimensions / 2;
            Position = new MathClasses.Vector2(Program.GameWidth / 2, Program.GameHeight / 3);
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
        public void Attacks()
        {
            Timer += Game.deltaTime;

            switch(AttackPhase)
            {
                case 0:
                {
                    Rotation += (float)(Math.PI/2) * SpinMulti;
                    SpinMulti = Lerp(SpinMulti,5,0.08f * Game.deltaTime);

                    if(SpinMulti >= 4.9f)
                    {
                        Timer = 0;
                        AttackPhase = 1;
                        SpinMulti = 5;
                    }
                    break;
                }

                case 1:
                {
                    Rotation += (float)(Math.PI / 2);
                    Shoot4Way(2);
                    break;
                }
            }
        }

        public void Shoot4Way(float Speed)
        {
            MathClasses.Vector2 vel = new MathClasses.Vector2((float)Math.Cos(Rotation * RAD2DEG),(float)Math.Sin(Rotation * RAD2DEG));
            MathClasses.Vector2 pos = new MathClasses.Vector2(Position.x + (Origin.x * vel.x), Position.y + (Origin.y * vel.y));

            SpindleBullet sb1 = new SpindleBullet(gameScene,BulletSprite);
            sb1.Velocity = vel*Speed;
            sb1.Position = pos;

            vel = new MathClasses.Vector2((float)Math.Cos(Rotation + (Math.PI/2)), (float)Math.Sin(Rotation + (Math.PI / 2)));
            pos = new MathClasses.Vector2(Position.x + (Origin.x * vel.x), Position.y + (Origin.y * vel.y));
            SpindleBullet sb2 = new SpindleBullet(gameScene, BulletSprite);
            sb2.Velocity = vel * Speed;
            sb2.Position = pos;

            gameScene.SpindleBullets.Add(sb1);
            gameScene.SpindleBullets.Add(sb2);
        }

        public override void Draw()
        {
            DrawSelf();
        }

        public override void OnDestroy()
        {
            UnloadTexture(Sprite);
        }

        public void SpindleHit()
        {
            if(State == 0)
            {
                gameScene.dialougeBox.Visible = true;
                gameScene.dialougeBox.Messages.Add("Did /2you/0 just /1Seriously/0 shoot me!");
                gameScene.dialougeBox.Messages.Add("I was literally just sleeping.../3I mean like what the hell/0.");
                gameScene.dialougeBox.Messages.Add("/3I have to live here /1forever/3 in a void with a bloody /2tank/3, just my luck/0.");
                gameScene.dialougeBox.Messages.Add("You know what, /1 Screw this/0!");
                State = 1;
            }
            else
            {
                //hp -= 1;
            }
        }
    }
}
