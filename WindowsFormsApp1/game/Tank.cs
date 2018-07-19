using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.game
{
    public class Tank
    {
        private int _posX;
        private int _posY;
        private int _angle;

        public int PosX
        {
            get => _posX;
            set
            {
                // 780 = window size - tank size
                if (value > 0 && value < 780)
                {
                    _posX = value;
                }
            }
        }

        public int PosY
        {
            get => _posY;
            set
            {
                // 400 = window size - tank size
                if (value > 0 && value < 440)
                {
                    _posY = value;
                }
            }
        }

        public int Angle
        {
            get => _angle;
            set
            {
                if (value > 0 && value < 180)
                {
                    _angle = value;
                }
            }
        }

        public Color TankColor { get; set; }
        public int Health { get; set; }
        public int ShootPower { get; set; }
        public bool IsShooting { get; set; }

        public Tank()
        {
            PosX = 10;
            PosY = 420;
            Angle = 90;
            TankColor = Color.Blue;
            Health = 100;
            IsShooting = false;
            ShootPower = 0;
        }

        public Tank(int pX, int pY, int ang, Color col)
        {
            PosX = pX;
            PosY = pY;
            Angle = ang;
            TankColor = col;
            Health = 100;
            IsShooting = false;
            ShootPower = 0;
        }

        public void Shoot(GameForm sender)
        {
            sender.missiles.Add(new Missile(sender.player.PosX, sender.player.PosY, (int)(Math.Cos(Angle * Math.PI / 180) * (int)(ShootPower / 2)), -(int)(Math.Sin(Angle * Math.PI / 180) * (int)(ShootPower/3))-8));
            ShootPower = 0;
        }
    }
}
