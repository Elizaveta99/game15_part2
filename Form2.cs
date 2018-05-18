using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace game_15
{
    public partial class Form2 : Form
    {
        private List<result> resList;

        public Form2(List<result> res)
        {
            InitializeComponent();
            resList = res;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            Application.OpenForms[0].Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (resList != null && resList.Count != 0)
            {
                CompareGameTime comp = new CompareGameTime();
                resList.Sort(comp);
                Print();
            }
            else
            {
                MessageBox.Show("Results aren't found!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (resList != null && resList.Count != 0)
            {
                CompareShifts comp = new CompareShifts();
                resList.Sort(comp);
                Print();
            }
            else
            {
                MessageBox.Show("Results aren't found!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (resList != null && resList.Count != 0)
            {
                CompareTime comp = new CompareTime();
                resList.Sort(comp);
                Print();
            }
            else
            {
                MessageBox.Show("Results aren't found!");
            }
        }

        private void Print()
        {
            textBox1.Clear();
            textBox1.AppendText("Name           Game time              Start time                            Shifts");
            for (int i = 0; i < Math.Min(10, resList.Count); i++)
            {
                String text = resList[i].Name + "           " + resList[i].GameTime.ToString(@"mm\:ss") + "           " + resList[i].StartTime.ToString(@"dd\/MM\/yyyy\; HH\:mm\:ss") + "                " + resList[i].Shifts.ToString();
                textBox1.AppendText(Environment.NewLine);
                textBox1.AppendText(text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime time;
            if (resList != null && resList.Count != 0)
            {
                if (DateTime.TryParse(textBox2.Text, out time))
                {
                    CompareTime comp = new CompareTime();
                    resList.Sort(comp);
                    for (int i = resList.Count - 1; i > -1 ; i--)
                        if (resList[i].StartTime <= time)
                            resList.RemoveAt(i);
                    textBox1.Clear();
                    textBox1.AppendText("Name           Game time              Start time                            Shifts");
                }
                else
                {
                    MessageBox.Show("Incorrect time!");
                }
            }
            else
            {
                MessageBox.Show("There are no results yet!");
            }
        }
    }
}

