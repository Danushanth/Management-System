using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;
using UnicomTicManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageStudent : Form
    {
        private int updateStudentId;

     

        public ManageStudent()
        {
            InitializeComponent();
            this.TopLevel = false;       
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            comboBox1.Items.Add("Arts stream");
            comboBox1.Items.Add("Commerce stream");
            comboBox1.Items.Add("Maths stream");
            comboBox1.Items.Add("Scince stream");
            comboBox1.SelectedIndex = 0;



            StudentController stcontroller = new StudentController();
            List<Student> st = stcontroller.GetStudents();
            DgvStudent.DataSource = st;

            txtN.Text = "";
            txtA.Text = "";
            txtAge.Text = string.Empty;

            if (UserSession.Role != "Admin")
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        // ADD ===================================================================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtN.Text) || string.IsNullOrWhiteSpace(txtA.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter Name, Address, Username and Password.");
                return;
            }

           
            Student student = new Student
            {
                Name = txtN.Text,
                Address = txtA.Text,
                Age = int.TryParse(txtAge.Text, out int ageVal) ? ageVal : 0,  
                Username = textBox1.Text,
                Password = textBox2.Text,
                Role = "Student" 
            };

            StudentController studentController = new StudentController();
            string outputmessage = studentController.AddStudentWithUser(student);
            MessageBox.Show(outputmessage);

          
            txtN.Text = "";
            txtA.Text = "";
            txtAge.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";

           
            List<Student> st = studentController.GetStudents();
            DgvStudent.DataSource = st;
        }

// UPDATE =============================================================================================================

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (updateStudentId == 0)
            {
                MessageBox.Show("Please select a student to update.");
                return;
            }

            Student student = new Student
            {
                Id = updateStudentId,
                Name = txtN.Text,
                Address = txtA.Text,
                Age = int.TryParse(txtAge.Text, out int ageVal) ? ageVal : 0,
                Username = textBox1.Text,
                Password = textBox2.Text,
                Role = "Student" 
            };

            StudentController stcontroller = new StudentController();
            string updateMessage = stcontroller.UpdateStudent(student);
            MessageBox.Show(updateMessage);

            txtN.Text = "";
            txtA.Text = "";
            txtAge.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";

            List<Student> st = stcontroller.GetStudents();
            DgvStudent.DataSource = st;
        }

// DELETE ============================================================================================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.Id = updateStudentId;

            StudentController stcontroller = new StudentController();
            string deleteMessage = stcontroller.DeleteStudent(student.Id);
            MessageBox.Show(deleteMessage);

            txtN.Text = "";
            txtA.Text = "";
            txtAge.Text = string.Empty;
            List<Student> st = stcontroller.GetStudents();
            DgvStudent.DataSource = st;
        }
// DGV BOX =============================================================================================================
        private void DgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {          
        }
        private void ManageStudent_Load(object sender, EventArgs e)
        {
        }
        private void txtAge_TextChanged(object sender, EventArgs e)
        {
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


// =====================================================================================================================
        private void DgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var student = (Student)DgvStudent.Rows[e.RowIndex].DataBoundItem;
                if (student != null)
                {
                    
                    txtN.Text = student.Name;
                    txtA.Text = student.Address;
                    txtAge.Text = student.Age.ToString();
                    textBox1.Text = student.Username;
                    textBox2.Text = student.Password;
                    
                }
            }
        }

// ======================================================================================================================


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                LoginForm Form = new LoginForm();
                Form.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Login Form: " + ex.Message);
            }
        }

        private void DgvStudent_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
