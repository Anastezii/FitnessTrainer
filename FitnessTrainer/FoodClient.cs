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
    public partial class FoodClient : Form
    {
        public FoodClient()
        {
            InitializeComponent();
        }

        private void FoodClient_Load(object sender, EventArgs e)
        {
            label1.Text = "Amount sum of calories for a day :";
            label2.Text = "Type of food : ";
            label3.Text = "Gramm :";
            label4.Text = "Calories:";
            label5.Text = "Part of day :";
            label6.Text = "Date : ";
            label7.Text = "Your username :";
            input.Text = "Input info";
           
            back.Text = "Back to profile window";
            
            

        }

        private void gr_TextChanged(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new ProfileClient();
            frm.Show();
        }

        private void caloriesForADay_Click(object sender, EventArgs e)
        {
           
        }

        private void beforeDays_Click(object sender, EventArgs e)
        {
            // add
        }

        private void input_Click(object sender, EventArgs e)
        {

            int id = 0;

            if (typeFood.Text == "" || gr.Text == "" || dateTimePicker1.Value == null || username.Text == "" || calories.Text == "" || partOfDay.Text=="")
            {
                MessageBox.Show("Please input all necessary data.");
                return;
            }

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `fitness_instructor`.`food` ( `food_type`, `food_amount`, `users_id`, `food_data`, `food_calories`,`amount_calories`, `partOfDay`) VALUES (@type,@amount,@client,@date,@calories,@sumCalories,@partOfDay)", db.getConnection());

            //INSERT INTO `fitness_instructor`.`food` (`idfood`, `food_type`, `food_amount`, `users_id`, `food_data`, `food_calories`, `amount_calories`, `partOfDay`) VALUES ('2', 'Chicken Soup', '1', '1', '2022-05-09', '100', '1786', 'dinner');


            command.Parameters.AddWithValue("@type",typeFood.Text);
            command.Parameters.AddWithValue("@amount",gr.Text);
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@sumCalories", amountCalories.Text);

            command.Parameters.AddWithValue("@calories", calories.Text);
            command.Parameters.AddWithValue("@partOfDay", partOfDay.Text);

            db.openConnection();


            MySqlCommand command2 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`user` WHERE `username`=@username", db.getConnection());
            command2.Parameters.Add("@username", MySqlDbType.VarChar).Value = username.Text;
            id = Convert.ToInt32(command2.ExecuteScalar());

            command.Parameters.AddWithValue("@client", MySqlDbType.Int32).Value = id;

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Successfully input food. ");
            else
                MessageBox.Show("Sorry error for input food.");




            db.closeConnection();

            StreamWriter activity = new StreamWriter(@"C:\Users\Анастасия\Desktop\универ\курсов проект тсп фитнес-инструктор\FitnessTrainer\FitnessTrainer\FitnessTrainer\Resources\food.txt");
            activity.WriteLine("Type food : " + typeFood.Text, '\n');
            activity.WriteLine("Gramms : " + gr.Text, '\n');
            activity.WriteLine("Date : " + dateTimePicker1.Value, '\n');
            activity.WriteLine("Calories burned : " + calories.Text, '\n');
            activity.WriteLine("Username : " + username.Text, '\n');
            activity.WriteLine("Amount of calories for a day : " + caloriesForADay.Text, '\n');
            activity.WriteLine("Part of the day : " + partOfDay.Text, '\n');

            activity.Close();

        

    }

        private void update_Click(object sender, EventArgs e)
        {
            // to do update for amount calories for a day ... 
        }


       /* public string getCalories() {

            DB db = new DB();

            SignIn sign=new SignIn() ;
           String user= sign.getUsername();

            int calories = 0;
            
            MySqlCommand command3 = new MySqlCommand("SELECT amount_calories FROM `fitness_instructor`.`food` WHERE ``=@tNb", db.getConnection());
            command3.Parameters.Add("@tNb", MySqlDbType.VarChar).Value = number.Text;
            calories = Convert.ToInt32(command3.ExecuteScalar());

            // MySqlCommand command5 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`food` WHERE `amount_calories`=@ckal", db.getConnection());
           // command3.Parameters.AddWithValue("@ckal", calories.Text);


            String calories = Convert.ToString(caloriesForADay.Text);

            return calories;
        
        }*/

    }
}
