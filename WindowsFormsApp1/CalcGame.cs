using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CalcGame : Form
    {
        private int count = 50;
        Boolean right = true;
        private int numOne;
        private int numTwo;
        private int result;
        private int basiccalc = 0;
        private static Random rnd = new Random();

        public CalcGame()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (secondsToolStripMenuItem.Checked) //3
            {
                count = 30;
            }
            else if (secondsToolStripMenuItem1.Checked) //5
            {
                count = 50;
            }
            else if (secondsToolStripMenuItem2.Checked) //10
            {
                count = 100;
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            numOne = rnd.Next(21) - 10;
            numTwo = rnd.Next(21) - 10;
            label1.Text = Convert.ToString(numOne);
            label2.Text = Convert.ToString(numTwo);
            switch (basiccalc)
            {
                case 0:
                    result = numOne + numTwo;
                    break;
            }

            button1.Text = Convert.ToString(rnd.Next(41) - 20);
            button2.Text = Convert.ToString(rnd.Next(41) - 20);
            button3.Text = Convert.ToString(rnd.Next(41) - 20);
            button4.Text = Convert.ToString(rnd.Next(41) - 20);

            switch (rnd.Next(3))
            {
                case 0:
                    button1.Text = Convert.ToString(result);
                    break;
                case 1:
                    button2.Text = Convert.ToString(result);
                    break;
                case 2:
                    button3.Text = Convert.ToString(result);
                    break;
                case 3:
                    button4.Text = Convert.ToString(result);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(button1.Text) == result)
            {
                right = true;
                Invalidate();
            }
            else
            {
                right = false;
                Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(button2.Text) == result)
            {
                right = true;
                Invalidate();
            }
            else
            {
                right = false;
                Invalidate();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(button3.Text) == result)
            {
                right = true;
                Invalidate();
            }
            else
            {
                right = false;
                Invalidate();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(button4.Text) == result)
            {
                right = true;
                Invalidate();
            }
            else
            {
                right = false;
                Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = true;
            subtractionToolStripMenuItem.Checked = false;
            multiplicationToolStripMenuItem.Checked = false;
            divisionToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = false;
        }

        private void subtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = false;
            subtractionToolStripMenuItem.Checked = true;
            multiplicationToolStripMenuItem.Checked = false;
            divisionToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = false;
        }

        private void multiplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = false;
            subtractionToolStripMenuItem.Checked = false;
            multiplicationToolStripMenuItem.Checked = true;
            divisionToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = false;
        }

        private void divisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = false;
            subtractionToolStripMenuItem.Checked = false;
            multiplicationToolStripMenuItem.Checked = false;
            divisionToolStripMenuItem.Checked = true;
            randomToolStripMenuItem.Checked = false;
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem.Checked = false;
            subtractionToolStripMenuItem.Checked = false;
            multiplicationToolStripMenuItem.Checked = false;
            divisionToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = true;
        }

        private void secondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            secondsToolStripMenuItem.Checked = true;
            secondsToolStripMenuItem1.Checked = false;
            secondsToolStripMenuItem2.Checked = false;
            timer1.Interval = 3000;
            count = 30;
        }

        private void secondsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            secondsToolStripMenuItem.Checked = false;
            secondsToolStripMenuItem1.Checked = true;
            secondsToolStripMenuItem2.Checked = false;
            timer1.Interval = 5000;
            count = 50;
        }

        private void secondsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            secondsToolStripMenuItem.Checked = false;
            secondsToolStripMenuItem1.Checked = false;
            secondsToolStripMenuItem2.Checked = true;
            timer1.Interval = 10000;
            count = 100;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillPie(Brushes.Black, 240, 75, 30, 30, 0, ((float)50/count)*360);
            if (right)
            {
                e.Graphics.FillEllipse(Brushes.GreenYellow, 240, 130, 30, 30);
            }
            else
            {
                e.Graphics.FillEllipse(Brushes.Red, 240, 130, 30, 30);
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            count--;

        }
    }
}