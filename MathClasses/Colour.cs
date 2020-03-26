using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Colour
    {

        public UInt32 colour { get { return GetColour(); } }

        private byte R = 0;
        private byte G = 0;
        private byte B = 0;
        private byte A = 0;

        public Colour(byte r = 0,byte g = 0, byte b = 0, byte a = 0)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        private UInt32 GetColour()
        {
            return BitConverter.ToUInt32(new byte[] { A,B,G,R},0);
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
