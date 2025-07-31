using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.util
{
    internal static class EnviromentVariableReader
    {
        public static void ReadEnvFile()
        {
            StreamReader sr = new StreamReader("../env.txt");
            
            using (sr)
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] lineArr = line.Split('=');
                    Environment.SetEnvironmentVariable(lineArr[0], lineArr[1]);
                    line = sr.ReadLine();
                }
            }
           
        }
    }
}
