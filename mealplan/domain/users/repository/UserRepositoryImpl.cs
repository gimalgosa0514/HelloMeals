using mealplan.domain.users.model;
using mealplan.util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.users.repository
{
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
                string sql = "SELECT * FROM SYS_SYSTEM_CODE_DATA_KHM WHERE TABLE_NAME=Users";

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
                string sql = $"SELECT * FROM SYS_SYSTEM_CODE_DATA_KHM WHERE CODE_NAME='{inputLoginId}'";

                // 2.쿼리문 넣어서 OracleCommand 객체 맹글어줌.
                using (OracleCommand oracleCommand = new OracleCommand(sql, conn))
                {   
                    // 결과는 이렇게 ResultSet처럼 끌어다 쓰면 됨
                    using (OracleDataReader result = oracleCommand.ExecuteReader())
                    {
                        
                        result.Read();
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

        // TODO1: 이거 짜는중임
        public bool InsertUser(User user)
        {

            using (OracleConnection conn = oracleUtil.GetConnection())
            {

                //새로운 유저를 넣기 전 마지막 MealPlan의 마지막 User의 code_seq를 보고 제일 마지막 seq + 1을 해주고 나서 넣어야함. 그래서 조회 먼저 해야함 ㅠ 
                string sql = $@"INSERT INTO SYS_SYSTEM_CODE_DATA 
                (plant, table_name, code_name, code_seq, description, code_group1, code_group2, code_group3, code_group4, code_group5)
                VALUES ('MealPlan', 'Users',{user.loginId},)";
                using(OracleCommand oracleCommand = new OracleCommand())
                {

           
                }
            }
            return false;
            
        }

        public bool RemoveUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
