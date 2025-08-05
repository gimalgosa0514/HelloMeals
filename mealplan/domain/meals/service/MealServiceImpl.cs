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
        public List<MealFood> getTodayMeal(string userId)
        {
            return mealRepository.selectTodayMealFood(userId);
        }

        

        public bool saveMeal(string loginId, int foodCodeName, MealFood mealFood)
        {
            return mealRepository.insertMealFood(mealFood, loginId, foodCodeName);
        }

        public bool removeMealFood(int mealFoodId)
        {
            return mealRepository.deleteMealFood(mealFoodId);
        }

        public List<MealFood> getMealByDate(string userId, string mealType, string date)
        {
            return mealRepository.selectMealFoodByDate(userId, mealType, date);
        }

        public List<MealFood> getMealByDate(string userId, string date)
        {
            return mealRepository.selectMealFoodByDate(userId, date);
        }
    }
}
