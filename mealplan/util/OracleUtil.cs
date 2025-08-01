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


        
        OracleConnection Conn = null;

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
            if(Conn == null)
            {
                ConnectDB();
                
            }
            return Conn;

        }

        
        // 유저가 커넥션을 달라하면 내부적으로 동작할 메서드
        private void ConnectDB()
        {
            // 환경변수에 일단 올림.
            EnviromentVariableReader.ReadEnvFile();
            string connectionStr = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Environment.GetEnvironmentVariable("HOST")})" +
            $"(PORT={Environment.GetEnvironmentVariable("PORT")}))" +
            $"(CONNECT_DATA=(SERVICE_NAME={Environment.GetEnvironmentVariable("SERVICE_NAME")})));" +
            $"User Id={Environment.GetEnvironmentVariable("USER_ID")};" +
            $"Password={Environment.GetEnvironmentVariable("PASSWORD")};";

            Conn = new OracleConnection(connectionStr);
  
            try
            {
                Conn.Open();
                Console.WriteLine("연결 성공!");
            } 
            catch(OracleException ex)
            {
                Console.WriteLine("연결에 실패했습니다.." + ex.Message);
                
            }

        }

        // 마지막에 끊어주는거임.
        public void DisconnectDb()
        {
            // 만약 서버랑 연결이 되어있고 DB도 연결되어있다? -> 사용이 끝났는데 반환을 안했다?
            if(Conn != null && Conn.State != System.Data.ConnectionState.Closed)
            {
                Conn.Close(); // 닫아주고 끊어.
                Conn.Dispose();
            }
        }


       




    }
}
