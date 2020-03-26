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
    public class TilePen : LevelEditorTool
    {
        public TilePen(LevelEditor _ls) : base(_ls)
        {
            Name = "Tile Pen";
        }

        public override void Update()
        {
            if (IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                levelScene.tileGrid.TileGridValues[(int)levelScene.GridMousePos.x, (int)levelScene.GridMousePos.y] = 1;
            }

            if (IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                levelScene.tileGrid.TileGridValues[(int)levelScene.GridMousePos.x, (int)levelScene.GridMousePos.y] = 0;
            }
        }

        public override void Draw()
        {
            DrawRectangleLinesEx(new Rectangle((int)levelScene.GridMousePos.x * 16, (int)levelScene.GridMousePos.y * 16, 16, 16), 2, new Color(0, 0, 0, 128));
        }
    }
}
