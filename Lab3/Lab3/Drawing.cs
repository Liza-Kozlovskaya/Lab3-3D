using System;
using System.Drawing;
using System.Globalization;

namespace Lab3
{
    public static class Drawing
    {
        private static void PutPixel(Bitmap bmp, Color col, Color col2, int x, int y, int alpha)
        {
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        static public void BresenhamLine(Bitmap bmp, Color clr1, Color clr2, int x1, int y1, int x2, int y2)
        {
            //изменение координат
            int dx = (x2 > x1) ? (x2 - x1) : (x1 - x2);
            int dy = (y2 > y1) ? (y2 - y1) : (y1 - y2);
            //направление приращения
            int sx = (x2 >= x1) ? (1) : (-1);
            int sy = (y2 >= y1) ? (1) : (-1);

            if (dy < dx)
            {
                int d = (dy << 1) - dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                PutPixel(bmp, clr1, clr2, x1, y1, 255);
                int x = x1 + sx;
                int y = y1;

                for (int i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PutPixel(bmp, clr1, clr2, x, y, 255);
                    x += sx;
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                PutPixel(bmp, clr1, clr2, x1, y1, 255);
                int x = x1;
                int y = y1 + sy;

                for (int i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PutPixel(bmp, clr1, clr2, x, y, 255);
                    y += sy;
                }
            }

        }
        public static Color Interpolate(Color color1, Color color2, double fraction)
        {
            double r = Interpolate(color1.R, color2.R, fraction);
            double g = Interpolate(color1.G, color2.G, fraction);
            double b = Interpolate(color1.B, color2.B, fraction);
            return Color.FromArgb((int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
        }

        private static double Interpolate(double d1, double d2, double fraction)
        {
            return d1 + (d2 - d1) * fraction;
        }
    }
}