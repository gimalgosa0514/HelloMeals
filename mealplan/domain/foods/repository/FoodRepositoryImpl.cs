using mealplan.domain.foods.model;
using mealplan.domain.users.model;
using mealplan.util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace mealplan.domain.foods.repository
{
    internal class FoodRepositoryImpl : IFoodRepository
    {
        private OracleUtil oracleUtil;

        public FoodRepositoryImpl()
        {
            oracleUtil = OracleUtil.getInstance();
        }

        public bool deleteFood(int foodCodeName)
        {
            using(OracleConnection conn = oracleUtil.GetConnection())
            {
                string sql = @"DELETE FROM SYS_SYSTEM_CODE_DATA_KHM WHERE CODE_NAME=:foodCodeName";
                using(OracleCommand cmd = new OracleCommand(sql,conn))
                {
                    cmd.Parameters.Add("foodCodeName", foodCodeName);
                    int result = cmd.ExecuteNonQuery();
                    if(result >= 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool insertFood(Food food)
        {
            int[] pks = GetLastCodeNameAndCodeSeq();
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                // 일단 마지막 찾아야겠지... USer랑은 다르게 CODE_NAME도 동적으로 줘야하기 땜시롱 가져옴
                // 이거 걍 메서드로 빼자

                // pk들
                int codeName = pks[0]+1;
                int codeSeq = pks[1]+1;


                string sql = @"INSERT INTO SYS_SYSTEM_CODE_DATA_KHM
                             (PLANT, TABLE_NAME, CODE_NAME, CODE_SEQ, DESCRIPTION, CODE_GROUP1, CODE_GROUP2, CODE_GROUP3, CODE_GROUP4,CODE_GROUP5,EXP_DESCRIPTION)
                             VALUES ('MealPlan', 'Foods', :codeName, :codeSeq,':foodName',':kcal',':carbo',':protein',':fat',':nrv',':nrvType')";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("codeName", codeName);
                    cmd.Parameters.Add("codeSeq", codeSeq);
                    cmd.Parameters.Add("foodName", food.Name);
                    cmd.Parameters.Add("kcal", food.Kcal);
                    cmd.Parameters.Add("carbo", food.Carbohydrate);
                    cmd.Parameters.Add("fat", food.Fat);
                    cmd.Parameters.Add("nrv", food.NutrientReferenceValue);
                    cmd.Parameters.Add("nrvType", food.NrvType);

                    if (cmd.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }
                    return false;
                }

                

            }
        }

        public List<Food> selectAllFoods()
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                string sql = "SELECT * FROM SYS_SYSTEM_CODE_DATA_KHM WHERE PLANT='MealPlan' AND TABLE_NAME='Foods'";

                using (OracleCommand oracleCommand = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader result = oracleCommand.ExecuteReader())
                    {
                        List<Food> foods = new List<Food>();

                        while (result.Read())
                        {
                            int foodCodeName = int.Parse(result.GetString(2));
                            string foodName = result.GetString(4);
                            int kcal = int.Parse(result.GetString(5));
                            int carbo = int.Parse(result.GetString(6));
                            int protein = int.Parse(result.GetString(7));
                            int fat = int.Parse(result.GetString(8));
                            int nrv = int.Parse(result.GetString(9));
                            string nrvType = result.GetString(10);

                            foods.Add(new Food(foodCodeName,foodName,kcal,carbo,protein,fat,nrv,nrvType));
                        }
                        return foods;
                    }
                }
            }
        }

        public Food selectFoodByFoodCodeName(int foodCodeName)
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                string sql = @"SELECT CODE_NAME, DESCRIPTION, CODE_GROUP1, CODE_GROUP2, CODE_GROUP3, CODE_GROUP4, CODE_GROUP5, EXP_DESCRIPTION
                               FROM SYS_SYSTEM_CODE_DATA_KHM
                               WHERE CODE_NAME=:foodCodeName";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("foodCodeName", foodCodeName);

                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        result.Read();
                        int _foodCodeName = int.Parse(result.GetString(0));
                        string foodName = result.GetString(1);
                        int kcal = int.Parse(result.GetString(2));
                        int carbo = int.Parse(result.GetString(3));
                        int protein = int.Parse(result.GetString(4));
                        int fat = int.Parse(result.GetString(5));
                        int nrv = int.Parse(result.GetString(6));
                        string nrvType = result.GetString(7);


                        return new Food(_foodCodeName, foodName, kcal, carbo, protein, fat, nrv, nrvType);
                    }
                }
            }
        }

        public List<Food> selectFoodByFoodName(string foodName)
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                string sql = @"SELECT CODE_NAME, DESCRIPTION, CODE_GROUP1, CODE_GROUP2, CODE_GROUP3, CODE_GROUP4, CODE_GROUP5, EXP_DESCRIPTION
                               FROM SYS_SYSTEM_CODE_DATA_KHM
                               WHERE DESCRIPTION=:foodName";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("foodName", foodName);

                    MessageBox.Show(cmd.ToString());
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        List<Food> sameNameFoods = new List<Food>();
                        while (result.Read())
                        {
                            int foodCodeName = int.Parse(result.GetString(0));
                            string _foodName = result.GetString(1);
                            int kcal = int.Parse(result.GetString(2));
                            int carbo = int.Parse(result.GetString(3));
                            int protein = int.Parse(result.GetString(4));
                            int fat = int.Parse(result.GetString(5));
                            int nrv = int.Parse(result.GetString(6));
                            string nrvType = result.GetString(7);
                            sameNameFoods.Add(new Food(foodCodeName, _foodName, kcal, carbo, protein, fat, nrv, nrvType));
                        }
                        return sameNameFoods;
                    }
                }
            }
        }




        public int[] GetLastCodeNameAndCodeSeq()
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                // 일단 마지막 찾아야겠지... USer랑은 다르게 CODE_NAME도 동적으로 줘야하기 땜시롱 가져옴
                // 이거 걍 메서드로 빼자
                string sql = @"SELECT CODE_NAME, CODE_SEQ FROM SYS_SYSTEM_CODE_DATA_KHM
                               WHERE 
                               PLANT='MealPlan' AND 
                               TABLE_NAME='Foods'
                               ORDER BY CODE_SEQ DESC
                               FETCH FIRST 1 ROWS ONLY";
                using(OracleCommand cmd = new OracleCommand(sql,conn))
                {
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        result.Read();

                        int codeName = int.Parse(result.GetString(0));
                        int codeSeq = int.Parse(result.GetString(1));
                        return new int[] { codeName, codeSeq };
                    }
                }

            }
        }
    }
}
