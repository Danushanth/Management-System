using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Views;

namespace UnicomTICManagementSystem
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
           // LoadForm(new Form1());
           if (UserSession.Role == "Staff" )
            {
                Admin.Enabled = false;
                Lecturer.Enabled = false;
            }
           else if (UserSession.Role =="Lectures")
            {
                Admin.Enabled = false;
                Staff.Enabled = false;
            }else if (UserSession.Role == "Student")
            {
                Admin.Enabled = false;
                Lecturer.Enabled = false;
                Staff.Enabled = false;
            }
        }

        // ===========================================================================================================================
        public void LoadForm(object formObj)
        {
            if (this.mainPanel.Controls.Count > 0)
            {
                this.mainPanel.Controls.RemoveAt(0);
            }

            Form form = formObj as Form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(form);
            this.mainPanel.Tag = form;
            form.Show();
        }
        // ==============================================================================================================================
        private void button2_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            form.Show();
            this.Hide();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRoleBasedForm();
           
        }
        private void LoadRoleBasedForm()
        {
            
        }



        //==================================================================================================================================

        private void button1_Click(object sender, EventArgs e)
        {
            
            
               LoginForm loginForm = new LoginForm();
               loginForm.Show();
               this.Hide();
            
           
                         
            
        }



        //======================================================= LECTURN ====================================================================
        private void button3_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManageLecture());
        }
        //===================================================== STAFF ========================================================================
        private void button4_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManageStaff());
        }
        // =================================================  STUDENT ==========================================================================

        private void button5_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManageStudent());
        }


        
        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void LoadFormWithPermission(Form form)
        {
            if (UserSession.Role == "Admin")
            {
                mainPanel.Controls.Clear();
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                mainPanel.Controls.Add(form);
                form.Show();
            }
            else
            {
                
                if ((UserSession.Role == "Student" && form is ManageStudent) ||
                    (UserSession.Role == "Lectures" && form is ManageLecture) ||
                    (UserSession.Role == "Staff" && form is ManageStaff))
                {
                    mainPanel.Controls.Clear();
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(form);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Access Denied. You are not allowed to view this section.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void LoadFormInPanel2(Form frm)
        {
            mainPanel.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(frm);
            frm.Show();
        }

        private void LoadFormInPanel1(Form frm)
        {
            mainPanel.Controls.Clear(); 
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(frm); 
            frm.Show(); 
        }
        
        private void LoadFormInPanel(Form frm)
        {
            DashBoard.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            DashBoard.Controls.Add(frm);
            frm.Show();
        }





        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManageCoursesForm());
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManageSubjectsForm());
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManageTimetable());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new MarkForm());
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            LoadFormInPanel(new ExamForm());
        }
    }

}
