using MathClasses;
using Project2D.TankGame;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class Program
    {
        //Resolution Settings
        public const int GameWidth = 320;
        public const int GameHeight = 180;
        public static int GameZoom = 4; // Change for smaller or bigger window

        public static Font Romulus;

        static void Main(string[] args)
        {
            Game game = new Game();

            InitWindow(GameWidth * GameZoom, GameHeight * GameZoom, "...");
            SetTargetFPS(60);

            //Creates a base surface to draw the game too, Scaled up later
            RenderTexture2D AppSurface = LoadRenderTexture(GameWidth, GameHeight);
            SetTextureFilter(AppSurface.texture, TextureFilterMode.FILTER_POINT);

            //load
            Romulus = LoadFont("resources/fonts/romulus.png");
            InitAudioDevice();
            game.Init();

            while (!WindowShouldClose())
            {

                game.Update();

                //Draw Scaled Window
                BeginDrawing();

                BeginTextureMode(AppSurface);

                ClearBackground(MathMore.toRayColour(new Colour(255, 255, 255, 1)));
                ClearBackground(Color.WHITE);

                game.Draw();

                EndTextureMode();
                //Draw Scaled Window
                DrawRenderTexture(AppSurface);
                EndMode2D();

                EndDrawing();
            }

            //unload
            UnloadFont(Romulus);
            CloseAudioDevice();
            game.Shutdown();

            CloseWindow();
        }
    }
}
