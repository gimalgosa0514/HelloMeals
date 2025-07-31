using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.util
{
    
    // DB관련 유틸리티 클래스
    public static class OracleUtil
    {


        
        static OracleConnection Conn = null;



        public static void ConnectDB()
        {
            // 환경변수에 일단 올림.
            EnviromentVariableReader.ReadEnvFile();
            string connectionStr = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Environment.GetEnvironmentVariable("HOST")})" +
            $"(PORT={Environment.GetEnvironmentVariable("PORT")}))" +
            $"(CONNECT_DATA=(SERVICE_NAME={Environment.GetEnvironmentVariable("SERVICE_NAME")})));" +
            $"User Id={Environment.GetEnvironmentVariable("USER_ID")};" +
            $"Password={Environment.GetEnvironmentVariable("PASSWORD")};";

            Conn = new OracleConnection(connectionStr);

            
            if(Conn != null)
            {
                Console.WriteLine("연결되었습니다.");
            }

        }
        




       




    }
}
