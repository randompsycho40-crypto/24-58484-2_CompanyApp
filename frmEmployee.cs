using System;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class frmEmployee : Form
    {
        private Employee employee = new Employee();


        public frmEmployee()
        {
            InitializeComponent();

            LoadEmployeeData();
        }


        private void LoadEmployeeData()
        {
            dgvEmployeeDetails.DataSource =
                employee.GetEmployees();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text.Trim() == "" ||
                txtEmpName.Text.Trim() == "" ||
                txtAge.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Please fill in the required employee information.",
                    "Add Employee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            employee.EmpId =
                txtEmpId.Text.Trim();

            employee.EmpName =
                txtEmpName.Text.Trim();

            employee.Age =
                txtAge.Text.Trim();

            employee.ContactNo =
                txtContactNo.Text.Trim();


            if (cboGender.SelectedItem != null)
                employee.Gender =
                    cboGender.SelectedItem.ToString();
            else
                employee.Gender = "";


            // Store the currently logged-in user's UserID
            employee.CreatedBy =
                Session.UserID;


            try
            {
                bool success =
                    employee.InsertEmployee(employee);

                if (success)
                {
                    MessageBox.Show(
                        "Employee has been added successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadEmployeeData();
                    ClearControls();
                }
                else
                {
                    MessageBox.Show(
                        "Error occurred. Employee was not added.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error occurred while adding employee.\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Please select an employee first.",
                    "Update Employee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            employee.EmpId =
                txtEmpId.Text.Trim();

            employee.EmpName =
                txtEmpName.Text.Trim();

            employee.Age =
                txtAge.Text.Trim();

            employee.ContactNo =
                txtContactNo.Text.Trim();


            if (cboGender.SelectedItem != null)
                employee.Gender =
                    cboGender.SelectedItem.ToString();
            else
                employee.Gender = "";


            try
            {
                bool success =
                    employee.UpdateEmployee(employee);

                if (success)
                {
                    MessageBox.Show(
                        "Employee has been updated successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadEmployeeData();
                    ClearControls();
                }
                else
                {
                    MessageBox.Show(
                        "No employee was updated.",
                        "Update Employee",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error occurred while updating employee.\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Please select an employee first.",
                    "Delete Employee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to delete this employee?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);


            if (result != DialogResult.Yes)
                return;


            employee.EmpId =
                txtEmpId.Text.Trim();


            try
            {
                bool success =
                    employee.DeleteEmployee(employee);

                if (success)
                {
                    MessageBox.Show(
                        "Employee has been deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadEmployeeData();
                    ClearControls();
                }
                else
                {
                    MessageBox.Show(
                        "Employee was not deleted.",
                        "Delete Employee",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error occurred while deleting employee.\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }


        private void ClearControls()
        {
            txtEmpId.Clear();
            txtEmpName.Clear();
            txtAge.Clear();
            txtContactNo.Clear();

            cboGender.SelectedIndex = -1;
        }


        private void dgvEmployeeDetails_RowHeaderMouseClick(
            object sender,
            DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;


            DataGridViewRow row =
                dgvEmployeeDetails.Rows[e.RowIndex];


            // Bind using column names instead of fixed indexes
            txtEmpId.Text =
                row.Cells["EmpId"].Value?.ToString() ?? "";


            txtEmpName.Text =
                row.Cells["EmpName"].Value?.ToString() ?? "";


            txtAge.Text =
                row.Cells["EmpAge"].Value?.ToString() ?? "";


            txtContactNo.Text =
                row.Cells["EmpContact"].Value?.ToString() ?? "";


            cboGender.Text =
                row.Cells["EmpGender"].Value?.ToString() ?? "";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void cboGender_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
        }


        private void dgvEmployeeDetails_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }


        private void txtEmpId_TextChanged(
            object sender,
            EventArgs e)
        {
        }
    }
}
