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

namespace WindowsFormsApp1
{
    public partial class CalcGame : Form
    {
        private double test = 0;
        ImageList il = new ImageList();
        int x;
        private int count = 50;
        Boolean right = true;
        private int numOne;
        private int numTwo;
        private int result;
        private int basiccalc = 0;
        private static Random rnd = new Random();

        public CalcGame()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (secondsToolStripMenuItem.Checked) //3
            {
                count = 30;
            }
            else if (secondsToolStripMenuItem1.Checked) //5
            {
                count = 50;
            }
            else if (secondsToolStripMenuItem2.Checked) //10
            {
                count = 100;
            }

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            numOne = rnd.Next(21) - 10;
            numTwo = rnd.Next(21) - 10;
            label1.Text = Convert.ToString(numOne);
            label2.Text = Convert.ToString(numTwo);
            switch (basiccalc)
            {
                case 0:
                    result = numOne + numTwo;
                    break;
            }

            button1.Text = Convert.ToString(rnd.Next(41) - 20);
            button2.Text = Convert.ToString(rnd.Next(41) - 20);
            button3.Text = Convert.ToString(rnd.Next(41) - 20);
            button4.Text = Convert.ToString(rnd.Next(41) - 20);

            switch (rnd.Next(3))
            {
                case 0:
                    button1.Text = Convert.ToString(result);
                    break;
                case 1:
                    button2.Text = Convert.ToString(result);
                    break;
                case 2:
                    button3.Text = Convert.ToString(result);
                    break;
                case 3:
                    button4.Text = Convert.ToString(result);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(button1.Text) == result)
            {
                right = true;
                Invalidate();
            }
            else
            {
                right = false;
                Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(button2.Text) == result)
            {
                right = true;
                Invalidate();
            }
            else
            {
                right = false;
                Invalidate();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(button3.Text) == result)
            {
                right = true;
                Invalidate();
            }
            else
            {
                right = false;
                Invalidate();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(button4.Text) == result)
            {
                right = true;
                Invalidate();
            }
            else
            {
                right = false;
                Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = true;
            subtractionToolStripMenuItem.Checked = false;
            multiplicationToolStripMenuItem.Checked = false;
            divisionToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = false;
        }

        private void subtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = false;
            subtractionToolStripMenuItem.Checked = true;
            multiplicationToolStripMenuItem.Checked = false;
            divisionToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = false;
        }

