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
        // other
        System.Timers.Timer time = new System.Timers.Timer();
        FileDialog fd = new OpenFileDialog();

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

        // Sound path
        private string sPath1 = @"C:\windows\media\Windows Default.wav";
        private string sPath2 = @"C:\windows\media\chord.wav";
        private string sPath3 = @"C:\windows\media\ding.wav";
        private string sPath4 = @"C:\windows\media\Windows Ding.wav";

        // Beep
        beep[] beeps = new beep[32];
        static Random rnd = new Random();

        // Time position
        private int pos = 0;
        private int speed = 5;

        public MusicMachine()
        {
            this.Size = new Size(310, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "MusicMachine Speed: 5";
            this.BackColor = Color.White;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            // Initialize Timer
            time.Interval = 1000;
            time.Elapsed += new ElapsedEventHandler(OnTick);
            //time.Start();

            /* Beep Test
             
            for (int i = 0; i < beeps.Length; i++)
            {
                beeps[i] = new beep();
                beeps[i].Freq = rnd.Next(1000)+50;
                beeps[i].Dur = 100;
            }

            beeps[0].Freq = 1200;
            beeps[1].Freq = 1000;
            beeps[2].Freq = 1000;
            beeps[3].Freq = 1100;
            beeps[4].Freq = 900;
            beeps[5].Freq = 900;
            beeps[6].Freq = 800;
            beeps[7].Freq = 900;
            beeps[8].Freq = 1000;
            beeps[9].Freq = 1100;
            beeps[10].Freq = 1200;
            beeps[11].Freq = 1200;
            beeps[12].Freq = 1200;


            for (int i = 0; i < beeps.Length; i++)
            {
                Console.Beep(beeps[i].Freq,beeps[i].Dur);
                //System.Threading.Thread.Sleep(100);
            }

            */
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                    fd.ShowDialog();
                    sPath1 = fd.InitialDirectory + fd.FileName;
                    break;
                case Keys.W:
                    fd.ShowDialog();
                    sPath2 = fd.InitialDirectory + fd.FileName;
                    break;
                case Keys.E:
                    fd.ShowDialog();
                    sPath3 = fd.InitialDirectory + fd.FileName;
                    break;
                case Keys.R:
                    fd.ShowDialog();
                    sPath4 = fd.InitialDirectory + fd.FileName;
                    break;
                case Keys.D1:
                    speed = 10;
                    this.Text = "MusicMachine Speed: 1";
                    break;
                case Keys.D2:
                    speed = 9;
                    this.Text = "MusicMachine Speed: 2";
                    break;
                case Keys.D3:
                    speed = 8;
                    this.Text = "MusicMachine Speed: 3";
                    break;
                case Keys.D4:
                    speed = 7;
                    this.Text = "MusicMachine Speed: 4";
                    break;
                case Keys.D5:
                    speed = 6;
                    this.Text = "MusicMachine Speed: 5";
                    break;
                case Keys.D6:
                    speed = 5;
                    this.Text = "MusicMachine Speed: 6";
                    break;
                case Keys.D7:
                    speed = 4;
                    this.Text = "MusicMachine Speed: 7";
                    break;
                case Keys.D8:
                    speed = 3;
                    this.Text = "MusicMachine Speed: 8";
                    break;
                case Keys.D9:
                    speed = 2;
                    this.Text = "MusicMachine Speed: 9";
                    break;
                case Keys.D0:
                    speed = 1;
                    this.Text = "MusicMachine Speed: 10";
                    break;

            }
        }

        private void OnTick(Object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine(e.SignalTime);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // for delay
            System.Threading.Thread.Sleep(speed);


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
                player1.Open(new System.Uri(sPath1));
                player1.Play();
            }
            if (sound2[pos / 15] == true && pos % 15 == 0 && pattern[1] == true)
            {
                player2.Open(new System.Uri(sPath2));
                player2.Play();
            }
            if (sound3[pos / 15] == true && pos % 15 == 0 && pattern[2] == true)
            {
                player3.Open(new System.Uri(sPath3));
                player3.Play();
            }
            if (sound4[pos / 15] == true && pos % 15 == 0 && pattern[3] == true)
            {
                player4.Open(new System.Uri(sPath4));
                player4.Play();
            }

            // Border
            e.Graphics.DrawRectangle(Pens.Black, 10, 10, 280, 340);

            // Draw Time Position
            e.Graphics.DrawLine(Pens.Black, 15 + pos, 15, 15 + pos, 200);

            // Draw Sound bar 1
            DrawPattern(e, sound1, 1);
            e.Graphics.DrawString("Taste Q: "+sPath1, new Font("system", 7), Brushes.Black, 15, 13);

            // Draw Sound bar 2
            DrawPattern(e, sound2, 2);
            e.Graphics.DrawString("Taste W: " + sPath2, new Font("system", 7), Brushes.Black, 15, 53);

            // Draw Sound bar 3
            DrawPattern(e, sound3, 3);
            e.Graphics.DrawString("Taste E: " + sPath3, new Font("system", 7), Brushes.Black, 15, 93);

            // Draw Sound bar 4
            DrawPattern(e, sound4, 4);
            e.Graphics.DrawString("Taste R: " + sPath4, new Font("system", 7), Brushes.Black, 15, 133);

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
            Console.Beep(e.Y*10,e.X*10);
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

    class beep
    {
        private int freq;
        private int dur;

        public int Freq { get => freq; set => freq = value; }
        public int Dur { get => dur; set => dur = value; }
    }

}
