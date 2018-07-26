using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Tetris : Form
    {
        System.Timers.Timer time = new System.Timers.Timer();
        private int[,] gameField = new int[10, 16];
        Block stone = new LBlock();

        public Tetris()
        {
            this.Size = new Size(240, 380);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "Tetris";
            this.BackColor = Color.White;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            // Initialize GameField
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    gameField[i, j] = 0;
                }
            }

            // Few Blocks
            gameField[9, 15] = 1;
            gameField[8, 15] = 1;
            gameField[7, 15] = 1;
            gameField[8, 14] = 1;

            gameField[5, 15] = 0;

            // Initialize Timer
            time.Interval = 100;
            time.Elapsed += new ElapsedEventHandler(OnTick);
            time.Start();
        }

        private void OnTick(Object sender, ElapsedEventArgs e)
        {
            stone.Move(gameField);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Border
            e.Graphics.DrawRectangle(Pens.Black, 10, 10, 200, 320);
            // Moving Block
            Brush b = new SolidBrush(stone.Col);
            e.Graphics.FillRectangle(b, 11 + stone.X * 20, 11 + stone.Y * 20, 19, 19);

            // Set Blocks
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    if (gameField[i, j] == 0)
                    {
                        //leeres Feld
                    }
                    else if (gameField[i, j] == 1)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, 11 + i * 20, 11 + j * 20, 19, 19);
                    }
                }
            }
            Invalidate();
        }

        class Block
        {
            private int x = 5;
            private int y = 0;
            private Color col = new Color();
            public Color Col { get => col; set => col = value; }
            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }

            public Block()
            {
                Col = Color.Blue;
            }

            public void Move(int[,] gf)
            {
                if (y + 1 < gf.GetLength(1))
                {
                    if (gf[x, y + 1] == 0)
                    {
                        Y++;
                    }
                    else
                    {
                       // hit
                    }
                }
 
            }

        }

        class LBlock : Block
        {
            bool[,] shape = new bool[2,3];

            public LBlock()
            {
                shape[0, 0] = true;
                shape[0, 1] = true;
                shape[0, 2] = true;
                shape[1, 0] = false;
                shape[1, 1] = false;
                shape[1, 2] = true;
            }


        }
    }
}
