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
                int mealCodeName = GetMealsLastCodeName() + 1;
                

                using(OracleConnection conn = oracleUtil.GetConnection())
                {
                    string sql = @"
                                INSERT ALL
                                    INTO SYS_SYSTEM_CODE_DATA_KHM
                                    VALUES('MealPlan2','Meals',:mealCodeName, 1, :userId,null,null,null,null,null,null,null,null,null,null,null)
                                    INTO SYS_SYSTEM_CODE_DATA_KHM
                                    VALUES('MealPlan2','Meals',:mealCodeName, 2, :mealType,null,null,null,null,null,null,null,null,null,null,null)
                                    INTO SYS_SYSTEM_CODE_DATA_KHM
                                    VALUES('MealPlan2','Meals',:mealCodeName, 3, to_date(sysdate),null,null,null,null,null,null,null,null,null,null,null)
                                SELECT * FROM DUAL
";

                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {

                        //MessageBox.Show(mealCodeName.ToString());
                        //MessageBox.Show(meal.LoginId);
                        //MessageBox.Show(meal.MealType);
                        cmd.BindByName = true;
                        cmd.Parameters.Add("mealCodeName", mealCodeName);
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

                string sql = @"
                            SELECT mid.CODE_NAME,mid.DESCRIPTION ,mtype.DESCRIPTION, created.DESCRIPTION
                            FROM SYS_SYSTEM_CODE_DATA_KHM mid
                                JOIN SYS_SYSTEM_CODE_DATA_KHM mtype
                                ON mtype.CODE_NAME = mid.CODE_NAME AND mtype.CODE_SEQ = 2
                                JOIN SYS_SYSTEM_CODE_DATA_KHM created
                                ON created.CODE_NAME = mid.CODE_NAME AND created.CODE_SEQ = 3
                            WHERE mid.PLANT='MealPlan2' 
                            AND mid.TABLE_NAME='Meals'
                            AND mid.DESCRIPTION=:userId 
                            AND mid.CODE_SEQ=1
                            AND mtype.DESCRIPTION=:mealType
                            AND created.DESCRIPTION=to_char(sysdate)
                            ";

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
        // 이거 하는중 15:34
        public List<MealFood> selectTodayMealFood(string userId)
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {

                string sql = @"
                            SELECT 
                                   meal_foods.meal_food_code_name 순번, 
                                   meals.meal_type 식사타입,
                                   meal_foods.amount 섭취량, 
                                   foods.food_nrv_type 섭취량타입,
                                   foods.food_name 음식이름,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_kcal 칼로리,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_carbo 탄수화물,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_protein 단백질, 
                                   (meal_foods.amount / foods.food_nrv) * foods.food_fat 지방
                            FROM 
                                (
                                SELECT userId.CODE_NAME meal_code_name, userId.DESCRIPTION meal_userId, mtype.DESCRIPTION meal_type, created.DESCRIPTION meal_created
                                FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM userId
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mtype
                                    ON mtype.CODE_NAME = userId.CODE_NAME AND mtype.CODE_SEQ=2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM created
                                    ON created.CODE_NAME = userId.CODE_NAME AND created.CODE_SEQ=3
                                WHERE 
                                    userId.PLANT='MealPlan2' AND
                                    userId.TABLE_NAME='Meals' AND
                                    userId.CODE_SEQ = 1 AND
                                    mtype.PLANT = 'MealPlan2' AND
                                    created.PLANT = 'MealPlan2'
                                ) meals
                                JOIN (
                                SELECT mfid.CODE_NAME meal_food_code_name, mid.DESCRIPTION meal_code_name, fid.DESCRIPTION food_code_name, amount.DESCRIPTION amount
                                FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM mfid
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mid
                                    ON mid.CODE_NAME = mfid.CODE_NAME AND mid.CODE_SEQ = 1
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM fid 
                                    ON fid.CODE_NAME = mfid.CODE_NAME AND fid.CODE_SEQ = 2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM amount
                                    ON amount.CODE_NAME = mfid.CODE_NAME AND amount.CODE_SEQ = 3
                                WHERE 
                                    mfid.PLANT ='MealPlan2' AND mfid.TABLE_NAME='Meal_Foods'AND mfid.CODE_SEQ = 1 AND
                                    mid.PLANT = 'MealPlan2' AND
                                    fid.PLANT = 'MealPlan2' AND 
                                    amount.PLANT = 'MealPlan2'
                                ) meal_foods
                                ON meals.meal_code_name= meal_foods.meal_code_name
                                JOIN 
                                    (
                                SELECT fname.CODE_NAME food_code_name, fname.DESCRIPTION food_name, kcal.DESCRIPTION food_kcal, carbo.DESCRIPTION food_carbo, protein.DESCRIPTION food_protein, fat.DESCRIPTION food_fat, nrv.DESCRIPTION food_nrv, nrv_type.DESCRIPTION food_nrv_type
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
                                WHERE 
                                    fname.PLANT='MealPlan2' AND fname.TABLE_NAME = 'Foods' AND fname.CODE_SEQ = 1 AND
                                    kcal.PLANT='MealPlan2' AND 
                                    carbo.PLANT='MealPlan2' AND
                                    protein.PLANT='MealPlan2' AND
                                    fat.PLANT='MealPlan2' AND
                                    nrv.PLANT='MealPlan2' AND
                                    nrv_type.PLANT='MealPlan2'
                                ) foods
                                ON meal_foods.food_code_name = foods.food_code_name
                            WHERE meals.meal_userId =:userId AND meals.meal_created=to_char(sysdate,'yy/mm/dd')
                            ORDER BY 
                                CASE meals.meal_type
                                WHEN '아침' THEN 1
                                WHEN '점심' THEN 2
                                WHEN '저녁' THEN 3
                                ELSE 4
                                END
                                ";
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

                string sql = @"
                            SELECT 
                                   meal_foods.meal_food_code_name 순번, 
                                   meals.meal_type 식사타입,
                                   meal_foods.amount 섭취량, 
                                   foods.food_nrv_type 섭취량타입,
                                   foods.food_name 음식이름,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_kcal 칼로리,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_carbo 탄수화물,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_protein 단백질, 
                                   (meal_foods.amount / foods.food_nrv) * foods.food_fat 지방
                            FROM 
                                (
                                SELECT userId.CODE_NAME meal_code_name, userId.DESCRIPTION meal_userId, mtype.DESCRIPTION meal_type, created.DESCRIPTION meal_created
                                FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM userId
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mtype
                                    ON mtype.CODE_NAME = userId.CODE_NAME AND mtype.CODE_SEQ=2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM created
                                    ON created.CODE_NAME = userId.CODE_NAME AND created.CODE_SEQ=3
                                WHERE 
                                    userId.PLANT='MealPlan2' AND
                                    userId.TABLE_NAME='Meals' AND
                                    userId.CODE_SEQ = 1 AND
                                    mtype.PLANT = 'MealPlan2' AND
                                    created.PLANT = 'MealPlan2'
                                ) meals
                                JOIN (
                                SELECT mfid.CODE_NAME meal_food_code_name, mid.DESCRIPTION meal_code_name, fid.DESCRIPTION food_code_name, amount.DESCRIPTION amount
                                FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM mfid
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mid
                                    ON mid.CODE_NAME = mfid.CODE_NAME AND mid.CODE_SEQ = 1
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM fid 
                                    ON fid.CODE_NAME = mfid.CODE_NAME AND fid.CODE_SEQ = 2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM amount
                                    ON amount.CODE_NAME = mfid.CODE_NAME AND amount.CODE_SEQ = 3
                                WHERE 
                                    mfid.PLANT ='MealPlan2' AND mfid.TABLE_NAME='Meal_Foods'AND mfid.CODE_SEQ = 1 AND
                                    mid.PLANT = 'MealPlan2' AND
                                    fid.PLANT = 'MealPlan2' AND 
                                    amount.PLANT = 'MealPlan2'
                                ) meal_foods
                                ON meals.meal_code_name= meal_foods.meal_code_name
                                JOIN 
                                    (
                                SELECT fname.CODE_NAME food_code_name, fname.DESCRIPTION food_name, kcal.DESCRIPTION food_kcal, carbo.DESCRIPTION food_carbo, protein.DESCRIPTION food_protein, fat.DESCRIPTION food_fat, nrv.DESCRIPTION food_nrv, nrv_type.DESCRIPTION food_nrv_type
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
                                WHERE 
                                    fname.PLANT='MealPlan2' AND fname.TABLE_NAME = 'Foods' AND fname.CODE_SEQ = 1 AND
                                    kcal.PLANT='MealPlan2' AND 
                                    carbo.PLANT='MealPlan2' AND
                                    protein.PLANT='MealPlan2' AND
                                    fat.PLANT='MealPlan2' AND
                                    nrv.PLANT='MealPlan2' AND
                                    nrv_type.PLANT='MealPlan2'
                                ) foods
                                ON meal_foods.food_code_name = foods.food_code_name
                            WHERE meals.meal_userId =:userId AND meals.meal_type=:mealType AND meals.meal_created=to_char(sysdate,'yy/mm/dd')
                            ";

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

                string sql = @"
                            SELECT 
                                   meal_foods.meal_food_code_name 순번, 
                                   meals.meal_type 식사타입,
                                   meal_foods.amount 섭취량, 
                                   foods.food_nrv_type 섭취량타입,
                                   foods.food_name 음식이름,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_kcal 칼로리,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_carbo 탄수화물,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_protein 단백질, 
                                   (meal_foods.amount / foods.food_nrv) * foods.food_fat 지방
                            FROM 
                                (
                                SELECT userId.CODE_NAME meal_code_name, userId.DESCRIPTION meal_userId, mtype.DESCRIPTION meal_type, created.DESCRIPTION meal_created
                                FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM userId
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mtype
                                    ON mtype.CODE_NAME = userId.CODE_NAME AND mtype.CODE_SEQ=2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM created
                                    ON created.CODE_NAME = userId.CODE_NAME AND created.CODE_SEQ=3
                                WHERE 
                                    userId.PLANT='MealPlan2' AND
                                    userId.TABLE_NAME='Meals' AND
                                    userId.CODE_SEQ = 1 AND
                                    mtype.PLANT = 'MealPlan2' AND
                                    created.PLANT = 'MealPlan2'
                                ) meals
                                JOIN (
                                SELECT mfid.CODE_NAME meal_food_code_name, mid.DESCRIPTION meal_code_name, fid.DESCRIPTION food_code_name, amount.DESCRIPTION amount
                                FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM mfid
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mid
                                    ON mid.CODE_NAME = mfid.CODE_NAME AND mid.CODE_SEQ = 1
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM fid 
                                    ON fid.CODE_NAME = mfid.CODE_NAME AND fid.CODE_SEQ = 2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM amount
                                    ON amount.CODE_NAME = mfid.CODE_NAME AND amount.CODE_SEQ = 3
                                WHERE 
                                    mfid.PLANT ='MealPlan2' AND mfid.TABLE_NAME='Meal_Foods'AND mfid.CODE_SEQ = 1 AND
                                    mid.PLANT = 'MealPlan2' AND
                                    fid.PLANT = 'MealPlan2' AND 
                                    amount.PLANT = 'MealPlan2'
                                ) meal_foods
                                ON meals.meal_code_name= meal_foods.meal_code_name
                                JOIN 
                                    (
                                SELECT fname.CODE_NAME food_code_name, fname.DESCRIPTION food_name, kcal.DESCRIPTION food_kcal, carbo.DESCRIPTION food_carbo, protein.DESCRIPTION food_protein, fat.DESCRIPTION food_fat, nrv.DESCRIPTION food_nrv, nrv_type.DESCRIPTION food_nrv_type
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
                                WHERE 
                                    fname.PLANT='MealPlan2' AND fname.TABLE_NAME = 'Foods' AND fname.CODE_SEQ = 1 AND
                                    kcal.PLANT='MealPlan2' AND 
                                    carbo.PLANT='MealPlan2' AND
                                    protein.PLANT='MealPlan2' AND
                                    fat.PLANT='MealPlan2' AND
                                    nrv.PLANT='MealPlan2' AND
                                    nrv_type.PLANT='MealPlan2'
                                ) foods
                                ON meal_foods.food_code_name = foods.food_code_name
                            WHERE meals.meal_userId =:userId AND meals.meal_type=:mealType AND meals.meal_created=:targetDate
                            ";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add("userId", userId);
                    cmd.Parameters.Add("mealType", mealType);
                    cmd.Parameters.Add("targetDate", date);
                    MessageBox.Show(date);
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

                string sql = @"
                            SELECT 
                                   meal_foods.meal_food_code_name 순번, 
                                   meals.meal_type 식사타입,
                                   meal_foods.amount 섭취량, 
                                   foods.food_nrv_type 섭취량타입,
                                   foods.food_name 음식이름,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_kcal 칼로리,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_carbo 탄수화물,
                                   (meal_foods.amount / foods.food_nrv) * foods.food_protein 단백질, 
                                   (meal_foods.amount / foods.food_nrv) * foods.food_fat 지방
                            FROM 
                                (
                                SELECT userId.CODE_NAME meal_code_name, userId.DESCRIPTION meal_userId, mtype.DESCRIPTION meal_type, created.DESCRIPTION meal_created
                                FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM userId
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mtype
                                    ON mtype.CODE_NAME = userId.CODE_NAME AND mtype.CODE_SEQ=2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM created
                                    ON created.CODE_NAME = userId.CODE_NAME AND created.CODE_SEQ=3
                                WHERE 
                                    userId.PLANT='MealPlan2' AND
                                    userId.TABLE_NAME='Meals' AND
                                    userId.CODE_SEQ = 1 AND
                                    mtype.PLANT = 'MealPlan2' AND
                                    created.PLANT = 'MealPlan2'
                                ) meals
                                JOIN (
                                SELECT mfid.CODE_NAME meal_food_code_name, mid.DESCRIPTION meal_code_name, fid.DESCRIPTION food_code_name, amount.DESCRIPTION amount
                                FROM 
                                    SYS_SYSTEM_CODE_DATA_KHM mfid
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM mid
                                    ON mid.CODE_NAME = mfid.CODE_NAME AND mid.CODE_SEQ = 1
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM fid 
                                    ON fid.CODE_NAME = mfid.CODE_NAME AND fid.CODE_SEQ = 2
                                    JOIN SYS_SYSTEM_CODE_DATA_KHM amount
                                    ON amount.CODE_NAME = mfid.CODE_NAME AND amount.CODE_SEQ = 3
                                WHERE 
                                    mfid.PLANT ='MealPlan2' AND mfid.TABLE_NAME='Meal_Foods'AND mfid.CODE_SEQ = 1 AND
                                    mid.PLANT = 'MealPlan2' AND
                                    fid.PLANT = 'MealPlan2' AND 
                                    amount.PLANT = 'MealPlan2'
                                ) meal_foods
                                ON meals.meal_code_name= meal_foods.meal_code_name
                                JOIN 
                                    (
                                SELECT fname.CODE_NAME food_code_name, fname.DESCRIPTION food_name, kcal.DESCRIPTION food_kcal, carbo.DESCRIPTION food_carbo, protein.DESCRIPTION food_protein, fat.DESCRIPTION food_fat, nrv.DESCRIPTION food_nrv, nrv_type.DESCRIPTION food_nrv_type
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
                                WHERE 
                                    fname.PLANT='MealPlan2' AND fname.TABLE_NAME = 'Foods' AND fname.CODE_SEQ = 1 AND
                                    kcal.PLANT='MealPlan2' AND 
                                    carbo.PLANT='MealPlan2' AND
                                    protein.PLANT='MealPlan2' AND
                                    fat.PLANT='MealPlan2' AND
                                    nrv.PLANT='MealPlan2' AND
                                    nrv_type.PLANT='MealPlan2'
                                ) foods
                                ON meal_foods.food_code_name = foods.food_code_name
                            WHERE meals.meal_userId =:userId AND meals.meal_created=:targetDate
                            ORDER BY 
                                CASE meals.meal_type
                                WHEN '아침' THEN 1
                                WHEN '점심' THEN 2
                                WHEN '저녁' THEN 3
                                ELSE 4
                                END
                            ";

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
              
                
                string sql = @"
                            SELECT userId.CODE_NAME
                            FROM SYS_SYSTEM_CODE_DATA_KHM userId
                                 JOIN SYS_SYSTEM_CODE_DATA_KHM mtype
                                 ON mtype.CODE_NAME = userId.CODE_NAME AND mtype.CODE_SEQ=2
                                 JOIN SYS_SYSTEM_CODE_DATA_KHM created
                                 ON created.CODE_NAME = userId.CODE_NAME AND created.CODE_SEQ=3
                            WHERE userId.PLANT= 'MealPlan2' AND
                                  userId.TABLE_NAME='Meals' AND
                                  userId.CODE_SEQ=1 AND
                                  userId.DESCRIPTION=:userId AND
                                  mtype.PLANT='MealPlan2' AND
                                  created.PLANT='MealPlan2' AND 
                                  created.DESCRIPTION=to_char(sysdate,'YY/MM/DD') AND 
                                  mtype.DESCRIPTION=:mealType
                            ";

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
                sql = @"
                        INSERT ALL
                            INTO SYS_SYSTEM_CODE_DATA_KHM
                            VALUES('MealPlan2', 'Meal_Foods', :mealFoodCodeName, 1, :mealCodeName , null, null, null, null, null, null, null, null, null, null, null)
                            INTO SYS_SYSTEM_CODE_DATA_KHM
                            VALUES('MealPlan2', 'Meal_Foods', :mealFoodCodeName, 2, :foodCodeName, null, null, null, null, null, null, null, null, null, null, null)
                            INTO SYS_SYSTEM_CODE_DATA_KHM
                            VALUES('MealPlan2', 'Meal_Foods', :mealFoodCodeName, 3, :amount ,null, null, null, null, null, null, null, null, null, null, null)
                        SELECT * FROM DUAL
";

                //하.. 넣기 전에 MealFoodCodeName이랑 MealFoodCodeSeq도 알아내야함.
                int pks = GetLastMealFoodsCodeName()+1;

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("mealFoodCodeName", pks);
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
                               WHERE PLANT='MealPlan2' AND
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



        public int GetMealsLastCodeName()
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                // 일단 마지막 찾아야겠지... USer랑은 다르게 CODE_NAME도 동적으로 줘야하기 땜시롱 가져옴
                // 이거 걍 메서드로 빼자
                string sql = @"
                            SELECT CODE_NAME 
                            FROM (SELECT CODE_NAME 
                                   FROM SYS_SYSTEM_CODE_DATA_KHM
                                   WHERE PLANT='MealPlan2' AND TABLE_NAME='Meals'
                                   ORDER BY CODE_NAME DESC)
                            WHERE ROWNUM = 1";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {
                        if(result.Read())
                        {
                            return int.Parse(result.GetString(0));
                        }
                        return 10000;

                        

                    }
                }

            }
        }

        public int GetLastMealFoodsCodeName()
        {
            using (OracleConnection conn = oracleUtil.GetConnection())
            {
                // 일단 마지막 찾아야겠지... USer랑은 다르게 CODE_NAME도 동적으로 줘야하기 땜시롱 가져옴
                // 이거 걍 메서드로 빼자
                string sql = @"SELECT CODE_NAME
                               FROM (
                                SELECT CODE_NAME, CODE_SEQ
                                FROM SYS_SYSTEM_CODE_DATA_KHM
                                WHERE 
                                PLANT='MealPlan2' AND 
                                TABLE_NAME='Meal_Foods'
                                ORDER BY CODE_NAME DESC)
                               WHERE ROWNUM = 1";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader result = cmd.ExecuteReader())
                    {

                        // 만약 없으면 데이터가 없다는거.. 그러니까 일단 기본값 -1 한 값 주자.
                        if (result.Read())
                        {
                            int codeName = int.Parse(result.GetString(0));
                            
                            return codeName;
                        }


                        return 30000;
                    }
                }

            }
        }


        // 맵핑테이블에 데이터 넣는 메서드 






    }
}
