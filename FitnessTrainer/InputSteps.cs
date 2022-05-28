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
    public partial class InputSteps : Form
    {
        public InputSteps()
        {
            InitializeComponent();
        }

        private void InputSteps_Load(object sender, EventArgs e)
        {
            label1.Text = "Amount of steps for a day :";
            label2.Text = "Amount of time :";
            label3.Text = "Amount of burned calories :";
            label4.Text = "Your username :";
            label5.Text = "Date :";
            input.Text = "Ok";
            back.Text = "Return to profile page";
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new ProfileClient();
            frm.Show();
        }

        private void input_Click(object sender, EventArgs e)
        {

            int id = 0;

            if (stepsAmount.Text == "" || minutes.Text == "" || dateTimePicker1.Value == null || username.Text == "" || calories.Text == "")
            {
                MessageBox.Show("Please input all necessary data.");
                return;
            }

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `fitness_instructor`.`activity` (`activity_type`, `activity_time`, `activity_data`, `userId`, `activity_calories`,`kol_foot`) VALUES ('steps',@time,@date,@client,@calories,@steps)", db.getConnection());


            command.Parameters.AddWithValue("@steps", stepsAmount.Text);
            command.Parameters.AddWithValue("@time", minutes.Text);
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@calories", calories.Text);



            db.openConnection();


            MySqlCommand command2 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`user` WHERE `username`=@username", db.getConnection());
            command2.Parameters.Add("@username", MySqlDbType.VarChar).Value = username.Text;
            id = Convert.ToInt32(command2.ExecuteScalar());

            command.Parameters.AddWithValue("@client", MySqlDbType.Int32).Value = id;

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Successfully input steps. ");
            int steps = Convert.ToInt32(stepsAmount.Text);

            if (steps >= 10000)
            {
                Form frm = new CongratulationSteps();
                frm.Show();

            }
            else
                MessageBox.Show("Sorry error for input steps.");



            //int id = getid(username.Text);


            db.closeConnection();

           

        }
    }
}
