using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FileReader
{
    public class FileHandling
    {
        public static Dictionary<string, string> Data;
        public static Dictionary<string, string> XPath;
        public static List<string> TestCasesList;
        public static string TestCaseName;

        static string TraceFile;

       public static void Initialize()
        {
            if (Data.ContainsKey("trace"))
            {
                TraceFile = Data["trace"];
            }
        }
        public static Dictionary<string,string> AddDataToDictionary(string file)
        {
            Dictionary<string, string> keyValuePairs = null;
            if (File.Exists(file))
            {
               keyValuePairs = new Dictionary<string, string>();

                var data = File.ReadAllLines(file);

                try
                {
                    foreach (var item in data)
                    {
                        if (item.Contains('\t'))
                        {
                            var text = item.Split('\t');

                            var key = text[0].Trim();
                            var val = text[1].Trim();

                            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(val) && !keyValuePairs.ContainsKey(key))
                            {
                                keyValuePairs.Add(key, val);
                            }
                        }
                    }
                }

                catch (Exception e)
                {
                    Trace(e.Message);
                }
            }

            else
            {
                Trace($"File {file} does not exist");
            }

            return keyValuePairs;
        }
        public static void AddDataToList(string file)
        {
            if (File.Exists(file))
            {
                TestCasesList = new List<string>();
                var data = File.ReadAllLines(file);

                foreach (var item in data)
                {
                    TestCasesList.Add(item);
                }

            }
        }
        public static void Trace(string text)
        {
            if (!File.Exists(TraceFile))
            {
                File.Create(TraceFile).Close();               
            }
            text = DateTime.Now.ToString() + " " + TestCaseName + " " + text;

            File.AppendAllText(TraceFile, text + Environment.NewLine);

        }
    }
}
