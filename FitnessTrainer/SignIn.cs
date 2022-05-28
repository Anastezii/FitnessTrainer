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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Load(object sender, EventArgs e)
        {
            label1.Text = " Please sign in to system ... ";
            label2.Text = " Username : ";
            label3.Text = " Password : ";
            SignInButton.Text = "SignIn";
          
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            String loginUser = username.Text;
            String passUser = pass.Text;

            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            //command for checking login and password
            MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `username`=@uL AND `password`=@uP", db.getConnection());

            //prisvoqem na parameters @uL @uP parameters from input text + type varchar
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            //say to adapter which command to do
            adapter.SelectCommand = command;

            //fill table if exist this user with inputen login and password
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            { //if exist

                if (isTrainer())
                {

                    this.Hide();
                    Form frm = new ProfileTrainer();
                    frm.Show();
                }
               else 
                {
                    this.Hide();
                    Form frm = new ProfileClient();
                    frm.Show();
                }
                ///  form -> profile  this.Hide();
            }                                           // Form frm = new Registration();
                                                        // frm.Show();
            else
            {                     //if not exist
                MessageBox.Show(" Not correct username or pass please check and try again. ");
            }
        }


        public Boolean isTrainer() {

           int type = 2;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command4 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`user` WHERE `username`=@username AND `user_type`=@uType", db.getConnection());
            command4.Parameters.Add("@username", MySqlDbType.VarChar).Value = username.Text;
            command4.Parameters.Add("@uType", MySqlDbType.Int32).Value = type;
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
        public Boolean isClient()
        {

            int type = 1;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command4 = new MySqlCommand("SELECT * FROM `fitness_instructor`.`user` WHERE `user_type`=@uType", db.getConnection());
            command4.Parameters.Add("@uType", MySqlDbType.Int32).Value = type;
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

        public String getUsername() {
            String user = Convert.ToString(username.Text);
            return user;
        }

    }
}
