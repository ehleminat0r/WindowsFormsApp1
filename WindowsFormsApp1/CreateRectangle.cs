using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CreateRectangle : Form
    {
        private Boolean jump = false;
        private int jumpAcc = -15;
        private Boolean hasContact;
        private int time = 1000;
        private Rectangle player;
        List<Rectangle> rects = new List<Rectangle>();
        private static Random rnd = new Random();

        public CreateRectangle()
        {
            InitializeComponent();
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            this.DoubleBuffered = true;
            player = new Rectangle(Width / 2, Height / 2-200, 20, 20);
            rects.Add(new Rectangle(Width + 500, Height / 2 + 100, 1000, 10));
            rects.Add(new Rectangle(Width / 2, Height / 2 + 20, 1000, 10));
        }

        private void CreateRectangle_MouseDown(object sender, MouseEventArgs e)
        {
            rects.Add(new Rectangle(e.X, e.Y, 20, 20));
            //player = new Rectangle(player.X, player.Y - 100, player.Width, player.Height);
            jump = true;
            Invalidate();
           
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (time==0)
            {
                rects.Add(new Rectangle(this.Width, rnd.Next(this.Height), rnd.Next(500), rnd.Next(50)));
                time = 100;
            }
            else
            {
                time--;
            }
            
            e.Graphics.FillRectangle(Brushes.Black, player);
            hasContact = false;
            for (int i = 0; i < rects.Count; i++)
            {
                rects[i] = new Rectangle(rects[i].X - 1, rects[i].Y, rects[i].Width, rects[i].Height);
                Brush be = new SolidBrush(Color.FromArgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
                //Pen pe = new Pen(Color.FromArgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
                e.Graphics.FillRectangle(be, rects[i]);

                if (rects[i].IntersectsWith(player))
                {
                    Console.WriteLine("hit");
                    //rects.RemoveAt(i);
                    hasContact = true;
                    
                    

                }
                else
                {
                    if (rects[i].X < -1000)
                    {
                        rects.RemoveAt(i);
                    }
                    
                }
                

            }

            if (jump)
            {
                player = new Rectangle(player.X, player.Y + jumpAcc, player.Width, player.Height);
                jumpAcc += 1;
            }

            if (hasContact)
            {
                jump = false;
                jumpAcc = -20;
            }
            else
            {
                if (time%2==0 && !jump)
                player = new Rectangle(player.X, player.Y + 1, player.Width, player.Height);
            }
            e.Graphics.DrawString(Convert.ToString(rects.Count), new Font("Arial", 16), new SolidBrush(Color.Black), 360, 5);
            Invalidate();
        }
    }

}
