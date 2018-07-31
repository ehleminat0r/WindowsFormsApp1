using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    class MusicMachine : Form
    {
        System.Timers.Timer time = new System.Timers.Timer();
        bool[] sound1 = new bool[16];
        private int pos = 0;

        // Sound api functions
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        public MusicMachine()
        {
            this.Size = new Size(400, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "MusicMachine";
            this.BackColor = Color.White;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            // Initialize Timer
            time.Interval = 10;
            time.Elapsed += new ElapsedEventHandler(OnTick);
            time.Start();
        }

        private void OnTick(Object sender, ElapsedEventArgs e)
        {
            if (pos < 235)
            {
                pos += 2;
            }
            else
            {
                pos = 0;
            }

            if (sound1[pos / 15] == true && pos % 15 < 2)
            {
                /*
                var p1 = new System.Windows.Media.MediaPlayer();
                p1.Open(new System.Uri(@"C:\windows\media\tada.wav"));
                p1.Play();
                */
                mciSendString(@"open C:\windows\media\tada.wav type waveaudio alias applause", null, 0, IntPtr.Zero);
                mciSendString(@"play applause", null, 0, IntPtr.Zero);

            }
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Border
            e.Graphics.DrawRectangle(Pens.Black, 10, 10, 360, 340);

            // Draw Time Position
            e.Graphics.DrawLine(Pens.Black, 15 + pos, 10, 15 + pos, 100);

            // Draw Sound bar
            for (int i = 0; i < sound1.Length; i++)
            {
                if (sound1[i] == false)
                {
                    e.Graphics.DrawRectangle(Pens.Black, 15 + i * 15, 20, 9, 20);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.Black, 15 + i * 15, 20, 9, 20);
                }
            }
            e.Graphics.FillRectangle(Brushes.Blue, 15 + (pos/15) * 15, 20, 9, 20);

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Console.WriteLine(e.X);
            Console.WriteLine((e.X-15)/15);
            if (sound1[(e.X - 15) / 15] == false)
            {
                sound1[(e.X - 15) / 15] = true;
            }
            else
            {
                sound1[(e.X - 15) / 15] = false;
            }
        }
    }
}
