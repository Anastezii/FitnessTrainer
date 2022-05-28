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
    public partial class ProfileTrainer : Form
    {
        public ProfileTrainer()
        {
            InitializeComponent();
        }

        private void ProfileTrainer_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome";
            button1.Text = "Send activity programm or diet";
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new AddDiet();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new AddActivityProgram();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
