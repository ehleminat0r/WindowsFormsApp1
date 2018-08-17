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
    public partial class CreateRectangle : Form
    {
        private Point first;
        List<Rectangle> rects = new List<Rectangle>();
        private static Random rnd = new Random();

        public CreateRectangle()
        {
            InitializeComponent();
        }

        private void CreateRectangle_MouseDown(object sender, MouseEventArgs e)
        {
            first = new Point(e.X, e.Y);
        }

        private void CreateRectangle_MouseUp(object sender, MouseEventArgs e)
        {
            rects.Add(new Rectangle(first,new Point(e.X,e.Y),Color.Black ));
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            for (int i = 0; i < rects.Count; i++)
            {
                Pen pe = new Pen(Color.FromArgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
                e.Graphics.DrawRectangle(pe,rects[i].topleft.X, rects[i].topleft.Y, rects[i].bottomright.X- rects[i].topleft.X, rects[i].bottomright.Y- rects[i].topleft.Y);
                for (int j = rects[i].topleft.Y; j < rects[i].bottomright.Y; j++)
                {
                    e.Graphics.DrawLine(pe, rects[i].topleft.X, j, rects[i].bottomright.X, j);
                    System.Threading.Thread.Sleep(rnd.Next(10));
                }
            }
            
        }
    }

    class Rectangle
    {
        public Point topleft;
        public Point bottomright;
        public Color col;

        public Rectangle(Point tl, Point br, Color col)
        {
            topleft = tl;
            bottomright = br;
            this.col = col;
        }


    }
}
