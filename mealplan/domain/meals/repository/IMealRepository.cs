using mealplan.domain.meals.model;
using mealplan.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.meals.repository
{
    internal interface IMealRepository
    {

        List<MealFood> selectTodayMealFood(string userId, string mealType);

        bool insertMeal(Meal meal);

        bool insertMealFood(MealFood mealFood, string userId, int foodCodeName);
        bool isExist(string userId, string mealType);

        bool deleteMealFood(int mealFoodCodeName);

    }
}
