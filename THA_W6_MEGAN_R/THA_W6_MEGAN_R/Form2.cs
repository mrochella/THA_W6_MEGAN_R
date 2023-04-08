using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THA_W6_MEGAN_R
{
    public partial class Form2 : Form
    {
        public Form1 frm1;
        List<string> words;
        string[] myKeyboard;
        string suiii, msg;
        Button[,] buttons;
        int banyaknya, panjangnya;
        int x, y, up, down;
        int guessX, guessY;
        int sudahBnr = 0; //cek bener gak
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            banyaknya = Form1.instance.input;
            panjangnya = 5;
            x = 10;
            y = 10;
            guessX = 0;
            guessY = 0;
            myKeyboard = new string[26] { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M" };
            buttons = new Button[panjangnya, banyaknya];
            for (int i = 0; i < panjangnya; i++)
            {
                for (int j = 0; j < banyaknya; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(50, 50);
                    buttons[i, j].Location = new Point(x, y);
                    buttons[i, j].Tag = i.ToString() + "," + j.ToString();

                    this.Controls.Add(buttons[i, j]);
                    y += 50;
                }
                x += 50;
                y = 10;
            }
            up = 303;
            down = 36;

            foreach (string apa in myKeyboard)
            {
                if (apa == "A")
                {
                    up = 330;
                    down = 85;
                }
                if (apa == "Z")
                {
                    up = 380;
                    down = 136;
                }
                Button butt_Key = new Button();
                butt_Key.Text = apa;
                butt_Key.Location = new Point(up, down);
                butt_Key.Size = new Size(45, 45);
                butt_Key.Click += butt_Key_Click;

                this.Controls.Add(butt_Key);
                up += 47;
            }
            Button butt_Enter = new Button();
            butt_Enter.Location = new Point(305, 137);
            butt_Enter.Size = new Size(71, 45);
            butt_Enter.Text = "Enter";
            butt_Enter.Click += butt_Enter_Click;
            this.Controls.Add(butt_Enter);
            Button butt_Del = new Button();
            butt_Del.Location = new Point(709, 137);
            butt_Del.Size = new Size(70, 45);
            butt_Del.Text = "Delete";
            butt_Del.Click += butt_Del_Click;
            this.Controls.Add(butt_Del);

            string file = "Wordle Word List.txt"; //nama file difolder
            string[] whatWord = File.ReadAllLines(file);
            words = new List<string>();
            foreach (string thisWord in whatWord)
            {
                words.AddRange(thisWord.Split(','));
            }
            suiii = words[new Random().Next(words.Count - 1)].ToUpper();
        }
        private void butt_Key_Click(object sender, EventArgs e)
        {
            var dot = sender as Button;
            if (guessX != 5)
            {
                buttons[guessX, guessY].Text = dot.Text;
                guessX++;
            }
        }
        private void butt_Enter_Click(object sender, EventArgs e)
        {
            if (guessX == 5)
            {
                string keyword = "";
                for (int i = 0; i < guessX; i++)
                {
                    keyword += buttons[i, guessY].Text;
                }
                string bingo = keyword.ToLower();
                if (!words.Contains(bingo)) //jawaban tidak ditemukan
                {
                    msg = "We can't find that word in our list...";
                    MessageBox.Show(msg, "BOOP BOOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (int i = 0; i < guessX; i++)
                    {
                        if (suiii.Contains(buttons[i, guessY].Text))
                        {
                            buttons[i, guessY].BackColor = Color.LightYellow;
                        }
                        if (suiii[i].ToString() == buttons[i, guessY].Text)
                        {
                            buttons[i, guessY].BackColor = Color.SpringGreen;
                            sudahBnr++;
                        }
                    }
                    guessY++;
                    if (sudahBnr == 5) //jawaban benar yey
                    {
                        msg = "You won this round. Play again?";
                        MessageBox.Show(msg, "Game end! Correct word: " + suiii, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else if (sudahBnr != 5 && guessY == banyaknya) //boo jawaban salah
                    {
                        msg = "You lost this round. Play again?";
                        MessageBox.Show(msg, "Game over! Correct word: " + suiii, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        guessX = 0;
                    }
                }
            }
            else
            {
                msg = "The guessed words must be 5 items!";
                MessageBox.Show(msg, "BOOP BOOP!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void butt_Del_Click(object sender, EventArgs e)
        {
            if (guessX != 0)
            {
                guessX--; //jalan mundur
                buttons[guessX, guessY].Text = ""; //delete biar isi button jadi kosong
            }
            else
            {
                msg = "There is nothing to delete..";
                MessageBox.Show(msg, "BOOP BOOP!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

