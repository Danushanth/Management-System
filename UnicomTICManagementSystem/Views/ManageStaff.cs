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
    public partial class ManageStaff : Form
    {
        private readonly StaffController _controller = new StaffController();
        private int selectedStaffId = -1;
        public ManageStaff()
        {
            InitializeComponent();
            LoadStaff();


    //============================================================= BUTTON HIDE ==============================================================
            if (UserSession.Role != "Admin")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }

        }
        private void LoadStaff()
        {
            dgvStaff.DataSource = null;
            dgvStaff.DataSource = _controller.GetAllStaff();
            dgvStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvStaff.Columns["StaffId"] != null)
                dgvStaff.Columns["StaffId"].Visible = false;
        }
        //============================================================ ADD =====================================================================

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) &&
                !string.IsNullOrWhiteSpace(txtAddress.Text) &&
                int.TryParse(txtNIC.Text, out int nicNo))
            {
                _controller.AddStaff(txtName.Text.Trim(), txtAddress.Text.Trim(), nicNo);
                MessageBox.Show("Staff added successfully!", "Success");
                LoadStaff();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Please enter valid details.");
            }
        }
        // BACK BUTTON ===========================================================================================================================
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
        // UPDATE =========================================================================================================================================

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedStaffId > 0 &&
               !string.IsNullOrWhiteSpace(txtName.Text) &&
               !string.IsNullOrWhiteSpace(txtAddress.Text) &&
               int.TryParse(txtNIC.Text, out int nicNo))
            {
                _controller.UpdateStaff(selectedStaffId, txtName.Text.Trim(), txtAddress.Text.Trim(), nicNo);
                MessageBox.Show("Staff updated successfully!", "Updated");
                LoadStaff();
                ClearInputs();
                selectedStaffId = -1;
            }
            else
            {
                MessageBox.Show("Please select staff and fill details.");
            }
        }
        // DELETE ==================================================================================================================================

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedStaffId > 0)
            {
                var result = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    _controller.DeleteStaff(selectedStaffId);
                    MessageBox.Show("Staff deleted.", "Deleted");
                    LoadStaff();
                    ClearInputs();
                    selectedStaffId = -1;
                }
            }
            else
            {
                MessageBox.Show("Please select staff to delete.");
            }
        }
        // DGV BOX =================================================================================================================================================

        private void dgvStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvStaff.Rows[e.RowIndex];
                selectedStaffId = Convert.ToInt32(row.Cells["StaffId"].Value);
                txtName.Text = row.Cells["StaffName"].Value.ToString();
                txtAddress.Text = row.Cells["StaffAddress"].Value.ToString();
                txtNIC.Text = row.Cells["StaffNicNo"].Value.ToString();

            }
        }
        private void ClearInputs()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtNIC.Clear();
        }

        private void ManageStaff_Load(object sender, EventArgs e)
        {

        }

        private void dgvStaff_CellContentClick(object sender, EventArgs e)
        {

        }
    }
}
