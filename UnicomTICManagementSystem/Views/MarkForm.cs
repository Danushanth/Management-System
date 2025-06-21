using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem.Repositories;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class MarkForm : Form
    {
        

        private  readonly MarkController _controller = new MarkController();
        public MarkForm()
        {

            InitializeComponent();
            LoadMarks();
            LoadStudents();
            LoadExams();
        }
        private void LoadMarks()
        {
            dgvMarks.DataSource = null;
            dgvMarks.DataSource = _controller.GetAllMarks();
            dgvMarks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadStudents()
        {
           
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new System.Data.SQLite.SQLiteCommand("SELECT StudentID, Name FROM Students", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ManageStudent.Items.Add(new ComboBoxItem
                    {
                        Text = reader.GetString(1),
                        Value = reader.GetInt32(0)
                    });
                }
            }
        }
        private void LoadExams()
        {
            
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new System.Data.SQLite.SQLiteCommand("SELECT ExamID, ExamName FROM Exams", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ExamForm.Items.Add(new ComboBoxItem
                    {
                        Text = reader.GetString(1),
                        Value = reader.GetInt32(0)
                    });
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var mark = new Mark
            {
                StudentId = ((ComboBoxItem)ManageStudent.SelectedItem).Value,
                ExamId = ((ComboBoxItem)ExamForm.SelectedItem).Value,
                Score = int.Parse(txtScore.Text)
            };
            _controller.AddMark(mark);
            LoadMarks();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMarks.CurrentRow != null)
            {
                var mark = new Mark
                {
                    MarkId = (int)dgvMarks.CurrentRow.Cells["Id"].Value,
                    StudentId = ((ComboBoxItem)ManageStudent.SelectedItem).Value,
                    ExamId = ((ComboBoxItem)ExamForm.SelectedItem).Value,
                    Score = int.Parse(txtScore.Text)
                };
                _controller.UpdateMark(mark);
                LoadMarks();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMarks.CurrentRow != null)
            {
                int id = (int)dgvMarks.CurrentRow.Cells["Id"].Value;
                _controller.DeleteMark(id);
                LoadMarks();
                ClearFields();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMarks.CurrentRow != null)
            {
                txtScore.Text = dgvMarks.CurrentRow.Cells["Score"].Value.ToString();
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void ClearFields()
        {
            txtScore.Text = "";
            ManageStudent.SelectedIndex = -1;
            ExamForm.SelectedIndex = -1;
        }

        private void dgvMarks_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}