        private void multiplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = false;
            subtractionToolStripMenuItem.Checked = false;
            multiplicationToolStripMenuItem.Checked = true;
            divisionToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = false;
        }

        private void divisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = false;
            subtractionToolStripMenuItem.Checked = false;
            multiplicationToolStripMenuItem.Checked = false;
            divisionToolStripMenuItem.Checked = true;
            randomToolStripMenuItem.Checked = false;
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = false;
            subtractionToolStripMenuItem.Checked = false;
            multiplicationToolStripMenuItem.Checked = false;
            divisionToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = true;
        }

        private void secondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            secondsToolStripMenuItem.Checked = true;
            secondsToolStripMenuItem1.Checked = false;
            secondsToolStripMenuItem2.Checked = false;
            timer1.Interval = 3000;
            count = 30;
        }

        private void secondsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            secondsToolStripMenuItem.Checked = false;
            secondsToolStripMenuItem1.Checked = true;
            secondsToolStripMenuItem2.Checked = false;
            timer1.Interval = 5000;
            count = 50;
        }

        private void secondsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            secondsToolStripMenuItem.Checked = false;
            secondsToolStripMenuItem1.Checked = false;
            secondsToolStripMenuItem2.Checked = true;
            timer1.Interval = 10000;
            count = 100;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (secondsToolStripMenuItem.Checked) //3
            {
                x = (int) (((float) count / (float) 30) * 360);
            }
            else if (secondsToolStripMenuItem1.Checked) //5
            {
                x = (int) (((float) count / (float) 50) * 360);
            }
            else if (secondsToolStripMenuItem2.Checked) //10
            {
                x = (int) (((float) count / (float) 100) * 360);
            }

            e.Graphics.FillPie(Brushes.Black, 240, 75, 30, 30, 0, x);
            e.Graphics.FillEllipse(right ? Brushes.GreenYellow : Brushes.Red, 240, 130, 30, 30);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            count--;
            Invalidate();

        }

        private void CalcGame_Load(object sender, EventArgs e)
        {
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;


            Graphics theGraphics = Graphics.FromHwnd(this.Handle);
            il.ImageSize = new Size(100, 50);
            il.Images.Add(Image.FromFile(@"C:\Users\lhassler\Desktop\a.jpg"));
            il.Images.Add(Image.FromFile(@"C:\Users\lhassler\Desktop\b.jpg"));
            il.TransparentColor = Color.White;

            Bitmap bmp = new Bitmap(100, 100);
            Bitmap b = new Bitmap(@"C:\Users\lhassler\Desktop\b.jpg");
            b = new Bitmap(b, 50, 50);

            Graphics g = Graphics.FromImage(bmp);
            g.DrawRectangle(Pens.Black, 0, 0, 99, 99);
            g.DrawImage(b, new Point(0, 0));

            pictureBox1.Image = bmp;
            timer3.Enabled = false;
            Bitmap pic = Mandelbrotset(pictureBox1, 2, -2, 2, -2);
            pictureBox1.Image = pic;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Bitmap pic = Mandelbrotset(pictureBox1, 0.45+test, 0.48+test, 0.54+test, 0.52+test);
            pictureBox1.Image = pic;
            test += 0.01;

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(100, 100);
            Bitmap b = new Bitmap(@"C:\Users\lhassler\Desktop\b.jpg");
            b = new Bitmap(b, 40, 40);

            Graphics g = Graphics.FromImage(bmp);

            g.TranslateTransform(50, 50);
            g.RotateTransform(count);
            g.DrawImage(b, new Point(-20, -20));
            g.FillRectangle(Brushes.Aquamarine, 20, 20, 20, 20);
            g.ResetTransform();
            g.DrawRectangle(Pens.Black, 0, 0, 99, 99);

            pictureBox1.Image = bmp;

        }

        static Bitmap Mandelbrotset(PictureBox pictureBox1, double Xmax, double Xmin, double Ymax, double Ymin)
        {
            double pXmax = Xmax;
            double pYmax = Ymax;
            double pXmin = Xmin;
            double pYmin = Ymin;
            Bitmap pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            double zx = 0;
            double zy = 0;
            double cx = 0;
            double cy = 0;
            double xzoom = ((Xmax - Xmin) / Convert.ToDouble(pic.Width));
            double yzoom = ((Ymax - Ymin) / Convert.ToDouble(pic.Height));
            double tempzx = 0;

            int loopgo = 0;
            for (int x = 0; x < pic.Width; x++)
            {
                cx = (xzoom * x) - Math.Abs(Xmin);
                for (int y = 0; y < pic.Height; y++)
                {
                    zx = 0;
                    zy = 0;
                    cy = (yzoom * y) - Math.Abs(Ymin);
                    loopgo = 0;

                    while (zx * zx + zy * zy <= 4 && loopgo < 1000)
                    {
                        loopgo++;
                        tempzx = zx;
                        zx = (zx * zx) - (zy * zy) + cx;
                        zy = (2 * tempzx * zy) + cy;
                    }

                    if (loopgo != 1000)
                        pic.SetPixel(x, y, Color.FromArgb(loopgo % 128 * 2, loopgo % 32 * 7, loopgo % 16 * 14));
                    else
                        pic.SetPixel(x, y, Color.Black);

                }
            }

            return pic;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            int x;
            int y;
            x = Math.Abs(pictureBox1.Width/2 - e.X);
            y = Math.Abs(pictureBox1.Width / 2 - e.Y);
            Console.WriteLine(x);
            Console.WriteLine(y);
            Bitmap pic = Mandelbrotset(pictureBox1, x*0.1,x*-0.1, y * 0.1, y * -0.1);
            pictureBox1.Image = pic;
            */
        }

        private void CalcGame_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;
        }
    }
}