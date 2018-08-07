using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Picross : Form
    {
        private bool[,] field = new bool[20, 20];
        private List<List<int>> countX = new List<List<int>>();
        private List<List<int>> countY = new List<List<int>>();

        public Picross()
        {
            this.Size = new Size(640, 670);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "Picross";
            this.BackColor = Color.White;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            // initialize List
            for (int i = 0; i < field.GetLength(0); i++)
            {
                countX.Add(new List<int>());
                countX[i].Add(0);
            }
            for (int i = 0; i < field.GetLength(1); i++)
            {
                countY.Add(new List<int>());
                countY[i].Add(0);
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    // Draw Blue lines
                    if (i % 5 == 0)
                    {
                        e.Graphics.DrawLine(Pens.Blue, 99 + i * 25, 0, 99 + i * 25, field.GetLength(0) * 25 + 98);
                    }
                    if (j % 5 == 0)
                    {
                        e.Graphics.DrawLine(Pens.Blue, 0, 99 + j * 25, field.GetLength(1) * 25 + 98, 99 + j * 25);
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
            for (int i = 0; i < countX.Count; i++)
            {
                for (int j = 0; j < countX[i].Count; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(countX[i][j]), new Font("system", 10), Brushes.Black,
                        10 + (15 * j), 104 + i * 25);
                }
                    
            }
            for (int i = 0; i < countY.Count; i++)
            {
                for (int j = 0; j < countY[i].Count; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(countY[i][j]), new Font("system", 10), Brushes.Black,
                        104 + i * 25, 10 + (15 * j));
                }

            }

            Invalidate();
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
