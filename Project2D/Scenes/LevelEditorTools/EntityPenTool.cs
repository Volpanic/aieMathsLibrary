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
    public class EntityPenTool : LevelEditorTool
    {
        public EntityPenTool(LevelEditor _ls) : base(_ls)
        {
        }

        public override void Draw()
        {
            DrawRectangleLinesEx(new Rectangle((int)levelScene.GridMousePos.x * 16, (int)levelScene.GridMousePos.y * 16, 16, 16), 2, new Color(0, 0, 0, 128));
        }

        public override void Update()
        {
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                Component newCom = new PlayerTank(null);

                newCom.Position = new Vector2((int)levelScene.GridMousePos.x, (int)levelScene.GridMousePos.y) * 16;

                levelScene.componentList.Add(newCom);
            }
        }

        public override void AllDraw()
        {
            foreach (Component com in levelScene.componentList)
            {
                com.InEditor();
            }
        }
    }
}
