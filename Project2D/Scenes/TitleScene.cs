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


namespace Project2D.Scenes
{
    public class TitleScene : Scene
    {
        public int MenuPos = 0;
        public int MenuPhase = 0;

        public int MenuTimer = 0;
        public bool MenuTran = false;

        //Raylibs not really meant to use a vector class other than it's own
        //For the menu i'm using the base vector2 class but ill use my library for the player...

        public TitleScene(Game _game) : base(_game)
        {
           
        }

        public override void Update()
        {
            base.Update();

            if(IsKeyPressed(KeyboardKey.KEY_Z))
            {
                MenuTran = true;
                MenuTimer = 0;
                game.CurrentGameScene = new GameScene(game);
            }

            if (MenuTran)
            {
                switch(MenuPhase)
                {
                    case 0:
                    {
                        if(MenuTimer > 60)
                        {

                        }
                    break;
                    }
                }
            }
        }

        MathClasses.Rectangle r1 = new MathClasses.Rectangle(16,16,16,16);
        MathClasses.Rectangle r2 = new MathClasses.Rectangle(64, 64, 16, 16);

        public override void Draw()
        {
            MenuTimer++;

            r2.x = PlayerTank.GetRelativeMousePosition().x;
            r2.y = PlayerTank.GetRelativeMousePosition().y;

            DrawRectangleLinesEx(new rl.Rectangle(r1.x, r1.y, r1.width, r1.height), 2, Color.BLACK);
            DrawRectangleLinesEx(new rl.Rectangle(r2.x, r2.y, r2.width, r2.height), 2, r1.CollidingWith(r2)? Color.GREEN : Color.RED);

            switch (MenuPhase)
            {
                case 0:
                {
                    PressStartButtonMenuPhase();
                    break;
                }
            }
        }

        public void PressStartButtonMenuPhase()
        {
            //Center Text
            rl.Vector2 Measure = MeasureTextEx(Program.Romulus, "TNK(s)", 36, 2);
            DrawTextEx(Program.Romulus, "TNK(s)", new rl.Vector2(Program.GameWidth / 2 - (Measure.x / 2), Program.GameHeight / 3 - (Measure.y / 2)), 36, 2, Color.BLACK);

            rl.Vector2 Pos = new rl.Vector2(Program.GameWidth / 2, Program.GameHeight / 3 * 2);

            if (!MenuTran)
            {
                DrawRectangleRec(new rl.Rectangle(0, Pos.y - 8, Program.GameWidth, 16), Color.BLACK);

                if (MenuTimer % 20 > 5)
                {
                    Measure = MeasureTextEx(Program.Romulus, "Press Z", 12, 1);
                    DrawTextEx(Program.Romulus, "Press Z", new rl.Vector2(Pos.x - (Measure.x / 2), Pos.y - (Measure.y / 2)), 12, 1, Color.WHITE);
                }
            }
            else if (MenuTimer % 5 > 2)
            {
                DrawRectangleRec(new rl.Rectangle(0, Pos.y - 8, Program.GameWidth, 16), Color.BLACK);

                Measure = MeasureTextEx(Program.Romulus, "Press Z", 12, 1);
                DrawTextEx(Program.Romulus, "Press Z", new rl.Vector2(Pos.x - (Measure.x / 2), Pos.y - (Measure.y / 2)), 12, 1, Color.WHITE);
            }
        }
    }
}
