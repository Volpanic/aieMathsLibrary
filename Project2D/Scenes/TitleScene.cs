using System;
using System.Collections.Generic;
using System.Linq;
using Raylib;
using static Raylib.Raylib;


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
                game.CurrentGameScene = new TitleScene(game);
            }

            if(MenuTran)
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

        public override void Draw()
        {
            MenuTimer++;

            switch(MenuPhase)
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
            Vector2 Measure = MeasureTextEx(Program.Romulus, "TNK(s)", 36, 2);
            DrawTextEx(Program.Romulus, "TNK(s)", new Vector2(Program.GameWidth / 2 - (Measure.x / 2), Program.GameHeight / 3 - (Measure.y / 2)), 36, 2, Color.BLACK);

            Vector2 Pos = new Vector2(Program.GameWidth / 2, Program.GameHeight / 3 * 2);

            if (!MenuTran)
            {
                DrawRectangleRec(new Rectangle(0, Pos.y - 8, Program.GameWidth, 16), Color.BLACK);

                if (MenuTimer % 20 > 5)
                {
                    Measure = MeasureTextEx(Program.Romulus, "Press Z", 12, 1);
                    DrawTextEx(Program.Romulus, "Press Z", new Vector2(Pos.x - (Measure.x / 2), Pos.y - (Measure.y / 2)), 12, 1, Color.WHITE);
                }
            }
            else if (MenuTimer % 5 > 2)
            {
                DrawRectangleRec(new Rectangle(0, Pos.y - 8, Program.GameWidth, 16), Color.BLACK);

                Measure = MeasureTextEx(Program.Romulus, "Press Z", 12, 1);
                DrawTextEx(Program.Romulus, "Press Z", new Vector2(Pos.x - (Measure.x / 2), Pos.y - (Measure.y / 2)), 12, 1, Color.WHITE);
            }
        }
    }
}
