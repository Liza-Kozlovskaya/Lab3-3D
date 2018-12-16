using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab3.Form1.Math3D;

namespace Lab3
{
    public partial class Form1 : Form
    {

        Cube cube;
        Cube cube2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //таймеры
            timer1.Interval = 20;
            timer2.Interval = 20;
            timer3.Interval = 20;

            cube = new Cube(100, new Math3D.Point3D(0, 0, 0));
            cube2 = new Cube(100);

            //куб рисуется в центре бокса
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);
        }

        private void tX_Scroll_1(object sender, EventArgs e)
        {
            cube.RotateX = tX.Value;
            cube2.RotateX = tX.Value;

            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);
        }

        private void tY_Scroll_1(object sender, EventArgs e)
        {
            cube.RotateY = tY.Value;
            cube2.RotateY = tY.Value;

            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);

        }

        private void tZ_Scroll_1(object sender, EventArgs e)
        {
            cube.RotateZ = tZ.Value;
            cube2.RotateZ = tZ.Value;

            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cube.RotateX = 0;
            cube2.RotateX = 0;
            tX.Value = 0;

            cube.RotateY = 0;
            cube2.RotateY = 0;
            tY.Value = 0;

            cube.RotateZ = 0;
            cube2.RotateZ = 0;
            tZ.Value = 0;

            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;

            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            cube.RotateX = (int)numericUpDown1.Value;
            cube2.RotateX = (int)numericUpDown1.Value;

            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            cube.RotateY = (int)numericUpDown2.Value;
            cube2.RotateY = (int)numericUpDown2.Value;
            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            cube.RotateZ = (int)numericUpDown3.Value;
            cube2.RotateZ = (int)numericUpDown3.Value;
            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);
        }

        //вращение
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Start();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer3.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cube.RotateX++;
            cube2.RotateX++;
            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            cube.RotateY++;
            cube2.RotateY++;
            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            cube.RotateZ++;
            cube2.RotateZ++;
            //центральная точка
            Point origin = new Point(picCube.Width / 2, picCube.Height / 2);
            Point origin2 = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            //куб рисуется в центре бокса
            picCube.Image = cube.drawCube(origin);
            pictureBox1.Image = cube2.drawCubeParallel(origin2);
        }

        //стоп
        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            timer3.Stop();
        }


        internal class Math3D
        {
            //точки 3д состоят из 3 координат
            public class Point3D
            {
                public float X;
                public float Y;
                public float Z;

                public Point3D(int x, int y, int z)
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                public Point3D(float x, float y, float z)
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                public Point3D(double x, double y, double z)
                {
                    X = (float)x;
                    Y = (float)y;
                    Z = (float)z;
                }

                public Point3D()
                {
                }

                public override string ToString()
                {
                    return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
                }
            }

            //точка, откуда смотрят на куб
            public class Camera
            {
                public Point3D Position = new Point3D();
            }


            public static Point3D RotateX(Point3D point3D, double degrees)
            {
                //Матрица поворота по Х
                //[ 1    0        0   ]
                //[ 0   cos(x)  sin(x)]
                //[ 0   -sin(x) cos(x)]

                double cDegrees = (Math.PI * degrees) / 180.0f; // Переводим градусы в радианы
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);

                double y = (point3D.Y * cosDegrees) + (point3D.Z * sinDegrees);
                double z = (point3D.Y * -sinDegrees) + (point3D.Z * cosDegrees);

                return new Point3D(point3D.X, y, z);
            }

            public static Point3D RotateY(Point3D point3D, double degrees)
            {
                //Матрица поворота по У
                //[ cos(x)   0    sin(x)]
                //[   0      1      0   ]
                //[-sin(x)   0    cos(x)]

                double cDegrees = (Math.PI * degrees) / 180.0; // Переводим градусы в радианы
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);

                double x = (point3D.X * cosDegrees) + (point3D.Z * sinDegrees);
                double z = (point3D.X * -sinDegrees) + (point3D.Z * cosDegrees);

                return new Point3D(x, point3D.Y, z);
            }

            public static Point3D RotateZ(Point3D point3D, double degrees)
            {
                //Матрица поворота по Z
                //[ cos(x)  sin(x) 0]
                //[ -sin(x) cos(x) 0]
                //[    0     0     1]

                double cDegrees = (Math.PI * degrees) / 180.0; // Переводим градусы в радианы
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);

                double x = (point3D.X * cosDegrees) + (point3D.Y * sinDegrees);
                double y = (point3D.X * -sinDegrees) + (point3D.Y * cosDegrees);

                return new Point3D(x, y, point3D.Z);
            }

            public static Point3D Translate(Point3D points3D, Point3D oldOrigin, Point3D newOrigin)
            {
                //Перемещение 3д точки
                Point3D difference = new Point3D(newOrigin.X - oldOrigin.X, newOrigin.Y - oldOrigin.Y, newOrigin.Z - oldOrigin.Z);
                points3D.X += difference.X;
                points3D.Y += difference.Y;
                points3D.Z += difference.Z;
                return points3D;
            }

            //поворот всех точек на основе массива трёхмерных точек
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


        internal class Cube
        {
           
            public int width = 0;
            public int height = 0;
            public int depth = 0;

            double xRotation = 0.0;
            double yRotation = 0.0;
            double zRotation = 0.0;

            public Math3D.Camera camera1 = new Math3D.Camera();
            Math3D.Point3D cubeOrigin;

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

            public Cube(int side)
            {
                width = side;
                height = side;
                depth = side;
                cubeOrigin = new Math3D.Point3D(width / 2, height / 2, depth / 2);
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


            //отрисовка куба
            public Bitmap drawCube(Point drawOrigin)
            {
                PointF[] point3D = new PointF[8]; 
                Point tmpOrigin = new Point(0, 0);

                Math3D.Point3D point0 = new Math3D.Point3D(0, 0, 0);

                Math3D.Point3D[] cubePoints = fillCubeVertices(width, height, depth);

                //вычисляем положение камеры при вращении           
                Math3D.Point3D anchorPoint = (Math3D.Point3D)cubePoints[4]; //опорная точка

                //перемещение точек
                cubePoints = Math3D.RotateX(cubePoints, xRotation); 
                cubePoints = Math3D.RotateY(cubePoints, yRotation); 
                cubePoints = Math3D.RotateZ(cubePoints, zRotation); 
                                                                    

                //Перевод 3д точек в 2д
                Math3D.Point3D vec;
                for (int i = 0; i < point3D.Length; i++)
                {
                    vec = cubePoints[i];
                    point3D[i].X = (int)((vec.X * camera1.Position.Z - vec.Z * camera1.Position.X) / (camera1.Position.Z - vec.Z) + drawOrigin.X);
                    point3D[i].Y = (int)((vec.Y * camera1.Position.Z - vec.Z * camera1.Position.Y) / (camera1.Position.Z - vec.Z) + drawOrigin.Y);
                }

               
                //расчёт точек
                Rectangle bounds = getBounds(point3D);
                bounds.Width += drawOrigin.X;
                bounds.Height += drawOrigin.Y;

                Bitmap tmpBmp = new Bitmap(bounds.Width, bounds.Height);


                float[] dist = new float[8];
                for(int i = 0; i < dist.Length; i++)
                {
                    dist[i] = Dist(cubePoints[i], camera1.Position);
                }

                Color[] colorDist = new Color[8];
                float minDist = dist[0];
                float maxDist = dist[0];
                for(int i = 0; i < dist.Length; i++)
                {
                    if (maxDist < dist[i])
                        maxDist = dist[i];
                    if (minDist > dist[i])
                        minDist = dist[i];
                }

                for(int i=0; i< colorDist.Length; i++)
                {
                    colorDist[i] = Drawing.Interpolate(Color.Black, Color.Red, ((dist[i] - minDist) / (maxDist - minDist)));
                }

                //задняя грань
                Drawing.BresenhamLine(tmpBmp, colorDist[0], colorDist[1], (int)point3D[0].X, (int)point3D[0].Y, (int)point3D[1].X, (int)point3D[1].Y );
                Drawing.BresenhamLine(tmpBmp, colorDist[1], colorDist[2], (int)point3D[1].X, (int)point3D[1].Y, (int)point3D[2].X, (int)point3D[2].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[2], colorDist[3], (int)point3D[2].X, (int)point3D[2].Y, (int)point3D[3].X, (int)point3D[3].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[3], colorDist[0], (int)point3D[3].X, (int)point3D[3].Y, (int)point3D[0].X, (int)point3D[0].Y );

                //передняя грань
                Drawing.BresenhamLine(tmpBmp, colorDist[4], colorDist[5], (int)point3D[4].X, (int)point3D[4].Y, (int)point3D[5].X, (int)point3D[5].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[5], colorDist[6], (int)point3D[5].X, (int)point3D[5].Y, (int)point3D[6].X, (int)point3D[6].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[6], colorDist[7], (int)point3D[6].X, (int)point3D[6].Y, (int)point3D[7].X, (int)point3D[7].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[7], colorDist[4], (int)point3D[7].X, (int)point3D[7].Y, (int)point3D[4].X, (int)point3D[4].Y);

                //боковые грани
                Drawing.BresenhamLine(tmpBmp, colorDist[0], colorDist[4], (int)point3D[0].X, (int)point3D[0].Y, (int)point3D[4].X, (int)point3D[4].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[1], colorDist[5], (int)point3D[1].X, (int)point3D[1].Y, (int)point3D[5].X, (int)point3D[5].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[2], colorDist[6], (int)point3D[2].X, (int)point3D[2].Y, (int)point3D[6].X, (int)point3D[6].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[3], colorDist[7], (int)point3D[3].X, (int)point3D[3].Y, (int)point3D[7].X, (int)point3D[7].Y);


                return tmpBmp;
            }
            public float Dist(Math3D.Point3D a, Math3D.Point3D b)
            {
                return (float)Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y) + (b.Z - a.Z) * (b.Z - a.Z));
            }

            //отрисовка куба в параллельной проекции
            public Bitmap drawCubeParallel(Point drawOrigin)
            {
                PointF[] point3D = new PointF[8];
                Point tmpOrigin = new Point(0, 0);

                Math3D.Point3D point0 = new Math3D.Point3D(0, 0, 0);

                Math3D.Point3D[] cubePoints = fillCubeVertices(width, height, depth);

                //перемещение точек
                cubePoints = Math3D.RotateX(cubePoints, xRotation);
                cubePoints = Math3D.RotateY(cubePoints, yRotation);
                cubePoints = Math3D.RotateZ(cubePoints, zRotation);

                //Перевод 3д точек в 2д
                //Параллельная проекция
                Math3D.Point3D vec;
                for (int i = 0; i < point3D.Length; i++)
                {
                    vec = cubePoints[i];
                    point3D[i].X = (int)(vec.X /*+ vec.Z / 2*/ + drawOrigin.X);
                    point3D[i].Y = (int)(vec.Y /*+ vec.Z / 2*/ + drawOrigin.Y);
                }

                //расчёт точек
                Rectangle bounds = getBounds(point3D);
                bounds.Width += drawOrigin.X;
                bounds.Height += drawOrigin.Y;

                Bitmap tmpBmp = new Bitmap(bounds.Width, bounds.Height);


                float[] dist = new float[8];
                for (int i = 0; i < dist.Length; i++)
                {
                    dist[i] = Dist(cubePoints[i], camera1.Position);
                }

                Color[] colorDist = new Color[8];
                float minDist = dist[0];
                float maxDist = dist[0];
                for (int i = 0; i < dist.Length; i++)
                {
                    if (maxDist < dist[i])
                        maxDist = dist[i];
                    if (minDist > dist[i])
                        minDist = dist[i];
                }

                for (int i = 0; i < colorDist.Length; i++)
                {
                    colorDist[i] = Drawing.Interpolate(Color.Black, Color.Red, ((dist[i] - minDist) / (maxDist - minDist)));
                }

                //задняя грань
                Drawing.BresenhamLine(tmpBmp, colorDist[0], colorDist[1], (int)point3D[0].X, (int)point3D[0].Y, (int)point3D[1].X, (int)point3D[1].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[1], colorDist[2], (int)point3D[1].X, (int)point3D[1].Y, (int)point3D[2].X, (int)point3D[2].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[2], colorDist[3], (int)point3D[2].X, (int)point3D[2].Y, (int)point3D[3].X, (int)point3D[3].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[3], colorDist[0], (int)point3D[3].X, (int)point3D[3].Y, (int)point3D[0].X, (int)point3D[0].Y);

                //передняя грань
                Drawing.BresenhamLine(tmpBmp, colorDist[4], colorDist[5], (int)point3D[4].X, (int)point3D[4].Y, (int)point3D[5].X, (int)point3D[5].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[5], colorDist[6], (int)point3D[5].X, (int)point3D[5].Y, (int)point3D[6].X, (int)point3D[6].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[6], colorDist[7], (int)point3D[6].X, (int)point3D[6].Y, (int)point3D[7].X, (int)point3D[7].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[7], colorDist[4], (int)point3D[7].X, (int)point3D[7].Y, (int)point3D[4].X, (int)point3D[4].Y);

                //боковые грани
                Drawing.BresenhamLine(tmpBmp, colorDist[0], colorDist[4], (int)point3D[0].X, (int)point3D[0].Y, (int)point3D[4].X, (int)point3D[4].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[1], colorDist[5], (int)point3D[1].X, (int)point3D[1].Y, (int)point3D[5].X, (int)point3D[5].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[2], colorDist[6], (int)point3D[2].X, (int)point3D[2].Y, (int)point3D[6].X, (int)point3D[6].Y);
                Drawing.BresenhamLine(tmpBmp, colorDist[3], colorDist[7], (int)point3D[3].X, (int)point3D[3].Y, (int)point3D[7].X, (int)point3D[7].Y);


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

