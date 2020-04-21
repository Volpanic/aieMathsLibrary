using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Raylib.Raylib;
using MathClasses;
using Project2D.Scenes;
using Raylib;
using Project2D.TankGame;
using System.Diagnostics;

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
        

        public GameScene(Game _game) : base(_game)
        {
            player = new PlayerTank(this);
            player.Position = new Vector2(Program.GameWidth / 4, Program.GameHeight / 2);

            spindleBoss = new Spindle(this);

            //SetupTileGrid

            int w = (int)Math.Ceiling((double)(Program.GameWidth / 16));
            int h = (int)Math.Ceiling((double)(Program.GameHeight / 16))+1;

            int[,] tempGrid = new int[w, h];

            for (int xx = 0; xx < tempGrid.GetLength(0); xx++)
            {
                for (int yy = 0; yy < tempGrid.GetLength(1); yy++)
                {
                    tempGrid[xx, yy] = 0;
                    //HoriWalls
                    if(xx == 0 || xx == tempGrid.GetLength(0)-1)
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

            tileGrid = new TileGrid(tempGrid,16,16);
            dialougeBox = new DialougeBox();
            dialougeBox.Visible = false;
            //SnowTexture = LoadTexture(Path.Combine("Resources", "Sprites", "spr_snow.png"));
        }

        public override void Update()
        {
            if (!dialougeBox.Visible)
            {
                player.Update();
                spindleBoss.Update();

                //Roll through Player Bullets updates
                for (int i = 0; i < PlayerBullets.Count; i++)
                {
                    PlayerBullets[i].Update();

                    //Hurt Boss
                    if(PlayerBullets[i].GetCollisionRectangle().CollidingWith(spindleBoss.GetCollisionRectangle()))
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

                for (int i = 0; i < SpindleBullets.Count; i++)
                {
                    SpindleBullets[i].Update();

                    //Hurt Boss
                    if (SpindleBullets[i].GetCollisionRectangle().CollidingWith(player.GetCollisionRectangle()))
                    {
                        //spindleBoss.SpindleHit();
                        SpindleBullets[i].Active = false;
                    }

                    //Destroy if need to.
                    if (SpindleBullets[i].Active == false)
                    {
                        SpindleBullets.RemoveAt(i);
                        i--;
                    }
                }

                //Temp
                if (IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
                {
                    Vector2 pos = player.MousePos;
                    pos = new Vector2((float)Math.Floor(pos.x / 16), (float)Math.Floor(pos.y / 16));

                    tileGrid.TileGridValues[(int)pos.x, (int)pos.y] = 1;
                }

                //CreateSnow
                for (int i = 0; i < rand.Next(2, 5); i++)
                {
                    partSystem.PartList.Add(new Particle(this, SnowTexture, new Vector2(rand.Next(0, Program.GameWidth), -4), new Vector2(-1, 2), 0.0f, true));
                }
            }
            else
            {
            
            }
        }

        public override void Draw()
        {
            
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

            
            player.Draw();

            tileGrid.DrawTiles();

            spindleBoss.Draw();

            

            dialougeBox.DrawDialougeBox();
        }
    }
}
