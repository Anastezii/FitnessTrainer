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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            groupBox1.Text = "Sex";
            radioButton1.Text = "Female";
            radioButton2.Text = "Male";
            RegistrateButton.Text = "Registrate";
            label1.Text = "Your name : ";
            label2.Text = "Your height (in cm) : ";
            label3.Text = "Your weight : ";
            groupBox2.Text = "Your goals";
            radioButton3.Text = "Lose weight";
            radioButton4.Text = "Weight gain";
            label5.Text = " Pick an username for login to system : ";
            label6.Text = " Pick a password for login to system : ";
            label4.Text = " Registration ";
            label7.Text = "Your amount calories for a day :";
        }

        private void RegistrateButton_Click(object sender, EventArgs e)
        {

            int id = 0;

            string sex = "";
            string goal = "";

            if (radioButton1.Checked)
            {
                sex = "Female";
            }
            else if (radioButton2.Checked)
            {
                sex = "Male";
            }



             if (radioButton3.Checked)
            {
                goal = "Lose weight";
            }
            else if (radioButton4.Checked) {
                goal = "Weight gain";
            }
                /*
                            Boolean isTrue;
                            string Pass = "";

                            if (password == passRepeat)
                            {
                                isTrue = true;
                                Pass = password.Text;
                            }
                            else {
                                isTrue = false;
                                MessageBox.Show("Not correct password in one of the field please check and try again.");
                                return;
                            }

                            */

                if (name.Text == "" || height.Text == "" || sex == "" || goal=="" || weight.Text == "" || username.Text == "" || pass.Text == "")
            {
                MessageBox.Show("Please input all necessary data.");
                return;
            }

            if (ifUserExist())
            {
                name.Clear();
                height.Clear();
                weight.Clear();
                username.Clear();
                pass.Clear();
                MessageBox.Show("This user is already exist.Please check and try again.");
                return;
            }



            DB db = new DB();


            MySqlCommand command = new MySqlCommand("INSERT INTO `fitness_instructor`.`client` (`client_name`, `client_height`, `client_weight`, `client_goals`, `client_sex`) VALUES (@name,@height,@weight,@goals,@sex)", db.getConnection());
            // INSERT INTO `fitness_instructor`.`client` (`idclient`, `client_name`, `client_height`, `client_weight`, `client_goals`, `client_sex`) VALUES
            command.Parameters.AddWithValue("@name", name.Text);
            command.Parameters.AddWithValue("@height", height.Text);
            command.Parameters.AddWithValue("@weight", weight.Text);
            command.Parameters.AddWithValue("@goals", goal);
            command.Parameters.AddWithValue("@sex", sex);
            


            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Successfully registration as a client. ");
            else
                MessageBox.Show("Sorry error for registration as a client.");

            MySqlCommand command3 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`client` WHERE `client_name`=@clName", db.getConnection());
            command3.Parameters.Add("@clName", MySqlDbType.VarChar).Value = name.Text;
            id = Convert.ToInt32(command3.ExecuteScalar());

            MySqlCommand command2 = new MySqlCommand("INSERT INTO `fitness_instructor`.`user` (`username`, `password`, `user_type`, `cl_id`) VALUES (@username,@pass,'1',@cl_id)", db.getConnection());
            command2.Parameters.Add("@username", MySqlDbType.VarChar).Value = username.Text;
            command2.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass.Text;
            command2.Parameters.Add("@cl_id", MySqlDbType.Int32).Value = id;


            MySqlCommand command5 = new MySqlCommand("INSERT INTO * FROM `fitness_instructor`.`food`(`amount_calorie`) VALUES (@ckal)", db.getConnection());
            command3.Parameters.AddWithValue("@ckal",calories.Text);

            if (command2.ExecuteNonQuery() == 1)
            {
                this.Hide();
                Form frm = new ProfileClient();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Sorry error for registration as a user.Please check and try again.");
            }


            db.closeConnection();




        }

        public Boolean ifUserExist()
        {

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command4 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`user` WHERE `username`=@uName", db.getConnection());
            command4.Parameters.Add("@uName", MySqlDbType.VarChar).Value = username.Text;
            adapter.SelectCommand = command4;
            adapter.Fill(table);


            if (table.Rows.Count > 0)
            {

                return true;
            }
            else
            {

                return false;
            }

        }
    }
}
