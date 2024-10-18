using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Driver
{
    public class DriverClass
    {
        public static void Initialize()
        {
            WebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.example.com");
        }
    }
}
