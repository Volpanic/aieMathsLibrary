using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class Program
    {
        public const int GameWidth = 320;
        public const int GameHeight = 180;
        public static int GameZoom = 4;

        public static Camera2D camera;

        public static Font Romulus;

        static void Main(string[] args)
        {
            Game game = new Game();

            InitWindow(GameWidth*GameZoom, GameHeight*GameZoom, "...");
            SetTargetFPS(60);

            RenderTexture2D AppSurface = LoadRenderTexture(GameWidth, GameHeight);
            SetTextureFilter(AppSurface.texture, TextureFilterMode.FILTER_POINT);

            //load
            Romulus = LoadFont("resources/fonts/romulus.png");

            game.Init();

            camera = new Camera2D();
            camera.zoom = 1;

            while (!WindowShouldClose())
            {
                //camera.offset -= new Vector2(1,1);

                game.Update();

                //Draw Scaled Window
                BeginDrawing();

                BeginTextureMode(AppSurface);
                BeginMode2D(camera);

                ClearBackground(Color.WHITE);
                DrawRectangleGradientV(0, 0, GameWidth, GameHeight, Color.WHITE, Color.LIGHTGRAY);

                game.Draw();

                EndTextureMode();
                DrawRenderTexture(AppSurface);
                EndMode2D();

                DrawText(GetFPS().ToString(), 8, 8, 12, Color.RED);

                EndDrawing();
            }

            //unload
            UnloadFont(Romulus);

            game.Shutdown();

            CloseWindow();
        }
    }
}
