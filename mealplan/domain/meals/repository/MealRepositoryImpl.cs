using mealplan.domain.meals.model;
using mealplan.util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace mealplan.domain.meals.repository
{
    internal class MealRepositoryImpl : IMealRepository
    {

        OracleUtil oracleUtil;
        public MealRepositoryImpl()
        {
            this.oracleUtil = OracleUtil.getInstance();
        }

        public bool insertMeal(Meal meal)
        {
            // 1. 식단을 넣기 전에 일단 그날 식단 데이터가 있는지 없는지부터 봐야됨.

            // 1-1 없으면 넣어! return true;
            if (!isExist(meal.LoginId, meal.MealType)) 
            {

                //일단 CodeName이랑 CodeSeq 늘려줘야하니깐 받아옴
                int[] pks = GetLastCodeNameAndCodeSeq();
                int mealCodeName = pks[0] + 1;
                int mealCodeSeq = pks[1] + 1;

                using(OracleConnection conn = oracleUtil.GetConnection())
                {
                    string sql = @"INSERT INTO SYS_SYSTEM_CODE_DATA_KHM
                                   (PLANT, TABLE_NAME, CODE_NAME, CODE_SEQ, DESCRIPTION, CODE_GROUP1, CODE_GROUP2)
                                   VALUES
                                   ('MealPlan', 'Meals', :codeName, :codeSeq, :userId, :mealType, to_date(sysdate))";

                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add("codeName", mealCodeName);
                        cmd.Parameters.Add("codeSeq", mealCodeSeq);
                        cmd.Parameters.Add("userId", meal.LoginId);
                        cmd.Parameters.Add("mealType", meal.MealType);

                        if(cmd.ExecuteNonQuery() >= 1)
                        {
                            return true;
                        }
                    }
                }
            }
            // 1-1 있으면 넣지마. return false;
            return false;
        }

        public bool isExist(string userId, string mealType)
        {

            using (OracleConnection conn = oracleUtil.GetConnection())
            {

                string sql = @"SELECT * FROM SYS_SYSTEM_CODE_DATA_KHM
                               WHERE PLANT='MealPlan' AND
                               TABLE_NAME = 'Meals' AND
                               DESCRIPTION=:userId AND
                               CODE_GROUP1=:mealType AND
                               CODE_GROUP2=to_date(sysdate)";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("userId", userId);
                    cmd.Parameters.Add("mealType", mealType);


                    // select해서 읽어가지고 있으면 오늘 날짜의 해당 식단 타입의 식단 데이터가 있는거임!
                    // 없으면 false 반환하고 만들어주면 되겠지 ㅎㅎ
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        if(result.Read())
                        {
                            return true;
                        } else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        public List<MealFood> selectTodayMealFood(string userId)
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {

                string sql = @"SELECT 
                                    mf.CODE_NAME AS 순번,
                                    m.CODE_GROUP1 AS 식사타입,
                                    mf.CODE_GROUP2 AS 섭취량,
                                    f.EXP_DESCRIPTION AS 섭취량타입,
                                    f.DESCRIPTION AS 음식이름,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP1) as 칼로리,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP2) as 탄수화물,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP3) as 단백질,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP4) as 지방
                               FROM SYS_SYSTEM_CODE_DATA_KHM m
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mf
                                        ON m.CODE_NAME = mf.DESCRIPTION
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM f
                                        ON mf.CODE_GROUP1 = f.CODE_NAME
                               WHERE 
                                    m.PLANT='MealPlan' AND
                                    mf.PLANT='MealPlan' AND
                                    f.PLANT='MealPlan' AND
                                    m.DESCRIPTION = :userId AND 
                                    m.CODE_GROUP2=to_date(sysdate)
                               ORDER BY 
                                    CASE m.CODE_GROUP1
                                    WHEN '아침' THEN 1
                                    WHEN '점심' THEN 2
                                    WHEN '저녁' THEN 3
                                    ELSE 4
                                    END";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("userId", userId);

                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        List<MealFood> mealFoods = new List<MealFood>();

                        while (result.Read())
                        {
                            int mealFoodCodeName = int.Parse(result.GetString(0));
                            string _mealType = result.GetString(1);
                            int amount = int.Parse(result.GetString(2));
                            string nrvType = result.GetString(3);
                            string foodName = result.GetString(4);
                            double kcal = double.Parse(result.GetString(5));
                            double carbo = double.Parse(result.GetString(6));
                            double protein = double.Parse(result.GetString(7));
                            double fat = double.Parse(result.GetString(8));

                            mealFoods.Add(new MealFood(mealFoodCodeName, _mealType, amount, nrvType, foodName, kcal, carbo, protein, fat));
                        }

                        return mealFoods;
                    }
                }
            }
        }
        public List<MealFood> selectTodayMealFood(string userId, string mealType)
        {
            using(OracleConnection conn = oracleUtil.GetConnection())
            {

                string sql = @"SELECT
                                    mf.CODE_NAME AS 순번,
                                    m.CODE_GROUP1 AS 식사타입,
                                    mf.CODE_GROUP2 AS 섭취량,
                                    f.EXP_DESCRIPTION AS 섭취량타입,
                                    f.DESCRIPTION AS 음식이름,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP1) as 칼로리,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP2) as 탄수화물,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP3) as 단백질,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP4) as 지방
                               FROM SYS_SYSTEM_CODE_DATA_KHM m
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mf
                                        ON m.CODE_NAME = mf.DESCRIPTION
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM f
                                        ON mf.CODE_GROUP1 = f.CODE_NAME
                               WHERE 
                                    m.PLANT='MealPlan' AND
                                    mf.PLANT='MealPlan' AND
                                    f.PLANT='MealPlan' AND
                                    m.DESCRIPTION = :userId AND 
                                    m.CODE_GROUP1 = :mealType AND 
                                    m.CODE_GROUP2=to_date(sysdate)";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("userId", userId);
                    cmd.Parameters.Add("mealType", mealType);

                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        List<MealFood> mealFoods = new List<MealFood>();

                        while(result.Read())
                        {
                            int mealFoodCodeName = int.Parse(result.GetString(0));
                            string _mealType = result.GetString(1);
                            int amount = int.Parse(result.GetString(2));
                            string nrvType = result.GetString(3);
                            string foodName = result.GetString(4);
                            double kcal = double.Parse(result.GetString(5));
                            double carbo = double.Parse(result.GetString(6));
                            double protein = double.Parse(result.GetString(7));
                            double fat = double.Parse(result.GetString(8));

                            mealFoods.Add(new MealFood(mealFoodCodeName, mealType, amount, nrvType, foodName,kcal, carbo, protein,fat));
                        }

                        return mealFoods;
                    }
                }
            }
        }

        public List<MealFood> selectMealFoodByDate(string userId, string mealType, string date)
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {

                string sql = @"SELECT 
                                    mf.CODE_NAME AS 순번,
                                    m.CODE_GROUP1 AS 식사타입,
                                    mf.CODE_GROUP2 AS 섭취량,
                                    f.EXP_DESCRIPTION AS 섭취량타입,
                                    f.DESCRIPTION AS 음식이름,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP1) as 칼로리,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP2) as 탄수화물,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP3) as 단백질,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP4) as 지방
                               FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM m
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mf
                                        ON m.CODE_NAME = mf.DESCRIPTION
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM f
                                        ON mf.CODE_GROUP1 = f.CODE_NAME
                               WHERE
                                    m.PLANT='MealPlan' AND
                                    mf.PLANT='MealPlan' AND
                                    f.PLANT='MealPlan' AND
                                    m.DESCRIPTION = :userId AND 
                                    m.CODE_GROUP1 = :mealType AND 
                                    m.CODE_GROUP2=:targetDate";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("userId", userId);
                    cmd.Parameters.Add("mealType", mealType);
                    cmd.Parameters.Add("targetDate", date);

                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        List<MealFood> mealFoods = new List<MealFood>();

                        while (result.Read())
                        {
                            int mealFoodCodeName = int.Parse(result.GetString(0));
                            string _mealType = result.GetString(1);
                            int amount = int.Parse(result.GetString(2));
                            string nrvType = result.GetString(3);
                            string foodName = result.GetString(4);
                            double kcal = double.Parse(result.GetString(5));
                            double carbo = double.Parse(result.GetString(6));
                            double protein = double.Parse(result.GetString(7));
                            double fat = double.Parse(result.GetString(8));

                            mealFoods.Add(new MealFood(mealFoodCodeName, mealType, amount, nrvType, foodName, kcal, carbo, protein, fat));
                        }

                        return mealFoods;
                    }
                }
            }
        }

        public List<MealFood> selectMealFoodByDate(string userId,string date)
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {

                string sql = @"SELECT 
                                    mf.CODE_NAME AS 순번,
                                    m.CODE_GROUP1 AS 식사타입,
                                    mf.CODE_GROUP2 AS 섭취량,
                                    f.EXP_DESCRIPTION AS 섭취량타입,
                                    f.DESCRIPTION AS 음식이름,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP1) as 칼로리,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP2) as 탄수화물,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP3) as 단백질,
                                    (mf.CODE_GROUP2/f.CODE_GROUP5) * (f.CODE_GROUP4) as 지방
                               FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM m
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mf
                                        ON m.CODE_NAME = mf.DESCRIPTION
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM f
                                        ON mf.CODE_GROUP1 = f.CODE_NAME
                               WHERE 
                                    m.PLANT='MealPlan' AND
                                    mf.PLANT='MealPlan' AND
                                    f.PLANT='MealPlan' AND
                                    m.DESCRIPTION = :userId AND 
                                    m.CODE_GROUP2=:targetDate
                               ORDER BY 
                                CASE m.CODE_GROUP1
                                WHEN '아침' THEN 1
                                WHEN '점심' THEN 2
                                WHEN '저녁' THEN 3
                                ELSE 4
                                END";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("userId", userId);
                    cmd.Parameters.Add("targetDate", date);

                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        List<MealFood> mealFoods = new List<MealFood>();

                        while (result.Read())
                        {
                            int mealFoodCodeName = int.Parse(result.GetString(0));
                            string _mealType = result.GetString(1);
                            int amount = int.Parse(result.GetString(2));
                            string nrvType = result.GetString(3);
                            string foodName = result.GetString(4);
                            double kcal = double.Parse(result.GetString(5));
                            double carbo = double.Parse(result.GetString(6));
                            double protein = double.Parse(result.GetString(7));
                            double fat = double.Parse(result.GetString(8));

                            mealFoods.Add(new MealFood(mealFoodCodeName, _mealType, amount, nrvType, foodName, kcal, carbo, protein, fat));
                        }

                        return mealFoods;
                    }
                }
            }
        }



        public bool insertMealFood(MealFood mealFood,string userId, int foodCodeName)
        {

            
            // 1. 데이터 넣기전에 일단 해당 끼니와 아이디로 된 당일 날 Meal이 먼저 있는지 부터 봐야겠지.
            if (!isExist(userId, mealFood.MealType))
            {
                //없으면 meal먼저 만들고 넣어주셈
                insertMeal(new Meal(userId, mealFood.MealType));
            }


            // 그 전에 넣으려고 하는 것의 식단 CODE_NAME를 알아야하기 때문에
            int targetMealCodeName = 0;

            using (OracleConnection conn = oracleUtil.GetConnection())
            {
              
                
                string sql = @"SELECT CODE_NAME FROM SYS_SYSTEM_CODE_DATA_KHM
                               WHERE PLANT='MealPlan' AND 
                               TABLE_NAME='Meals' AND
                               DESCRIPTION=:userId AND
                               CODE_GROUP1=:mealType AND
                               CODE_GROUP2=to_date(sysdate)";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("userId", userId);
                    cmd.Parameters.Add("mealType", mealFood.MealType);

                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        result.Read();
                        targetMealCodeName = int.Parse(result.GetString(0)); // 얻어왔음.
                    }

                }


                // 이제 넣자..
                sql = @"INSERT INTO SYS_SYSTEM_CODE_DATA_KHM 
                        (PLANT, TABLE_NAME, CODE_NAME, CODE_SEQ, DESCRIPTION, CODE_GROUP1, CODE_GROUP2)
                        VALUES('MealPlan', 'Meal_Foods', :mealFoodCodeName, :mealFoodCodeSeq, :mealCodeName, :foodCodeName, :amount)";

                //하.. 넣기 전에 MealFoodCodeName이랑 MealFoodCodeSeq도 알아내야함.
                int[] pks = GetLastCodeNameAndCodeSeqForMealFood();
                int mealFoodCodeName = pks[0] + 1;
                int mealFoodCodeSeq = pks[1] + 1;

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("mealFoodCodeName", mealFoodCodeName);
                    cmd.Parameters.Add("mealFoodCodeSeq", mealFoodCodeSeq);
                    cmd.Parameters.Add("mealCodeName", targetMealCodeName);
                    cmd.Parameters.Add("foodCodeName", foodCodeName);
                    cmd.Parameters.Add("amount", mealFood.Amount);

                    if (cmd.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public bool deleteMealFood(int mealFoodCodeName)
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                string sql = @"DELETE FROM SYS_SYSTEM_CODE_DATA_KHM
                               WHERE PLANT='MealPlan' AND
                               TABLE_NAME = 'Meal_Foods' AND 
                               CODE_NAME = :mealFoodCodeName";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("mealFoodCodeName", mealFoodCodeName);

                    if(cmd.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }



        public int[] GetLastCodeNameAndCodeSeq()
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                // 일단 마지막 찾아야겠지... USer랑은 다르게 CODE_NAME도 동적으로 줘야하기 땜시롱 가져옴
                // 이거 걍 메서드로 빼자
                string sql = @"SELECT CODE_NAME, CODE_SEQ
                               FROM (
                                SELECT CODE_NAME, CODE_SEQ
                                FROM SYS_SYSTEM_CODE_DATA_KHM
                                WHERE 
                                PLANT='MealPlan' AND 
                                TABLE_NAME='Meals'
                                ORDER BY CODE_SEQ DESC)
                               WHERE ROWNUM = 1";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {

                        // 만약 없으면 데이터가 없다는거.. 그러니까 일단 기본값 -1 한 값 주자.
                        if(result.Read())
                        {
                            int codeName = int.Parse(result.GetString(0));
                            int codeSeq = int.Parse(result.GetString(1));
                            return new int[] { codeName, codeSeq };
                        }

                        
                        return new int[] { 10000, 0 };
                    }
                }

            }
        }

        public int[] GetLastCodeNameAndCodeSeqForMealFood()
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                // 일단 마지막 찾아야겠지... USer랑은 다르게 CODE_NAME도 동적으로 줘야하기 땜시롱 가져옴
                // 이거 걍 메서드로 빼자
                string sql = @"SELECT CODE_NAME, CODE_SEQ
                               FROM (
                                SELECT CODE_NAME, CODE_SEQ
                                FROM SYS_SYSTEM_CODE_DATA_KHM
                                WHERE 
                                PLANT='MealPlan' AND 
                                TABLE_NAME='Meal_Foods'
                                ORDER BY CODE_SEQ DESC)
                               WHERE ROWNUM = 1";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {

                        // 만약 없으면 데이터가 없다는거.. 그러니까 일단 기본값 -1 한 값 주자.
                        if (result.Read())
                        {
                            int codeName = int.Parse(result.GetString(0));
                            int codeSeq = int.Parse(result.GetString(1));
                            return new int[] { codeName, codeSeq };
                        }


                        return new int[] { 30000, 0 };
                    }
                }

            }
        }


        // 맵핑테이블에 데이터 넣는 메서드 






    }
}
