using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Colour
    {
        private byte R = 0;
        private byte G = 0;
        private byte B = 0;
        private byte A = 255;

        public Colour(byte r,byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public void SetRed(byte newRed)
        {
            R = newRed;
        }

        public byte GetRed()
        {
            return R;
        }

        public void SetGreen(byte newGreen)
        {
            G = newGreen;
        }

        public byte GetGreen()
        {
            return G;
        }

        public void SetBlue(byte newBlue)
        {
            B = newBlue;
        }

        public byte GetBlue()
        {
            return B;
        }

        public void SetAlpha(byte newAlpha)
        {
            A = newAlpha;
        }

        public byte GetAlpha()
        {
            return A;
        }

    }
}
