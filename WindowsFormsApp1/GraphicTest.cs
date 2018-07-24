using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class GraphicTest : Form
    {
        private Pen penWhite = new Pen(Color.White);
        private Stopwatch sw;
        private int lastPaint = 0;
        static Random rnd = new Random();
        private System.Timers.Timer time = new System.Timers.Timer();

        // Line List
        private List<Point> lines = new List<Point>();
        // Polygon List
        private int moveTimer = 0;
        private List<MovingPolygon> polys = new List<MovingPolygon>();

        public GraphicTest()
        {
            this.Size = new Size(600, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.Black;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            sw = new Stopwatch();
            sw.Start();

            // Timer
            time.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            time.Interval = 1000;
            time.Enabled = true;

            // Line
            // lines.Add(new Point(0,0));
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            int count = rnd.Next(3, 8);
            PointF[] points = new PointF[count];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new PointF(rnd.Next(50,500), rnd.Next(50,200));
            }

            polys.Add(new MovingPolygon(points, rnd.Next(4) - 2, rnd.Next(4) - 2));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int timeOffset = (int)sw.ElapsedMilliseconds - lastPaint;
            lastPaint = (int) sw.ElapsedMilliseconds;
            e.Graphics.DrawString("T Offs:"+Convert.ToString(timeOffset), new Font("system", 10), Brushes.White, 400, 30);
            e.Graphics.DrawString("Time:  "+Convert.ToString(sw.ElapsedMilliseconds), new Font("system", 10), Brushes.White, 400, 10);

            //DrawLines(e);
            DrawPolygons(e);
            DrawMath(e);
            Invalidate();
        }

        private void DrawMath(PaintEventArgs e)
        {
            for (int i = 0; i < Width; i += 5)
            {
                if (i % 35 == 0)
                {
                    e.Graphics.DrawString("Sinus", new Font("system", 10), Brushes.White, i,
                        300 + (float)Math.Sin(i / 50F) * 50);
                }
                else
                {
                    e.Graphics.DrawEllipse(penWhite, i, 300 + (float)Math.Sin(i / 50F) * 50, 4, 4);
                }
                
                //e.Graphics.DrawEllipse(penWhite, i, 300 + (float) Math.Cos(i / 50F) * 50, 4, 4);
            }
        }


        private void DrawPolygons(PaintEventArgs e)
        {
            try
            {
                foreach (MovingPolygon pol in polys)
                {
                    e.Graphics.DrawPolygon(penWhite, pol.Points);
                    if (moveTimer > 5)
                    {
                        pol.Move();
                    }
                        
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            if (moveTimer > 5)
            {
                moveTimer = 0;
            }
            else
            {
                moveTimer++;
            }
                

        }

        private void DrawLines(PaintEventArgs e)
        {
            e.Graphics.DrawString("List Objects: " + Convert.ToString(lines.Count), new Font("system", 10), Brushes.White, 400,
                50);

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[lines.Count - 1].X > 600 | lines[lines.Count - 1].Y > 600)
                {
                    lines.Add(new Point(0, 0));
                }
                else
                {
                    lines[lines.Count - 1] = new Point(lines[lines.Count - 1].X + rnd.Next(20),
                        lines[lines.Count - 1].Y + rnd.Next(20));
                }

                e.Graphics.DrawLine(penWhite, 0, 0, lines[i].X, lines[i].Y);
            }
        }
    }

    class MovingPolygon
    {
        private PointF[] points;
        private int speedX;
        private int speedY;

        public PointF[] Points { get => points; set => points = value; }
        public int SpeedX { get => speedX; set => speedX = value; }
        public int SpeedY { get => speedY; set => speedY = value; }


        public MovingPolygon(PointF[] points, int sX, int sY)
        {
            this.Points = points;
            SpeedX = sX;
            SpeedY = sY;
        }

        public void Move()
        {
            for (int i=0; i<points.Length; i++)
            {
                points[i] = new PointF(points[i].X+SpeedX,points[i].Y+speedY);
            }
        }


    }
}
