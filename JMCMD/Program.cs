using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace JMCMD
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                string noticesever = ConfigurationManager.AppSettings["noticesever"].ToString();
                string terminalsever = ConfigurationManager.AppSettings["terminalsever"].ToString();
                string payinfosever = ConfigurationManager.AppSettings["payinfosever"].ToString();
                string categoryn = ConfigurationManager.AppSettings["categoryn"].ToString();
                string categoryt = ConfigurationManager.AppSettings["categoryt"].ToString();
                string categoryp = ConfigurationManager.AppSettings["categoryp"].ToString();
                bool n = true;
                bool t = true;
                bool p = true;

                System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process process in processList)
                {
                    if (process.ProcessName.ToUpper() == noticesever)
                    {
                        n = false;
                        continue;
                    }

                    if (process.ProcessName.ToUpper() == terminalsever)
                    {
                        t = false;
                        continue;
                    }

                    if (process.ProcessName.ToUpper() == payinfosever)
                    {
                        p = false;
                        continue;
                    }

                }

                if (n)
                {
                    Process processn = new Process();
                    processn.StartInfo.UseShellExecute = true;
                    processn.StartInfo.FileName = categoryn + noticesever + ".exe";
                    processn.StartInfo.CreateNoWindow = true;
                    processn.Start();
                }

                if (t)
                {
                    Process processt = new Process();
                    processt.StartInfo.UseShellExecute = true;
                    processt.StartInfo.FileName = categoryt + terminalsever + ".exe";
                    processt.StartInfo.CreateNoWindow = true;
                    processt.Start();
                }

                if (p)
                {
                    Process processp = new Process();
                    processp.StartInfo.UseShellExecute = true;
                    processp.StartInfo.FileName = categoryp + payinfosever + ".exe";
                    processp.StartInfo.CreateNoWindow = true;
                    processp.Start();
                }
            }
            catch { }
        }
    }
}
