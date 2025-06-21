using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{


    public partial class ExamForm : Form
    {
        ExamController controller = new ExamController();
        int selectedExamId = 0;

        public ExamForm()
        {

            InitializeComponent();
            cmdSubject.Items.Add("C#");
            cmdSubject.Items.Add("Pytho");
            cmdSubject.Items.Add("Java");
            cmdSubject.Items.Add("Artificial Intelligence ");
            cmdSubject.Items.Add("JavaSkript");

        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            var exam = new Exam
            {
                ExamName = txtName.Text,
                SubjectId = Convert.ToInt32(cmdSubject.SelectedValue)
            };
            controller.AddExam(exam);
            LoadExams();
            ClearFields();
        }




        private void ExamForm_Load(object sender, EventArgs e)
        {
            LoadExams();
            LoadSubjects();
        }
        private void LoadExams()
        {
            dgvExams.DataSource = controller.GetAllExams();
        }



        private void LoadSubjects()
        {

            var subjectController = new SubjectController();
            var subjects = subjectController.GetAllSubjects();

            if (subjects.Count > 0)
            {
                cmdSubject.DataSource = subjects;
                cmdSubject.DisplayMember = "SubjectName"; 
                cmdSubject.ValueMember = "SubjectId";     
                cmdSubject.SelectedIndex = -1;            
            }
            else
            {
                MessageBox.Show("No subjects found. Please add subjects first.");
            }
        }




        private void dgvExams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedExamId = Convert.ToInt32(dgvExams.Rows[e.RowIndex].Cells["ExamId"].Value);
                txtName.Text = dgvExams.Rows[e.RowIndex].Cells["ExamName"].Value.ToString();
                cmdSubject.SelectedValue = dgvExams.Rows[e.RowIndex].Cells["SubjectId"].Value;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var exam = new Exam
            {
                ExamId = selectedExamId,
                ExamName = txtName.Text,
                SubjectId = Convert.ToInt32(cmdSubject.SelectedValue)
            };
            controller.UpdateExam(exam);
            LoadExams();
            ClearFields();
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedExamId > 0)
            {
                controller.DeleteExam(selectedExamId);
                LoadExams();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            txtName.Clear();
            cmdSubject.SelectedIndex = -1;
            selectedExamId = 0;
        }

        private void dgvExams_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}
