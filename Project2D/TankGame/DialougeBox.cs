using MathClasses;
using Raylib;
using System;
using System.Collections.Generic;
using static Raylib.Raylib;
using rl = Raylib;


namespace Project2D.TankGame
{
    public class DialougeBox
    {
        public List<string> Messages = new List<string>();
        public int CurrentMessage;

        public bool Visible = true;
        public rl.Rectangle boxRect = new rl.Rectangle(8, Program.GameHeight - 64, Program.GameWidth - 16, Program.GameHeight / 4);
        public int LetterCount = 0;
        public int Modifier = 0;

        public float TypeWritterTimer = 0;

        Random rand = new Random();

        public DialougeBox()
        {

        }

        public void DrawDialougeBox()
        {
            if (!Visible) return;
            DrawRectangleRec(boxRect, Color.WHITE);
            DrawRectangleLinesEx(boxRect, 2, Color.BLACK);
            //Set up some basic variables
            float CharWidth = 0;
            int YLine = 0;
            rl.Vector2 topLeft = new rl.Vector2(boxRect.x + 8, boxRect.y + 8);
            Font fnt = Program.Romulus;

            string mess = Messages[CurrentMessage];

            if (LetterCount >= mess.Length - 1)
            {
                //Go to next message
                if (IsKeyPressed(KeyboardKey.KEY_SPACE) || IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    CurrentMessage++;
                    LetterCount = 0;
                    if (CurrentMessage >= Messages.Count)
                    {
                        Messages.Clear();
                        CurrentMessage = 0;
                        Visible = false;
                    }
                }
            }
            else //TypeWritter Effect
            {
                if (IsKeyDown(KeyboardKey.KEY_SPACE) || IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    TypeWritterTimer = 3;
                }

                if (TypeWritterTimer > 2)
                {
                    LetterCount = Numbers.Approach(LetterCount, mess.Length - 1, 1);
                    TypeWritterTimer = 0;
                }
                TypeWritterTimer++;
            }
            Modifier = 0;
            //Draw Text
            for (int i = 0; i <= LetterCount; i++)
            {
                char currentChar = mess[i];

                if (currentChar == '/')
                {
                    Int32.TryParse(mess[i + 1].ToString(), out Modifier);
                    i += 2;
                    if (i > LetterCount) break;
                }

                float temp = 0;
                int length = 0;
                while (mess[i].ToString() != " " && i < mess.Length - 1)
                {
                    temp += MeasureTextEx(fnt, mess[i].ToString(), 12, 1).x + 1;
                    i++;
                    length++;
                }

                CharWidth += temp;

                if (CharWidth >= boxRect.width - 16)
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

                switch (Modifier) // System does not scale at all
                {
                    case 0: // Normal Draw
                    {
                        DrawTextEx(fnt, currentChar.ToString(), new rl.Vector2(topLeft.x + CharWidth, topLeft.y + (12 * YLine)), 12, 1, Color.BLACK);
                        break;
                    }

                    case 1: // ShakeyDraw
                    {
                        DrawTextEx(fnt, currentChar.ToString(), new rl.Vector2(topLeft.x + CharWidth + rand.Next(-1, 1), topLeft.y + (12 * YLine) + rand.Next(-1, 1)), 12, 1, Color.BLACK);
                        break;
                    }

                    case 2: // Blue Draw
                    {
                        DrawTextEx(fnt, currentChar.ToString(), new rl.Vector2(topLeft.x + CharWidth, topLeft.y + (12 * YLine)), 12, 1, Color.DARKBLUE);
                        break;
                    }

                    case 3: // Gray Draw
                    {
                        DrawTextEx(fnt, currentChar.ToString(), new rl.Vector2(topLeft.x + CharWidth, topLeft.y + (12 * YLine)), 12, 1, Color.LIGHTGRAY);
                        break;
                    }
                }

                CharWidth += MeasureTextEx(fnt, mess[i].ToString(), 12, 1).x + 1;
            }
        }
    }
}
