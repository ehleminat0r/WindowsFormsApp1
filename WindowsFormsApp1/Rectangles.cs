using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Rectangles : Form
    {
        public List<Particle> parts = new List<Particle>();

        public Rectangles()
        {
            InitializeComponent();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            parts.Add(new Particle());
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                System.Threading.Thread.Sleep(50);
                Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                SolidBrush brush = new SolidBrush(Color.Black);
                using (Graphics gr = Graphics.FromImage(bm))
                {
                    foreach (Particle par in parts)
                    {
                        gr.FillRectangle(brush, par.rect);
                        par.rect = new Rectangle(par.rect.X + par.speedx, par.rect.Y + par.speedy, par.rect.Width,
                            par.rect.Height);
                        if (par.rect.X < 0)
                            par.speedx *= -1;
                        else if (par.rect.X > 790)
                            par.speedx *= -1;
                        if (par.rect.Y < 0)
                            par.speedy *= -1;
                        else if (par.rect.Y > 440)
                            par.speedy *= -1;


                    }

                }
                pictureBox1.Image = bm;
            }

        }

        private void Rectangles_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
    }

    public class Particle
    {
        public Rectangle rect = new Rectangle();
        static Random rnd = new Random();
        public int speedx;
        public int speedy;

        public Particle()
        {
            rect.X = rnd.Next(700);
            rect.Y = rnd.Next(400);
            rect.Width = rnd.Next(25) + 25;
            rect.Height = rnd.Next(25) + 25;
            speedy = rnd.Next(20) - 10;
            speedx = rnd.Next(20) - 10;
        }
    }
}
