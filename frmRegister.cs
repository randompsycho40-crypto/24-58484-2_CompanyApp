using System;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class frmRegister : Form
    {
        private User user = new User();

    public frmRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Check required fields
            if (txtUsername.Text.Trim() == "" ||
                txtPassword.Text == "" ||
                txtConPassword.Text == "")
            {
                MessageBox.Show(
                    "Please fill in all fields.",
                    "Registration Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // Check password confirmation
            if (txtPassword.Text != txtConPassword.Text)
            {
                MessageBox.Show(
                    "Passwords do not match. Please re-enter.",
                    "Registration Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtConPassword.Clear();
                txtPassword.Focus();

                return;
            }

            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                // Check whether username already exists
                if (user.UsernameExists(username))
                {
                    MessageBox.Show(
                        "Username already exists. Please choose another username.",
                        "Registration Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtUsername.Focus();

                    return;
                }

                // Register the new user
                bool success = user.RegisterUser(username, password);

                if (success)
                {
                    MessageBox.Show(
                        "Your account has been successfully created.",
                        "Registration Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Clear fields after successful registration
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtConPassword.Clear();
                    txtUsername.Focus();
                }
                else
                {
                    MessageBox.Show(
                        "Registration failed. Please try again.",
                        "Registration Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Registration failed.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConPassword.Clear();
            txtUsername.Focus();
        }

        private void clickLogin_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();

            login.Show();

            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
        }
    }

}
