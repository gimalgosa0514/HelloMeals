using mealplan.domain.foods.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.foods.service
{
    internal interface IFoodService
    {
        List<Food> getAllFood();
        List<Food> searchFoodByName(string foodName);

        Food getFood(int foodCodeName);
    }
}
