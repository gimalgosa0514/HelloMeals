using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.meals.model
{
    internal class MealFood
    {
        public MealFood(int mealFoodCodeName, string mealType, int amount, string nrvType, string foodName, double kcal, double carboHydrate, double protein, double fat)
        {
            MealFoodCodeName = mealFoodCodeName;
            MealType = mealType;
            Amount = amount;
            NrvType = nrvType;
            FoodName = foodName;
            Kcal = kcal;
            CarboHydrate = carboHydrate;
            Protein = protein;
            Fat = fat;
        }

        public MealFood(int amount, string mealType)
        {
            Amount = amount;
            MealType = mealType;
        }

        public int MealFoodCodeName { get; set; }
        public string MealType { get; set; }
        public int Amount { get; set; }
        public string NrvType { get; set; }
        public string FoodName { get; set; }
        public double Kcal { get; set; }     
        public double CarboHydrate { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        
       
    }
}
