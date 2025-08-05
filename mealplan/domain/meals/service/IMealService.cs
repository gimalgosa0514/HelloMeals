using mealplan.domain.meals.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.meals.service
{
    internal interface IMealService
    {
        List<MealFood> getTodayMeal(string userId, string mealType);
        List<MealFood> getTodayMeal(string userId);
        List<MealFood> getMealByDate(string userId, string mealType, string date);
        List<MealFood> getMealByDate(string userId, string date);
        bool saveMeal(string loginId, int foodCodeName,MealFood mealFood);
        bool removeMealFood(int mealFoodCodeName);
    }
}
