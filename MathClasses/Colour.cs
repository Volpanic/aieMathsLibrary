﻿using System;

namespace MathClasses
{
    public class Colour
    {

        public UInt32 colour { get; set; }

        public Colour(byte r = 0, byte g = 0, byte b = 0, byte a = 0)
        {
            colour |= (UInt32)(r << 24);
            colour |= (UInt32)(g << 16);
            colour |= (UInt32)(b << 8);
            colour |= (UInt32)(a);
        }

        public void SetRed(byte newRed)
        {
            colour = (colour & 0x00FFFFFF) | ((UInt32)newRed << 24);
        }

        public byte GetRed()
        {
            return (byte)(colour >> 24);
        }

        public void SetGreen(byte newGreen)
        {
            colour = (colour & 0xFF00FFFF) | ((UInt32)newGreen << 16);
        }

        public byte GetGreen()
        {
            return (byte)(colour >> 16);
        }

        public void SetBlue(byte newBlue)
        {
            colour = (colour & 0xFFFF00FF) | ((UInt32)newBlue << 8);
        }

        public byte GetBlue()
        {
            return (byte)(colour >> 8);
        }

        public void SetAlpha(byte newAlpha)
        {
            colour = (colour & 0xFFFFFF00) | ((UInt32)newAlpha);
        }

        public byte GetAlpha()
        {
            return (byte)(colour);
        }

        public void RedToGreen()
        {
            SetGreen(GetRed());
            SetRed(0);
        }

        //Static Colours
        public static Colour White = new Colour(255, 255, 255, 255);
        public static Colour Black = new Colour(0, 0, 0, 0);
        public static Colour Gray = new Colour(24, 34, 42, 255);
        public static Colour Red = new Colour(255, 34, 16, 255);

    }
}
