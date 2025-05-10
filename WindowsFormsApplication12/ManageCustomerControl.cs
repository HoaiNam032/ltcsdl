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
using WaterBillManagementSystem.Entities;

namespace WindowsFormsApplication12
{
    public partial class ManageCustomerControl : UserControl
    {
        private readonly UserService _userService;
        private CustomerDTO _currentCustomer = null;
        public ManageCustomerControl()
        {
            InitializeComponent();
            _userService = new UserService();
            SetupCustomerTypeComboBox();
            ClearCustomerDetails();
        }

        private void SetupCustomerTypeComboBox()
        {
            var customerTypes = new Dictionary<int?, string>();
            customerTypes.Add(0, "Other Household");
            customerTypes.Add(1, "Policy/Poor/Needy");

            cmbCustomerType.DataSource = new BindingSource(customerTypes, null);
            cmbCustomerType.DisplayMember = "Value";
            cmbCustomerType.ValueMember = "Key";
            cmbCustomerType.SelectedIndex = -1;
        }

        // Xóa thông tin khách hàng hiển thị trên form
        private void ClearCustomerDetails()
        {
            _currentCustomer = null;
            txtDisplaySerialId.Text = "";
            txtDisplayUsername.Text = "";
            txtDisplayNationalId.Text = "";
            txtDisplayAddress.Text = "";
            cmbCustomerType.SelectedIndex = -1;
            btnUpdateType.Enabled = false;
            lblStatus.Text = "";
        }

        // Sự kiện Click nút Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter Serial ID or Username to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CustomerDTO foundCustomer = null;

            try
            {
                if (rdoSearchBySerialId.Checked)
                {
                    if (int.TryParse(searchTerm, out int serialId) && serialId > 0)
                    {
                        foundCustomer = _userService.GetCustomerDetailsBySerialId(serialId);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid positive number for Serial ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else // Tìm theo Username
                {
                    foundCustomer = _userService.GetUserDetails(searchTerm);
                }

                // Hiển thị kết quả
                if (foundCustomer != null)
                {
                    _currentCustomer = foundCustomer;
                    txtDisplaySerialId.Text = _currentCustomer.SerialID.ToString();
                    txtDisplayUsername.Text = _currentCustomer.UserName;
                    txtDisplayNationalId.Text = _currentCustomer.NationalID.ToString();
                    txtDisplayAddress.Text = _currentCustomer.Address;

                    int typeValueToSelect = _currentCustomer.CustomerType ?? 0;
                    cmbCustomerType.SelectedValue = typeValueToSelect;

                    btnUpdateType.Enabled = true;
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "Customer found.";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "Customer not found.";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        MessageBox.Show("Customer not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (lblStatus != null)
                {
                    lblStatus.Text = "Search error.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void btnUpdateType_Click(object sender, EventArgs e)
        {
            if (_currentCustomer == null)
            {
                MessageBox.Show("Please search and find a customer first.", "No Customer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCustomerType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer type.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedTypeValue = (int)cmbCustomerType.SelectedValue;
            int? typeToUpdate = selectedTypeValue;

            var confirmResult = MessageBox.Show($"Update Customer Type for Serial ID {_currentCustomer.SerialID} to '{cmbCustomerType.Text}'?",
                                        "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No) return;

            try
            {
                bool success = _userService.UpdateCustomerType(_currentCustomer.SerialID, typeToUpdate);

                if (success)
                {
                    MessageBox.Show("Customer type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "Update successful.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    _currentCustomer.CustomerType = typeToUpdate;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Update error.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void CenterContentPanel()
        {
            if (pnlContent != null)
            {
                int parentWidth = this.ClientSize.Width;
                int parentHeight = this.ClientSize.Height;

                // Tính toán vị trí Left và Top để căn giữa pnlContent
                int panelLeft = (parentWidth - pnlContent.Width) / 2;
                int panelTop = (parentHeight - pnlContent.Height) / 2;

                // Đảm bảo vị trí không bị âm nếu panel lớn hơn usercontrol
                panelLeft = Math.Max(0, panelLeft);
                panelTop = Math.Max(0, panelTop);

                // Cập nhật vị trí cho pnlContent
                pnlContent.Location = new Point(panelLeft, panelTop);
            }
        }

        private void ManageCustomerControl_Resize(object sender, EventArgs e)
        {
            CenterContentPanel();
        }

        private void ManageCustomerControl_Load(object sender, EventArgs e)
        {
            CenterContentPanel();
        }

        private void txtSearchInput_Enter(object sender, EventArgs e)
        {
            ClearCustomerDetails();
        }
    }
}
