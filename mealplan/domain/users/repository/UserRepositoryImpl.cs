using mealplan.domain.users.model;
using mealplan.util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mealplan.domain.users.repository
{
    // TODO: 예외처리 해야하는데, SQL 실행하는 부분에서 많이 터짐, 일단은 빨리 개발해야하니깐, 개발 하고 잡으셈.
    public class UserRepositoryImpl : IUserRepository
    {
        OracleUtil oracleUtil;

        public UserRepositoryImpl() {

            oracleUtil = OracleUtil.getInstance();
        }

        public List<User> SelectAllUser()
        {
            using(OracleConnection conn = oracleUtil.GetConnection())
            {
                string sql = "SELECT * FROM SYS_SYSTEM_CODE_DATA_KHM WHERE PLANT='MealPlan' AND TABLE_NAME='Users'";

                using (OracleCommand oracleCommand = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader result = oracleCommand.ExecuteReader())
                    {
                        List<User> users = new List<User>();
                        
                        while(result.Read())
                        {
                            string loginId = result.GetString(2);
                            string password = result.GetString(4);
                            string name = result.GetString(5);
                            string gender = result.GetString(6);
                            string birthdate = result.GetString(7);
                            string height = result.GetString(8);
                            string weight = result.GetString(9);

                            users.Add(new User(loginId, password, name, gender, birthdate, height, weight));
                        }
                        return users;
                    }
                }
            }
        }

        public User SelectUserByLoginId(string inputLoginId)
        {

            // 1. 커넥션을 얻어옴.
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                string sql = $"SELECT * FROM SYS_SYSTEM_CODE_DATA_KHM WHERE CODE_NAME='{inputLoginId}' AND PLANT='MealPlan' AND TABLE_NAME='Users'";

                // 2.쿼리문 넣어서 OracleCommand 객체 맹글어줌.
                using (OracleCommand oracleCommand = new OracleCommand(sql, conn))
                {   
                    // 결과는 이렇게 ResultSet처럼 끌어다 쓰면 됨
                    using (OracleDataReader result = oracleCommand.ExecuteReader())
                    {
             
                        if (!result.Read())
                        {
                            return null;
                        }

                        string loginId = result.GetString(2);
                        string password = result.GetString(4);
                        string name = result.GetString(5);
                        string gender = result.GetString(6);
                        string birthdate = result.GetString(7);
                        string height = result.GetString(8);
                        string weight = result.GetString(9);

                        return new User(loginId,password,name,gender,birthdate,height,weight);
                    }
                }
            }
            
        }

        
        public bool InsertUser(User user)
        {

            using (OracleConnection conn = oracleUtil.GetConnection())
            {


                //새로운 유저를 넣기 전 마지막 MealPlan의 마지막 User의 code_seq를 보고 제일 마지막 seq + 1을 해주고 나서 넣어야함. 그래서 조회 먼저 해야함 ㅠ 
                string selectSql = $@"SELECT CODE_SEQ FROM SYS_SYSTEM_CODE_DATA_KHM WHERE
                PLANT = 'MealPlan' AND
                TABLE_NAME = 'Users'
                ORDER BY CODE_SEQ DESC
                FETCH FIRST 1 ROWS ONLY";

                int lastSeq = 1;
                using(OracleCommand cmd = new OracleCommand(selectSql, conn))
                {
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                    
                        // 만약 읽어지면 있는거니깐 lastSeq 갱신
                        if(result.Read())
                        {
                            int savedLastSeq = int.Parse(result.GetString(0));
                            lastSeq += savedLastSeq;
                        }
                        // 안읽히면? 1부터 시작하면 되니까 그대로 ㄱ
                    }
                }
              
                // ORACLE에서는 VALUES 이런식으로 안하고, SELECT로 넣음
                // 그리고 아이디는 중복될 수 없기 땜시롱... 제약조건을 걸 수는 없으니, 서브쿼리를 이용해서
                // 아이디가 중복되지 않으면 들어갈 수 있도록 쿼리를 작성함.
                string insertSql = $@"INSERT INTO SYS_SYSTEM_CODE_DATA_KHM 
                (plant, table_name, code_name, code_seq, description, code_group1, code_group2, code_group3, code_group4, code_group5)
                SELECT 'MealPlan', 'Users','{user.LoginId}','{lastSeq}', '{user.Password}','{user.Name}','{user.Birthdate}','{user.Gender}','{user.Height}','{user.Weight}'
                FROM DUAL
                WHERE NOT EXISTS
                (SELECT CODE_NAME FROM SYS_SYSTEM_CODE_DATA_KHM WHERE CODE_NAME='{user.LoginId}')";
                using(OracleCommand cmd = new OracleCommand(insertSql, conn))
                {
                    if(cmd.ExecuteNonQuery() >= 1)
                    {
                        MessageBox.Show("회원가입 완료");
                        return true;
                    } else
                    {
                        MessageBox.Show("회원가입 실패 : 중복된 아이디입니다.");
                    }
                    
           
                }
            }
            return false;
            
        }

        public bool RemoveUser(string loginId)
        {

            using (OracleConnection conn = oracleUtil.GetConnection()) {

                string sql = @"DELETE FROM SYS_CODE_DATA_KHM
                               WHERE 
                               PLANT='MealPlan' AND 
                               TABLE_NAME='Users' AND
                               CODE_NAME=:LoginId";

                using(OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OracleParameter("LoginId", loginId));

                    int result = cmd.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
