using MathClasses;
using System;
using Project2D.TankGame;
using Raylib;
using static Raylib.Raylib;
using System.IO;

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

            InitWindow(GameWidth * GameZoom, GameHeight * GameZoom, "Tnk");
            SetTargetFPS(60);

            //Creates a base surface to draw the game too, Scaled up later
            RenderTexture2D AppSurface = LoadRenderTexture(GameWidth, GameHeight);
            SetTextureFilter(AppSurface.texture, TextureFilterMode.FILTER_POINT);

            //Load
            Romulus = LoadFont("resources/fonts/romulus.png");
            InitAudioDevice();

            //Load All sprites at runtime
            Sprites.InitSprites();

            //Camera for screenshake
            Camera2D camera = new Camera2D();
            camera.zoom = 1;

            //Music
            IntPtr GameMusic = LoadMusicStream("Resources/Audio/mus_boss.ogg");
            PlayMusicStream(GameMusic);
            bool MusicMuted = false;
            game.Init();

            Random Rand = new Random();

            while (!WindowShouldClose())
            {
                //Music
                UpdateMusicStream(GameMusic);

                if(IsKeyPressed(KeyboardKey.KEY_M))
                {
                    if (MusicMuted)
                    {
                        ResumeMusicStream(GameMusic);
                    }
                    else
                    {
                        MusicMuted = true;
                        PauseMusicStream(GameMusic);
                    }
                }

                game.Update(ref camera.offset);

                //Draw Scaled Window
                BeginDrawing();
  
                BeginTextureMode(AppSurface);
                BeginMode2D(camera);

                //ClearBackground(MathMore.toRayColour(new Colour(255, 255, 255, 1)));
                ClearBackground(Color.WHITE);

                game.Draw();

                EndTextureMode();

                //Draw Scaled Window
                DrawRenderTexture(AppSurface);
                EndMode2D();
                
                EndDrawing();
            }

            //unload
            UnloadRenderTexture(AppSurface);
            UnloadFont(Romulus);
            StopMusicStream(GameMusic);
            UnloadMusicStream(GameMusic);
            CloseAudioDevice();
            Sprites.UnloadTextures();
            game.Shutdown();

            CloseWindow();
        }
    }
}
