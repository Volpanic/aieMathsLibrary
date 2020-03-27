using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Project2D.TankGame;
using Raylib;
using static Raylib.Raylib;

namespace Project2D.Scenes.LevelEditorTools
{
    using MathClasses;
    public class CameraZoneTool : LevelEditorTool
    {
        //CamZones
        
        
        public Rectangle TempCamZone;
        public int SelectedCamZoneIndex = -1;
        public bool CreatingZone = false;

        public CameraZoneTool(LevelEditor _ls) : base(_ls)
        {
            Name = "Camera Zone Tool";
        }

        public override void Update()
        {
            //Selecting a CamZone
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                bool HasClickOnZone = false;
                for (int i = 0; i < levelScene.CamZones.Count; i++)
                {
                    Rectangle recCam = levelScene.CamZones[i];

                    if (i != SelectedCamZoneIndex && CheckCollisionPointRec(new Raylib.Vector2(levelScene.MousePos.x, levelScene.MousePos.y), recCam))
                    {
                        SelectedCamZoneIndex = i;
                        HasClickOnZone = true;
                        break;
                    }
                }

                //Nothing to select set to 0
                if (!HasClickOnZone)
                {
                    SelectedCamZoneIndex = -1;
                }
            }

            //Delete camzone
            if (IsKeyPressed(KeyboardKey.KEY_DELETE))
            {
                if (SelectedCamZoneIndex != -1)
                {
                    levelScene.CamZones.RemoveAt(SelectedCamZoneIndex);
                }
            }

            //Create new camzone
            if (IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                CreatingZone = true;
                if (IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
                {
                    SelectedCamZoneIndex = -1;
                    TempCamZone = new Rectangle((int)levelScene.GridMousePos.x * 16, (int)levelScene.GridMousePos.y * 16, 16, 16);
                }

                TempCamZone.width = Math.Max(16, ((int)levelScene.GridMousePos.x * 16) - TempCamZone.x);
                TempCamZone.height = Math.Max(16, ((int)levelScene.GridMousePos.y * 16) - TempCamZone.y);

            }

            if (SelectedCamZoneIndex == -1 && IsMouseButtonReleased(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                levelScene.CamZones.Add(TempCamZone);
                SelectedCamZoneIndex = levelScene.CamZones.Count - 1;
                CreatingZone = false;
            }
        }

        public override void Draw()
        {
            if (CreatingZone == true)
            {
                DrawRectangleRec(TempCamZone, new Color(128, 255, 128, 128));
                DrawRectangleLinesEx(TempCamZone, 2, new Color(128, 128, 128, 128));
            }
        }

        public override void AllDraw()
        {
            //CamZones
            for (int i = 0; i < levelScene.CamZones.Count; i++)
            {
                Rectangle recCam = levelScene.CamZones[i];

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
        }
    }
}
