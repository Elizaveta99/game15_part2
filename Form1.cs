using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;


namespace game_15
{
    public partial class Form1 : Form
    {
        private Game15 g = new Game15();
        private DateTime sttime;
        private string name;
        private int count;
        public static List<result> resList = new List<result>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (FileStream fr = new FileStream("result.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                resList = (List<result>)bf.Deserialize(fr);
            }

            count = 0;
            dgvGame15.RowCount = g.SIZE;
            dgvGame15.ColumnCount = g.SIZE;
            for (int i = 0; i < g.SIZE; i++)
            {
                dgvGame15.Columns[i].Width = 75;
                dgvGame15.Rows[i].Height = 75;
            }
            Show_game();
        }

        private void dgvGame15_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;
            int y = e.ColumnIndex;

            if (x - 1 >= 0 && g.table[x - 1, y] == 16)
            {
                g.change(x - 1, y, x, y);
                count++;
            }
            if (y - 1 >= 0 && g.table[x, y - 1] == 16)
            {
                g.change(x, y - 1, x, y);
                count++;
            }
            if (x + 1 <= 3 && g.table[x + 1, y] == 16)
            {
                g.change(x + 1, y, x, y);
                count++;
            }
            if (y + 1 <= 3 && g.table[x, y + 1] == 16)
            {
                g.change(x, y + 1, x, y);
                count++;
            }
            tbx1.Text = count.ToString();
            tbx1.Text = (DateTime.Now - sttime).ToString(@"mm\:ss");
            Show_game();
            if (isBuilt())
            {
                timer.Stop();
                MessageBox.Show("Game is built!" + "\r\n" + "Amount of shifts : " + count.ToString() + "\r\n" + "Time : " + (DateTime.Now - sttime).ToString(@"mm\:ss"));
            }
        }  

        private bool isBuilt()
        {
            int num = 1;
            for (int i = 0; i < g.SIZE; i++)
                for (int j = 0; j < g.SIZE; j++)
                    if (g.table[i, j] != num++)
                        return false;
            return true;
        }

        private void Show_game()
        {
            for (int i = 0; i < g.SIZE; i++)
                for (int j = 0; j < g.SIZE; j++)
                {
                    if (g.table[i, j] == 16)
                    {
                        dgvGame15.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                        dgvGame15.Rows[i].Cells[j].Value = "";
                    }
                    else
                    {
                        dgvGame15.Rows[i].Cells[j].Value = g.table[i, j].ToString();
                        dgvGame15.Rows[i].Cells[j].Style.BackColor = Color.LawnGreen;
                    }
                    dgvGame15.Rows[i].Cells[j].Selected = false;
                }
        }

        private void Process(int[] a)
        {
            count = 0;
            tbx1.Text = count.ToString();
            Show_game();
            sttime = DateTime.Now;
            timer.Start();
            if (g.no_desicion(a))
                MessageBox.Show("This game can't be built!");
            if (isBuilt())
            {
                timer.Stop();
                result res = new result();
                res.Name = name;
                res.StartTime = sttime;
                res.GameTime = DateTime.Now - sttime;
                res.Shifts = count;
                resList.Add(res);
                MessageBox.Show("Game is built!" + "\r\n"  +"Game number " + resList.Count.ToString() +  "\r\nAmount of shifts : " + count.ToString() + "\r\n" + "Time : " + (DateTime.Now - sttime).ToString(@"mm\:ss"));
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            int[] a = new int[g.SIZE * g.SIZE];
            a = g.startGame();
            Process(a);
        }

        private void retryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] a = new int[g.SIZE * g.SIZE];
            a = g.Read();
            int cnt = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] == 0) cnt++;          
            if (cnt < a.Length)
            {
                Process(a);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            tbx2.Text = (DateTime.Now - sttime).ToString(@"mm\:ss");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 F2 = new Form2(resList);
            this.Hide();
            F2.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (FileStream fs = new FileStream("result.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, resList);
            }
        }
    }
}
