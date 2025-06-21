using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnicomTICManagementSystem.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UnicomTICManagementSystem.Views
{
    public partial class LoginForm : Form
    {
       
        private Dictionary<string, (string Password, string Role)> users = new Dictionary<string, (string, string)>
        {
            { "admin", ("admin123", "Admin") },
            { "student01", ("student123", "Student") },
            { "lecturer01", ("lec123", "Lectures") },
            { "staff01", ("staff123", "Staff") }
        };

        public LoginForm()
        {
            InitializeComponent();
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Student");
            comboBox1.Items.Add("Lectures");
            comboBox1.Items.Add("Staff");

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            if (users.ContainsKey(username) && users[username].Password == password)
            {
                string role = users[username].Role;

                
                UserSession.Role = role;
                if (role == "Admin")
                {
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();

                }
                else
                {
                    
                    Form1 mainForm = new Form1();
                    mainForm.Show();
                }
               

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Clear();
                txtPassword.Clear();
                txtUserName.Focus();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


   
}
