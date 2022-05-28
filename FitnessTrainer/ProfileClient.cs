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
    public partial class ProfileClient : Form
    {
        public ProfileClient()
        {
            InitializeComponent();
        }

        private void ProfileClient_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome";
            button1.Text = "Request for diet or workout";
           
            button3.Text = "Add activity for today";
            button4.Text = "Add food for today";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new AddDiet();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new ActivityClient();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new FoodClient();
            frm.Show();
        }
    }
}
