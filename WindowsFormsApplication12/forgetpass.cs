using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterBillManagementSystem.BLL;

namespace WindowsFormsApplication12
{
    public partial class forgetpass : Form
    {
        private readonly UserService _userService;

        public forgetpass()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string newPassword = txtNewPass.Text;
            string confirmPassword = txtConfirmPass.Text;

            if (!int.TryParse(txtSerialId.Text.Trim(), out int serialId) || serialId <= 0) // Giả sử TextBox mới tên là txtSerialId
            {
                MessageBox.Show("Please enter a valid positive Serial ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSerialId.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("New Password cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New Password and Confirm Password do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Clear();
                txtConfirmPass.Clear();
                txtNewPass.Focus();
                return;
            }

            try
            {
                bool success = _userService.ResetPasswordBySerialId(serialId, newPassword); // Gọi hàm mới

                if (success)
                {
                    MessageBox.Show("Password has been reset successfully. Please log in with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    // Mở lại form Login
                    login loginForm = Application.OpenForms.OfType<login>().FirstOrDefault();
                    if (loginForm != null && !loginForm.Visible) { loginForm.Show(); }
                    else if (loginForm == null) { new login().Show(); }
                }
                else
                {
                    // Lỗi có thể do Serial ID không tồn tại hoặc lỗi DB khi update
                    MessageBox.Show("Failed to reset password. Serial ID might not exist or a database error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

            // Hiển thị lại form Login nếu nó đang bị ẩn
            login loginForm = Application.OpenForms.OfType<login>().FirstOrDefault();
            if (loginForm != null && !loginForm.Visible)
            {
                loginForm.Show();
            }
            else if (loginForm == null)
            {
                new login().Show();
            }
        }
    }
}
