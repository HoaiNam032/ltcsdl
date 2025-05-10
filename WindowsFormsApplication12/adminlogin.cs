using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterBillManagementSystem.BLL;

namespace WindowsFormsApplication12
{
    public partial class adminlogin : Form
    {
        private readonly AdminService _adminService;
        public adminlogin()
        {
            InitializeComponent();
            _adminService = new AdminService();
            this.AcceptButton = adminpassw;
        }

        private void adminpassw_Click(object sender, EventArgs e)
        {
            string enteredPassword = adminpass.Text;

            if (string.IsNullOrWhiteSpace(enteredPassword))
            {
                MessageBox.Show("Please enter the admin password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                adminpass.Focus();
                return;
            }

            try
            {
                bool isAuthenticated = _adminService.AuthenticateAdmin(enteredPassword);

                if (isAuthenticated)
                {
                    adminhome ad1 = new adminhome(); // Mở form Home của Admin
                    ad1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    adminpass.Clear();
                    adminpass.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred during admin login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Admin Login Exception: {ex.ToString()}");
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            login mainLoginForm = Application.OpenForms.OfType<login>().FirstOrDefault();

            if (mainLoginForm == null)
            {
                mainLoginForm = new login(); 
                mainLoginForm.Show();
            }
            else
            {
                mainLoginForm.Show();
            }

            this.Hide();
        }
    }
}
