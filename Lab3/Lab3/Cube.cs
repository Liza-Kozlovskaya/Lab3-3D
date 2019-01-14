using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Cube
    {
        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }

        double xRotation;
        double yRotation;
        double zRotation;
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

        //Куб в перспективной проекции
        public Bitmap drawCubePersp(Point drawOrigin)
        {
            PointF[] point2Dconvert = new PointF[8];
            Point3D[] cubePoints = fillCubeVertices(Width, Height, Depth);

            //изменение положения точек при повороте
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

        //Куб в параллельной проекции
        public Bitmap drawCubeParal(Point drawOrigin)
        {
            //точки куба, которые конвертируем в 2д
            PointF[] point2Dconvert = new PointF[8];
            Point3D[] cubePoints = fillCubeVertices(Width, Height, Depth);

            //изменение положения точек при повороте
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

        //точки куба в 3д
        public Point3D[] fillCubeVertices(int width, int height, int depth)
        {
            Point3D[] verts = new Point3D[8];

            //передняя сторона
            verts[0] = new Point3D(-width / 2, -height / 2, -depth / 2);
            verts[1] = new Point3D(-width / 2, height / 2, -depth / 2);
            verts[2] = new Point3D(width / 2, height / 2, -depth / 2);
            verts[3] = new Point3D(width / 2, -height / 2, -depth / 2);

            //задняя сторона          
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
            Bitmap tmpBmp = new Bitmap(bounds.Width, bounds.Height);

            //создаём источник света(точка, как камера)
            Camera light = new Camera();
            //позиция источника света на pictureBox 
            light.Position = new Point3D(Width / 2, Height / 2 + 50, Depth / 2);

            //конвертированная в 2д точка источника света
            //параллель
            Point light2D = new Point();
            light2D.X = (int)(light.Position.X + drawOrigin.X);
            light2D.Y = (int)(light.Position.Y + drawOrigin.Y);

            ////отрисовка источника света
            //tmpBmp.SetPixel(light2D.X, light2D.Y, Color.Blue);
            //tmpBmp.SetPixel(light2D.X + 1, light2D.Y, Color.Blue);
            //tmpBmp.SetPixel(light2D.X - 1, light2D.Y, Color.Blue);
            //tmpBmp.SetPixel(light2D.X + 1, light2D.Y + 1, Color.Blue);
            //tmpBmp.SetPixel(light2D.X + 1, light2D.Y - 1, Color.Blue);
            //tmpBmp.SetPixel(light2D.X - 1, light2D.Y - 1, Color.Blue);
            //tmpBmp.SetPixel(light2D.X - 1, light2D.Y + 1, Color.Blue);
            //tmpBmp.SetPixel(light2D.X, light2D.Y + 1, Color.Blue);
            //tmpBmp.SetPixel(light2D.X, light2D.Y - 1, Color.Blue);

            //поиск интенсивности
            Color[] colorDist = GetGuroColors(cubePoints, camera1.Position, light.Position, Color.White);
            DrawGuro(tmpBmp, point2Dconvert, colorDist, cubePoints);
            return tmpBmp;
        }

        //Грани
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


        //отрисовка полигонов
        public Bitmap DrawGuro(Bitmap tmpBmp, PointF[] point2D, Color[] colors, Point3D[] cubePoints)
        {

            var normals = Normal.CalculateNormals(cubePoints);

            DrawWatchablePolygons(tmpBmp, normals.Front,
            new List<PointF> { point2D[3], point2D[7], point2D[6], point2D[2], point2D[3] },
            new List<Color>() { colors[3], colors[7], colors[6], colors[2], colors[3] });

            DrawWatchablePolygons(tmpBmp, normals.Right,
            new List<PointF> { point2D[0], point2D[4], point2D[7], point2D[3], point2D[0] },
            new List<Color>() { colors[0], colors[4], colors[7], colors[3], colors[0] });

            DrawWatchablePolygons(tmpBmp, normals.Back,
            new List<PointF> { point2D[1], point2D[5], point2D[4], point2D[0], point2D[1] },
            new List<Color>() { colors[1], colors[5], colors[4], colors[0], colors[1] });

            DrawWatchablePolygons(tmpBmp, normals.Left,
            new List<PointF> { point2D[2], point2D[6], point2D[5], point2D[1], point2D[2] },
            new List<Color>() { colors[2], colors[6], colors[5], colors[1], colors[2] });

            DrawWatchablePolygons(tmpBmp, normals.Top,
            new List<PointF> { point2D[7], point2D[4], point2D[5], point2D[6], point2D[7] },
            new List<Color>() { colors[7], colors[4], colors[5], colors[6], colors[7] });

            DrawWatchablePolygons(tmpBmp, normals.Bottom,
            new List<PointF> { point2D[0], point2D[3], point2D[2], point2D[1], point2D[0] },
            new List<Color>() { colors[0], colors[3], colors[2], colors[1], colors[0] });

            return tmpBmp;
        }

        //
        public static Color[] GetGuroColors(Point3D[] cubePoints, Point3D camera, Point3D light, Color baseColor)
        {
            Color[] colors = new Color[8];
            var intences = Normal.GetIntense(cubePoints, camera, light);
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = GetInterpolateColor(Color.White, Color.Black, intences[i]);
            }
            return colors;
        }
        public static Color GetInterpolateColor(Color color1, Color color2, double interpolation)
        {
            Color newColor;
            int R = Clip((int)(color1.R * (1 - interpolation) + color2.R * interpolation));
            int G = Clip((int)(color1.G * (1 - interpolation) + color2.G * interpolation));
            int B = Clip((int)(color1.B * (1 - interpolation) + color2.B * interpolation));
            newColor = Color.FromArgb(R, G, B);
            return newColor;
        }

        private static int Clip(int num)
        {
            return num <= 0 ? 0 : (num >= 255 ? 255 : num);
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
}
