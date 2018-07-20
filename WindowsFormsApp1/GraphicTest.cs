using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class GraphicTest : Form
    {
        private Pen penWhite = new Pen(Color.White);
        private Stopwatch sw;
        private List<Point> lines = new List<Point>();
        static Random rnd = new Random();

        private int lastPaint = 0;

        public GraphicTest()
        {
            this.Size = new Size(600, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.Black;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            sw = new Stopwatch();
            sw.Start();
            lines.Add(new Point(0,0));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int timeOffset = (int)sw.ElapsedMilliseconds - lastPaint;
            lastPaint = (int) sw.ElapsedMilliseconds;
            e.Graphics.DrawString("T Offs:"+Convert.ToString(timeOffset), new Font("system", 10), Brushes.White, 400, 30);

            e.Graphics.DrawString("Time:  "+Convert.ToString(sw.ElapsedMilliseconds), new Font("system", 10), Brushes.White, 400, 10);
            e.Graphics.DrawString("List Objects: "+Convert.ToString(lines.Count), new Font("system", 10), Brushes.White, 400, 50);

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[lines.Count - 1].X > 600 | lines[lines.Count - 1].Y > 600)
                {
                    lines.Add(new Point(0, 0));
                }
                else
                {
                    lines[lines.Count - 1] = new Point(lines[lines.Count - 1].X + rnd.Next(20), lines[lines.Count - 1].Y + rnd.Next(20));
                }
                e.Graphics.DrawLine(penWhite, 0, 0, lines[i].X, lines[i].Y);
            }
            Invalidate();
        }
    }
}
