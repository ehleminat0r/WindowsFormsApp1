using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Timers;
using System.Windows.Forms;


namespace WindowsFormsApp1
{

    class MusicMachine : Form
    {
        System.Timers.Timer time = new System.Timers.Timer();
        bool[] sound1 = new bool[16];
        private int pos = 0;
        private Pen pen;

        // Soundplayer
        System.Media.SoundPlayer player = new SoundPlayer();

        // Textbox
        TextBox tb = new TextBox();

        public MusicMachine()
        {
            this.Size = new Size(285, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "MusicMachine";
            this.BackColor = Color.White;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            // Initialize Timer
            time.Interval = 1000;
            time.Elapsed += new ElapsedEventHandler(OnTick);
            time.Start();

            //soundplayer
            player.SoundLocation = @"C:\windows\media\Windows Default.wav";
            player.Load();

            // Pen
            pen = new Pen(Color.DarkBlue, 5);
            pen.StartCap = LineCap.ArrowAnchor;

            // Textbox
            tb.Left = 20;
            tb.Top = 320;
            tb.Width = 40;
            tb.Height = 80;
            tb.Text = "5";

            this.Controls.Add(tb);
        }



        private void OnTick(Object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine(e.SignalTime);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // for delay
            try
            {
                System.Threading.Thread.Sleep(Convert.ToInt32(tb.Text));
            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(5);
            }
            

            // Increase Position
            if (pos < 239)
            {
                pos += 1;
            }
            else
            {
                pos = 0;
            }

            // play sound
            if (sound1[pos / 15] == true && pos % 15 == 0)
            {
                player.Play();
            }

            // Border
             e.Graphics.DrawRectangle(Pens.Black, 10, 10, 250, 340);

            // Draw Time Position
            e.Graphics.DrawLine(pen, 15 + pos, 45, 15 + pos, 70);

            // Draw Sound bar
            for (int i = 0; i < sound1.Length; i++)
            {
                if (sound1[i] == false)
                {
                    if (i % 4 != 0)
                    {
                        e.Graphics.DrawRectangle(Pens.Black, 15 + i * 15, 20, 9, 20);
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(Pens.Red, 15 + i * 15, 20, 9, 20);
                    }
                    
                }
                else
                {
                    if (i % 4 != 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, 15 + i * 15, 20, 9, 20);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Red, 15 + i * 15, 20, 9, 20);
                    }
                }
            }
            e.Graphics.FillRectangle(Brushes.Blue, 15 + (pos/15) * 15, 20, 9, 20);

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                if (sound1[(e.X - 15) / 15] == false)
                {
                    sound1[(e.X - 15) / 15] = true;
                }
                else
                {
                    sound1[(e.X - 15) / 15] = false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
    }
}
