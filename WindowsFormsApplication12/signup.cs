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
using WaterBillManagementSystem.Entities;

namespace WindowsFormsApplication12
{
    public partial class signup : Form
    {
        private readonly UserService _userService;
        public signup()
        {
            InitializeComponent();
            _userService = new UserService();
            this.AcceptButton = button2;
        }

        private void closebtn_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
     

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string username = textBox6.Text.Trim(); // Username từ textBox6
            string password = textBox2.Text.Trim(); // Password từ textBox2

            long nationalId;
            int serialId;

            // Lấy NationalID từ textBox1
            if (!long.TryParse(textBox1.Text.Trim(), out nationalId))
            {
                MessageBox.Show("Please enter a valid National ID (numeric).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            // Lấy SerialID từ textBox3
            if (!int.TryParse(textBox3.Text.Trim(), out serialId) || serialId <= 0)
            {
                MessageBox.Show("Please enter a valid positive Serial ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }
            string address = textBox4.Text.Trim(); // Address từ textBox4

            // Kiểm tra các trường bắt buộc không được trống
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please fill in all required fields (Username, Password, Address).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CustomerDTO newUser = new CustomerDTO
            {
                UserName = username,
                NationalID = nationalId,
                SerialID = serialId,
                Address = address,
                CustomerType = 0 // Mặc định là "Other"
            };

            try
            {
                bool success = _userService.RegisterUser(newUser, password);

                if (success)
                {
                    MessageBox.Show("Signup successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    login loginForm = Application.OpenForms.OfType<login>().FirstOrDefault();
                    if (loginForm == null)
                    {
                        loginForm = new login();
                        loginForm.Show();
                    }
                    else
                    {
                        loginForm.Show();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Signup failed. Username or Serial ID might already exist, or a database error occurred. Check logs for more details.", "Signup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred during signup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Signup Exception: {ex.ToString()}");
            }
        }
    }
}
