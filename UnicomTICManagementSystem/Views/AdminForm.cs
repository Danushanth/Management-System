using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicomTICManagementSystem.Views
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            LoadForm(new Form());
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

// =========================================================================================================================================================================
        public void LoadForm(object formObj)
        {
            Form form = formObj as Form;   

            if (form == null) return;    

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            AdminPanel.Controls.Clear();
            AdminPanel.Controls.Add(form);
            form.Show();
        }


// ===========================================================================================================================================================================
        private void AdminPanel_Paint(object sender, PaintEventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LoadForm(new ManageLecture());
        }

// ================================================================================== PANEL SCREEN =============================================================================       
        private void LoadFormInPanel1(AdminForm frm)
        {
            AdminPanel.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            AdminPanel.Controls.Add(frm);
            frm.Show();
        }
        private void LoadFormInPanel(AdminForm frm)
        {
            AdminPanel.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            AdminPanel.Controls.Add(frm);
            frm.Show();
        }

// ===================================================================================================================================================================

        private void button2_Click(object sender, EventArgs e)
        {
                    
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }     
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

//======================================================================= VIWE ==========================================================================================
        private void button3_Click_1(object sender, EventArgs e)
        {
            LoadForm(new ManageLecture());
        }
        private void button6_Click(object sender, EventArgs e)
        {
            LoadForm(new ManageStudent());
        }
        private void button5_Click(object sender, EventArgs e)
        {
            LoadForm(new ManageStaff());
        }
        private void button7_Click(object sender, EventArgs e)
        {
            LoadForm(new ManageCoursesForm());
        }
        private void button8_Click(object sender, EventArgs e)
        {
            LoadForm(new ManageSubjectsForm());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Hide();
        }
        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            LoadForm(new ExamForm());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            LoadForm(new MarkForm());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            LoadForm(new ManageTimetable());
        }
    }
}
