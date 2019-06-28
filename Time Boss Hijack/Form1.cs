using System;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using System.Drawing;
using System.ServiceProcess;

namespace Time_Boss_Hijack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region CodeBase
        private void AdminRun()
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool administrativeMode = principal.IsInRole(WindowsBuiltInRole.Administrator);
            if (!administrativeMode)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Verb = "runas";
                startInfo.FileName = Assembly.GetExecutingAssembly().CodeBase;
                try
                {
                    Process.Start(startInfo);
                    MessageBox.Show("You are running the project with Administrator level!", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Could not grant access as Admin!" + Environment.NewLine + "The operations performed may have Access Denied!", "Fuck It", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
        int pidApp = 0, pidService;
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) dataGridView1.Rows.Clear(); 
            ManagementClass management = new ManagementClass("Win32_Process");
            ManagementObjectCollection mCollection = management.GetInstances();
            foreach (ManagementObject process in mCollection)
            {
                if (process["Name"].ToString().Contains("time_boss.exe"))
                {
                    dataGridView1.Rows.Add((process["ProcessId"].ToString()), (string)process["Name"], (string)process["ExecutablePath"]);
                    txtLog.Text += Environment.NewLine + ">>Time Boss was found to be successful PID>>" + (process["ProcessId"].ToString());
                    pidApp = int.Parse((process["ProcessId"].ToString()));
                }
                else if (process["Name"].ToString().Contains("time_boss_s.exe"))
                {
                    dataGridView1.Rows.Add((process["ProcessId"].ToString()), (string)process["Name"], (string)process["ExecutablePath"]);
                    txtLog.Text += Environment.NewLine + ">>Time Boss Service was found to be successful PID>>"+ (process["ProcessId"].ToString());
                    pidService = int.Parse((process["ProcessId"].ToString()));
                }
            }
            if(dataGridView1.Rows.Count <= 0)
            {
                txtLog.Text += Environment.NewLine + ">>Time Boss Service or Time Boss was not found";
            }
            else
            {
                try{
                    Engine.SuspendProcess(pidApp);
                    Engine.SuspendProcess(pidService);
                    txtLog.Text += Environment.NewLine + ">>Hijack With Sucess!!!";
                    button4.Visible = true;
                }
                catch (Exception ex){
                    txtLog.ForeColor = Color.Red ;
                    txtLog.Text = "Error Exception...."+ ex.Message;
                }                
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ManagementClass management = new ManagementClass("Win32_Process");
            ManagementObjectCollection mCollection = management.GetInstances();
            foreach (ManagementObject process in mCollection)
            {
                if (process["Name"].ToString().Contains("time_boss.exe"))
                {
                    dataGridView1.Rows.Add((process["ProcessId"].ToString()), (string)process["Name"], (string)process["ExecutablePath"]);
                    txtLog.Text += Environment.NewLine + ">>Time Boss was found to be successful PID>>" + (process["ProcessId"].ToString());
                    pidApp = int.Parse((process["ProcessId"].ToString()));
                }
                else if (process["Name"].ToString().Contains("time_boss_s.exe"))
                {
                    dataGridView1.Rows.Add((process["ProcessId"].ToString()), (string)process["Name"], (string)process["ExecutablePath"]);
                    txtLog.Text += Environment.NewLine + ">>Time Boss Service was found to be successful PID>>" + (process["ProcessId"].ToString());
                    pidService = int.Parse((process["ProcessId"].ToString()));
                }
            }
            if (dataGridView1.Rows.Count <= 0)
            {
                txtLog.Text += Environment.NewLine + ">>Time Boss Service or Time Boss was not found";
            }
            else
            {
                try
                {
                    Engine.ResumeProcess(pidApp);
                    Engine.ResumeProcess(pidService);
                    txtLog.Text += Environment.NewLine + ">>Hijack With Sucess!!!";
                    button4.Visible = true;
                }
                catch (Exception ex)
                {
                    txtLog.ForeColor = Color.Red;
                    txtLog.Text = "Error Exception...." + ex.Message;
                }
            }
            if (pidApp != 0)
            {
                Engine.ResumeProcess(pidApp);
                Engine.ResumeProcess(pidService);
                button4.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pidApp != 0)
            {
                Engine.ResumeProcess(pidApp);
                Engine.ResumeProcess(pidService);
                button4.Visible = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pidApp != 0)
            {
                Engine.ResumeProcess(pidApp);
                Engine.ResumeProcess(pidService);
                button4.Visible = true;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminRun();
        }
    }
}
