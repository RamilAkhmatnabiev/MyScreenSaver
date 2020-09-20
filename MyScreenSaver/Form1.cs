using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyScreenSaver
{
    public partial class Form1 : Form
    {
        private Star[] stars = new Star[15000];
        private Random random = new Random();
        private Graphics graphics;
        private float k = 10;


        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            foreach (var star in stars)
            {
                DrawStar(star);
                MoveStar(star);
            }
            pictureBox1.Refresh();
        }

        private void MoveStar(Star star)
        {
            star.Z -= k;
            if (star.Z < 1)
            {
                star.X = random.Next(-pictureBox1.Width, pictureBox1.Width);
                star.Y = random.Next(-pictureBox1.Height, pictureBox1.Height);
                star.Z = random.Next(1, pictureBox1.Width);
            }
        }

        private void DrawStar(Star star)
        {
            float starSize = Map(star.Z, 0, pictureBox1.Width, 7, 0);
            //float starSize = 7;
            float x = Map(star.X / star.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width / 2;
            float y = Map(star.Y / star.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height / 2;
            //float z = star.Z + 1;
            graphics.FillEllipse(Brushes.BlueViolet, x, y, starSize, starSize);
        }

        private float Map(float n, float start1, float stop1, float start2, float stop2)
        {
            return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2; // +500
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star()
                {
                    X = random.Next(-pictureBox1.Width, pictureBox1.Width),
                    Y = random.Next(-pictureBox1.Height, pictureBox1.Height),
                    Z = random.Next(1, pictureBox1.Width)
                };
            }
            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                k += 1;
            }
            if (e.KeyData == Keys.Down)
            {
                k -= 1;
            }
            if (k > 50)
            {
                k = 50;
            }
            if (k < 5)
            {
                k = 5;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
    }
}
