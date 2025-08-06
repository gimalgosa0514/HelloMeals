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
                string sql = @"DELETE FROM SYS_SYSTEM_CODE_DATA_KHM WHERE CODE_NAME=:foodCodeName AND PLANT='MealPlan2' AND TABLE_NAME= 'Foods'";
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

            // PK 찾아주고.
            int codeName = GetFoodsLastCodeName()+1;
            using (OracleConnection conn = oracleUtil.GetConnection())
            {



                string sql = @"
                            INSERT ALL
                                INTO SYS_SYSTEM_CODE_DATA_KHM
                                VALUES('MealPlan2', 'Foods', :codeName, 1,:foodName, null, null, null, null, null, null, null, null, null, null, null)
                                INTO SYS_SYSTEM_CODE_DATA_KHM
                                VALUES('MealPlan2', 'Foods', :codeName, 2,:kcal, null, null, null, null, null, null, null, null, null, null, null)
                                INTO SYS_SYSTEM_CODE_DATA_KHM
                                VALUES('MealPlan2', 'Foods', :codeName, 3,:carbo, null, null, null, null, null, null, null, null, null, null, null)
                                INTO SYS_SYSTEM_CODE_DATA_KHM
                                VALUES('MealPlan2', 'Foods', :codeName, 4,:protein, null, null, null, null, null, null, null, null, null, null, null)
                                INTO SYS_SYSTEM_CODE_DATA_KHM
                                VALUES('MealPlan2', 'Foods', :codeName, 5,:fat, null, null, null, null, null, null, null, null, null, null, null)
                                INTO SYS_SYSTEM_CODE_DATA_KHM
                                VALUES('MealPlan2', 'Foods', :codeName, 6,:nrv, null, null, null, null, null, null, null, null, null, null, null)
                                INTO SYS_SYSTEM_CODE_DATA_KHM
                                VALUES('MealPlan2', 'Foods', :codeName, 7,:nrvType, null, null, null, null, null, null, null, null, null, null, null)
                            SELECT * FROM DUAL
                            ";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("codeName", codeName);
                    cmd.Parameters.Add("foodName", food.Name);
                    cmd.Parameters.Add("kcal", food.Kcal);
                    cmd.Parameters.Add("carbo", food.Carbohydrate);
                    cmd.Parameters.Add("protein", food.Protein);
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
                string sql = @"
                            SELECT fname.CODE_NAME, fname.DESCRIPTION, kcal.DESCRIPTION, carbo.DESCRIPTION, protein.DESCRIPTION, fat.DESCRIPTION, nrv.DESCRIPTION, nrv_type.DESCRIPTION
                            FROM SYS_SYSTEM_CODE_DATA_KHM fname
                                JOIN SYS_SYSTEM_CODE_DATA_KHM kcal
                                ON kcal.CODE_NAME  = fname.CODE_NAME AND kcal.CODE_SEQ = 2
                                JOIN SYS_SYSTEM_CODE_DATA_KHM carbo
                                ON carbo.CODE_NAME  = fname.CODE_NAME AND carbo.CODE_SEQ = 3
                                JOIN SYS_SYSTEM_CODE_DATA_KHM protein
                                ON protein.CODE_NAME  = fname.CODE_NAME AND protein.CODE_SEQ = 4
                                JOIN SYS_SYSTEM_CODE_DATA_KHM fat
                                ON fat.CODE_NAME  = fname.CODE_NAME AND fat.CODE_SEQ = 5
                                JOIN SYS_SYSTEM_CODE_DATA_KHM nrv
                                ON nrv.CODE_NAME  = fname.CODE_NAME AND nrv.CODE_SEQ = 6
                                JOIN SYS_SYSTEM_CODE_DATA_KHM nrv_type
                                ON nrv_type.CODE_NAME  = fname.CODE_NAME AND nrv_type.CODE_SEQ = 7
                            WHERE fname.PLANT='MealPlan2' AND fname.TABLE_NAME = 'Foods' AND fname.CODE_SEQ = 1
                            ORDER BY fname.CODE_NAME ASC";

                using (OracleCommand oracleCommand = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader result = oracleCommand.ExecuteReader())
                    {
                        List<Food> foods = new List<Food>();

                        while (result.Read())
                        {
                            int foodCodeName = int.Parse(result.GetString(0));
                            string foodName = result.GetString(1);
                            double kcal = double.Parse(result.GetString(2));
                            double carbo = double.Parse(result.GetString(3));
                            double protein = double.Parse(result.GetString(4));
                            double fat = double.Parse(result.GetString(5));
                            int nrv = int.Parse(result.GetString(6));
                            string nrvType = result.GetString(7);

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
                string sql = @"
                            SELECT fname.CODE_NAME, fname.DESCRIPTION, kcal.DESCRIPTION, carbo.DESCRIPTION, protein.DESCRIPTION, fat.DESCRIPTION, nrv.DESCRIPTION, nrv_type.DESCRIPTION
                            FROM SYS_SYSTEM_CODE_DATA_KHM fname
                                JOIN SYS_SYSTEM_CODE_DATA_KHM kcal
                                ON kcal.CODE_NAME  = fname.CODE_NAME AND kcal.CODE_SEQ = 2
                                JOIN SYS_SYSTEM_CODE_DATA_KHM carbo
                                ON carbo.CODE_NAME  = fname.CODE_NAME AND carbo.CODE_SEQ = 3
                                JOIN SYS_SYSTEM_CODE_DATA_KHM protein
                                ON protein.CODE_NAME  = fname.CODE_NAME AND protein.CODE_SEQ = 4
                                JOIN SYS_SYSTEM_CODE_DATA_KHM fat
                                ON fat.CODE_NAME  = fname.CODE_NAME AND fat.CODE_SEQ = 5
                                JOIN SYS_SYSTEM_CODE_DATA_KHM nrv
                                ON nrv.CODE_NAME  = fname.CODE_NAME AND nrv.CODE_SEQ = 6
                                JOIN SYS_SYSTEM_CODE_DATA_KHM nrv_type
                                ON nrv_type.CODE_NAME  = fname.CODE_NAME AND nrv_type.CODE_SEQ = 7
                            WHERE fname.PLANT='MealPlan2' AND fname.TABLE_NAME = 'Foods' AND fname.CODE_SEQ = 1 AND fname.CODE_NAME =:foodCodeName
                            ";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("foodCodeName", foodCodeName);

                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        result.Read();
                        int _foodCodeName = int.Parse(result.GetString(0));
                        string foodName = result.GetString(1);
                        double kcal = double.Parse(result.GetString(2));
                        double carbo = double.Parse(result.GetString(3));
                        double protein = double.Parse(result.GetString(4));
                        double fat = double.Parse(result.GetString(5));
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
                string sql = @"
                            SELECT fname.CODE_NAME, fname.DESCRIPTION, kcal.DESCRIPTION, carbo.DESCRIPTION, protein.DESCRIPTION, fat.DESCRIPTION, nrv.DESCRIPTION, nrv_type.DESCRIPTION
                            FROM SYS_SYSTEM_CODE_DATA_KHM fname
                                JOIN SYS_SYSTEM_CODE_DATA_KHM kcal
                                ON kcal.CODE_NAME  = fname.CODE_NAME AND kcal.CODE_SEQ = 2
                                JOIN SYS_SYSTEM_CODE_DATA_KHM carbo
                                ON carbo.CODE_NAME  = fname.CODE_NAME AND carbo.CODE_SEQ = 3
                                JOIN SYS_SYSTEM_CODE_DATA_KHM protein
                                ON protein.CODE_NAME  = fname.CODE_NAME AND protein.CODE_SEQ = 4
                                JOIN SYS_SYSTEM_CODE_DATA_KHM fat
                                ON fat.CODE_NAME  = fname.CODE_NAME AND fat.CODE_SEQ = 5
                                JOIN SYS_SYSTEM_CODE_DATA_KHM nrv
                                ON nrv.CODE_NAME  = fname.CODE_NAME AND nrv.CODE_SEQ = 6
                                JOIN SYS_SYSTEM_CODE_DATA_KHM nrv_type
                                ON nrv_type.CODE_NAME  = fname.CODE_NAME AND nrv_type.CODE_SEQ = 7
                            WHERE fname.PLANT='MealPlan2' AND fname.TABLE_NAME = 'Foods' AND fname.CODE_SEQ = 1 AND fname.DESCRIPTION LIKE '%' || :foodName || '%'
                            ";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("foodName", foodName);

                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        List<Food> sameNameFoods = new List<Food>();
                        while (result.Read())
                        {
                            int foodCodeName = int.Parse(result.GetString(0));
                            string _foodName = result.GetString(1);
                            double kcal = double.Parse(result.GetString(2));
                            double carbo = double.Parse(result.GetString(3));
                            double protein = double.Parse(result.GetString(4));
                            double fat = double.Parse(result.GetString(5));
                            int nrv = int.Parse(result.GetString(6));
                            string nrvType = result.GetString(7);
                            sameNameFoods.Add(new Food(foodCodeName, _foodName, kcal, carbo, protein, fat, nrv, nrvType));
                        }
                        return sameNameFoods;
                    }
                }
            }
        }




        public int GetFoodsLastCodeName()
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                // 일단 마지막 찾아야겠지... USer랑은 다르게 CODE_NAME도 동적으로 줘야하기 땜시롱 가져옴
                // 이거 걍 메서드로 빼자
                string sql = @"
                            SELECT CODE_NAME 
                            FROM (SELECT CODE_NAME 
                                   FROM SYS_SYSTEM_CODE_DATA_KHM
                                   WHERE PLANT='MealPlan2' AND TABLE_NAME='Foods'
                                   ORDER BY CODE_NAME DESC)
                            WHERE ROWNUM = 1";
                using(OracleCommand cmd = new OracleCommand(sql,conn))
                {
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        result.Read();

                        return int.Parse(result.GetString(0));
                        
                    }
                }

            }
        }
    }
}
