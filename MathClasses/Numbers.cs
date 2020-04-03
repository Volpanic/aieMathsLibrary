using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public static class Numbers
    {
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
    }
}
