using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.IO;
using SeleniumExtras.WaitHelpers;
using FileReader;

namespace Driver
{
    public class DriverClass : FileHandling
    {
        static WebDriver driver;
        static bool IsInitialized = false;
        public DriverClass()
        {
            if (!IsInitialized)
            {
                Init();
            }
        }
        public static void Init(int time = 120)
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(time);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(time);


            IsInitialized = true;
        }

        public static void Maximize()
        {
            try
            {
                driver.Manage().Window.Maximize();
            }
            catch (Exception e)
            {
                Trace(e.Message);
            }
        }

        public static void GetUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                url = Data.ContainsKey(url) ? Data[url] : url;

                try
                {
                    driver.Navigate().GoToUrl(url);
                    Trace($"Launched URL: {url}");
                }

                catch (Exception e)
                {
                    Trace(e.Message);
                }
            }

            else
            {
                Trace("URL is null.");
            }
        }

        public static void Click(string xpath)
        {
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                var xpathval = XPath.ContainsKey(xpath) ? XPath[xpath] : xpath;

                if (IsElementVisible(xpathval))
                {
                    try
                    {
                        driver.FindElement(By.XPath(xpathval)).Click();
                        Trace($"Cicked {xpath}.");
                    }
                    catch (Exception e)
                    {
                        Trace(e.Message);
                    }

                }
                else
                {
                    Trace($"Element {xpath} is not visible.");
                }
            }
        }

        public static void SendKeys(string xpath, string text)
        {
            //  if (!string.IsNullOrWhiteSpace(xpath) && IsElementVisible(xpath) && !string.IsNullOrEmpty(text))
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                text = Data.ContainsKey(text) ? Data[text] : text;

                var xpathVal = XPath.ContainsKey(xpath) ? XPath[xpath] : xpath;

                if (IsElementVisible(xpathVal) && !string.IsNullOrEmpty(text))
                {
                    try
                    {
                        driver.FindElement(By.XPath(xpathVal)).SendKeys(text);
                        Trace($"Entered text - {text} in {xpath}.");
                    }

                    catch (Exception e)
                    {
                        Trace(e.Message);
                    }

                }
                else
                {
                    Trace($"Element {xpath} is not visible.");
                }
            }
        }

        public static void Clear(string xpath)
        {
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                var xpathval = XPath.ContainsKey(xpath) ? XPath[xpath] : xpath;

                if (IsElementVisible(xpath))
                {
                    driver.FindElement(By.XPath(xpathval)).Clear();
                }

                else
                {
                    Trace($"Element {xpath} is not visible.");
                }
            }
        }

        public static void Sleep(int time)
        {
            if (time > 0)
                Thread.Sleep(time);
        }

        public static void Wait(string xpath, int timeout = 120)
        {
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                var xpathVal = XPath.ContainsKey(xpath) ? XPath[xpath] : xpath;

                if (IsElementVisible(xpathVal))
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

                    try
                    {
                        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathVal)));
                    }

                    catch (Exception e)
                    {
                        Trace(e.Message);
                    }
                }
                else
                {
                    Trace($"Element {xpath} is not visible.");
                }
            }
        }

        public static void Wait(int time)
        {
            Thread.Sleep(time);
        }
        public static void Screenshot(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                path = Data.ContainsKey(path) ? Data[path] : path;

                if (File.Exists(path))
                {
                    try
                    {
                        Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                        screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
                    }
                    catch (Exception e)
                    {
                        Trace(e.Message);
                    }
                }
            }
        }

        public static void Scroll(string xpath)
        {
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                var xpathVal = XPath.ContainsKey(xpath) ? XPath[xpath] : xpath;
                if (IsElementVisible(xpathVal))
                {
                    IJavaScriptExecutor jsExecutor = driver;
                    var element = driver.FindElement(By.XPath(xpathVal));
                    jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                }
                else
                {
                    Trace($"Element {xpath} is not visible.");
                }
            }
        }

        public static bool IsElementVisible(string xpath)
        {
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                xpath = XPath.ContainsKey(xpath) ? XPath[xpath] : xpath;

                try
                {
                    var element = driver.FindElement(By.XPath(xpath));

                    if (element.Size != null && element.Displayed)
                    {
                        return true;
                    }
                }

                catch (Exception)
                {
                   // Trace(e.Message);
                }
              //  Trace("Element is ")
                return false;
            }
            else
            {
                return false;
            }
        }

        public static void Close()
        {
            driver.Close();
        }
        public static void Quit()
        {
            driver.Quit();
        }

    }
}
