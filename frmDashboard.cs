using System;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }


        private void frmDashboard_Load(object sender, EventArgs e)
        {
            // Show currently logged-in user
            lblCreatedBy.Text =
                "Logged in User ID: " + Session.UserID +
                " | Username: " + Session.Username;
        }


        private void btnEmployeeDetails_Click(object sender, EventArgs e)
        {
            // Open Employee Details
            frmEmployee employeeForm =
                new frmEmployee();

            employeeForm.ShowDialog();
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to logout?",
                    "Logout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                // Clear logged-in user information
                Session.Clear();


                // Create a NEW Login form
                frmLogin login =
                    new frmLogin();

                login.Show();


                // Close Dashboard
                this.Close();
            }
        }
    }
}
