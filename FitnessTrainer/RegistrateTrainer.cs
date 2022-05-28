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
    public partial class RegistrateTrainer : Form
    {
        public RegistrateTrainer()
        {
            InitializeComponent();
        }

        private void RegistrateTrainer_Load(object sender, EventArgs e)
        {
            button1.Text = " Registrate ";
            groupBox1.Text = " Sex : ";
            radioButton1.Text = " Female ";
            radioButton2.Text = " Male ";
            label1.Text = " Your name : ";
            label2.Text = " Your worker number : ";
            label3.Text = " Your experience :  ";
            label4.Text = " Pick an username for login to system : ";
            label5.Text = " Pick a password for login to system : ";
            label7.Text = " Rigistartion ";
          
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new Form();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            int id = 0;

            string sex = "";

            if (radioButton1.Checked)
            {
                sex = "Female";
            }
            else if (radioButton2.Checked)
            {
                sex = "Male";
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

            if (name.Text == "" || number.Text == "" || sex == "" || experience.Text=="" || username.Text=="" || password.Text=="" ) {
                MessageBox.Show("Please input all necessary data.");
                return;
            }

            if (ifUserExist()) {
                name.Clear();
                number.Clear();
                experience.Clear();
                username.Clear();
                password.Clear();
                MessageBox.Show("This user is already exist.Please check and try again.");
                return;
            }
               


            DB db = new DB();


            MySqlCommand command = new MySqlCommand("INSERT INTO `fitness_instructor`.`trainer` (`trainer_name`, `trainer_number`, `trainer_sex`, `trainer_exprience`) VALUES (@name,@number,@sex,@expirience)", db.getConnection());
            // INSERT INTO `fitness_instructor`.`trainer` (`idtrainer`, `trainer_name`, `trainer_number`, `trainer_sex`, `trainer_exprience`) VALUES ('3', 'Harry Style', '225', 'Male', '2');
            command.Parameters.AddWithValue("@name",name.Text);
            command.Parameters.AddWithValue("@number", number.Text);
            command.Parameters.AddWithValue("@sex",sex);
            command.Parameters.AddWithValue("@expirience",experience.Text);


            

            // INSERT INTO `fitness_instructor`.`user` (`iduser`, `username`, `password`, `user_type`, `cl_id`) VALUES ('1', 'mimi', '0909', '1', '1');


            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Successfully registration as a trainer. ");
            else 
                MessageBox.Show("Sorry error for registration as a trainer.");

            MySqlCommand command3 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`trainer` WHERE `trainer_number`=@tNb", db.getConnection());
            command3.Parameters.Add("@tNb", MySqlDbType.VarChar).Value = number.Text;
            id = Convert.ToInt32(command3.ExecuteScalar());

            MySqlCommand command2 = new MySqlCommand("INSERT INTO `fitness_instructor`.`user` (`username`, `password`, `user_type`, `tr_id`) VALUES (@username,@pass,'2',@tr_id)", db.getConnection());
            command2.Parameters.Add("@username", MySqlDbType.VarChar).Value = username.Text;
            command2.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password.Text;
            command2.Parameters.Add("@tr_id", MySqlDbType.Int32).Value = id;


            if (command2.ExecuteNonQuery() == 1)
            {
                this.Hide();
                Form frm = new ProfileTrainer();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Sorry error for registration as a user.Please check and try again.");
            }


            db.closeConnection();




            /*  if (command2.ExecuteNonQuery() == 1)
              {
                  MessageBox.Show("Successfully registration as a user. ");
              }
              else
              {
                  MessageBox.Show("Sorry error for registration as a user.");
              }*/





            /* DB db1 = new DB();

             MySqlCommand command3 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`trainer` WHERE `trainer_number`=@tNb", db1.getConnection());
             command3.Parameters.Add("@tNb", MySqlDbType.VarChar).Value = trNumber;
             int id = Convert.ToInt32(command3.ExecuteScalar());

            */
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
            else {
               
                return false;
            }
        }



    }


}
