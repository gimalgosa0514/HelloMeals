using mealplan.domain.meals.model;
using mealplan.domain.meals.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.meals.service
{
    internal class MealServiceImpl : IMealService
    {
        IMealRepository mealRepository;
        public MealServiceImpl(IMealRepository mealRepository) 
        {
            this.mealRepository = mealRepository;
        }

        public List<MealFood> getTodayMeal(string userId, string mealType)
        {
            return mealRepository.selectTodayMealFood(userId, mealType);
        }

        public bool saveMeal(string loginId, int foodCodeName, MealFood mealFood)
        {
            return mealRepository.insertMealFood(mealFood, loginId, foodCodeName);
        }

        public bool removeMealFood(int mealFoodId)
        {
            return mealRepository.deleteMealFood(mealFoodId);
        }
    }
}
