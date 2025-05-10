using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using WaterBillManagementSystem.DAL;

namespace WindowsFormsApplication12
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string connectionStringToUse = null;
            try
            {
                string testAppSettingValue = ConfigurationManager.AppSettings["TestAppSetting"];
                System.Diagnostics.Debug.WriteLine($"DEBUG: TestAppSetting Value from Program.cs = '{testAppSettingValue}'");

                ConnectionStringSettings waterBillDbSettings = ConfigurationManager.ConnectionStrings["WaterBillDb"];
                if (waterBillDbSettings != null)
                {
                    connectionStringToUse = waterBillDbSettings.ConnectionString;
                    System.Diagnostics.Debug.WriteLine($"DEBUG: WaterBillDb ConnectionString read in Program.cs = '{connectionStringToUse}'");

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: ConnectionString with name 'WaterBillDb' NOT FOUND in Program.cs!");
                    MessageBox.Show("CRITICAL ERROR: Connection string 'WaterBillDb' not found in configuration. Application cannot start.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch (ConfigurationErrorsException configEx)
            {
                System.Diagnostics.Debug.WriteLine($"DEBUG: CONFIGURATION ERROR in Program.cs = {configEx.ToString()}");
                MessageBox.Show($"Configuration Error: {configEx.Message}\n\nFile: {configEx.Filename}\nLine: {configEx.Line}", "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DEBUG: General Error in Program.cs config test = {ex.Message}");
                MessageBox.Show($"An unexpected error occurred during startup: {ex.Message}", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!string.IsNullOrEmpty(connectionStringToUse))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new login());
            }
            else
            {
                MessageBox.Show("Application cannot start because the database connection string is missing or invalid.", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
