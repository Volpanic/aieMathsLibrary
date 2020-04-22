using Raylib;
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

            if (IsKeyPressed(KeyboardKey.KEY_Z))
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

        MathClasses.Colour red = new MathClasses.Colour(255, 0,0xff, 0);
        bool done = false;
        public override void Draw()
        {
            MenuTimer++;

            if (!done)
            {
                done = true;
                red.RedToGreen();
                red.SetRed(55);
            }
            DrawText($"0x{red.colour:X}",16,16,12,rl.Color.BLACK);

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
