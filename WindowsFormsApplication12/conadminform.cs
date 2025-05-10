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
using WaterBillManagementSystem.Entities;
using System.Globalization;

namespace WindowsFormsApplication12
{
    public partial class conadminform : UserControl
    {
        private readonly BillingService _billingService;
        private readonly UserService _userService;

        public conadminform()
        {
            InitializeComponent();
            _billingService = new BillingService();
            _userService = new UserService();
            lblCustomerTypeDisplay.Text = "";
            textBox3.ReadOnly = true;
            dataGridView1.DataSource = null;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            // Lấy SerialID từ TextBox
            if (int.TryParse(textBox1.Text.Trim(), out int serialId) && serialId > 0)
            {
                try
                {
                    CustomerDTO customer = _userService.GetCustomerDetailsBySerialId(serialId);

                    if (customer != null)
                    {
                        // Hiển thị loại khách hàng dựa vào giá trị CustomerType
                        // Quy ước: 1 = Policy/Poor, 0 hoặc null = Other
                        string customerTypeDescription = "Other"; // Mặc định
                        if (customer.CustomerType.HasValue && customer.CustomerType.Value == 1)
                        {
                            customerTypeDescription = "Policy/Poor/Needy"; // Hoặc tên loại cụ thể
                        }
                        lblCustomerTypeDisplay.Text = $"Type: {customerTypeDescription}";
                        lblCustomerTypeDisplay.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        // Không tìm thấy khách hàng
                        lblCustomerTypeDisplay.Text = "Type: Not Found";
                        lblCustomerTypeDisplay.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    lblCustomerTypeDisplay.Text = "Type: Error";
                    lblCustomerTypeDisplay.ForeColor = System.Drawing.Color.Red;
                    MessageBox.Show($"Error retrieving customer type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Nếu SerialID không hợp lệ hoặc trống thì xóa hiển thị loại KH
                lblCustomerTypeDisplay.Text = "";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int serialId) || serialId <= 0)
            {
                MessageBox.Show("Please enter a valid positive Serial ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime monthToUse = dtpMonthConsumption.Value;

            if (!decimal.TryParse(textBox2.Text.Trim(), out decimal consumptionAmount) || consumptionAmount < 0)
            {
                MessageBox.Show("Please enter a valid non-negative Consumption Amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _billingService.AddConsumption(serialId, monthToUse, consumptionAmount);

                if (success)
                {
                    MessageBox.Show("Consumption record added and priced successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox2.Clear();
                    textBox3.Clear();
                    dtpMonthConsumption.Value = DateTime.Today;

                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        button2_Click(null, null);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to add consumption record. Customer might not exist or data for this date/month might already exist. Check logs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int serialId) || serialId <= 0)
            {
                MessageBox.Show("Please enter a valid positive Serial ID in the SerialID field to search.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView1.DataSource = null;
                return;
            }

            try
            {
                DataTable dt = _billingService.GetConsumptionBySerialId(serialId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show($"No consumption records found for Serial ID: {serialId}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching consumption data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a row in the grid to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int serialId;
                DateTime monthToDelete;

                if (!int.TryParse(dataGridView1.CurrentRow.Cells["SerialID"].Value?.ToString(), out serialId))
                {
                    MessageBox.Show("Cannot get Serial ID from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!(dataGridView1.CurrentRow.Cells["months"].Value is DateTime))
                {
                    if (!DateTime.TryParse(dataGridView1.CurrentRow.Cells["months"].Value?.ToString(), out monthToDelete))
                    {
                        MessageBox.Show("Cannot get valid Date from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    monthToDelete = (DateTime)dataGridView1.CurrentRow.Cells["months"].Value;
                }


                // Xác nhận xóa với thông tin lấy được từ grid
                var confirmResult = MessageBox.Show($"Are you sure you want to delete the record for Serial ID {serialId} and Date {monthToDelete:dd/MM/yyyy}?", // <-- Hiển thị ngày đầy đủ
                                           "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    bool success = _billingService.DeleteConsumptionRecord(serialId, monthToDelete);

                    if (success)
                    {
                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadConsumptionData(); 
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete record. It might have been deleted already or cannot be deleted (check for related debts?).", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox2.Text, out decimal consumptionAmount))
            {
                // Gọi BLL để lấy segment number gợi ý dựa trên ngưỡng mới
                int segment = _billingService.CalculateSegmentNumberBasedOnTiers(consumptionAmount);
                textBox3.Text = segment.ToString();
            }
            else
            {
                textBox3.Clear();
            }
        }

        private void LoadConsumptionData()
        {
            try
            {
                DataTable dt = _billingService.GetAllConsumption();
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading consumption data: {ex.Message}", "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "CustomerType")
            {
                int? customerTypeValue = null;
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (int.TryParse(e.Value.ToString(), out int typeValue))
                    {
                        customerTypeValue = typeValue;
                    }
                }

                // Dựa vào giá trị đã lấy được để hiển thị text mô tả
                if (customerTypeValue.HasValue)
                {
                    if (customerTypeValue.Value == 1)
                    {
                        e.Value = "Policy/Poor/Needy"; // Hiển thị text cho giá trị 1
                    }
                    else
                    {
                        e.Value = "Other"; // Hiển thị text cho giá trị 0
                    }
                }
                else // Nếu giá trị là NULL
                {
                    e.Value = "Other"; // Hiển thị text cho giá trị NULL
                }

                e.FormattingApplied = true;
            }

            else if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "price")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    try
                    {
                        decimal priceValue = Convert.ToDecimal(e.Value);
                        e.Value = priceValue.ToString("N0");
                        e.FormattingApplied = true;
                    }
                    catch (FormatException) { }
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "months")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    try
                    {
                        DateTime dateValue = Convert.ToDateTime(e.Value);
                        e.Value = dateValue.ToString("dd/MM/yyyy"); 
                        e.FormattingApplied = true;
                    }
                    catch (FormatException) { }
                }
            }
        }
    }
}
