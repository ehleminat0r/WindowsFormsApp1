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

namespace WindowsFormsApp1.game
{
    public partial class GameForm : Form
    {
        // Controls
        private bool left = false;
        private bool right = false;
        private bool up = false;
        private bool down = false;

        // Objects
        private Tank player = new Tank();

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
                // Delay
                System.Threading.Thread.Sleep(10);
                // Control
                CheckControls();
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
                gr.DrawString(Convert.ToString(player.Health), new Font("Arial", 16), new SolidBrush(Color.Black), 760, 5);
                gr.DrawEllipse(pen, player.PosX, player.PosY, 20, 20);
                gr.DrawLine(pen, player.PosX + 10, player.PosY + 10,
                    player.PosX + 10 + (int) (Math.Cos(player.Angle * Math.PI / 180) * 30),
                    player.PosY + 10 - (int) (Math.Sin(player.Angle * Math.PI / 180) * 30));
                if (player.IsShooting)
                {
                    gr.DrawRectangle(penBlack, player.PosX - 11, player.PosY - 41, 41, 6);
                    gr.FillRectangle(greenBrush , player.PosX - 10, player.PosY - 40, (float)(player.ShootPower*0.4), 5);
                }
            }
            pictureBoxGame.Image = bm;
            // not neccesary ?!
            //pictureBoxGame.Refresh();
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
                player.Shoot();
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
