using Project2D.TankGame;
using Raylib;
using System;
using System.Collections.Generic;
using static Raylib.Raylib;

namespace Project2D.Scenes
{
    using MathClasses;
    using Project2D.TankGame.Boss;
    using Project2D.TankGame.Particles;
    using Project2D.TankGame.Tiles;

    public class GameScene : Scene
    {
        //Game Items
        public PlayerTank player;
        public TileGrid tileGrid;
        public DialougeBox dialougeBox;
        public Spindle spindleBoss;

        public List<PlayerBullet> PlayerBullets = new List<PlayerBullet>();
        public List<SpindleBullet> SpindleBullets = new List<SpindleBullet>();

        Random rand = new Random();
        public Texture2D SnowTexture;

        bool FirstFrame = false;

        public GameScene(Game _game) : base(_game)
        {
            //Set Player Pos
            player = new PlayerTank(this);
            player.Position = new Vector2(Program.GameWidth / 4, Program.GameHeight / 2);

            //Create Boss
            spindleBoss = new Spindle(this);

            //Setup Tile Grid
            int w = (int)Math.Ceiling((double)(Program.GameWidth / 16));
            int h = (int)Math.Ceiling((double)(Program.GameHeight / 16)) + 1;

            int[,] tempGrid = new int[w, h];

            for (int xx = 0; xx < tempGrid.GetLength(0); xx++)
            {
                for (int yy = 0; yy < tempGrid.GetLength(1); yy++)
                {
                    tempGrid[xx, yy] = 0;
                    //HoriWalls
                    if (xx == 0 || xx == tempGrid.GetLength(0) - 1)
                    {
                        tempGrid[xx, yy] = 1;
                    }

                    //VertWalls
                    if (yy == 0 || yy == tempGrid.GetLength(1) - 1)
                    {
                        tempGrid[xx, yy] = 1;
                    }


                }
            }

            tileGrid = new TileGrid(tempGrid, 16, 16);

            //Create Dialouge Box, but don't show it
            dialougeBox = new DialougeBox();
            dialougeBox.Visible = false;
           
        }

        public override void EndScene()
        {
            player.OnDestroy();
            spindleBoss.OnDestroy();
        }

        public override void Update()
        {
            if(!FirstFrame)
            {
                Console.Clear();
                Console.WriteLine("##~~--  Controls  --~~##");
                Console.WriteLine("Rotate Tank Right  ~ D");
                Console.WriteLine("Rotate Tank Left   ~ A");
                Console.WriteLine("Move Tank Forward  ~ A");
                Console.WriteLine("Mute Game Music    ~ M");
                Console.WriteLine("Slow Down Tank     ~ Shift");
                Console.WriteLine("Aim Tank Gun       ~ Mouse");
                Console.WriteLine("Shoot Tank Gun     ~ Left Mouse");
                FirstFrame = true;
            }

            //Update everything unless dialouge box is showing
            if (!dialougeBox.Visible)
            {
                player.Update();

                if(spindleBoss.Active) spindleBoss.Update();

                //Roll through Player Bullets updates
                for (int i = 0; i < PlayerBullets.Count; i++)
                {
                    PlayerBullets[i].Update();

                    //Hurt Boss
                    if (spindleBoss.Active && PlayerBullets[i].GetCollisionRectangle().CollidingWith(spindleBoss.GetCollisionRectangle()))
                    {
                        spindleBoss.SpindleHit();
                        PlayerBullets[i].Active = false;
                    }

                    //Destroy if need to.
                    if (PlayerBullets[i].Active == false)
                    {
                        PlayerBullets.RemoveAt(i);
                        i--;
                    }
                }

                //Loop spindle bullets (Boss Bullets)
                for (int i = 0; i < SpindleBullets.Count; i++)
                {
                   
                    SpindleBullets[i].Update();

                    //Hurt Boss
                    if (SpindleBullets[i].GetCollisionRectangle().CollidingWith(player.GetCollisionRectangle()))
                    {
                        //spindleBoss.SpindleHit();
                        SpindleBullets[i].Active = false;
                        player.HurtPlayer();
                    }

                    //Destroy if need to.
                    if (SpindleBullets[i].Active == false)
                    {
                        SpindleBullets.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {

            }
        }

        public override void Draw()
        {
            //Draw Paticles
            partSystem.Draw();

            //loop through Player Bullets Draw
            for (int i = 0; i < PlayerBullets.Count; i++)
            {
                PlayerBullets[i].Draw();
            }

            for (int i = 0; i < SpindleBullets.Count; i++)
            {
                SpindleBullets[i].Draw();
            }

            tileGrid.DrawTiles();

            if (spindleBoss.Active) spindleBoss.Draw();
            player.Draw();

            dialougeBox.DrawDialougeBox();
        }
    }
}
