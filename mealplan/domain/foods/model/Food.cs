using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.foods.model
{
    public class Food
    {

        public Food(int foodId, string name, double kcal, double carbohydrate, double protein, double fat, int nutrientReferenceValue, string nrvType)
        {
            FoodId = foodId;
            Name = name;
            Kcal = kcal;
            Carbohydrate = carbohydrate;
            Protein = protein;
            Fat = fat;
            NutrientReferenceValue = nutrientReferenceValue;
            NrvType = nrvType;
        }
        public Food(string name, double kcal, double carbohydrate, double protein, double fat, int nutrientReferenceValue, string nrvType)
        {
            Name = name;
            Kcal = kcal;
            Carbohydrate = carbohydrate;
            Protein = protein;
            Fat = fat;
            NutrientReferenceValue = nutrientReferenceValue;
            NrvType = nrvType;
        }


        public int FoodId { get; set; }//CodeName
        public string Name { get; set; }
        public double Kcal {  get; set; }
        public double Carbohydrate {  get; set; }
        public double Protein { get; set; }
        public double Fat {  get; set; }
        public int NutrientReferenceValue { get; set; }
        public string NrvType { get; set; }

    }
}
