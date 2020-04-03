using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Raylib.Raylib;
using MathClasses;
using Project2D.Scenes;
using Raylib;
using Project2D.TankGame;

namespace Project2D
{
    class Program
    {
        public const int GameWidth = 320;
        public const int GameHeight = 180;
        public static int GameZoom = 4;

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

            while (!WindowShouldClose())
            {
                //camera.offset -= new Vector2(1,1);

                game.Update();

                //Draw Scaled Window
                BeginDrawing();

                BeginTextureMode(AppSurface);

                ClearBackground(MathMore.toRayColour(new Colour(255,255,255,1)));
                ClearBackground(Color.WHITE);

                game.Draw();

                EndTextureMode();
                DrawRenderTexture(AppSurface);
                EndMode2D();

                DrawText(GetFPS().ToString(), 8, 8, 12, MathMore.toRayColour(new Colour(255, 0, 0, 1)));

                EndDrawing();
            }

            //unload
            UnloadFont(Romulus);

            game.Shutdown();

            CloseWindow();
        }
    }
}
