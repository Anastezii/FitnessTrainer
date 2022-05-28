using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTrainer
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

            label1.Text = " Welcome to your personal fitness trainer ";
            label2.Text = " Please choose ... ";
           
            button1.Text = " SignIn ";
            button2.Text = "Registarte as client";
            button3.Text = "Registarte as trainer";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
             Form frm = new SignIn();
             frm.Show();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new Registration();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new RegistrateTrainer();
            frm.Show();
        }
    }
}
