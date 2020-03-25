using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Project2D.TankGame;
using Raylib;
using static Raylib.Raylib;

namespace Project2D.Scenes
{
    using MathClasses;
    using Project2D.TankGame.Tiles;

    public class GameScene : Scene
    {
        public PlayerTank player;
        public List<PlayerBullet> PlayerBullets = new List<PlayerBullet>();
        public TileGrid tileGrid;

        public GameScene(Game _game) : base(_game)
        {
            player = new PlayerTank(this);
            player.Position = new Vector2(Program.GameWidth / 2, Program.GameHeight / 2);

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
        }

        public override void Update()
        {
            player.Update();
            camera.target = new Raylib.Vector2(player.Position.x,player.Position.y);

            //Roll through Player Bullets updates
            for(int i  = 0; i < PlayerBullets.Count; i++)
            {
                PlayerBullets[i].Update();

                //Destroy if need to.
                if(PlayerBullets[i].Active == false)
                {
                    PlayerBullets.RemoveAt(i);
                    i--;
                }
            }

            //Temp
            if(IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                Vector2 pos = player.MousePos;
                pos = new Vector2((float)Math.Floor(pos.x/16), (float)Math.Floor(pos.y / 16));

                tileGrid.TileGridValues[(int)pos.x,(int)pos.y] = 1;
            }
        }

        public override void Draw()
        {
            tileGrid.DrawTiles();

            player.Draw();

            //loop through Player Bullets Draw
            for (int i = 0; i < PlayerBullets.Count; i++)
            {
                PlayerBullets[i].Draw();
            }
        }
    }
}
