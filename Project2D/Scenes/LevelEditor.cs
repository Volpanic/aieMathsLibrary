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

    public class LevelEditor : Scene
    {
        public int ToolSelected = 1; // Draw - CameraZone - Settings

        //CamZones
        public TileGrid tileGrid;
        public List<Rectangle> CamZones = new List<Rectangle>();
        public Rectangle TempCamZone;
        public int SelectedCamZoneIndex = -1;
        public bool CreatingZone = false;

        //Mouse
        public Vector2 MousePos { get { return new Vector2(GetMousePosition().x, GetMousePosition().y) / Program.GameZoom; } }
        public Vector2 GridMousePos { get { return (new Vector2(GetMousePosition().x, GetMousePosition().y) / Program.GameZoom)/16; } }

        public LevelEditor(Game _game) : base(_game)
        {
            //Fill grid empty
            int w = (int)Math.Ceiling((double)(Program.GameWidth / 16));
            int h = (int)Math.Ceiling((double)(Program.GameHeight / 16)) + 1;

            int[,] tempGrid = new int[w, h];

            for (int xx = 0; xx < tempGrid.GetLength(0); xx++)
            {
                for (int yy = 0; yy < tempGrid.GetLength(1); yy++)
                {
                    tempGrid[xx, yy] = 0;
                }
            }

            tileGrid = new TileGrid(tempGrid, 16, 16);
        }

        public override void Update()
        {
            if(IsKeyPressed(KeyboardKey.KEY_ONE))
            {
                ToolSelected = 0;
            }

            if (IsKeyPressed(KeyboardKey.KEY_TWO))
            {
                ToolSelected = 1;
            }


            switch (ToolSelected)
            {
                case 0: // Draw
                {
                    if(IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        tileGrid.TileGridValues[(int)GridMousePos.x, (int)GridMousePos.y] = 1;
                    }

                    if (IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
                    {
                        tileGrid.TileGridValues[(int)GridMousePos.x, (int)GridMousePos.y] = 0;
                    }

                    break;
                }

                case 1: // CamZones
                {
                    //Selecting a CamZone
                    if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        bool HasClickOnZone = false;
                        for(int i = 0; i < CamZones.Count; i++)
                        {
                            Rectangle recCam = CamZones[i];

                            if (i != SelectedCamZoneIndex && CheckCollisionPointRec(new Raylib.Vector2(MousePos.x, MousePos.y), recCam))
                            {
                                SelectedCamZoneIndex = i;
                                HasClickOnZone = true;
                                break;
                            }
                        }

                        //Nothing to select set to 0
                        if(!HasClickOnZone)
                        {
                            SelectedCamZoneIndex = -1;
                        }
                    }

                    //Delete camzone
                    if(IsKeyPressed(KeyboardKey.KEY_DELETE))
                    {
                        if(SelectedCamZoneIndex != -1)
                        {
                            CamZones.RemoveAt(SelectedCamZoneIndex);
                        }
                    }

                    //Create new camzone
                    if (IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
                    {
                        CreatingZone = true;
                        if (IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
                        {
                            SelectedCamZoneIndex = -1;
                            TempCamZone = new Rectangle((int)GridMousePos.x*16,(int) GridMousePos.y*16, 16, 16);
                        }

                        TempCamZone.width = Math.Max(16,((int)GridMousePos.x * 16) - TempCamZone.x);
                        TempCamZone.height = Math.Max(16,((int)GridMousePos.y * 16) - TempCamZone.y);

                    }
                    
                    if(SelectedCamZoneIndex == -1 && IsMouseButtonReleased(MouseButton.MOUSE_RIGHT_BUTTON))
                    {
                        CamZones.Add(TempCamZone);
                        SelectedCamZoneIndex = CamZones.Count - 1;
                        CreatingZone = false;
                    }

                    break;
                }
            }
        }

        public override void Draw()
        {
            //Draw 2d Grid
            for (int xx = 0; xx < tileGrid.TileGridValues.GetLength(0); xx++)
            {
                for (int yy = 0; yy < tileGrid.TileGridValues.GetLength(1); yy++)
                {
                    DrawRectangleLinesEx(new Rectangle(xx * 16, yy * 16, 16, 16), 1, Color.LIGHTGRAY);
                }
            }

            switch (ToolSelected)
            {
                case 0: // Draw
                {
                    DrawRectangleLines((int)GridMousePos.x * 16,(int)GridMousePos.y * 16,16,16,new Color(0,0,0,128));

                    break;
                }

                case 1: // Draw
                {
                    if (CreatingZone == true)
                    {
                        DrawRectangleRec(TempCamZone, new Color(128, 128, 128, 128));
                        DrawRectangleLinesEx(TempCamZone, 2, new Color(128, 128, 128, 128));
                    }

                    break;
                }
            }

            //CamZones
            for (int i = 0; i < CamZones.Count; i++)
            {
                Rectangle recCam = CamZones[i];

                if (SelectedCamZoneIndex != -1 && i == SelectedCamZoneIndex)
                {
                    DrawRectangleRec(recCam, new Color(16, 255, 16, 64));
                    DrawRectangleLinesEx(recCam, 2, new Color(32, 32, 32, 128));
                }
                else
                {
                    DrawRectangleRec(recCam, new Color(16, 16, 16, 64));
                    DrawRectangleLinesEx(recCam, 2, new Color(32, 32, 32, 128));
                }
            }

            tileGrid.DrawTiles();
        }
    }
}
