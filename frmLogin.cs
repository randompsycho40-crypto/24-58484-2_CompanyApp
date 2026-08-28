
using System;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class frmLogin : Form
    {
        private User user = new User();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "" ||
                txtPassword.Text == "")
            {
                MessageBox.Show(
                    "Please enter username and password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                int userId = user.ValidateLogin(username, password);

                if (userId > 0)
                {
                    // Store logged-in user's information
                    Session.UserID = userId;
                    Session.Username = username;

                    // Open Dashboard
                    frmDashboard dashboard = new frmDashboard();
                    dashboard.Show();

                    // Hide Login form
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "Username or Password is incorrect.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database connection failed.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
                txtPassword.PasswordChar = '\0';
            else
                txtPassword.PasswordChar = '•';
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void clickRegister_Click(object sender, EventArgs e)
        {
            frmRegister register = new frmRegister();

            register.Show();

            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
