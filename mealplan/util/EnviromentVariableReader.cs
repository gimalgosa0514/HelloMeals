using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.util
{
    // DB정보를 깃에 올리면 안되니, 환경변수 파일을 만들어서 그 파일은 깃에 안올리되, 관리하기는 좋도록.
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
