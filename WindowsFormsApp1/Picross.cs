﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Picross : Form
    {
        private int size = 4;
        private bool[,] field = new bool[20, 20];
        private bool[,] fieldImport = new bool[20, 20];
        private List<List<int>> countX = new List<List<int>>();
        private List<List<int>> countY = new List<List<int>>();
        

        public Picross()
        {
            this.Size = new Size(635, 655);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "Picross";
            this.BackColor = Color.White;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            // initialize List
            for (int i = 0; i < 20; i++)
            {
                countX.Add(new List<int>());
                countX[i].Add(0);
            }
            for (int i = 0; i < 20; i++)
            {
                countY.Add(new List<int>());
                countY[i].Add(0);
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < size * 5; i++)
            {
                for (int j = 0; j < size * 5; j++)
                {
                    // Draw Blue lines
                    if (i % 5 == 0)
                    {
                        e.Graphics.DrawLine(Pens.Blue, 99 + i * 25, 0, 99 + i * 25, size * 5 * 25 + 98);
                    }
                    if (j % 5 == 0)
                    {
                        e.Graphics.DrawLine(Pens.Blue, 0, 99 + j * 25, size * 5 * 25 + 98, 99 + j * 25);
                    }

                    // Draw field
                    if (field[i, j] == false)
                    {
                        e.Graphics.DrawRectangle(Pens.Black, 100 + i * 25, 100 + j * 25, 23, 23);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Black, 100 + i * 25, 100 + j * 25, 24, 24);
                    }
                }
            }
            // Draw Text
            for (int i = 0; i < size * 5; i++)
            {
                for (int j = 0; j < countX[i].Count; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(countX[i][j]), new Font("system", 10), Brushes.Black,
                        10 + (15 * j), 104 + i * 25);
                }
                    
            }
            for (int i = 0; i < size * 5; i++)
            {
                for (int j = 0; j < countY[i].Count; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(countY[i][j]), new Font("system", 10), Brushes.Black,
                        104 + i * 25, 10 + (15 * j));
                }

            }
            // Select Size
            e.Graphics.DrawString(" 5 x  5", new Font("system", 10), Brushes.Black, 5, 10);
            e.Graphics.DrawString("10 x 10", new Font("system", 10), Brushes.Black, 5, 30);
            e.Graphics.DrawString("15 x 15", new Font("system", 10), Brushes.Black, 5, 50);
            e.Graphics.DrawString("20 x 20", new Font("system", 10), Brushes.Black, 5, 70);
            e.Graphics.DrawRectangle(Pens.Black, 55, 15, 10, 10);
            e.Graphics.DrawRectangle(Pens.Black, 55, 35, 10, 10);
            e.Graphics.DrawRectangle(Pens.Black, 55, 55, 10, 10);
            e.Graphics.DrawRectangle(Pens.Black, 55, 75, 10, 10);
            switch (size)
            {
                case 1:
                    e.Graphics.FillRectangle(Brushes.Black, 55, 15, 10, 10);
                    break;
                case 2:
                    e.Graphics.FillRectangle(Brushes.Black, 55, 35, 10, 10);
                    break;
                case 3:
                    e.Graphics.FillRectangle(Brushes.Black, 55, 55, 10, 10);
                    break;
                case 4:
                    e.Graphics.FillRectangle(Brushes.Black, 55, 75, 10, 10);
                    break;
            }
            System.Threading.Thread.Sleep(50);
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            string[] zeilen = new string[20];
            // create reader & open file
            TextReader tr = new StreamReader("save.txt");

            for (int i = 0; i < 20; i++)
            {
                string zeile = tr.ReadLine();
                zeilen = zeile.Split(' ');
                for (int j = 0; j < 20; j++)
                {
                    if (zeilen[j] == "False")
                    {
                        field[j, i] = false;
                    }
                    else
                    {
                        field[j, i] = true;
                    }
                }
            }

            for (int i = 0; i < 20; i++)
            {
                countX[i].Clear();
                countY[i].Clear();
            }
            int count = 0;
            string save = tr.ReadLine();
            while (save != "countX")
            {
                zeilen = save.Split(' ');
                for (int i = 0; i < zeilen.Length-1; i++)
                {
                    countX[count].Add(Convert.ToInt32(zeilen[i]));
                }
                count++;
                save = tr.ReadLine();
            }
            save = tr.ReadLine();
            count = 0;
            while (save != "countY")
            {
                zeilen = save.Split(' ');
                for (int i = 0; i < zeilen.Length - 1; i++)
                {
                    countY[count].Add(Convert.ToInt32(zeilen[i]));
                }
                count++;
                save = tr.ReadLine();
            }

            // close the stream
            tr.Close();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            
            if (e.X - 100 >= 0 && ((e.X - 100) / 25) <= field.GetLength(0) -1 &&
                e.Y - 100 >= 0 && ((e.Y - 100) / 25) <= field.GetLength(1) -1)
            {
                if (field[(e.X - 100) / 25, (e.Y - 100) / 25] == true)
                {
                    field[(e.X - 100) / 25, (e.Y - 100) / 25] = false;
                }
                else
                {
                    field[(e.X - 100) / 25, (e.Y - 100) / 25] = true;
                }
                CheckField();
            }

            if (e.X >= 55 && e.X <= 65 && e.Y >= 15 && e.Y <= 25)
            {
                size = 1;
                this.Size = new Size(260, 280);
            }
            if (e.X >= 55 && e.X <= 65 && e.Y >= 35 && e.Y <= 45)
            {
                size = 2;
                this.Size = new Size(385, 405);
            }
            if (e.X >= 55 && e.X <= 65 && e.Y >= 55 && e.Y <= 65)
            {
                size = 3;
                this.Size = new Size(510, 530);
            }
            if (e.X >= 55 && e.X <= 65 && e.Y >= 75 && e.Y <= 85)
            {
                size = 4;
                this.Size = new Size(635, 655);
            }
        }

        void CheckField()
        {
            // X
            int count = 0;
            bool lastwastrue = false;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                countX[i].Clear();
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[j, i] == true)
                    {
                        count++;
                        lastwastrue = true;
                    }
                    else
                    {
                        if (lastwastrue)
                        {
                            countX[i].Add(count);
                            count = 0;
                            lastwastrue = false;
                        }
                    }
                }
                // letztes feld checken
                if (field[field.GetLength(1) - 1, i])
                {
                    countX[i].Add(count);
                    lastwastrue = false;
                }

                // falls nichts im feld ist 0 schreiben
                if (countX[i].Count == 0)
                {
                    countX[i].Add(0);
                }
                count = 0;
            }

            // Y
            lastwastrue = false;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                countY[i].Clear();
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == true)
                    {
                        count++;
                        lastwastrue = true;
                    }
                    else
                    {
                        if (lastwastrue)
                        {
                            countY[i].Add(count);
                            count = 0;
                            lastwastrue = false;
                        }
                    }
                }
                // letztes feld checken
                if (field[i, field.GetLength(0) - 1])
                {
                    countY[i].Add(count);
                    lastwastrue = false;
                }

                // falls nichts im feld ist 0 schreiben
                if (countY[i].Count == 0)
                {
                    countY[i].Add(0);
                }
                count = 0;
            }
        }
    }
}
