using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageSubjectsForm : Form
    {
        private readonly SubjectController _subjectController = new SubjectController();
        private readonly CourseController _courseController = new CourseController();
        private int selectedSubjectId = -1;

        public ManageSubjectsForm()
        {
            InitializeComponent();
            LoadCourses();
            LoadSubjects();

            if (UserSession.Role != "Admin")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }
//=============================================================================================================================================================
        private void LoadCourses()
        {
            var courses = _courseController.GetAllCourses();
            cmbCourses.DataSource = courses;
            cmbCourses.DisplayMember = "CourseName";
            cmbCourses.ValueMember = "CourseId";
        }

        private void LoadSubjects()
        {
            dgvSubjects.DataSource = null;
            dgvSubjects.DataSource = _subjectController.GetAllSubjects();
            dgvSubjects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSubjects.Columns["SubjectId"].Visible = false;
            dgvSubjects.Columns["CourseId"].Visible = false;
        }
        // ADD ================================================================================================================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        // UPDATE ==============================================================================================================================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        // DELETE ===============================================================================================================================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        // DATAGBOX===============================================================================================================================================
        private void dgvSubjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvSubjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvSubjects.Rows[e.RowIndex];
                selectedSubjectId = Convert.ToInt32(row.Cells["SubjectId"].Value);
                txtSubjectName.Text = row.Cells["SubjectName"].Value.ToString();
                cmbCourses.SelectedIndex = cmbCourses.FindStringExact(row.Cells["CourseName"].Value.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbCourses.SelectedValue != null && !string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                _subjectController.AddSubject(txtSubjectName.Text.Trim(), (int)cmbCourses.SelectedValue);
                MessageBox.Show("Subject added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSubjects();
                txtSubjectName.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a subject name and select a course.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedSubjectId > 0 && cmbCourses.SelectedValue != null && !string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                _subjectController.UpdateSubject(selectedSubjectId, txtSubjectName.Text.Trim(), (int)cmbCourses.SelectedValue);
                MessageBox.Show("Subject updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSubjects();
                txtSubjectName.Clear();
                selectedSubjectId = -1;
            }
            else
            {
                MessageBox.Show("Please select a subject to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedSubjectId > 0)
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this subject?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    _subjectController.DeleteSubject(selectedSubjectId);
                    MessageBox.Show("Subject deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSubjects();
                    txtSubjectName.Clear();
                    selectedSubjectId = -1;
                }
            }
            else
            {
                MessageBox.Show("Please select a subject to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ManageSubjectsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
