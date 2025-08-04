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
        bool saveMeal(string loginId, int foodCodeName,MealFood mealFood);

        bool removeMealFood(int mealFoodCodeName);
    }
}
