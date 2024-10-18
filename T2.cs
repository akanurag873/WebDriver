using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReader;
using Driver;

namespace TestSuite
{
    public class T2
    {
        public static void Test()
        {
            try
            {
                Console.WriteLine("From T2");
                var a = 1;
                var b = 0;
                var c = a / b;
            }
            catch(Exception e)
            {

            }
        }
    }
}
