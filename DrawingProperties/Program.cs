using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            var swInstance = new SldWorks.SldWorks();
            var model = (ModelDoc2)swInstance.ActiveDoc;

            // read from app data file
            var appDataPath = @"C:\Users\bolinger\Documents\SolidWorks Projects\Prefab Blob - Cover Blob\app data\rebuild.txt";
            var appDataLines = System.IO.File.ReadAllLines(appDataPath);

            // read from config path
            var configPath = appDataLines[0];
            var configLines = System.IO.File.ReadAllLines(configPath);

            // generate property value dictionary
            var propertyValueDict = new Dictionary<string, string>();
            foreach (string line in configLines)
            {
                if (line.Contains("Property"))
                {
                    var property = line.Split(':')[1].Split('=')[0].Replace("\"", "").Trim();
                    var value = line.Split('=')[1].Trim();

                    propertyValueDict.Add(property, value);
                }
            }

            // set custom property data
            var modelExtension = (ModelDocExtension)model.Extension;
            var customPropertyManager = modelExtension.CustomPropertyManager[""];
            foreach (string property in propertyValueDict.Keys)
            {
                customPropertyManager.Set2(property, propertyValueDict[property]);
            }

            model.ForceRebuild3(true);
        }
        private static void displayLines(string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        private static void displayLines(string line)
        {
            Console.WriteLine(line);
        }
        private static void displayLines(int line)
        {
            Console.WriteLine(line);
        }
        private static void displayLines(double line)
        {
            Console.WriteLine(line);
        }
        private static void displayLines(double[] lines)
        {
            foreach (double number in lines)
            {
                Console.WriteLine(number);
            }
        }
        private static void displayLines(Dictionary<string, string> dict)
        {
            foreach (string property in dict.Keys)
            {
                Console.WriteLine(property + " : " + dict[property]);
            }
        }
    }
}
