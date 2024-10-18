using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Reports
{
    public class Report
    {
        public static ExtentTest test;
        public static ExtentSparkReporter reporter;
        public static ExtentReports extent;
        public static bool isInitialized = false;

        public static void Initialize()
        {
            reporter = new ExtentSparkReporter("C:\\ProgramData\\E3Retail\\Automation\\POS\\Reports\\report.html");

            extent = new ExtentReports();
            extent.AttachReporter(reporter);
            isInitialized = true;
        }

        public static void CreateTest(String TestCaseName)
        {
            if (!isInitialized)
            {
                Initialize();
            }
            test = extent.CreateTest(TestCaseName);
        }

        public static void TestStatus(Status status, String details)
        {
            test.Log(status, details);
        }

        public static void AddScreenshot(String path)
        {
            test.AddScreenCaptureFromPath(path);
        }

        public static void Flush()
        {
            extent.Flush();
        }
    }
}

