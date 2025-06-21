using System;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageCoursesForm : Form
    {
        private readonly CourseController _controller = new CourseController();

        public ManageCoursesForm()
        {
            InitializeComponent();
            LoadCourses();
            LoadCourseComboBox();

            if (UserSession.Role != "Admin")
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void LoadCourses()
        {
            dgvCourses.DataSource = null;
            dgvCourses.DataSource = _controller.GetAllCourses();
            dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadCourseComboBox()
        {
            comboBox1.Items.Clear();
            var courses = _controller.GetAllCourses();

            foreach (var course in courses)
            {
                comboBox1.Items.Add(course.CourseName);
            }
        }

        // ==================================== ADD ====================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                _controller.AddCourse(comboBox1.Text.Trim());
                LoadCourses();
                LoadCourseComboBox();
                comboBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a course name.");
            }
        }

        // ==================================== UPDATE ====================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCourses.CurrentRow != null && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                int courseId = (int)dgvCourses.CurrentRow.Cells["CourseId"].Value;
                _controller.UpdateCourse(courseId, comboBox1.Text.Trim());
                LoadCourses();
                LoadCourseComboBox();
                comboBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Please select a course and enter a new name.");
            }
        }

        // ==================================== DELETE ====================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.CurrentRow != null)
            {
                int courseId = (int)dgvCourses.CurrentRow.Cells["CourseId"].Value;

                var result = MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    _controller.DeleteCourse(courseId);
                    LoadCourses();
                    LoadCourseComboBox();
                    comboBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please select a course to delete.");
            }
        }

        private void dgvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCourses.CurrentRow != null)
            {
                comboBox1.Text = dgvCourses.CurrentRow.Cells["CourseName"].Value.ToString();
            }
        }

        private void ManageCoursesForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
