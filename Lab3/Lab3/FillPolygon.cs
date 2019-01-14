using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class FillPolygon
    {
        private static int Sign(int x)
        {
            return (x > 0) ? 1 : (x < 0) ? -1 : 0;
        }

        //линии Брезенхейма
        public static List<Pixel> DrawLine(Bitmap bitmap, int x1, int y1, Color color1, int x2, int y2, Color color2)
        {
            List<Point> points = new List<Point>();
            int pdx, pdy;
            int element, pelement;

            int dx = x2 - x1;
            int dy = y2 - y1;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                pdx = Sign(dx);
                pdy = 0;
                element = Math.Abs(dx);
                pelement = Math.Abs(dy);
            }
            else
            {
                pdx = 0;
                pdy = Sign(dy);
                element = Math.Abs(dy);
                pelement = Math.Abs(dx);
            }

            int x = x1;
            int y = y1;
            int e = element / 2;
            points.Add(new Point(x, y));

            for (int i = 0; i < element; i++)
            {
                e -= pelement;
                if (e < 0)
                {
                    e += element;
                    x += Sign(dx);
                    y += Sign(dy);
                }
                else
                {
                    x += pdx;
                    y += pdy;
                }

                points.Add(new Point(x, y));
            }
            List<Pixel> pixels = new List<Pixel>();

            //градиент
            double dr = (double)(color2.R - color1.R) / points.Count;
            double dg = (double)(color2.G - color1.G) / points.Count;
            double db = (double)(color2.B - color1.B) / points.Count;
            Color color = color1;
            for (int i = 0; i < points.Count; i++)
            {
                color = Color.FromArgb(Convert.ToInt32(color1.R + dr * i), Convert.ToInt32(color1.G + dg * i), Convert.ToInt32(color1.B + db * i));
                pixels.Add(new Pixel(points[i].X, points[i].Y, color));
                bitmap.SetPixel(points[i].X, points[i].Y, color);
            }
            return pixels;
        }

        //закрашивание полигонов
        public static void FillPolygons(Bitmap bitmap, List<Pixel> polygon)
        {
            List<Pixel> pixels = new List<Pixel>();
            for (int i = 0; i < polygon.Count - 1; i++)
            {
                var p1 = polygon[i];
                var p2 = polygon[i + 1];
                var linePixels = DrawLine(bitmap, p1.X, p1.Y, p1.Color, p2.X, p2.Y, p2.Color);
                pixels.AddRange(linePixels);
            }
            polygon.Sort((i, j) => i.X.CompareTo(j.X));
            int minX = polygon[0].X;
            int maxX = polygon[polygon.Count - 1].X;
            for (int i = minX; i < maxX; i++)
            {
                var ys = pixels.Where(p => p.X == i).ToList();

                for (int j = 0; j < ys.Count - 1; j++)
                {
                    DrawLine(bitmap, ys[j].X, ys[j].Y, ys[j].Color, ys[j + 1].X, ys[j + 1].Y, ys[j + 1].Color);
                }
            }
        }
    }
}
