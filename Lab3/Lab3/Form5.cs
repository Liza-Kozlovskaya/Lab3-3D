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
            rotationX = CalculateSpeed(rotationX, speedX);
            rotationY = CalculateSpeed(rotationY, speedY);
            rotationZ = CalculateSpeed(rotationZ, speedZ);

            Render(rotationX, rotationY, rotationZ);
        }
        private double CalculateSpeed(double Rotation, double speed)
        {
            double newRotation = speed;
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
}
