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
    public partial class Form2 : Form
    {
        Cube cube1;
        Cube cube2;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cube1 = new Cube(100, new Math3D.Point3D(350, 500, 500));
            cube2 = new Cube(100, new Math3D.Point3D(0, 0, 0));
            //куб рисуется в центре бокса
            Point origin = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            pictureBox1.Image = cube1.drawCubePespect(origin);
            pictureBox2.Image = cube2.drawCubeParallel(origin);
        }

        internal class Math3D
        {
            //точки 3д состоят из 3 координат
            public class Point3D
            {
                public double X;
                public double Y;
                public double Z;

                public Point3D(int x, int y, int z)
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                public Point3D(float x, float y, float z)
                {
                    X = (double)x;
                    Y = (double)y;
                    Z = (double)z;
                }

                public Point3D(double x, double y, double z)
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                public Point3D()
                {
                }

            }

            //точка, откуда смотрят на куб
            public class Camera
            {
                public Point3D Position = new Point3D();
            }

        }
            internal class Cube
            {

                public int width = 0;
                public int height = 0;
                public int depth = 0;

                public Math3D.Camera camera1 = new Math3D.Camera();
                Math3D.Point3D cubeOrigin;

                public Cube(int side)
                {
                    width = side;
                    height = side;
                    depth = side;
                    cubeOrigin = new Math3D.Point3D(width , height , depth );
                    camera1.Position = new Math3D.Point3D(cubeOrigin.X, cubeOrigin.Y, 1000);

                }

                public Cube(int side, Math3D.Point3D origin)
                {
                    width = side;
                    height = side;
                    depth = side;
                    cubeOrigin = origin;
                    camera1.Position = new Math3D.Point3D(cubeOrigin.X, cubeOrigin.Y, 1000);

                }
                //стороны куба
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

                //отрисовка куба в перспективной проекции
                public Bitmap drawCubePespect(Point drawOrigin)
                {
                    PointF[] point3D = new PointF[8];
                    Point tmpOrigin = new Point(0, 0);

                    Math3D.Point3D point0 = new Math3D.Point3D(0, 0, 0);

                    Math3D.Point3D[] cubePoints = fillCubeVertices(width, height, depth);

                    //Перевод 3д точек в 2д
                    //Перспективная проекция
                     Math3D.Point3D vec;
                     for (int i = 0; i < point3D.Length; i++)
                       {
                          vec = cubePoints[i];
                    //double k = -(camera1.Position.Z);
                    //point3D[i].X = (int)((k * vec.X) / (vec.Z + k)) + drawOrigin.X;
                    //point3D[i].Y = (int)((k * vec.Y) / (vec.Z + k)) + drawOrigin.Y;

                    point3D[i].X = (int)((vec.X * camera1.Position.Z - vec.Z * camera1.Position.X) / (camera1.Position.Z - vec.Z) + drawOrigin.X);
                    point3D[i].Y = (int)((vec.Y * camera1.Position.Z - vec.Z * camera1.Position.Y) / (camera1.Position.Z - vec.Z) + drawOrigin.Y);

                }

                    //расчёт точек
                    Rectangle bounds = getBounds(point3D);
                    bounds.Width += drawOrigin.X;
                    bounds.Height += drawOrigin.Y;

                    Bitmap tmpBmp = new Bitmap(bounds.Width, bounds.Height);

                    //задняя грань
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[0].X, (int)point3D[0].Y, (int)point3D[1].X, (int)point3D[1].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[1].X, (int)point3D[1].Y, (int)point3D[2].X, (int)point3D[2].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[2].X, (int)point3D[2].Y, (int)point3D[3].X, (int)point3D[3].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[3].X, (int)point3D[3].Y, (int)point3D[0].X, (int)point3D[0].Y);

                    //передняя грань
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[4].X, (int)point3D[4].Y, (int)point3D[5].X, (int)point3D[5].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[5].X, (int)point3D[5].Y, (int)point3D[6].X, (int)point3D[6].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[6].X, (int)point3D[6].Y, (int)point3D[7].X, (int)point3D[7].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[7].X, (int)point3D[7].Y, (int)point3D[4].X, (int)point3D[4].Y);

                    //боковые грани
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[0].X, (int)point3D[0].Y, (int)point3D[4].X, (int)point3D[4].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[1].X, (int)point3D[1].Y, (int)point3D[5].X, (int)point3D[5].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[2].X, (int)point3D[2].Y, (int)point3D[6].X, (int)point3D[6].Y);
                    Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[3].X, (int)point3D[3].Y, (int)point3D[7].X, (int)point3D[7].Y);


                    return tmpBmp;
                }

            //отрисовка куба в параллельной проекции
            public Bitmap drawCubeParallel(Point drawOrigin)
            {
                PointF[] point3D = new PointF[8];
                Point tmpOrigin = new Point(0, 0);

                Math3D.Point3D point0 = new Math3D.Point3D(0, 0, 0);

                Math3D.Point3D[] cubePoints = fillCubeVertices(width, height, depth);

                //Перевод 3д точек в 2д
                //Параллельная проекция
                Math3D.Point3D vec;
                for (int i = 0; i < point3D.Length; i++)
                {
                    vec = cubePoints[i];
                    point3D[i].X = (int)(vec.X + vec.Z / 2 + drawOrigin.X);
                    point3D[i].Y = (int)(vec.Y + vec.Z / 2 + drawOrigin.Y);
                }

                //расчёт точек
                Rectangle bounds = getBounds(point3D);
                bounds.Width += drawOrigin.X;
                bounds.Height += drawOrigin.Y;

                Bitmap tmpBmp = new Bitmap(bounds.Width, bounds.Height);

                //задняя грань
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[0].X, (int)point3D[0].Y, (int)point3D[1].X, (int)point3D[1].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[1].X, (int)point3D[1].Y, (int)point3D[2].X, (int)point3D[2].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[2].X, (int)point3D[2].Y, (int)point3D[3].X, (int)point3D[3].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[3].X, (int)point3D[3].Y, (int)point3D[0].X, (int)point3D[0].Y);

                //передняя грань
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[4].X, (int)point3D[4].Y, (int)point3D[5].X, (int)point3D[5].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[5].X, (int)point3D[5].Y, (int)point3D[6].X, (int)point3D[6].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[6].X, (int)point3D[6].Y, (int)point3D[7].X, (int)point3D[7].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[7].X, (int)point3D[7].Y, (int)point3D[4].X, (int)point3D[4].Y);

                //боковые грани
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[0].X, (int)point3D[0].Y, (int)point3D[4].X, (int)point3D[4].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[1].X, (int)point3D[1].Y, (int)point3D[5].X, (int)point3D[5].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[2].X, (int)point3D[2].Y, (int)point3D[6].X, (int)point3D[6].Y);
                Drawing.BresenhamLine(tmpBmp, Color.Black, Color.Black, (int)point3D[3].X, (int)point3D[3].Y, (int)point3D[7].X, (int)point3D[7].Y);


                return tmpBmp;
            }



            public static Math3D.Point3D[] fillCubeVertices(int width, int height, int depth)
                {
                    Math3D.Point3D[] verts = new Math3D.Point3D[8];

                    verts[0] = new Math3D.Point3D(-width / 2, -height / 2, -depth / 2);
                    verts[1] = new Math3D.Point3D(-width / 2, height / 2, -depth / 2);
                    verts[2] = new Math3D.Point3D(width / 2, height / 2, -depth / 2);
                    verts[3] = new Math3D.Point3D(width / 2, -height / 2, -depth / 2);

                    verts[4] = new Math3D.Point3D(-width / 2, -height / 2, depth / 2);
                    verts[5] = new Math3D.Point3D(-width / 2, height / 2, depth / 2);
                    verts[6] = new Math3D.Point3D(width / 2, height / 2, depth / 2);
                    verts[7] = new Math3D.Point3D(width / 2, -height / 2, depth / 2);
                    return verts;
                }
            }
    }
}
