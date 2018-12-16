using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form5 : Form
    {
        Cube cubeParal;
        Cube cubePersp;

        double rotationX;
        double rotationY;
        double rotationZ;

        double speedX;
        double speedY;
        double speedZ;

        Point Origin { get; set; }
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Interval = 20;

            cubePersp = new Cube(150);
            Point origin = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            pictureBox1.Image = cubePersp.drawCubePersp(origin);

            cubeParal = new Cube(150);
            Point origin2 = new Point(pictureBox2.Width / 2, pictureBox2.Height / 2);
            pictureBox2.Image = cubeParal.drawCubeParal(origin2);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            
            double.TryParse(textBox1.Text, out speedX);
            double.TryParse(textBox2.Text, out speedY);
            double.TryParse(textBox3.Text, out speedZ);
            timer1.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rotationX = CalculateSpeed(rotationX, speedX / 100);
            rotationY = CalculateSpeed(rotationY, speedY / 100);
            rotationZ = CalculateSpeed(rotationZ, speedZ / 100);

            Render(rotationX, rotationY, rotationZ);
        }
        private double CalculateSpeed(double Rotation, double speed)
        {
            double newRotation = speed * timer1.Interval;
            return Rotation += newRotation;
        }

        private void Render(double rotateX, double rotateY, double rotateZ)
        {
            cubeParal.RotateX = cubePersp.RotateX = rotateX;
            cubeParal.RotateY = cubePersp.RotateY = rotateY;
            cubeParal.RotateZ = cubePersp.RotateZ = rotateZ;

            Point origin = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            Point origin2 = new Point(pictureBox2.Width / 2, pictureBox2.Height / 2);
            pictureBox1.Image = cubePersp.drawCubePersp(origin);
            pictureBox2.Image=cubeParal.drawCubeParal(origin2);
        }
    }


    public class Point3D
    {
        public double X;
        public double Y;
        public double Z;

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Camera
    {
        public Point3D Position = new Point3D(0, 0, 0);
    }

    public class TransformCube
    { 
        //[ 1    0        0   ]
        //[ 0   cos(x)  sin(x)]
        //[ 0   -sin(x) cos(x)]
        private static Point3D RotateX(Point3D point3D, double degrees)
        {
            double cDegrees = (Math.PI * degrees) / 180.0f;
            double cosDegrees = Math.Cos(cDegrees);
            double sinDegrees = Math.Sin(cDegrees);

            double y = (point3D.Y * cosDegrees) + (point3D.Z * sinDegrees);
            double z = (point3D.Y * -sinDegrees) + (point3D.Z * cosDegrees);

            return new Point3D(point3D.X, y, z);
        }

        //[ cos(x)   0    sin(x)]
        //[   0      1      0   ]
        //[-sin(x)   0    cos(x)]
        private static Point3D RotateY(Point3D point3D, double degrees)
        {
            double cDegrees = (Math.PI * degrees) / 180.0;
            double cosDegrees = Math.Cos(cDegrees);
            double sinDegrees = Math.Sin(cDegrees);

            double x = (point3D.X * cosDegrees) + (point3D.Z * sinDegrees);
            double z = (point3D.X * -sinDegrees) + (point3D.Z * cosDegrees);

            return new Point3D(x, point3D.Y, z);
        }

        //[ cos(x)  sin(x) 0]
        //[ -sin(x) cos(x) 0]
        //[    0     0     1]
        private static Point3D RotateZ(Point3D point3D, double degrees)
        {
            double radianDegrees = (Math.PI * degrees) / 180.0;
            double cosDegrees = Math.Cos(radianDegrees);
            double sinDegrees = Math.Sin(radianDegrees);

            double x = (point3D.X * cosDegrees) + (point3D.Y * sinDegrees);
            double y = (point3D.X * -sinDegrees) + (point3D.Y * cosDegrees);

            return new Point3D(x, y, point3D.Z);
        }

        public static Point3D Translate(Point3D points3D, Point3D oldOrigin, Point3D newOrigin)
        {
            Point3D difference = new Point3D(newOrigin.X - oldOrigin.X, newOrigin.Y - oldOrigin.Y, newOrigin.Z - oldOrigin.Z);
            points3D.X += difference.X;
            points3D.Y += difference.Y;
            points3D.Z += difference.Z;
            return points3D;
        }

        public static Point3D[] RotateX(Point3D[] points3D, double degrees)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = RotateX(points3D[i], degrees);
            }
            return points3D;
        }

        public static Point3D[] RotateY(Point3D[] points3D, double degrees)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = RotateY(points3D[i], degrees);
            }
            return points3D;
        }

        public static Point3D[] RotateZ(Point3D[] points3D, double degrees)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = RotateZ(points3D[i], degrees);
            }
            return points3D;
        }

        public static Point3D[] Translate(Point3D[] points3D, Point3D oldOrigin, Point3D newOrigin)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = Translate(points3D[i], oldOrigin, newOrigin);
            }
            return points3D;
        }
    }

    public class Cube
    {
        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }

        double xRotation = 0.0;
        double yRotation = 0.0;
        double zRotation = 0.0;
        public double RotateX
        {
            get { return xRotation; }
            set { xRotation = value; }
        }

        public double RotateY
        {
            get { return yRotation; }
            set { yRotation = value; }
        }

        public double RotateZ
        {
            get { return zRotation; }
            set { zRotation = value; }
        }

        Camera camera1 = new Camera();
        Point3D cubeOrigin;

        public Cube(int side)
        {
            Width = side;
            Height = side;
            Depth = side;
            cubeOrigin = new Point3D(Width / 2, Height / 2, Depth / 2);
            camera1.Position = new Point3D(cubeOrigin.X, cubeOrigin.Y, 1000);
        }

        public static Rectangle getBounds(PointF[] points)
        {
            double left = points[0].X;
            double right = points[0].X;
            double top = points[0].Y;
            double bottom = points[0].Y;
            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].X < left)
                    left = points[i].X;
                if (points[i].X > right)
                    right = points[i].X;
                if (points[i].Y < top)
                    top = points[i].Y;
                if (points[i].Y > bottom)
                    bottom = points[i].Y;
            }

            return new Rectangle(0, 0, (int)Math.Round(right - left), (int)Math.Round(bottom - top));
        }

        public Bitmap drawCubePersp(Point drawOrigin)
        {
            PointF[] point2Dconvert = new PointF[8];

            Point3D[] cubePoints = fillCubeVertices(Width, Height, Depth);

            cubePoints = TransformCube.RotateX(cubePoints, RotateX);
            cubePoints = TransformCube.RotateY(cubePoints, RotateY);
            cubePoints = TransformCube.RotateZ(cubePoints, RotateZ);

            //Перевод 3д точек в 2д
            Point3D vec;
            for (int i = 0; i < point2Dconvert.Length; i++)
            {
                vec = cubePoints[i];
                point2Dconvert[i].X = (int)((vec.X * camera1.Position.Z - vec.Z * camera1.Position.X) / (camera1.Position.Z - vec.Z) + drawOrigin.X);
                point2Dconvert[i].Y = (int)((vec.Y * camera1.Position.Z - vec.Z * camera1.Position.Y) / (camera1.Position.Z - vec.Z) + drawOrigin.Y);
            }

            var tmpBmp = draw2DCube(point2Dconvert, drawOrigin, cubePoints);
            return tmpBmp;
        }

        public Bitmap drawCubeParal(Point drawOrigin)
        {
            PointF[] point2Dconvert = new PointF[8];

            Point3D[] cubePoints = fillCubeVertices(Width, Height, Depth);

            cubePoints = TransformCube.RotateX(cubePoints, RotateX);
            cubePoints = TransformCube.RotateY(cubePoints, RotateY);
            cubePoints = TransformCube.RotateZ(cubePoints, RotateZ);

            Point3D vec;
            for (int i = 0; i < point2Dconvert.Length; i++)
            {
                vec = cubePoints[i];
                point2Dconvert[i].X = (int)(vec.X + drawOrigin.X);
                point2Dconvert[i].Y = (int)(vec.Y + drawOrigin.Y);
            }
            var tmpBmp = draw2DCube(point2Dconvert, drawOrigin, cubePoints);
            return tmpBmp;

        }
        public Point3D[] fillCubeVertices(int width, int height, int depth)
        {
            Point3D[] verts = new Point3D[8];

            //front face
            verts[0] = new Point3D(-width / 2, -height / 2, -depth / 2);
            verts[1] = new Point3D(-width / 2, height / 2, -depth / 2);
            verts[2] = new Point3D(width / 2, height / 2, -depth / 2);
            verts[3] = new Point3D(width / 2, -height / 2, -depth / 2);

            //back face           
            verts[4] = new Point3D(-width / 2, -height / 2, depth / 2);
            verts[5] = new Point3D(-width / 2, height / 2, depth / 2);
            verts[6] = new Point3D(width / 2, height / 2, depth / 2);
            verts[7] = new Point3D(width / 2, -height / 2, depth / 2);

            return verts;
        }
        private Bitmap draw2DCube(PointF[] point2Dconvert, Point drawOrigin, Point3D[] cubePoints)
        {
            Rectangle bounds = getBounds(point2Dconvert);
            bounds.Width += drawOrigin.X;
            bounds.Height += drawOrigin.Y;

            Camera light = new Camera();
            light.Position = new Point3D(Width / 2, Height / 2 +50, Depth / 2);

            Bitmap tmpBmp = new Bitmap(bounds.Width, bounds.Height);

            Point light2D = new Point();
            light2D.X = (int)(light.Position.X + drawOrigin.X);
            light2D.Y = (int)(light.Position.Y + drawOrigin.Y);

            //точка-источник света
            tmpBmp.SetPixel(light2D.X, light2D.Y, Color.Blue);
            tmpBmp.SetPixel(light2D.X + 1, light2D.Y, Color.Blue);
            tmpBmp.SetPixel(light2D.X - 1, light2D.Y, Color.Blue);
            tmpBmp.SetPixel(light2D.X + 1, light2D.Y + 1, Color.Blue);
            tmpBmp.SetPixel(light2D.X + 1, light2D.Y - 1, Color.Blue);
            tmpBmp.SetPixel(light2D.X - 1, light2D.Y - 1, Color.Blue);
            tmpBmp.SetPixel(light2D.X - 1, light2D.Y + 1, Color.Blue);
            tmpBmp.SetPixel(light2D.X, light2D.Y + 1, Color.Blue);
            tmpBmp.SetPixel(light2D.X, light2D.Y - 1, Color.Blue);


            Color[] colorDist = Intence.GetIntence(cubePoints, light);
            DrawCubePolygons(tmpBmp, point2Dconvert, colorDist, cubePoints);

            return tmpBmp;
        }

        //отрисовка полигонов
        public Bitmap DrawCubePolygons(Bitmap tmpBmp, PointF[] point2D, Color[] colors, Point3D[] cubePoints)
        {

            var normals = Normal.CalculateNormals(cubePoints);

            DrawWatchablePolygons(tmpBmp, normals.front,
            new List<PointF> { point2D[3], point2D[7], point2D[6], point2D[2], point2D[3] },
            new List<Color>() { colors[3], colors[7], colors[6], colors[2], colors[3] });

            DrawWatchablePolygons(tmpBmp, normals.right,
            new List<PointF> { point2D[0], point2D[4], point2D[7], point2D[3], point2D[0] },
            new List<Color>() { colors[0], colors[4], colors[7], colors[3], colors[0] });

            DrawWatchablePolygons(tmpBmp, normals.back,
            new List<PointF> { point2D[1], point2D[5], point2D[4], point2D[0], point2D[1] },
            new List<Color>() { colors[1], colors[5], colors[4], colors[0], colors[1] });

            DrawWatchablePolygons(tmpBmp, normals.left,
            new List<PointF> { point2D[2], point2D[6], point2D[5], point2D[1], point2D[2] },
            new List<Color>() { colors[2], colors[6], colors[5], colors[1], colors[2] });

            DrawWatchablePolygons(tmpBmp, normals.top,
            new List<PointF> { point2D[7], point2D[4], point2D[5], point2D[6], point2D[7] },
            new List<Color>() { colors[7], colors[4], colors[5], colors[6], colors[7] });

            DrawWatchablePolygons(tmpBmp, normals.bottom,
            new List<PointF> { point2D[0], point2D[3], point2D[2], point2D[1], point2D[0] },
            new List<Color>() { colors[0], colors[3], colors[2], colors[1], colors[0] });

            return tmpBmp;
        }

        //удаление обратных граней
        public void DrawWatchablePolygons(Bitmap tmpBmp, Point3D normal, List<PointF> points, List<Color> colors)
        {
            var cos = normal.Z / Math.Sqrt(Math.Pow(normal.X, 2) + Math.Pow(normal.Y, 2) + Math.Pow(normal.Z, 2));
            if (cos >= 0 && cos <= 1)
            {
                List<Pixel> pixels = new List<Pixel>();
                for (int i = 0; i < points.Count; i++)
                {
                    pixels.Add(new Pixel((int)points[i].X, (int)points[i].Y, colors[i]));
                }
                FillPolygon.FillPolygons(tmpBmp, pixels);
            }
        }

    }
    public class Normal
    {
        public static Point3D GetNormalVector(Point3D p1, Point3D p2, Point3D p3)
        {
            Point3D p = new Point3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
            Point3D q = new Point3D(p3.X - p2.X, p3.Y - p2.Y, p3.Z - p2.Z);

            Point3D normal = new Point3D(p.Y * q.Z - q.Y * p.Z,
                                         p.Z * q.X - q.Z * p.X,
                                         p.X * q.Y - q.X * p.Y);
            var normalLength = Math.Sqrt(Math.Pow(normal.X, 2) + Math.Pow(normal.Y, 2) + Math.Pow(normal.Z, 2));
            normal = new Point3D(normal.X / normalLength, normal.Y / normalLength, normal.Z / normalLength);
            return normal;
        }

        public static (Point3D front, Point3D back, Point3D top, Point3D bottom, Point3D right, Point3D left) CalculateNormals(Point3D[] cubePoints)
        {
            var frontNormal = GetNormalVector(cubePoints[3], cubePoints[7], cubePoints[6]);
            var backNormal = GetNormalVector(cubePoints[4], cubePoints[0], cubePoints[1]);
            var topNormal = GetNormalVector(cubePoints[7], cubePoints[4], cubePoints[5]);
            var bottomNormal = GetNormalVector(cubePoints[0], cubePoints[3], cubePoints[2]);
            var rightNormal = GetNormalVector(cubePoints[0], cubePoints[4], cubePoints[7]);
            var leftNormal = GetNormalVector(cubePoints[2], cubePoints[6], cubePoints[5]);

            return (frontNormal, backNormal, topNormal, bottomNormal, rightNormal, leftNormal);
        }
    }

    public class Pixel
    {
        public int X;
        public int Y;
        public Color Color;

        public Pixel(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }
    }

    public class FillPolygon
    {
        //проверка
        private static int Sign(int x)
        {
            return (x > 0) ? 1 : (x < 0) ? -1 : 0;
        }

        //линии
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

    public class Intence
    {
        public static Color[] GetIntence(Point3D[] cubePoints, Camera light)
        {
            float[] distances = new float[8];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = DistanceFromPointToCamera(cubePoints[i], light.Position);
            }

            Color[] colorDist = new Color[8];
            float minDist = distances[0];
            float maxDist = distances[0];
            for (int i = 0; i < distances.Length; i++)
            {
                if (maxDist < distances[i]) maxDist = distances[i];
                if (minDist > distances[i]) minDist = distances[i];
            }

            for (int i = 0; i < colorDist.Length; i++)
            {
                var interpolation = (distances[i] - minDist) / (maxDist + 0.00001f - minDist);
                colorDist[i] = Interpolate(Color.Blue, Color.Black, interpolation);
            }
            return colorDist;
        }

        public static float DistanceFromPointToCamera(Point3D a, Point3D b)
        {
            return (float)Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2) + Math.Pow((b.Z - a.Z), 2));
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
