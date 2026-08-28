
namespace EmployeeDetails
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnEmployeeDetails = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblCreatedBy = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI",
                20F,
                System.Drawing.FontStyle.Bold
            );
            this.lblTitle.Location = new System.Drawing.Point(250, 50);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Employee Management";

            // 
            // btnEmployeeDetails
            // 
            this.btnEmployeeDetails.Location = new System.Drawing.Point(280, 150);
            this.btnEmployeeDetails.Name = "btnEmployeeDetails";
            this.btnEmployeeDetails.Size = new System.Drawing.Size(240, 50);
            this.btnEmployeeDetails.TabIndex = 1;
            this.btnEmployeeDetails.Text = "EMPLOYEE DETAILS";
            this.btnEmployeeDetails.UseVisualStyleBackColor = true;
            this.btnEmployeeDetails.Click += new System.EventHandler(
                this.btnEmployeeDetails_Click
            );

            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(280, 230);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(240, 50);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "LOGOUT";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(
                this.btnLogout_Click
            );

            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font(
                "Segoe UI",
                11F,
                System.Drawing.FontStyle.Regular
            );
            this.lblCreatedBy.Location = new System.Drawing.Point(280, 320);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(100, 20);
            this.lblCreatedBy.TabIndex = 3;
            this.lblCreatedBy.Text = "Logged in User ID:";

            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);

            this.Controls.Add(this.lblCreatedBy);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnEmployeeDetails);
            this.Controls.Add(this.lblTitle);

            this.Name = "frmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Management Dashboard";

            this.Load += new System.EventHandler(
                this.frmDashboard_Load
            );

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnEmployeeDetails;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblCreatedBy;
    }
}
