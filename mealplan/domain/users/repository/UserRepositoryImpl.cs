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
                string sql = $@"SELECT userId.DESCRIPTION, pwd.DESCRIPTION, name.DESCRIPTION, gender.DESCRIPTION, birthdate.DESCRIPTION, height.DESCRIPTION, weight.DESCRIPTION
                                FROM SYS_SYSTEM_CODE_DATA_KHM userId
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM pwd
                                        ON pwd.CODE_NAME = userId.CODE_NAME AND pwd.CODE_SEQ =2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM name
                                        ON name.CODE_NAME = userId.CODE_NAME AND name.CODE_SEQ = 3
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM gender
                                        ON gender.CODE_NAME = userId.CODE_NAME AND gender.CODE_SEQ = 4
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM birthdate
                                        ON birthdate.CODE_NAME = userId.CODE_NAME AND birthdate.CODE_SEQ=5
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM height
                                        ON height.CODE_NAME = userId.CODE_NAME AND height.CODE_SEQ=6
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM weight
                                        ON weight.CODE_NAME = userId.CODE_NAME AND weight.CODE_SEQ=7
                                WHERE userId.PLANT='MealPlan2' AND userId.TABLE_NAME= 'Users' AND userId.CODE_NAME =:userId AND userId.CODE_SEQ = 1";

                // 2.쿼리문 넣어서 OracleCommand 객체 맹글어줌.
                using (OracleCommand oracleCommand = new OracleCommand(sql, conn))
                {
                    oracleCommand.Parameters.Add("userId",inputLoginId);
                    // 결과는 이렇게 ResultSet처럼 끌어다 쓰면 됨
                    using (OracleDataReader result = oracleCommand.ExecuteReader())
                    {
             
                        if (!result.Read())
                        {
                            return null;
                        }

                        string loginId = result.GetString(0);
                        string password = result.GetString(1);
                        string name = result.GetString(2);
                        string gender = result.GetString(3);
                        string birthdate = result.GetString(4);
                        string height = result.GetString(5);
                        string weight = result.GetString(6);

                        return new User(loginId,password,name,gender,birthdate,height,weight);
                    }
                }
            }
            
        }

        
        public bool InsertUser(User user)
        {

            using (OracleConnection conn = oracleUtil.GetConnection())
            {


                //새 버전은 CODE_SEQ 찾는거 피룡없음.
              
                // ORACLE에서는 VALUES 이런식으로 안하고, SELECT로 넣음
                // 그리고 아이디는 중복될 수 없기 땜시롱... 제약조건을 걸 수는 없으니, 서브쿼리를 이용해서
                // 아이디가 중복되지 않으면 들어갈 수 있도록 쿼리를 작성함.
                string insertSql = $@"INSERT ALL
                                        INTO SYS_SYSTEM_CODE_DATA_KHM
                                        VALUES ('MealPlan2','Users',:userId,1,:userId,null,null,null,null,null,null,null,null,null,null,null)
                                        INTO SYS_SYSTEM_CODE_DATA_KHM
                                        VALUES ('MealPlan2','Users',:userId,2,:pwd,null,null,null,null,null,null,null,null,null,null,null)
                                        INTO SYS_SYSTEM_CODE_DATA_KHM
                                        VALUES ('MealPlan2','Users',:userId,3,:name,null,null,null,null,null,null,null,null,null,null,null)
                                        INTO SYS_SYSTEM_CODE_DATA_KHM
                                        VALUES ('MealPlan2','Users',:userId,4,:gender,null,null,null,null,null,null,null,null,null,null,null)
                                        INTO SYS_SYSTEM_CODE_DATA_KHM
                                        VALUES ('MealPlan2','Users',:userId,5,:height,null,null,null,null,null,null,null,null,null,null,null)
                                        INTO SYS_SYSTEM_CODE_DATA_KHM
                                        VALUES ('MealPlan2','Users',:userId,6,:weight,null,null,null,null,null,null,null,null,null,null,null)
                                        INTO SYS_SYSTEM_CODE_DATA_KHM
                                        VALUES ('MealPlan2','Users',:userId,7,:birthdate,null,null,null,null,null,null,null,null,null,null,null)
                                      SELECT * FROM DUAL
                                        ";
                using(OracleCommand cmd = new OracleCommand(insertSql, conn))
                {
                    // 디폴트가 순서 바인딩이라서, 이름 바인딩으로 바꿔주는거임.
                    cmd.BindByName = true;
                    cmd.Parameters.Add("userId", user.LoginId);
                    cmd.Parameters.Add("pwd", user.Password);
                    cmd.Parameters.Add("name", user.Name);
                    cmd.Parameters.Add("gender", user.Gender);
                    cmd.Parameters.Add("height", user.Height);
                    cmd.Parameters.Add("weight", user.Weight);
                    cmd.Parameters.Add("birthdate", user.Birthdate);
                    try
                    {
                        if (cmd.ExecuteNonQuery() >= 1)
                        {
                            MessageBox.Show("회원가입 완료");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("회원가입 실패 : 중복된 아이디입니다.");
                        }
                    } catch(OracleException ex)
                    {
                        MessageBox.Show("이미 중복된 계정입니다.");
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
