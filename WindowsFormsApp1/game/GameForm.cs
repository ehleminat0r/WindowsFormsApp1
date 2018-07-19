using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1.game
{
    public partial class GameForm : Form
    {
        // Mouse
        private const UInt32 MouseEventLeftDown = 0x0002;
        private const UInt32 MouseEventLeftUp = 0x0004;
        [DllImport("user32", EntryPoint = "mouse_event")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

        // Controls
        private bool left = false;
        private bool right = false;
        private bool up = false;
        private bool down = false;

        // Objects
        public Tank player = new Tank();
        public List<Missile> missiles = new List<Missile>();  

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //GameLoop
            while (true)
            {
                /*
                // Mouseclick
                mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                */
                // Delay
                System.Threading.Thread.Sleep(10);
                // Control
                CheckControls();
                // Move Missiles
                MoveMissiles();
                // Draw
                DrawGame();
            }
        }

        private void DrawGame()
        {
            Bitmap bm = new Bitmap(pictureBoxGame.Width, pictureBoxGame.Height);
            Pen pen = new Pen(player.TankColor);
            Pen penBlack = new Pen(Color.Black);
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                // Ground
                gr.FillRectangle(greenBrush, 0, 441, 800, 80);
                // Health
                gr.DrawString(Convert.ToString(player.Health), new Font("Arial", 16), new SolidBrush(Color.Black), 760, 5);
                // Player
                gr.DrawEllipse(pen, player.PosX, player.PosY, 20, 20);
                gr.DrawLine(pen, player.PosX + 10, player.PosY + 10,
                    player.PosX + 10 + (int) (Math.Cos(player.Angle * Math.PI / 180) * 30),
                    player.PosY + 10 - (int) (Math.Sin(player.Angle * Math.PI / 180) * 30));
                // Power Bar
                if (player.IsShooting)
                {
                    gr.DrawRectangle(penBlack, player.PosX - 11, player.PosY - 41, 41, 6);
                    gr.FillRectangle(greenBrush , player.PosX - 10, player.PosY - 40, (float)(player.ShootPower*0.4), 5);
                }
                // Missiles
                foreach (Missile miss in missiles)
                {
                    gr.DrawEllipse(penBlack, miss.X, miss.Y, 10, 10);
                }
                
            }
            pictureBoxGame.Image = bm;
            // not neccesary ?!
            //pictureBoxGame.Refresh();
        }

        private void MoveMissiles()
        {
            foreach (Missile miss in missiles)
            {
                if (miss.Y < 440)
                {
                    miss.X += miss.AccX;
                    miss.Y += miss.AccY;
                    miss.AccY += 1;
                }
                else
                {
                    miss.AccX = 0;
                    miss.AccY = 0;
                }
                
                
            }
        }

        private void CheckControls()
        {
            if (left)
            {
                player.PosX -= 2;
            }
            if (right)
            {
                player.PosX += 2;
            }
            if (up)
            {
                player.Angle++;
            }
            if (down)
            {
                player.Angle--;
            }
            if (player.IsShooting && player.ShootPower < 100)
            {
                player.ShootPower++;
            }
            else if (!player.IsShooting && player.ShootPower > 0)
            {
                player.Shoot(this);
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    left = true;
                    break;
                case Keys.D:
                    right = true;
                    break;
                case Keys.W:
                    up = true;
                    break;
                case Keys.S:
                    down = true;
                    break;
                case Keys.Space:
                    player.IsShooting = true;
                    break;
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    left = false;
                    break;
                case Keys.D:
                    right = false;
                    break;
                case Keys.W:
                    up = false;
                    break;
                case Keys.S:
                    down = false;
                    break;
                case Keys.Space:
                    player.IsShooting = false;
                    break;
            }
        }
    }
}
