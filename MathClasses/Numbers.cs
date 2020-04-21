using System;

namespace MathClasses
{
    public static class Numbers
    {
        public static int Approach(int value, int target, int amount)
        {
            if (value < target)
            {
                value = Math.Min(value + amount, target);
            }
            else
            {
                value = Math.Max(value - amount, target);
            }
            return value;
        }

        public static float Approach(float value, float target, float amount)
        {
            if (value < target)
            {
                value = Math.Min(value + amount, target);
            }
            else
            {
                value = Math.Max(value - amount, target);
            }
            return value;
        }

        public static float SinWave(float from, float to, float duration, float offset, float timer)
        {
            float a4 = (to - from) * 0.5f;

            float si = from + a4 + (float)Math.Sin((((timer * 0.01f) + duration * offset) / duration) * ((float)Math.PI * 2.0f)) * a4;

            return si;
        }
    }
}
