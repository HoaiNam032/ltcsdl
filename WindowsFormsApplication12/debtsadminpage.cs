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

namespace WindowsFormsApplication12
{
    public partial class debtsadminpage : UserControl
    {
        private readonly DebtService _debtService;
        public debtsadminpage()
        {
            InitializeComponent();
            _debtService = new DebtService();
            dataGridView1.DataSource = null;
        }

        private void LoadDebtData()
        {
            try
            {
                dataGridView1.DataSource = _debtService.GetAllDebts();
                if (dataGridView1.Columns["months"] != null) // Kiểm tra cột tồn tại
                    dataGridView1.Columns["months"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading all debt data: {ex.Message}", "Load Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
                System.Diagnostics.Debug.WriteLine($"LoadAllDebtData Exception: {ex.ToString()}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(serialtxt.Text.Trim(), out int serialId) || serialId <= 0)
            {
                MessageBox.Show("Please enter a valid positive Serial ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime selectedDate = dtpMonthDebt.Value;
            DateTime monthToAdd = selectedDate;

            try
            {
                bool success = _debtService.AddDebt(serialId, monthToAdd);
                if (success)
                {
                    MessageBox.Show("Debt record added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    serialtxt.Clear();
                    dtpMonthDebt.Value = DateTime.Today;
                    dataGridView1.DataSource = null;
                }
                else
                {
                    MessageBox.Show("Failed to add debt record. Ensure consumption record exists for the exact date/month and debt record doesn't already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding debt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"AddDebt Exception: {ex.ToString()}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(serialtxt.Text.Trim(), out int serialId) || serialId <= 0)
            {
                MessageBox.Show("Please enter a valid positive Serial ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime selectedDate = dtpMonthDebt.Value;
            DateTime monthToDelete = selectedDate;


            var confirmResult = MessageBox.Show($"Are you sure you want to delete the debt record for Serial ID {serialId} and Date {monthToDelete:dd/MM/yyyy}?",
                                       "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    bool success = _debtService.DeleteDebt(serialId, monthToDelete);
                    if (success)
                    {
                        MessageBox.Show("Debt record removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        serialtxt.Clear();
                        dtpMonthDebt.Value = DateTime.Today;
                        dataGridView1.DataSource = null;
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove debt record. It might not exist (check Serial ID and exact Date).", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine($"DeleteDebt Exception: {ex.ToString()}");
                }
            }
        }
            
        

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(serialtxt.Text.Trim(), out int serialId) || serialId <= 0)
            {
                MessageBox.Show("Please enter a valid positive Serial ID to search.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView1.DataSource = null;
                return;
            }

            try
            {
                DataTable dt = _debtService.GetDebtsBySerialId(serialId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    if (dataGridView1.Columns["months"] != null)
                        dataGridView1.Columns["months"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                else
                {
                    MessageBox.Show($"No debt records found for Serial ID: {serialId}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching debt data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
                System.Diagnostics.Debug.WriteLine($"SearchDebtBySerialId Exception: {ex.ToString()}");
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void debtsadminpage_Load(object sender, EventArgs e)
        {
            //LoadDebtData();
        }
    }
}
