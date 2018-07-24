using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class Asteriods : Form
{
    static void Main2()
    {
        Application.Run(new Asteriods());
    }
    Stopwatch sw;
    int lastPaint = 0;
    int lastShot = 0;
    int lastBlock = 0;
    List<Particle> asteroids = new List<Particle>();
    List<Particle> missils = new List<Particle>();
    RectangleF center = new RectangleF(240f, 240f, 20f, 20f);
    bool end = false;

    public Asteriods()
    {
        this.Size = new Size(500, 500);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.BackColor = Color.Black;
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        DoubleBuffered = true;
        for (int i = 0; i < 5; i++)
        {
            asteroids.Add(new Particle());
        }
        sw = new Stopwatch();
        sw.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        int snapshot = (int)sw.ElapsedMilliseconds;
        int timeOffset = snapshot - lastPaint;
        lastPaint = snapshot;
        Particle p1;
        Particle p2;
        bool updateMissiles = true;
        for (int i = asteroids.Count - 1; i >= 0; i--)
        {
            p1 = asteroids[i];
            p1.rect = new RectangleF(p1.rect.Left + p1.speedx * timeOffset,
                                     p1.rect.Top + p1.speedy * timeOffset,
                                     p1.rect.Width, p1.rect.Height);
            if (p1.rect.IntersectsWith(this.ClientRectangle))
            {
                if (p1.rect.IntersectsWith(center))
                {
                    end = true;
                }
                for (int j = missils.Count - 1; j >= 0; j--)
                {
                    p2 = missils[j];
                    if (updateMissiles)
                    {
                        p2.rect = new RectangleF(p2.rect.Left + p2.speedx * timeOffset,
                                                 p2.rect.Top + p2.speedy * timeOffset,
                                                 p2.rect.Width, p2.rect.Height);
                        updateMissiles = (j != 0);
                    }
                    if (p2.rect.IntersectsWith(this.ClientRectangle))
                    {
                        if (p1.rect.IntersectsWith(p2.rect))
                        {
                            asteroids[i] = new Particle();
                            missils.RemoveAt(j);
                        }
                    }
                    else
                    {
                        missils.RemoveAt(j);
                    }
                }
            }
            else
            {
                asteroids[i] = new Particle();
            }
            e.Graphics.FillRectangle(Brushes.Green, asteroids[i].rect);
        }
        if ((lastBlock + 5000) <= snapshot)
        {
            lastBlock = snapshot - (snapshot % 5000);
            asteroids.Add(new Particle());
        }
        foreach (Particle p in missils)
        {
            e.Graphics.FillRectangle(Brushes.Red, p.rect);
        }
        e.Graphics.FillRectangle(Brushes.Blue, center);
        if (end)
        {
            e.Graphics.DrawString((snapshot / 1000f).ToString("0.000") + "s",
                                  new Font("system", 25), Brushes.White,
                                  Point.Empty);
        }
        else
        {
            Invalidate();
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        int snapshot = (int)sw.ElapsedMilliseconds;
        if ((lastShot + 250) <= snapshot)
        {
            lastShot = snapshot;
            missils.Add(new Particle(e.X - 250, e.Y - 250));
        }
    }

    class Particle
    {
        static Random rand = new Random();
        public RectangleF rect;
        public float speedx;
        public float speedy;

        public Particle()
        {
            float height = rand.Next(10, 50);
            int width = rand.Next(10, 50);
            rect = new RectangleF(rand.Next(0, 500 - width), 1 - height, rand.Next(10, 50), height);
            speedx = rand.Next(-50, 50) / 1000f;
            speedy = rand.Next(50, 100) / 1000f;
        }
        public Particle(int mouseX, int mouseY)
        {
            rect = new RectangleF(245f, 245f, 10f, 10f);
            float dia = (float)Math.Sqrt(mouseX * mouseX + mouseY * mouseY);
            speedx = mouseX / dia / 5;
            speedy = mouseY / dia / 5;
        }
    }
}