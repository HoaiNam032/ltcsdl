using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WaterBillManagementSystem.BLL;

namespace WindowsFormsApplication12
{
    public partial class login : Form
    {
        private readonly UserService _userService;

        public login()
        {
            InitializeComponent();
            _userService = new UserService();
            this.AcceptButton = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username and Password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool isAuthenticated = _userService.AuthenticateUser(username, password);

                if (isAuthenticated)
                {
                    Home H = new Home();
                    H.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                    textBox2.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Login Exception: {ex.ToString()}");
            }
        }

        private void button3_Click(object sender, EventArgs e) // Nút Signup
        {
            signup s = new signup();
            s.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e) // Nút Admin Login
        {
            adminlogin Al = new adminlogin();
            Al.Show();
            this.Hide();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            forgetpass forgetPassForm = new forgetpass();
            this.Hide();
            forgetPassForm.ShowDialog();

            if (!this.Visible && (Application.OpenForms.OfType<forgetpass>().Count() == 0))
            {
                if (!this.IsDisposed)
                {
                    if (Application.OpenForms.OfType<login>().Count() == 0)
                    {
                        new login().Show();
                    }
                }
                else
                {
                    new login().Show();
                }
            }
        }
    }

}