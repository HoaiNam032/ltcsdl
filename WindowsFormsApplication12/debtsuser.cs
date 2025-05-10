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
    public partial class debtsuser : UserControl
    {
        private readonly DebtService _debtService;
        public debtsuser()
        {
            InitializeComponent();
            _debtService = new DebtService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(serialtxt.Text.Trim(), out int serialId) || serialId <= 0)
            {
                MessageBox.Show("Please enter a valid Serial ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView1.DataSource = null;
                return;
            }

            try
            {
                DataTable dt = _debtService.GetUserDebtsWithPrice(serialId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    if (dataGridView1.Columns["months"] != null)
                        dataGridView1.Columns["months"].DefaultCellStyle.Format = "MM/yyyy"; // Hoặc "d/M/yyyy"
                    if (dataGridView1.Columns["price"] != null)
                        dataGridView1.Columns["price"].DefaultCellStyle.Format = "N0";
                }
                else
                {
                    MessageBox.Show("No outstanding debts found for this Serial ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving debt information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
                // Log lỗi
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
