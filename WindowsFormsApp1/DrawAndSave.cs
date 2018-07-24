using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class DrawAndSave : Form
    {
        Bitmap bm = new Bitmap(600, 600);

        public DrawAndSave()
        {
            this.Size = new Size(600, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            //this.BackColor = Color.Black;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.White);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            /*
            e.Graphics.DrawEllipse(Pens.Black,10,10,100,100);
            Invalidate();
            */
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            
            double xhoch3 = 0.01;
            double xhoch2 = 0.03;
            double x = 1;
            for (int i = 0; i < bm.Width; i++)
            {
                bm.SetPixel(i, 200+(int)(Math.Sin(i/40F)*40), Color.Black);
                if ((int)Math.Pow(i * xhoch3, 3) + (int)Math.Pow(i * xhoch2, 2) + i * x < 600)
                  bm.SetPixel(i,(int) (Math.Pow(i*xhoch3,3) + (int)Math.Pow(i*xhoch2, 2) + (int)i*x), Color.Black);
            }
            
            BackgroundImage = bm;
            if (e.KeyData==Keys.S)
                bm.Save("test.jpg",ImageFormat.Jpeg);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    /*
                    bm.SetPixel(e.X, e.Y, Color.Black);
                    bm.SetPixel(e.X - 1, e.Y, Color.Black);
                    bm.SetPixel(e.X + 1, e.Y, Color.Black);
                    bm.SetPixel(e.X, e.Y + 1, Color.Black);
                    bm.SetPixel(e.X, e.Y - 1, Color.Black);
                    */
                    int x = e.X;
                    int y = e.Y;
                    int xold = x;
                    int yold = y;
                    int inc = 5;
                    Graphics g = Graphics.FromImage(bm);

                    for (int i = 0; i < 10; i++)
                    {
                        g.DrawLine(Pens.Black,xold,yold,x,y);
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
            

            BackgroundImage = bm;
            Invalidate();
        }
    }
}
