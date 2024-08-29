using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AxisConfigurator
{
    public class HSV
    {
        public float Hue { get; set; }
        public float Saturation { get; set; }
        public float Value { get; set; }

        public HSV(float hue, float saturation, float value)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }
        public static Color ToColor(HSV hsv)
        {
            // Normalize HSV values from 0-255 to 0-1
            float h = hsv.Hue / 360f;
            float s = hsv.Saturation / 255f;
            float v = hsv.Value / 255f;

            // Convert HSV to RGB
            float r, g, b;

            if (s == 0)
            {
                // Achromatic (grey)
                r = g = b = v;
            }
            else
            {
                float hue = h * 6f;
                int sector = (int)hue;
                float fraction = hue - sector;
                float p = v * (1 - s);
                float q = v * (1 - s * fraction);
                float t = v * (1 - s * (1 - fraction));

                switch (sector)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = p;
                        b = q;
                        break;
                    default:
                        r = g = b = 0;
                        break;
                }
            }

            // Convert RGB values from 0-1 to 0-255
            byte rInt = (byte)(r * 255);
            byte gInt = (byte)(g * 255);
            byte bInt = (byte)(b * 255);

            // Create and return the Color object
            return Color.FromArgb(255, rInt, gInt, bInt);
        }

        public static HSV FromRGB(Color color)
        {
            // Convert RGB values from [0, 255] range to [0.0, 1.0] range
            double rNorm = color.R / 255.0;
            double gNorm = color.G / 255.0;
            double bNorm = color.B / 255.0;

            // Find the maximum and minimum values among the normalized RGB values
            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;

            // Calculate Hue
            double h = 0;
            if (delta != 0)
            {
                if (max == rNorm)
                {
                    h = (gNorm - bNorm) / delta;
                }
                else if (max == gNorm)
                {
                    h = 2 + (bNorm - rNorm) / delta;
                }
                else
                {
                    h = 4 + (rNorm - gNorm) / delta;
                }
                h *= 60; // Convert to 255
                if (h < 0)
                {
                    h += 360;
                }
            }

            // Calculate Saturation
            double s = (max == 0) ? 0 : delta / max;

            // Calculate Value
            double v = max;

            return new HSV((float)h, (float)s*255, (float)v*255);
        }

        // Optional: Override ToString() for easy debugging
        public override string ToString()
        {
            return $"Hue: {Hue:F2}°, Saturation: {Saturation:P0}, Value: {Value:P0}";
        }
    }
}
