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
            camera.zoom = 2;

            while (!WindowShouldClose())
            {
                game.Update();

                ClearBackground(Color.WHITE);

                //Draw Scaled Window
                BeginDrawing();

                
                BeginTextureMode(AppSurface);
                //BeginMode2D(camera);

                DrawRectangleGradientV(0,0,GameWidth,GameHeight,Color.WHITE,Color.LIGHTGRAY);

                game.Draw();

                //EndMode2D();
                EndTextureMode();
                            
                DrawRenderTexture(AppSurface);

                //EndDrawing();
            }

            //unload
            UnloadFont(Romulus);

            game.Shutdown();

            CloseWindow();
        }
    }
}
