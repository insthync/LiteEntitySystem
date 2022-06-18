using System;
using System.Globalization;
using LiteEntitySystem.Internal;

namespace LiteEntitySystem
{
    public struct FloatAngle
    {
        public const float PI = (float)Math.PI;
        public const float Rad2Deg = (float)(180.0 / Math.PI);
        public const float Deg2Rad = (float)(Math.PI / 180.0);

        public float Radians => Degrees * Deg2Rad;
        public float Degrees;
        
        public FloatAngle Normalized
        {
            get
            {
                FloatAngle r = new FloatAngle(Degrees);
                while (r.Degrees >= 180f)
                    r.Degrees -= 360f;
                while (r.Degrees < -180f)
                    r.Degrees += 360f;
                return r;
            }
        }

        public float Sin()
        {
            return (float)Math.Sin(Radians);
        }

        public float Cos()
        {
            return (float)Math.Cos(Radians);
        }
        
        public FloatAngle(float value)
        {
            Degrees = value;
        }
        
        public void Normalize()
        {
            while (Degrees >= 180f)
                Degrees -= 360f;
            while (Degrees < -180f)
                Degrees += 360f;
        }

        public override string ToString()
        {
            return Degrees.ToString(CultureInfo.InvariantCulture);
        }

        public static implicit operator FloatAngle(float degrees)
        {
            return new FloatAngle(degrees);
        }
        
        public static implicit operator float(FloatAngle value)
        {
            return value.Degrees;
        }

        public static FloatAngle Lerp(FloatAngle a, FloatAngle b, float t)
        {
            return Utils.Lerp(a.Degrees, b.Degrees, t);
        }
    }
}