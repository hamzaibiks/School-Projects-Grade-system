using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hihi
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            OpenForm5(4);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            OpenForm5(2);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenForm5(1);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenForm5(3);
        }

        private void OpenForm5(int juryNumber)
        {
            Form5 gradingForm = new Form5(juryNumber);

            gradingForm.FormClosed += (s, args) => this.Show();

            gradingForm.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Form4 calcForm = new Form4();

            calcForm.FormClosed += (s, args) => this.Show();

            calcForm.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
