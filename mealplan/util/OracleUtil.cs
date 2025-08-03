using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mealplan.util
{
    
    // DB관련 유틸리티 클래스
    public class OracleUtil
    {

        private static OracleUtil instance = new OracleUtil();

        private OracleUtil()
        {

        }

        public static OracleUtil getInstance()
        {
            return instance;
        }

        // Connnection요청시 
        public OracleConnection GetConnection()
        {
            
             return  ConnectDB();
                
            

        }

        
        // 유저가 커넥션을 달라하면 내부적으로 동작할 메서드
        private OracleConnection ConnectDB()
        {
            // 환경변수에 일단 올림.
            EnviromentVariableReader.ReadEnvFile();
            string connectionStr = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Environment.GetEnvironmentVariable("HOST")})" +
            $"(PORT={Environment.GetEnvironmentVariable("PORT")}))" +
            $"(CONNECT_DATA=(SERVICE_NAME={Environment.GetEnvironmentVariable("SERVICE_NAME")})));" +
            $"User Id={Environment.GetEnvironmentVariable("USER_ID")};" +
            $"Password={Environment.GetEnvironmentVariable("PASSWORD")};";

            OracleConnection conn = new OracleConnection(connectionStr);
  
            try
            {
                conn.Open();

                Console.WriteLine("연결 성공!");
                return conn;
                
            } 
            catch(OracleException ex)
            {

                Console.WriteLine("연결에 실패했습니다.." + ex.Message);
                return null;
                
                
            }

        }


       




    }
}
