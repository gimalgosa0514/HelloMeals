using mealplan.domain.foods.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.foods.repository
{
    internal interface IFoodRepository
    {
        List<Food> selectFoodByFoodName(string foodName);
        Food selectFoodByFoodCodeName(int foodCodeName);
        List<Food> selectAllFoods();
        bool insertFood(Food food);
        bool deleteFood(int foodCodeName);

        int[] GetLastCodeNameAndCodeSeq();
    }
}
