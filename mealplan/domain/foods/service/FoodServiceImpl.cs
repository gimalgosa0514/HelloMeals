using mealplan.domain.foods.model;
using mealplan.domain.foods.repository;
using mealplan.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.foods.service
{
    internal class FoodServiceImpl : IFoodService
    {
        private IFoodRepository foodRepository;

        public FoodServiceImpl(IFoodRepository foodRepository)
        {
            this.foodRepository = foodRepository;
        }
        public List<Food> getAllFood()
        {
            return foodRepository.selectAllFoods();
        }

        public Food getFood(int foodCodeName)
        {
            return foodRepository.selectFoodByFoodCodeName(foodCodeName);
        }

        public List<Food> searchFoodByName(string foodName)
        {
            return foodRepository.selectFoodByFoodName(foodName);
        }
    }
}
