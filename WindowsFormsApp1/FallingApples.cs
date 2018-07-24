using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FallingApples : Form
    {
        Random rnd = new Random();
        Graphics g;
        Pen pen = new Pen(Color.Black,5);

        // Apple Game
        List<Point> balls = new List<Point>();
        private int balltime = 100;
        private Boolean moveLeft = false, moveRight = false;
        private int playerPos = 200;
        private int points = 0;

        //Bounce
        private int x = 200, y = 100;
        private int speedx = 20, speedy = 10;
        List<Point> apples = new List<Point>();

        // Cannon
        private Boolean moveUp = false, moveDown = false;
        private int angle = 90;
        private int canx, cany, canspeedx, canspeedy;
        private Boolean shoot = false;
        private int power = 25;

        public FallingApples()
        {
            InitializeComponent();
            g = this.pictureBox1.CreateGraphics();
            
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            // Bouncing Apple
            speedy += 2;
            x += speedx;
            y += speedy;
            if (x > 380)
                speedx = (int) (Math.Abs(speedx) * -0.7);
            else if (x < 0)
            {
                speedx = (int)(Math.Abs(speedx) * 0.7);
            }

            if (speedx > 0 || speedy > 0)
            {
                if (y > 380)
                {
                    y = 380;
                    speedy = (int)(Math.Abs(speedy) * -0.7);
                    speedx = (int)(speedx * 0.7);
                    apples.Add(new Point(x, y));
                }
            }

            // Cannon
            if (moveUp && angle < 180)
                angle += 5;
            if (moveDown && angle > 0)
                angle -= 5;
            if (!shoot)
            {
                canx = 290 + (int)(Math.Cos(angle * Math.PI / 180) * 50);
                cany = 370 - (int)(Math.Sin(angle * Math.PI / 180) * 50);
                canspeedx = (int) (Math.Cos(angle * Math.PI / 180) * power*2);
                canspeedy = -(int)(Math.Sin(angle * Math.PI / 180) * power*2);
            }
            else
            {
                canspeedy += 2;
                canx += canspeedx;
                cany += canspeedy;
            }
            if (cany > 380 && canspeedy >0)
                canspeedy = (int)(Math.Abs(canspeedy) * -0.7);

            if (canx < 10)
                canspeedx *= -1;
            else if (canx > 370)
            {
                canspeedx *= -1;
            }




            // Apple creation
            if (balls.Count < 5)
            {
                if (balltime > 0)
                {
                    balltime--;
                }
                else
                {
                    balls.Add(new Point(rnd.Next(300) + 50, rnd.Next(150) + 30));
                    balltime = rnd.Next(100);
                }
            }
                
            // Player movement
            if (moveLeft && playerPos > 25)
            {
                playerPos -= 10;
                power--;
            }

            if (moveRight && playerPos < 375)
            {
                power++;
                playerPos += 10;
            }

            // Remove apples
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i] = new Point(balls[i].X, balls[i].Y + 5);
                if (balls[i].Y > 400)
                {
                    balls.RemoveAt(i);
                    points -= 5;
                }
            }

            // Check for collision
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].X > playerPos - 25 && balls[i].X < playerPos + 25 && balls[i].Y > 350 && balls[i].Y < 370)
                {
                    points++;
                    balls.RemoveAt(i);
                }
            }

            // Draw with double-buffering.
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Image imgTree = (Image)(new Bitmap(WindowsFormsApp1.Properties.Resources.tree,400,400));
            Image imgApple = (Image)(new Bitmap(WindowsFormsApp1.Properties.Resources.apple, 20, 20));
            Image imgBasket = (Image)(new Bitmap(WindowsFormsApp1.Properties.Resources.basket, 50, 50));

            using (Graphics gr = Graphics.FromImage(bm))
            {
                
                // Apple Game
                gr.DrawString(Convert.ToString(points), new Font("Arial", 16), new SolidBrush(Color.Black),360,5);
                gr.DrawImage(imgTree, 0, 0);
                gr.DrawImage(imgBasket, playerPos - 25, 350);
                foreach (Point ball in balls)
                {
                    gr.DrawImage(imgApple, ball.X, ball.Y);
                }
                // Bouncing Apple
                foreach (Point apple in apples)
                {
                    gr.DrawImage(imgApple, apple.X, apple.Y);
                }
                gr.DrawImage(imgApple, x, y);
                // Cannon
                gr.DrawLine(pen,300,380,(300+(int)(Math.Cos(angle*Math.PI/180)*50)), (380 - (int)(Math.Sin(angle * Math.PI / 180) * 50)));
                gr.DrawImage(imgApple, canx, cany);
                gr.DrawString(Convert.ToString(power), new Font("Arial", 16), new SolidBrush(Color.Black), 360, 25);
            }
            pictureBox1.Image = bm;
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            speedx = rnd.Next(40) - 20;
            speedy = rnd.Next(20) - 30;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void Form4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                moveRight = false;
            }

            if (e.KeyCode == Keys.W)
            {
                moveUp = false;
            }
            if (e.KeyCode == Keys.S)
            {
                moveDown = false;
            }
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                moveLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                moveRight = true;
            }

            if (e.KeyCode == Keys.W)
            {
                moveUp = true;
            }
            if (e.KeyCode == Keys.S)
            {
                moveDown = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                shoot = !shoot;
            }
        }
    }
}
