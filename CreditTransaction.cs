using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReader;
using Driver;
using static TestSuite.Common;

namespace TestSuite
{
    public class CreditTransaction : DriverClass
    {
        public static void Test()
        {
         //   var creditCardList = new string[] { "Visa", "Discover", "Amex" };
         
            foreach (var item in Enum.GetValues(typeof(CreditCards)))
            {
                Credit(item.ToString());
            }
        }

        public static void Credit(string creditCard)
        {
            Login();           
            Wait(5000);
            SendKeys("POS_item_entry", "4202");
            Click("POS_item_entry_button");

            Wait(5000);

            Wait("POS_Total_btn");

            Click("POS_Total_btn");

            Minalldms();

            Wait("POS_BBY_CreditBtn");

            Click("POS_BBY_CreditBtn");
            Wait(5000);

            MSR(creditCard);

        }      
    }
}
