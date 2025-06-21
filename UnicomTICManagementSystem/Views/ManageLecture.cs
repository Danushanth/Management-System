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
using UnicomTicManagementSystem.Controllers;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageLecture : Form
    {
        private readonly LectureController _controller = new LectureController();
        private int selectedLectureId = -1;
        public ManageLecture()
        {
            InitializeComponent();
            LoadLectures();
            LoadSubjects();

            if (UserSession.Role != "Admin")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }

            // ====================================== COMBOBOX ===========================================================================================

            cmbSubject.Items.Add("C#");
            cmbSubject.Items.Add("Python");
            cmbSubject.Items.Add("Java");
            cmbSubject.Items.Add("Java Skrept");
            cmbSubject.SelectedIndex = 0;
        }



        private void LoadLectures()
        {
            dgvLectures.DataSource = null;
            dgvLectures.DataSource = _controller.GetAllLectures();
            dgvLectures.Columns["LectureId"].Visible = false;
            dgvLectures.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            selectedLectureId = -1;
        }



        private void LoadSubjects()
        {
            cmbSubject.Items.Clear();
            cmbSubject.Items.AddRange(_controller.GetAllSubjects().ToArray());
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


        // ====================================== ADD ===========================================================================================

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                if (!int.TryParse(txtNic.Text.Trim(), out int nicNo))
                {
                    MessageBox.Show("Please enter a valid numeric NIC number (e.g. 123456789).", "Invalid NIC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var lecture = new Lecture
                {
                    LectureName = txtName.Text.Trim(),
                    LectureAddress = txtAddress.Text.Trim(),
                    LectureNicNo = int.Parse(txtNic.Text.Trim()),
                    Subject = cmbSubject.Text
                };

                _controller.AddLecture(lecture);
                MessageBox.Show("Lecture added successfully!");
                LoadLectures();
                ClearInputs();
            }
        }

        // ====================================== UPDATE ===========================================================================================

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedLectureId > 0 && ValidateInputs())
            {
                var lecture = new Lecture
                {
                    LectureId = selectedLectureId,
                    LectureName = txtName.Text.Trim(),
                    LectureAddress = txtAddress.Text.Trim(),
                    LectureNicNo = int.Parse(txtNic.Text.Trim()),
                    Subject = cmbSubject.Text
                };
                _controller.UpdateLecture(lecture);
                MessageBox.Show("Lecture updated successfully!");
                LoadLectures();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Please select a lecture to update.");
            }
        }

        // ====================================== DELETE ===========================================================================================

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedLectureId > 0)
            {
                var result = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    _controller.DeleteLecture(selectedLectureId);
                    MessageBox.Show("Lecture deleted successfully!");
                    LoadLectures();
                    ClearInputs();
                }
            }
            else
            {
                MessageBox.Show("Select a lecture to delete.");
            }
        }



        // ====================================== DGV BOX ===========================================================================================

        private void dgvLectures_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvLectures.Rows[e.RowIndex];
                selectedLectureId = Convert.ToInt32(row.Cells["LectureId"].Value);
                txtName.Text = row.Cells["LectureName"].Value.ToString();
                txtAddress.Text = row.Cells["LectureAddress"].Value.ToString();
                txtNic.Text = row.Cells["LectureNicNo"].Value.ToString();
                cmbSubject.Text = row.Cells["Subject"].Value.ToString();
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtNic.Text) ||
                string.IsNullOrWhiteSpace(cmbSubject.Text))
            {
                MessageBox.Show("All fields are required.");
                return false;
            }
            return true;
        }




        private void ClearInputs()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtNic.Clear();
            cmbSubject.SelectedIndex = -1;
            selectedLectureId = -1;
        }



        private void ManageLecture_Load(object sender, EventArgs e)
        {
        }

        private void dgvLectures_SelectionChanged(object sender, EventArgs e)
        {
            
        }
    }
}
