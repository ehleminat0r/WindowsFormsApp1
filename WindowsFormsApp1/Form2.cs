using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (textBox1 == null)
            {
            }
            else
            {
                textBox1.Text = Convert.ToString(hScrollBar1.Value / (double) 2000);
            }

            Invalidate();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphicsObj;

            graphicsObj = this.CreateGraphics();

            Pen myPen = new Pen(System.Drawing.Color.Black, 2);

            graphicsObj.DrawLine(myPen,0,205+vScrollBar1.Value,this.Width,205 + vScrollBar1.Value);

            

            int save = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (i * i * Convert.ToDouble(textBox1.Text) > 200)
                {
                    save = i;
                    break;
                }
            }
            graphicsObj.DrawLine(myPen,save, +vScrollBar1.Value, save,205 + vScrollBar1.Value);

            myPen.Color = Color.Red;

            for (int i = -save; i < save; i++)
            {
                graphicsObj.DrawLine(myPen, i+save,vScrollBar1.Value+ i*i* (float)Convert.ToDouble(textBox1.Text), i + save+1, vScrollBar1.Value + (i+1) * (i+1) * (float)Convert.ToDouble(textBox1.Text));
                if (i>save-2)
                graphicsObj.DrawEllipse(myPen, i + save, 190, 10, 10);
            }
            graphicsObj.DrawEllipse(myPen,0,0,100,100);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void VScrollBar1_ValueChanged(object sender, EventArgs e) => this.Invalidate();
    }
}
