using Project2D.TankGame;
using Raylib;
using System;
using static Raylib.Raylib;

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

            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                MenuTran = true;
                MenuTimer = 0;
                game.CurrentGameScene = new GameScene(game);
            }

            if (MenuTran)
            {
                switch (MenuPhase)
                {
                    case 0:
                    {
                        if (MenuTimer > 60)
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
            rl.Vector2 Measure = MeasureTextEx(Program.Romulus, "TNK", 36, 2);
            DrawTextEx(Program.Romulus, "TNK", new rl.Vector2(Program.GameWidth / 2 - (Measure.x / 2), Program.GameHeight / 3 - (Measure.y / 2)), 36, 2, Color.BLACK);

            rl.Vector2 Pos = new rl.Vector2(Program.GameWidth / 2, Program.GameHeight / 3 * 2);

            if (!MenuTran)
            {
                DrawRectangleRec(new rl.Rectangle(0, Pos.y - 8, Program.GameWidth, 16), Color.BLACK);

                if (MenuTimer % 20 > 5)
                {
                    Measure = MeasureTextEx(Program.Romulus, "Press Space", 12, 1);
                    DrawTextEx(Program.Romulus, "Press Space", new rl.Vector2(Pos.x - (Measure.x / 2), Pos.y - (Measure.y / 2)), 12, 1, Color.WHITE);
                }
            }
            else if (MenuTimer % 5 > 2)
            {
                DrawRectangleRec(new rl.Rectangle(0, Pos.y - 8, Program.GameWidth, 16), Color.BLACK);

                Measure = MeasureTextEx(Program.Romulus, "Press Space", 12, 1);
                DrawTextEx(Program.Romulus, "Press Space", new rl.Vector2(Pos.x - (Measure.x / 2), Pos.y - (Measure.y / 2)), 12, 1, Color.WHITE);
            }
        }
    }
}
