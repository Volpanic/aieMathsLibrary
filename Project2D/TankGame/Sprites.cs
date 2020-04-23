using Raylib;
using System;
using System.Collections.Generic;
using System.IO;
using static Raylib.Raylib;

namespace Project2D.TankGame
{
    public static class Sprites
    {
        static SortedDictionary<string, Texture2D> SpriteList;

        public static void InitSprites()
        {
            SpriteList = new SortedDictionary<string, Texture2D>();

            string[] Files = Directory.GetFiles(Path.Combine("Resources", "Sprites"));

            foreach(string name in Files)
            {
                if(Path.GetExtension(name) == ".png")
                {
                    Texture2D tex = LoadTexture(name);
                    SpriteList.Add(Path.GetFileNameWithoutExtension(name),tex);
                    Console.WriteLine(Path.GetFileNameWithoutExtension(name));
                }
            }
        }

        public static void UnloadTextures()
        {
            foreach (KeyValuePair<string, Texture2D> tex in SpriteList)
            {
                UnloadTexture(tex.Value);
            }
        }

        public static Texture2D GetSprite(string FileName)
        {
            if (SpriteList.ContainsKey(FileName))
            {
                return SpriteList[FileName];
            }

            return new Texture2D();
        }
    }
}
