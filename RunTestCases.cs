using System.Reflection;
using AventStack.ExtentReports;
using FileReader;
using Reports;
using Driver;
using System;

namespace TestSuite
{
    public class RunTestCases : FileHandling
    {
        public static void Run()
        {
            Assembly assembly = Assembly.Load("TestSuite");

            foreach (var item in TestCasesList)
            {
                try
                {
                    var cl = assembly.GetType("TestSuite." + item);

                    MethodInfo methodInfo = cl.GetMethod("Test");
                    TestCaseName = item;
                    Report.CreateTest(item);
                    Trace("Begin");
                    methodInfo.Invoke(null, null);

                    if (!(Common.isTestPass))
                    {
                        OnError();
                        Common.isTestPass = true;
                        Trace("Status of Test Case: " + TestCaseName + " is Fail.");
                    }

                    else
                    {
                        Report.TestStatus(Status.Pass, "");
                        Trace("Status of Test Case: " + TestCaseName + " is Pass.");
                    }

                 //   Report.Flush();
                }

                catch (Exception e)
                {
                    Report.TestStatus(Status.Fail, e.ToString());
                    Trace(e.Message);
                    Common.CloseDMS();
                    DriverClass.Wait(5000);
                    DriverClass.Quit();
                    Common.IsDmsLaunched = false;
                    Common.IsURLLaunched = false;
                    Common.IsDMSValidated = false;
                }

                finally
                {
                    Report.Flush();
                }
            }
        }
        static void OnError()
        {
            Report.TestStatus(Status.Error, Common.errorText);
            Refresh();
        }

        static void Refresh()
        {
            TestCaseName = "";
            Common.CloseDMS();
            //wait();
            DriverClass.Quit();

            Common.IsDmsLaunched = false;
            Common.IsURLLaunched = false;
            Common.IsDMSValidated = false;
            DriverClass.Init();
            //Common. LaunchBrowser();
        }

        public void Quit()
        {
            Report.Flush();
            Common.CloseDMS();
            //wait();
            DriverClass.Quit();
        }
    }
}
