using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Media;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;


namespace WindowsFormsApp1
{

    class MusicMachine : Form
    {
        // Timer (atm not necessary)
        System.Timers.Timer time = new System.Timers.Timer();

        // Music pattern
        bool[] sound1 = new bool[16];
        bool[] sound2 = new bool[16];
        bool[] sound3 = new bool[16];
        bool[] sound4 = new bool[16];

        // Pattern on off
        private bool[] pattern = {true, true, true, true};

        // MediaPlayers
        MediaPlayer player1 = new MediaPlayer();
        MediaPlayer player2 = new MediaPlayer();
        MediaPlayer player3 = new MediaPlayer();
        MediaPlayer player4 = new MediaPlayer();

        // Time position
        private int pos = 0;

        // Textbox (speed)
        TextBox tb = new TextBox();

        public MusicMachine()
        {
            this.Size = new Size(310, 400);
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
            if (sound1[pos / 15] == true && pos % 15 == 0 && pattern[0] == true)
            {
                player1.Open(new System.Uri(@"C:\windows\media\Windows Default.wav"));
                player1.Play();
            }
            if (sound2[pos / 15] == true && pos % 15 == 0 && pattern[1] == true)
            {
                player2.Open(new System.Uri(@"C:\windows\media\chord.wav"));
                player2.Play();
            }
            if (sound3[pos / 15] == true && pos % 15 == 0 && pattern[2] == true)
            {
                player3.Open(new System.Uri(@"C:\windows\media\ding.wav"));
                player3.Play();
            }
            if (sound4[pos / 15] == true && pos % 15 == 0 && pattern[3] == true)
            {
                player4.Open(new System.Uri(@"C:\windows\media\Windows Ding.wav"));
                player4.Play();
            }

            // Border
            e.Graphics.DrawRectangle(Pens.Black, 10, 10, 280, 340);

            // Draw Time Position
            e.Graphics.DrawLine(Pens.Black, 15 + pos, 15, 15 + pos, 200);

            // Draw Sound bar 1
            DrawPattern(e, sound1, 1);

            // Draw Sound bar 2
            DrawPattern(e, sound2, 2);

            // Draw Sound bar 3
            DrawPattern(e, sound3, 3);

            // Draw Sound bar 4
            DrawPattern(e, sound4, 4);

            // Draw switch
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == false)
                {
                    e.Graphics.DrawEllipse(Pens.Black, 260, 25 + i * 40, 20, 20);
                }
                else
                {
                    e.Graphics.FillEllipse(Brushes.LightGreen, 260, 25+i * 40, 20, 20);
                }
            }

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

            if (pattern[row - 1] == true)
            {
                e.Graphics.FillRectangle(Brushes.Blue, 15 + (pos / 15) * 15, row * 40 - 15, 9, 20);
            }
            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Console.WriteLine(e.X);
            try
            {
                // sound pattern 1
                if (e.Y >= 26 && e.Y <= 46)
                {
                    if (e.X <= 250)
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

                    if (e.X >= 260 && e.X <= 280)
                    {
                        if (pattern[0] == true)
                        {
                            pattern[0] = false;
                        }
                        else
                        {
                            pattern[0] = true;
                        }
                    }
                }

                // sound pattern 2
                if (e.Y >= 66 && e.Y <= 86)
                {
                    if (e.X <= 250)
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

                    if (e.X >= 260 && e.X <= 280)
                    {
                        if (pattern[1] == true)
                        {
                            pattern[1] = false;
                        }
                        else
                        {
                            pattern[1] = true;
                        }
                    }
                }

                // sound pattern 3
                if (e.Y >= 106 && e.Y <= 126)
                {
                    if (e.X <= 250)
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

                    if (e.X >= 260 && e.X <= 280)
                    {
                        if (pattern[2] == true)
                        {
                            pattern[2] = false;
                        }
                        else
                        {
                            pattern[2] = true;
                        }
                    }
                }

                // sound pattern 4
                if (e.Y >= 146 && e.Y <= 166)
                {
                    if (e.X <= 250)
                    {
                        if (sound4[(e.X - 15) / 15] == false)
                        {
                            sound4[(e.X - 15) / 15] = true;
                        }
                        else
                        {
                            sound4[(e.X - 15) / 15] = false;
                        }
                    }

                    if (e.X >= 260 && e.X <= 280)
                    {
                        if (pattern[3] == true)
                        {
                            pattern[3] = false;
                        }
                        else
                        {
                            pattern[3] = true;
                        }
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
