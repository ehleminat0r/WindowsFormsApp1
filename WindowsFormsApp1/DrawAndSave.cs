using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    class DrawAndSave : Form
    {
        static Bitmap bm = new Bitmap(600, 600);
        Graphics g = Graphics.FromImage(bm);
        System.Timers.Timer time = new System.Timers.Timer();
        
        private int count = 0;
        private int dreh = 0;
        private Point punkt = new Point(300, 300);

        public DrawAndSave()
        {
            this.Size = new Size(600, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "Graph";
            //this.BackColor = Color.Black;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.White);

            //timer
            time.Interval = 10;
            time.Start();
            time.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            dreh += 5;
            if (dreh >= 360)
                dreh = 0;
            if (count < 583)
                count += 2;
            else
            {
                count = 0;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Clear and Frame
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, 0, 0, 583, 560);

            // Sinus
            for (int i = 0; i < bm.Width; i++)
            {
                bm.SetPixel(i, 100 + (int)(Math.Sin(i / 40F) * 40), Color.Black);
                bm.SetPixel(i, 400 + (int)(Math.Sin(i / 40F) * 40), Color.Black);
            }

            // Sinus dots
            if (count % 100 < 20)
            {
                g.FillEllipse(Brushes.Blue, count, -10 + 100 + (int)(Math.Sin(count / 40F) * 40), 20, 20);
                g.FillEllipse(Brushes.Green, count, -5 + 400 + (int)(Math.Sin(count / 40F) * 40), 10, 10);
            }
            else
            {
                g.FillEllipse(Brushes.Blue, count, -5 + 100 + (int)(Math.Sin(count / 40F) * 40), 10, 10);
            }

            // spirale
            //spiral();



            
            Graphics grb = Graphics.FromImage(bm);
            grb.TranslateTransform(punkt.X, punkt.Y);
            grb.RotateTransform(dreh*2);
            grb.DrawImage(new Bitmap("b.bmp"), new Point(-20, -20));

            Graphics gra = Graphics.FromImage(bm);
            gra.TranslateTransform(count, -10 + 400 + (int)(Math.Sin(count / 40F) * 40));
            gra.RotateTransform(dreh);
            gra.DrawImage(new Bitmap("a.bmp"), new Point(-20,-20));

            // draw
            BackgroundImage = bm;
            Invalidate();
        }

        private void spiral()
        {
            try
            {
                int x = 300;
                int y = 300;
                int xold = x;
                int yold = y;
                int inc = 5;

                for (int i = 0; i < count / 40; i++)
                {
                    g.DrawLine(Pens.Black, xold, yold, x, y);
                    xold = x;
                    yold = y;
                    y += inc;
                    g.DrawLine(Pens.Black, xold, yold, x, y);
                    xold = x;
                    yold = y;
                    x -= inc;
                    g.DrawLine(Pens.Black, xold, yold, x, y);
                    xold = x;
                    yold = y;
                    inc += 5;
                    y -= inc;
                    g.DrawLine(Pens.Black, xold, yold, x, y);
                    xold = x;
                    yold = y;
                    x += inc;
                    g.DrawLine(Pens.Black, xold, yold, x, y);
                    xold = x;
                    yold = y;
                    inc += 5;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.W)
                punkt = new Point(punkt.X, punkt.Y - 10);
            if (e.KeyData == Keys.S)
                punkt = new Point(punkt.X, punkt.Y + 10);
            if (e.KeyData == Keys.A)
                punkt = new Point(punkt.X - 10, punkt.Y);
            if (e.KeyData == Keys.D)
                punkt = new Point(punkt.X + 10, punkt.Y);

            if (e.KeyData==Keys.Enter)
                bm.Save("test.jpg",ImageFormat.Jpeg);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            punkt = new Point(e.X, e.Y);
        }
    }
}
