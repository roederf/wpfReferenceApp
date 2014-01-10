using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace UIControls.Utilities
{
    public class ColorUtilities
    {
        public static Color HUEtoRGB(double H)
        {
            double vR = Math.Abs(H * 6 - 3) - 1;
            double vG = 2 - Math.Abs(H * 6 - 2);
            double vB = 2 - Math.Abs(H * 6 - 4);

            vR = (vR < 0) ? 0 : (vR > 1) ? 1 : vR;
            vG = (vG < 0) ? 0 : (vG > 1) ? 1 : vG;
            vB = (vB < 0) ? 0 : (vB > 1) ? 1 : vB;

            return new Color() { R = Convert.ToByte(vR * 255), G = Convert.ToByte(vG * 255), B = Convert.ToByte(vB * 255) };
        }

        public static Color HSVtoRGB(double[] HSV)
        {
            Color c = HUEtoRGB(HSV[0]);
            c.R = Convert.ToByte(((c.R - 255) * HSV[1] + 255) * HSV[2]);
            c.G = Convert.ToByte(((c.G - 255) * HSV[1] + 255) * HSV[2]);
            c.B = Convert.ToByte(((c.B - 255) * HSV[1] + 255) * HSV[2]);
            c.A = 255;

            return c;
        }

        public static double[] RGBToHSV(Color c)
        {
            double[] hsl = new double[3];

            double max = Math.Max(Math.Max(c.R, c.G), c.B);
            double min = Math.Min(Math.Min(c.R, c.G), c.B);
            double diff = max - min;
            double sum = max + min;

            // lightness
            hsl[2] = (double)max / 255;

            // Saturation
            if (max == 0)
            {
                hsl[1] = 0;
            }
            else
            {
                hsl[1] = (double)diff / max;
            }

            double q;
            if (diff == 0)
            {
                q = 0;
            }
            else
            {
                q = (double)60 / diff;
            }

            //hue
            if (max == c.R)
            {
                if (c.G < c.B)
                {
                    hsl[0] = (double)(360 + q * (c.G - c.B)) / 360;
                }
                else
                {
                    hsl[0] = (double)(q * (c.G - c.B)) / 360;
                }
            }
            else if (max == c.G)
            {
                hsl[0] = (double)(120 + q * (c.B - c.R)) / 360;
            }
            else if (max == c.B)
            {
                hsl[0] = (double)(240 + q * (c.R - c.G)) / 360;
            }
            else
            {
                hsl[0] = 0.0;
            }
            return hsl;
        }

        public static Point colorToUV(double[] c)
        {
            double _a = (0.75 - c[0]) * 2 * Math.PI;   // [-pi..pi] - 0.25 shift

            return new Point(Math.Cos(_a) * c[1] * 0.5, Math.Sin(_a) * c[1] * 0.5);
        }
    }
}
