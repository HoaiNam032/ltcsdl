using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WaterBillManagementSystem.BLL;

namespace WindowsFormsApplication12
{
    public partial class Techsupport : UserControl
    {
        private readonly SupportService _supportService;
        public Techsupport()
        {
            InitializeComponent();
            _supportService = new SupportService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string description = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please enter your problem description.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            try
            {
                bool success = _supportService.SubmitTicket(description);

                if (success)
                {
                    MessageBox.Show("We have received your problem description and will respond as soon as possible.", "Submission Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                }
                else
                {
                    MessageBox.Show("Failed to submit your request. Please try again later.", "Submission Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
