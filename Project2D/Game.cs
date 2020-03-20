using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2D.Scenes;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    public class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        public static float deltaTime = 0.005f;

        private Scene currentRunningScene;
        private Scene nextScene = null;

        private byte transitionAlpha = 0;
        private bool  isTransistioning = false;

        public Scene CurrentGameScene { get { return currentRunningScene; } set { SetNextScene(value); } }

        public Game()
        {
            currentRunningScene = new TitleScene(this);
        }

        private void SetNextScene(Scene _scene)
        {
            isTransistioning = true;
            transitionAlpha = 0;
            nextScene = _scene;
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }
        }

        public void Shutdown()
        {
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            // insert game logic here   
            currentRunningScene.Update();
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);

            currentRunningScene.Draw();

            Color tranColor = Color.BLACK;

            //Transition Scenes
            if(isTransistioning)
            {
                if(transitionAlpha < 255)
                {
                    transitionAlpha += 5;
                }
                else
                {
                    currentRunningScene = nextScene;
                    nextScene = null;
                    isTransistioning = false;
                }
            }
            else
            {
                if (transitionAlpha > 0)
                {
                    transitionAlpha -= 5;
                }
            }

            tranColor.a = transitionAlpha;

            DrawRectangle(0,0,Program.GameWidth,Program.GameHeight,tranColor);

            DrawText(fps.ToString(), 8, 8, 12, Color.RED);

            EndDrawing();
        }

    }
}
