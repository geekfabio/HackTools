using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Time_Boss_Hijack;
using System.IO;
using System.Threading.Tasks;
using System.Management;

namespace Hijack_Time_Boss___Fast_Mode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void AdicionarAplicacaoAoIniciar()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("Intel Fast Mode", "\"" + Application.ExecutablePath + "\"");
                }
            }
            catch
            {
                throw;
            }
        }
        int pid1, pid2;
        private void Form1_Load(object sender, EventArgs e)
        {
            AdicionarAplicacaoAoIniciar();
            try{
                string currentUser = Environment.UserName;
                if (currentUser.Contains("geek")) {
                    if (File.Exists(@"c:\src\bool")) {
                        ManagementClass management = new ManagementClass("Win32_Process");
                        ManagementObjectCollection mCollection = management.GetInstances();
                        foreach (ManagementObject process in mCollection)
                        {
                            if (process["Name"].ToString().Contains("time_boss.exe"))
                            {
                                pid1 = int.Parse((process["ProcessId"].ToString()));
                                Engine.SuspendProcess(pid1);
                            }
                            else if (process["Name"].ToString().Contains("time_boss_s.exe"))
                            {
                                pid2 = int.Parse((process["ProcessId"].ToString()));
                                Engine.SuspendProcess(pid2);
                            }
                        }
                    }
                    else { this.Close(); }
                }
            }
            catch{
                MessageBox.Show("FATAL");
            }
        }
    }
}
