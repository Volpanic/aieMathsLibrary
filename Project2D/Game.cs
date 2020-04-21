using Project2D.Scenes;
using Raylib;
using System;
using System.Diagnostics;
using static Raylib.Raylib;

namespace Project2D
{
    public class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int frames;

        public static float cleandeltaTime = 0.005f;
        public static float deltaTime { get { return (cleandeltaTime * 60); } }

        private Scene currentRunningScene;
        private Scene nextScene = null;

        private byte transitionAlpha = 0;
        private bool isTransistioning = false;

        public Scene CurrentGameScene { get { return currentRunningScene; } set { SetNextScene(value); } }

        public Game()
        {
            currentRunningScene = new TitleScene(this);
        }

        private void SetNextScene(Scene _scene)
        {
            //Resets Scene Transition
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

        //End Of Game
        public void Shutdown()
        {
            currentRunningScene.EndScene();
        }

        public void Update()
        {
            //DeltaTime
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            cleandeltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                frames = 0;
                timer -= 1;
            }
            frames++;

            //Game Running   
            currentRunningScene.Update();
        }

        public void Draw()
        {
            //Game Draw
            currentRunningScene.Draw();

            Color tranColor = Color.BLACK;

            //Transition Scenes
            if (isTransistioning)
            {
                if (transitionAlpha < 255)
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

            DrawRectangle(0, 0, Program.GameWidth, Program.GameHeight, tranColor);
        }

    }
}
