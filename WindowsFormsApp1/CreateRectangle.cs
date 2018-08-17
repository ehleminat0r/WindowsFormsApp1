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
                e.Graphics.DrawRectangle(Pens.Black,rects[i].topleft.X, rects[i].topleft.Y, rects[i].bottomright.X- rects[i].topleft.X, rects[i].bottomright.Y- rects[i].topleft.Y);
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
