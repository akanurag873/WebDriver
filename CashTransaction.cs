using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReader;
using Driver;
using Reports;
using AventStack.ExtentReports;

namespace TestSuite
{
    public class CashTransaction : DriverClass
    {
        public static void Test()
        {
            Console.WriteLine("From Test1");
            Common.Login();

            Wait(5000);
            SendKeys("POS_item_entry", "4202");
            Click("POS_item_entry_button");

            Wait(5000);

            Wait("POS_Total_btn");

            Click("POS_Total_btn");

            Wait("POS_BBY_CashBttn2");

            Click("POS_BBY_CashBttn2");

            Screenshot(@"C:\Users\akumar2\Pictures\screens\CashTran.jpg");

            Report.AddScreenshot(@"C:\Users\akumar2\Pictures\screens\CashTran.jpg");

            Common.RunExeFile(@"C:\Users\akumar2\Documents\autoItFiles\close cash drawer.exe");
        }

    }
}

