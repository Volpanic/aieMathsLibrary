using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Raylib;
using static Raylib.Raylib;
using MathClasses;
using Project2D.Scenes;
using Project2D.TankGame;

using rl = Raylib;


namespace Project2D.TankGame
{
    public class DialougeBox
    {
        public List<string> Messages = new List<string>();
        public int CurrentMessage;

        public bool Visible = true;
        public rl.Rectangle boxRect = new rl.Rectangle(8,Program.GameHeight - 84, Program.GameWidth - 16, Program.GameHeight/4);
        public int LetterCount = 0;

        public void DrawDialougeBox()
        {
            if (!Visible) return;
            DrawRectangleRec(boxRect,Color.WHITE);
            DrawRectangleLinesEx(boxRect, 2, Color.BLACK);
            //Set up some basic variables
            float CharWidth = 0;
            int YLine = 0;
            rl.Vector2 topLeft = new rl.Vector2(boxRect.x+8,boxRect.y+8);
            Font fnt = Program.Romulus;

            string mess = Messages[CurrentMessage];

            if(LetterCount >= mess.Length-1)
            {
                //Go to next message
                if(IsKeyPressed(KeyboardKey.KEY_Z))
                {
                    CurrentMessage++;
                    if(CurrentMessage >= Messages.Count)
                    {
                        Visible = false;
                    }
                }
            }
            else //TypeWritter Effect
            {
                LetterCount = Numbers.Approach(LetterCount,mess.Length-1,1);
            }

            //Draw Text
            for (int i = 0; i <= LetterCount; i++)
            {
                char currentChar = mess[i];

                float temp = 0;
                int length = 0;
                while(mess[i].ToString() != " " && i < mess.Length-1)
                {
                    temp += MeasureTextEx(fnt, mess[i].ToString(), 12, 1).x+1;
                    i++;
                    length++;
                }

                CharWidth += temp;

                if(CharWidth >= boxRect.width-16)
                {
                    CharWidth = 0;
                    YLine += 1;
                }
                else
                {
                    CharWidth -= temp;
                }

                i -= length;
                currentChar = mess[i];

                DrawTextEx(fnt, currentChar.ToString(), new rl.Vector2(topLeft.x + CharWidth, topLeft.y + (12 * YLine)),12,1,Color.BLACK);
                CharWidth += MeasureTextEx(fnt, mess[i].ToString(), 12, 1).x+1;
            }
        }
    }
}
