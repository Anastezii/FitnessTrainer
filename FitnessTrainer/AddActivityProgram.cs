using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessTrainer
{
    public partial class AddActivityProgram : Form
    {
        public AddActivityProgram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MailMessage mm = new MailMessage(address.Text.Trim(), client.Text.Trim()))
            {
                if (radioButton1.Checked) { mm.Subject = "Activity program."; }
                else if (radioButton2.Checked) { mm.Subject = "Diet."; }
                //mm.Subject = subject.Text;
                mm.Body = body.Text;
                foreach (string filePath in openFileDialog1.FileNames)
                {
                    if (File.Exists(filePath))
                    {
                        string fileName = Path.GetFileName(filePath);
                        mm.Attachments.Add(new Attachment(filePath));
                    }
                }
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(address.Text.Trim(), pass.Text.Trim());
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                MessageBox.Show("Email sent.", "Message");
            }
        }

        private void AddActivityProgram_Load(object sender, EventArgs e)
        {
            label1.Text = "To :";
            label2.Text = "Subject :";
            label3.Text = "Body :";
            label4.Text = "Attachment :";
            label5.Text = "Gmail address :";
            label6.Text = "Password :";
            linkLabel1.Text = "Attach";
            button1.Text = "Send";
            button2.Text = "Return to profile page";
            groupBox1.Text = "Type of program:";
            radioButton1.Text = "Activity";
            radioButton2.Text = "Diet";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frm = new ProfileTrainer();
            frm.Show();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            foreach (string filePath in openFileDialog1.FileNames)
            {
                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);
                    label4.Text += fileName + Environment.NewLine;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
