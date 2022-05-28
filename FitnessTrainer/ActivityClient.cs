using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTrainer
{
    public partial class ActivityClient : Form
    {
        public ActivityClient()
        {
            InitializeComponent();
        }

        private void ActivityClient_Load(object sender, EventArgs e)
        {
            label1.Text = "Activity type :";
            label2.Text = "Time of activity :";
            label3.Text = "Calories burned :";
            label4.Text = "Your username :";
            label5.Text = "Date :";
            input.Text = "Input data";
            back.Text = "Back to profile page";
            
            steps.Text = "Input steps";
        }

        private void input_Click(object sender, EventArgs e)
        {

            int id = 0;

            if (type.Text == "" || time.Text == "" || dateTimePicker1.Value == null || username.Text == "" || calories.Text == "")
            {
                MessageBox.Show("Please input all necessary data.");
                return;
            }

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `fitness_instructor`.`activity` (`activity_type`, `activity_time`, `activity_data`, `userId`, `activity_calories`) VALUES (@type,@time,@date,@client,@calories)", db.getConnection());

           
            command.Parameters.AddWithValue("@type", type.Text);
            command.Parameters.AddWithValue("@time", time.Text);
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            
            command.Parameters.AddWithValue("@calories", calories.Text);


            db.openConnection();


            MySqlCommand command2 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`user` WHERE `username`=@username", db.getConnection());
            command2.Parameters.Add("@username", MySqlDbType.VarChar).Value = username.Text;
            id = Convert.ToInt32(command2.ExecuteScalar());

            command.Parameters.AddWithValue("@client", MySqlDbType.Int32).Value = id;

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Successfully input activity. ");
            else
                MessageBox.Show("Sorry error for input activity.");



            //int id = getid(username.Text);


            db.closeConnection();

            StreamWriter activity = new StreamWriter(@"C:\Users\Анастасия\Desktop\универ\курсов проект тсп фитнес-инструктор\FitnessTrainer\FitnessTrainer\FitnessTrainer\Resources\activity.txt");
            activity.WriteLine("Type : "+type.Text, '\n');
            activity.WriteLine("Minutes : " + time.Text, '\n');
            activity.WriteLine("Date : " + dateTimePicker1.Value, '\n');
            activity.WriteLine("Calories burned : " + calories.Text, '\n');
            activity.WriteLine("Username : " + username.Text, '\n');
            activity.Close();

        }

      /*  public int getid(string usernameStr) {

            int id = 0;

            DB db = new DB();

           
            db.openConnection();


            if (command2.ExecuteNonQuery() == 1)

                return id; 
            else
                MessageBox.Show("Sorry error for your username.Check and try again.");
            return 0;

            db.closeConnection();


        }*/

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new ProfileClient();
            frm.Show();
        }

        private void steps_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new InputSteps();
            frm.Show();
        }

        private void previousDays_Click(object sender, EventArgs e)
        {
           // to add previous days by file
           
        }
    }
}
