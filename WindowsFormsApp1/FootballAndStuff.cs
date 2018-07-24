using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FootballAndStuff : Form
    {
        Stopwatch sw = new Stopwatch();
        String timeString;
        private Point pt, ptguess;
        private int randomNumber = new Random().Next(100);
        private int bounceY = 30;
        private int bounceYCount = 5;

        private int x = 0;
        private int y = 0;
        private Boolean reverse = false;

        private int degree = 0;

        private int ballx = 5;
        private int bally = 1;
        private int ballsave = 10;


        public FootballAndStuff()
        {
            InitializeComponent();
            panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            panel1.Left = hScrollBar1.Value*8;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)

        {

            Pen blackpen = new Pen(Color.Black, 3);

            Graphics g = e.Graphics;

            g.DrawLine(blackpen, 0, 0, 200, 200);

            g.DrawRectangle(blackpen, 20,20,20,20);

            g.Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sw.Reset();
            sw.Start();
            StringBuilder sb = new StringBuilder(Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                sb.Append("x");
            }
            sw.Stop();
            textBox3.Text = Convert.ToString(sw.Elapsed);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            sw.Reset();
            sw.Start();
            string a = "";
            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
            {
                a += "x";
            }
            sw.Stop();
            timeString = Convert.ToString(sw.Elapsed);
        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            textBox4.Text = timeString;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                panel1.Top -= 10;
            }
        }

        private void hScrollBar1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                panel1.Top -= 10;
            }
            if (e.KeyCode == Keys.Down)
            {
                panel1.Top += 10;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pt.X = new Random().Next(100);
            pt.Y = new Random().Next(50);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar1.Value.Equals(pt.X) && trackBar2.Value.Equals(pt.Y))
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
            if (!ptguess.IsEmpty)
            {
                if( (pt.X - Math.Abs(trackBar1.Value)) < (pt.X - Math.Abs(ptguess.X)) )
                {
                    textBox5.Text = Convert.ToString(Math.Abs(pt.X - Math.Abs(trackBar1.Value)));
                }
                else
                {
                    textBox5.Text = Convert.ToString(Math.Abs(pt.X - Math.Abs(trackBar1.Value)));
                }
            }
            ptguess.X = trackBar1.Value;

        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Convert.ToInt32(textBox5.Text) > randomNumber)
                {
                    textBox6.Text = "lower";
                }
                else if (Convert.ToInt32(textBox5.Text) < randomNumber)
                {
                    textBox6.Text = "higher";
                }
                else if (Convert.ToInt32(textBox5.Text).Equals(randomNumber))
                {
                    textBox6.Text = "correct";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            hScrollBar1.Top += new Random().Next(50)-22;
            hScrollBar1.Left += new Random().Next(50) - 22;
            textBox1.Top += new Random().Next(50) - 22;
            textBox1.Left += new Random().Next(50) - 22;
            textBox2.Top += new Random().Next(50) - 22;
            textBox2.Left += new Random().Next(50) - 22;
            textBox3.Top += new Random().Next(50) - 22;
            textBox3.Left += new Random().Next(50) - 22;
            textBox4.Top += new Random().Next(50) - 22;
            textBox4.Left += new Random().Next(50) - 22;
            textBox5.Top += new Random().Next(50) - 22;
            textBox5.Left += new Random().Next(50) - 22;
            textBox6.Top += new Random().Next(50) - 22;
            textBox6.Left += new Random().Next(50) - 22;
            button1.Top += new Random().Next(50) - 22;
            button1.Left += new Random().Next(50) - 22;
            button2.Top += new Random().Next(50) - 22;
            button2.Left += new Random().Next(50) - 22;
            hScrollBar1.Top += new Random().Next(50) - 22;
            hScrollBar1.Left += new Random().Next(50) - 22;
            panel1.Top += new Random().Next(50) - 22;
            panel1.Left += new Random().Next(50) - 22;
            trackBar1.Top += new Random().Next(50) - 22;
            trackBar1.Left += new Random().Next(50) - 22;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            colorDialog1.ShowDialog();
            button3.BackColor = colorDialog1.Color;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (button3.Top < 330)
            {
                if (bounceY < 0 && bounceYCount > 0)
                {
                    button3.Top += bounceY;
                    bounceYCount--;
                }
                else if (bounceY > 0 && bounceYCount > 0)
                {
                    button3.Top += bounceY;
                }
                else if (bounceYCount == 0)
                {
                    if (bounceY != 0)
                    {
                        if (bounceY > 0)
                        {
                            bounceY = (bounceY - 1) * -1;
                        }
                        else
                        {
                            bounceY = (bounceY + 1) * -1;
                        }
                        
                        bounceYCount = 5;
                    }   
                }
                else
                {
                    button3.Top += 1;
                }
            }
            else
            {
                if (bounceY != 0)
                {
                    if (bounceY > 0)
                    {
                        bounceY = (bounceY - 1) * -1;
                    }
                    else
                    {
                        bounceY = (bounceY + 1) * -1;
                    }
                    button3.Top += bounceY;
                    bounceYCount = 5;
                }
                else
                {
                    timer2.Enabled = false;
                    button3.Width += 100;
                }
            } 
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox6.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MinusXHoch2 myForm2 = new MinusXHoch2();
            myForm2.ShowDialog();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphicsObj;

            graphicsObj = this.CreateGraphics();

            Pen myPen = new Pen(System.Drawing.Color.Black, 3);

            graphicsObj.DrawLine(myPen, 150, 300, 150-x, 300-y );

            graphicsObj.DrawLine(myPen, 300, 200, 300+((int)(Math.Cos((double)degree / (double)180 * Math.PI) *100)), 200+(int)(Math.Sin((double)degree / (double)180 * Math.PI) *100));
            graphicsObj.DrawLine(myPen, 300, 200, 300 + ((int)(Math.Cos((double)(degree +90) / (double)180 * Math.PI) * 50)), 200 + (int)(Math.Sin((double)(degree + 90) / (double)180 * Math.PI) * 50));

            graphicsObj.DrawEllipse(myPen, 200, 100, 200, 200);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //a²+b²=c²
            //c² = 100² = 10.000
            // Math.Pow(x, 2) + y² = 10000
            if (reverse == false)
            {
                x += 2;
            }
            else
            {
                x -= 2;
            }

            if (x >= 100)
            {
                reverse = true;
            }
            else if (x <= -100)
            {
                reverse = false;
            }
            y = (int)Math.Sqrt(10000 - Math.Pow(x, 2));
            textBox7.Text = Convert.ToString(y);
            this.Invalidate();

            if (degree < 360)
            {
                degree +=3;
            }
            else
            {
                degree = 0;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ballx = new Random().Next(20) - 10;
            bally = new Random().Next(20) - 10;
        }

        private void ballTimer_Tick(object sender, EventArgs e)
        {
            if (ballsave < 10)
            {
                ballsave++;
            }
            if (pictureBox1.Left > this.Width-125)
            {
                ballx = Math.Abs(ballx )* -1;
            }
            else if (pictureBox1.Left < 10)
            {
                ballx *= -1;

            }

            if (pictureBox1.Top < 10)
            {
                bally *= -1;
            }
            else if (pictureBox1.Top > this.Height-140)
            {
                bally *= -1;
            }

            pictureBox1.Left += ballx ;
            pictureBox1.Top += bally;
        
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.X);
            Console.WriteLine(e.Y);
            if (ballsave == 10)
            {
                if (e.Y > 90 && e.X <= 50)
                {
                    //bally = (Math.Abs(bally) + 1)*-1;
                    bally = -5;
                    ballx = -1 * (int) ((-50 + e.X) * 0.2);

                }

                if (e.Y > 90 && e.X > 50)
                {
                    bally = -5;
                    ballx = -1 * (int) ((-50 + e.X) * 0.2);
                }

                ballsave = 0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height,
                PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                Screen.PrimaryScreen.Bounds.Y,
                0,
                0,
                Screen.PrimaryScreen.Bounds.Size,
                CopyPixelOperation.SourceCopy);

            // Save the screenshot to the specified path that the user has chosen.
            bmpScreenshot.SetPixel(0,0,Color.Black);
            bmpScreenshot.Save("Screenshot.png", ImageFormat.Png);
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar1.Value.Equals(pt.X) && trackBar2.Value.Equals(pt.Y))
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
            if (!ptguess.IsEmpty)
            {
                if ((pt.Y - Math.Abs(trackBar2.Value)) < (pt.Y - Math.Abs(ptguess.Y)))
                {
                    textBox5.Text = Convert.ToString(Math.Abs(pt.Y - Math.Abs(trackBar2.Value)));
                }
                else
                {
                    textBox5.Text = Convert.ToString(Math.Abs(pt.Y - Math.Abs(trackBar2.Value)));
                }
            }
            ptguess.Y = trackBar2.Value;

            textBox6.Text = Convert.ToString(Math.Cos((double)trackBar2.Value*180/Math.PI));

        }
    }
}
