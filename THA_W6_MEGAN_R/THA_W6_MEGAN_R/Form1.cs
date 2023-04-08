using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THA_W6_MEGAN_R
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public Form1 form1;
        public int input;
        public Form1()
        {
            InitializeComponent();
            form1 = this;
            instance = this;
        }
        Label label = new Label();
        MaskedTextBox tbox = new MaskedTextBox();
        Button butt_play = new Button();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.RosyBrown;
            label.Text = "     WELCOME TO WORDLE!\nSet how much you can guess..";
            label.Font = new Font("Arial", label.Font.Size, FontStyle.Bold);
            label.Size = new Size(200, 200);
            label.Location = new Point(90, 50);

            tbox.Location = new Point(90, 100);
            tbox.Size = new Size(150, 200);
            tbox.Mask = "000";
            tbox.ValidatingType = typeof(int);

            butt_play.Text = "Play!";
            butt_play.Location = new Point(130, 125);
            butt_play.BackColor = Color.White;

            this.Controls.Add(butt_play);
            this.Controls.Add(tbox);
            this.Controls.Add(label);
            butt_play.Click += butt_play_Click;
        }
        private void butt_play_Click(object sender, EventArgs e)
        {
            input = Convert.ToInt32(tbox.Text);
            if (input < 3)
            {
                string msg = "The set number must be greater than 3.";
                MessageBox.Show(msg, "BOOP BOOP!", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                Form2 frm2 = new Form2();
                frm2.frm1 = this;
                frm2.ShowDialog();
            }
        }
    }
}
