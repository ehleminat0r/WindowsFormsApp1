using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Media;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;


namespace WindowsFormsApp1
{

    class MusicMachine : Form
    {
        // Timer (atm not necessary)
        System.Timers.Timer time = new System.Timers.Timer();

        // Music patern
        bool[] sound1 = new bool[16];
        bool[] sound2 = new bool[16];
        bool[] sound3 = new bool[16];

        // MediaPlayers
        MediaPlayer player1 = new MediaPlayer();
        MediaPlayer player2 = new MediaPlayer();
        MediaPlayer player3 = new MediaPlayer();

        // Time position
        private int pos = 0;

        // Textbox (speed)
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
            //time.Start();

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
                player1.Open(new System.Uri(@"C:\windows\media\Windows Default.wav"));
                player1.Play();
            }
            if (sound2[pos / 15] == true && pos % 15 == 0)
            {
                player2.Open(new System.Uri(@"C:\windows\media\chord.wav"));
                player2.Play();
            }
            if (sound3[pos / 15] == true && pos % 15 == 0)
            {
                player3.Open(new System.Uri(@"C:\windows\media\ding.wav"));
                player3.Play();
            }


            // Border
            e.Graphics.DrawRectangle(Pens.Black, 10, 10, 250, 340);

            // Draw Time Position
            e.Graphics.DrawLine(Pens.Black, 15 + pos, 15, 15 + pos, 200);

            // Draw Sound bar 1
            DrawPattern(e, sound1, 1);

            // Draw Sound bar 2
            DrawPattern(e, sound2, 2);

            // Draw Sound bar 3
            DrawPattern(e, sound3, 3);

            Invalidate();
        }

        private void DrawPattern(PaintEventArgs e, bool[] sound, int row)
        {
            for (int i = 0; i < sound.Length; i++)
            {
                if (sound[i] == false)
                {
                    if (i % 4 != 0)
                    {
                        e.Graphics.DrawRectangle(Pens.Black, 15 + i * 15, row * 40 - 15, 9, 20);
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(Pens.Red, 15 + i * 15, row * 40 - 15, 9, 20);
                    }
                }
                else
                {
                    if (i % 4 != 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, 15 + i * 15, row * 40 - 15, 9, 20);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Red, 15 + i * 15, row * 40 - 15, 9, 20);
                    }
                }
            }

            e.Graphics.FillRectangle(Brushes.Blue, 15 + (pos / 15) * 15, row * 40 - 15, 9, 20);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                // sound pattern 1
                if (e.Y >= 26 && e.Y <= 46)
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

                // sound pattern 2
                if (e.Y >= 66 && e.Y <= 86)
                {
                    if (sound2[(e.X - 15) / 15] == false)
                    {
                        sound2[(e.X - 15) / 15] = true;
                    }
                    else
                    {
                        sound2[(e.X - 15) / 15] = false;
                    }
                }

                // sound pattern 3
                if (e.Y >= 106 && e.Y <= 126)
                {
                    if (sound3[(e.X - 15) / 15] == false)
                    {
                        sound3[(e.X - 15) / 15] = true;
                    }
                    else
                    {
                        sound3[(e.X - 15) / 15] = false;
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
    }
}
