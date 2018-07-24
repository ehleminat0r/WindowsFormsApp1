using System;
using System.Collections;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class EncryptDecrypt : Form
    {
        Hashtable hash = new Hashtable();
        public EncryptDecrypt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                listBox2.Items.Add(listBox1.SelectedItem.ToString());
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Boolean teilbar = false;
            int zahl = 30;
            while (!teilbar)
            {
                zahl++;
                teilbar = true;
                for (int i = 1; i < 20; i++)
                {
                    
                    if (zahl % i != 0)
                    {
                        teilbar = false;
                    }
                }
            }

            button3.Text = Convert.ToString(zahl);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                if (hash.ContainsKey(textBox1.Text.GetHashCode()))
                {
                    hash[textBox1.Text.GetHashCode()] = (int)hash[textBox1.Text.GetHashCode()]+1;
                }
                else
                {
                    hash[textBox1.Text.GetHashCode()] = 0;
                }

                listBox3.Items.Clear();
                foreach (DictionaryEntry test in hash)
                {
                    listBox3.Items.Add(test.Key+" "+test.Value);
                }


                var data = Encoding.UTF8.GetBytes(textBox1.Text);
                foreach (var bits in data)
                {
                    
                    listBox4.Items.Add(Convert.ToString(bits,2).PadLeft(8,'0'));
                }
                using (SHA512 shaM = new SHA512Managed())
                {
                    byte[] test = shaM.ComputeHash(data);
                    textBox2.Text = BitConverter.ToString(test).Replace("-", "");
                    
                }
                
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(e), "test");
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            progressBar1.ForeColor = Color.Aqua;
            progressBar1.PerformStep();
            progressBar1.BackColor = Color.Black;
            Invalidate();
            MessageBox.Show(Convert.ToString(e), "a");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxEncrypt.Text = Encrypt.EncryptString(textBoxString.Text, textBoxPassword.Text);
            Properties.Settings.Default.enc = textBoxEncrypt.Text;
            Properties.Settings.Default.Save();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxEncrypt.Text = Encrypt.DecryptString(textBoxEncrypt.Text, textBoxPassword.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                
                
            }
            
        }


        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            textBoxEncrypt.Text = Convert.ToString(tabControl1.SelectedIndex);
            switch (tabControl1.SelectedIndex)
            {
                    case 0:
                        new FootballAndStuff().ShowDialog();
                    break;
                case 1:
                    new MinusXHoch2().ShowDialog();
                    break;
                case 2:
                    new EncryptDecrypt().ShowDialog();
                    break;
                case 3:
                    new FallingApples().ShowDialog();
                    break;
                case 4:
                    new Asteriods().ShowDialog();
                    break;

            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBoxEncrypt.Text = Properties.Settings.Default.enc;
            //textBoxEncrypt.Text = "test";
        }
    }
}
