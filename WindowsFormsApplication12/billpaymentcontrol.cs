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
    public partial class billpaymentcontrol : UserControl
    {
        private readonly BillingService _billingService;
        public billpaymentcontrol()
        {
            InitializeComponent();
            _billingService = new BillingService();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(serialtxt.Text.Trim(), out int serialId))
            {
                MessageBox.Show("Please enter a valid Serial ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView1.DataSource = null; // Xóa dữ liệu cũ trên grid
                return;
            }

            try
            {
                DataTable dt = _billingService.GetBillDetailsForUser(serialId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No billing information found for the entered Serial ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Xóa dữ liệu cũ
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving bill details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Bitmap objBmp = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
                if (this.dataGridView1.Visible && this.dataGridView1.Width > 0 && this.dataGridView1.Height > 0)
                {
                    this.dataGridView1.DrawToBitmap(objBmp, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
                    float scale = Math.Min(1f, (float)e.MarginBounds.Width / objBmp.Width);
                    int drawWidth = (int)(objBmp.Width * scale);
                    int drawHeight = (int)(objBmp.Height * scale);
                    int drawX = e.MarginBounds.Left + (e.MarginBounds.Width - drawWidth) / 2;
                    int drawY = 200;

                    e.Graphics.DrawImage(objBmp, drawX, drawY, drawWidth, drawHeight);
                }

                using (Font titleFont = new Font("Verdana", 20, FontStyle.Bold))
                {
                    string title = label1.Text;
                    SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
                    float titleX = e.MarginBounds.Left + (e.MarginBounds.Width - titleSize.Width) / 2;
                    float titleY = e.MarginBounds.Top + 50;
                    e.Graphics.DrawString(title, titleFont, Brushes.Black, titleX, titleY);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during printing: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("No data to preview or print.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
