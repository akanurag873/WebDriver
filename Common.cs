using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using Driver;
using FileReader;

namespace TestSuite
{
    public class Common : DriverClass
    {
        internal static bool IsDMSLaunched = false;
        internal static bool IsLoggedIn = false;
        internal static bool IsURLLaunched = false;
        internal static string AutoIT;
        internal static string StartDMS;
        internal static string StopDMS;
        internal static string errorText = null;
       // internal static String TestCaseName = null;
        internal static bool IsDmsLaunched = false;
        internal static bool IsDMSValidated = false;
        internal static bool isTestPass = true;

        public static void Initialize()
        {
            
            if (Data.ContainsKey("AutoIT"))
            {
                AutoIT = Data["AutoIT"];
            }

            if (Data.ContainsKey("StartDMS"))
            {
                StartDMS = Data["StartDMS"];
            }

            if (Data.ContainsKey("StopDMS"))
            {
                StopDMS = Data["StopDMS"];
            }
        }

        internal static void Login()
        {
            if (!IsDMSLaunched)
            {
                RunCmdFile(@"C:\DMSSimsBBY\StartRBASims.cmd");
                IsDMSLaunched = true;
                Minalldms();
            }

            Wait(5000);

            //if (!IsLoggedIn)
            //{
            if (!IsURLLaunched)
            {
                GetUrl("url");
                IsURLLaunched = true;
            }

            Wait("POS_userId");
            Maximize();
            SendKeys("POS_userId", "userid");
            SendKeys("POS_password", "pswd");
            Click("POS_loginBtn");

            if (IsElementVisible("POS_regopenclick"))
            {
                Click("POS_regopenclick");
                SendKeys("POS_regopenamount", "20000");
                Click("POS_regopenamountenter");
                Click("POS_regopenamountconfirm");
                RunExeFile(AutoIT + "close cash drawer.exe");
                SendKeys("POS_userId", "userid");
                SendKeys("POS_password", "pswd");
                Click("POS_loginBtn");
            }


            Wait("POS_saleperson_cancelbutton");
            Click("POS_saleperson_cancelbutton");
            IsLoggedIn = true;
            //}
        }

        //internal static void OpenRegister()
        //{
        //    if (IsElementVisible("POS_regopenverify"))
        //    {
        //        Click("POS_regopenclick");
        //        SendKeys("POS_regopenclick", "20000");
        //        Click("POS_regopenamountenter");
        //        Click("POS_regopenamountconfirm");
        //        RunExeFile(AutoIT + "close cash drawer.exe");
        //        SendKeys("POS_userId", "userid");
        //        SendKeys("POS_password", "pswd");
        //        Click("POS_loginBtn");

        //    }
        //}

        static void RunCmdFile(string cmdFile)
        {
            cmdFile = FileHandling.Data.ContainsKey(cmdFile) ? FileHandling.Data[cmdFile] : cmdFile;

            string cmdFilePath = cmdFile;

            // Create a new process start info
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(cmdFilePath)
            };

            // Create a new process
            Process process = new Process
            {
                StartInfo = psi
            };

            // Start the process
            process.Start();

            // Send the command to run the CMD script or batch file
            process.StandardInput.WriteLine($"\"{cmdFilePath}\"");

            // Read the output
            // string output = process.StandardOutput.ReadToEnd();

            // Display the output
            // Console.WriteLine(output);

            // Close the process
            //   process.WaitForExit();
            process.Close();
        }

        internal static void MSR(string creditCard)
        {
            if (creditCard == CreditCards.Visa.ToString())
            {
                RunExeFile(AutoIT + "MSRswipe.exe");
            }

            else if (creditCard == CreditCards.Discover.ToString())
            {
                RunExeFile(AutoIT + "Discover.exe");
                Wait(5000);
                EnterCID(777);
            }
            else if (creditCard == CreditCards.Amex.ToString())
            {
                RunExeFile(AutoIT + "Amex.exe");
                Wait(5000);
                EnterCID(8888);
            }
        }

        internal static void EnterCID(int cid)
        {
            SendKeys("CID", cid.ToString());
            Click("Enter");
        }

        public enum CreditCards
        {
            Visa = 1, Discover = 2, Amex = 3
        }

        internal static void RunExeFile(string exeFilePath)
        {
            // Create a new process start info
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = AutoIT + exeFilePath,
                UseShellExecute = true // Set this to true if you want to use the shell (default program association)
            };

            // Create a new process
            Process process = new Process
            {
                StartInfo = psi
            };

            try
            {
                // Start the process
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void GetIP()
        {
            // Specify the network interface name you want to retrieve information for
            string interfaceName = "PPP adapter CORP_iE3";

            // Find the network interface with the specified name
            NetworkInterface selectedInterface = NetworkInterface
                .GetAllNetworkInterfaces()
                .FirstOrDefault(ni => ni.Description == interfaceName);

            if (selectedInterface != null)
            {
                // Get the IPv4 addresses associated with the selected interface
                var ipv4Addresses = selectedInterface
                    .GetIPProperties()
                    .UnicastAddresses
                    .Where(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Select(addr => addr.Address.ToString());

                if (ipv4Addresses.Any())
                {
                    // Display the IPv4 address(es)
                    Console.WriteLine($"IPv4 Address(es) for {interfaceName}:");
                    foreach (var ipv4Address in ipv4Addresses)
                    {
                        Console.WriteLine(ipv4Address);
                    }
                }
                else
                {
                    Console.WriteLine($"No IPv4 addresses found for {interfaceName}.");
                }
            }
            else
            {
                Console.WriteLine($"Network interface '{interfaceName}' not found.");
            }
        }

        public static void Minalldms()
        {
            RunExeFile("Scannermin.exe");
            RunExeFile("Micrmin.exe");
            RunExeFile("CIDmin.exe");
            RunExeFile("MSRmin.exe");
            RunExeFile("Cashdrawermin.exe");
            RunExeFile("Scannermin.exe");

            Trace("DMS minimised sucessfully.");
        }

        public static void CloseDMS()
        {
            //var path = OR.getProperty("CloseDMS");
            RunExeFile(StopDMS);
        }
    }
}

